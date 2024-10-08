using Godot;
using Nexeh.helpers;
using Nexeh.levels;
using System;

// I should probably start over and try to generate more focused levels with open background (add some kind of skybox later?)
public partial class RandomLevel : GameLevel
{
	private TileMap _tileMap;
	private Vector2I _lastTilePosition = new Vector2I(0, 0);
	private Vector2I _lastBorderPosition = new Vector2I(-25, -25);

	private const int TILE_SIZE = 32;
	// Formula: divide intended aspect ratio by tile size multiplied by camera zoom.
	// Then multiply by how big you want to scale the level
	private const int INITIAL_WIDTH = (1920 / TILE_SIZE * 1) * 2;
	private const int INITIAL_HEIGHT = (1080 / TILE_SIZE * 1) * 2;

	private int _width = INITIAL_WIDTH;
	private int _height = INITIAL_HEIGHT;

	// Important: these should always match the intended TileSet and Layer id's viewed in Godot!
	private const int TILESET_GRASS = 0;
	private const int TILESET_STONE = 1;
	private const int TILESET_WALL = 2;
	private const int TILESET_PROPS = 4;
	private const int TILESET_PLANT = 5;
	private const int TILESET_STRUCT = 6;

	private const int LAYER_TERRAIN = 0;
	private const int LAYER_WALLS = 1;
	private const int LAYER_PROPS = 2;
	private const int LAYER_PROPS_2 = 3;

	public override Vector2 _startingPosition => new Vector2(500, 500);

	public override void _Ready()
	{
		_tileMap = GetNode<TileMap>("TileMap");

		GenerateMap();
		GeneratePath();
        GenerateTerrain();

        var cursor = ResourceLoader.Load("res://assets/UI/Cursor1.png");
		Input.SetCustomMouseCursor(cursor);

		base._Ready();
	}

	private void GenerateTerrain()
	{
		// Generate terrain outside the borders as well
		var terrainWidth = _width * 2;
		var terrainHeight = _height * 2;
		var terrainWidthOffset = _width - terrainWidth;
		var terrainHeightOffset = _height - terrainHeight;

		for (int x = terrainWidthOffset; x < terrainWidth; x++)
		{
			for (int y = terrainHeightOffset; y < terrainHeight; y++)
			{
				var coords = new Vector2I(x, y);

				// Check whether this tile was already used by a previous drawing method
				if (_tileMap.GetCellSourceId(LAYER_TERRAIN, coords) == -1)
				{
                    // Select random grass tile
                    var random = new Random();
                    var randomTile = new Vector2I(random.Next(0, 8), random.Next(0, 4));
                    _tileMap.SetCell(LAYER_TERRAIN, coords, TILESET_GRASS, randomTile);
                }
			}
		}
	}

	private void GenerateMap()
	{
		for (int x = 0; x < _width; x++)
		{
			for (int y = 0; y < _height; y++)
			{
				// Generate border
				if (x == 0 || x == _width - 1 || y == 0 || y == _height - 1)
				{
					//_tileMap.SetCell(LAYER_PROPS, new Vector2I(x, y), TILESET_PLANT, new Vector2I(1, 6));

					if ((x - _lastBorderPosition.X) >= 10)
					{
						PlaceForest(1, new Vector2I(x, y));
						_lastBorderPosition = new Vector2I(x, y);
					}

					if ((y - _lastBorderPosition.Y) >= 5)
					{
						PlaceForest(2, new Vector2I(x, y));
						_lastBorderPosition = new Vector2I(x, y);
					}
				}

				GeneratePropsAndBuildings(new Vector2I(x, y));
			}
		}
	}

	private void GeneratePropsAndBuildings(Vector2I position, Vector2I? minimumOffset = null)
	{
		minimumOffset ??= new Vector2I(15, 15);

		if ((position.X - _lastTilePosition.X) >= minimumOffset.Value.X || (position.Y - _lastTilePosition.Y) >= minimumOffset.Value.Y)
		{
			var random = new Random();
			var randomizer = random.Next(0, 60);

			if (randomizer < 30)
			{
				if (randomizer < 2)
				{
					PlaceBuilding(2, position);
				}
				else if (randomizer < 5)
				{
					PlaceBuilding(1, position);
				}
				else
				{
					var propRandomizer = random.Next(0, 100);

					if (propRandomizer < 5)
					{
						// pillar
						_tileMap.SetCell(LAYER_PROPS, position, TILESET_PROPS, new Vector2I(11, 5));
						position.Y++;
						_tileMap.SetCell(LAYER_PROPS, position, TILESET_PROPS, new Vector2I(11, 6));
						position.Y++;
						_tileMap.SetCell(LAYER_PROPS, position, TILESET_PROPS, new Vector2I(11, 7));
					}
					else if (propRandomizer < 15)
					{
						// gravestone
						_tileMap.SetCell(LAYER_PROPS, position, TILESET_PROPS, new Vector2I(7, 5));
						position.Y++;
						_tileMap.SetCell(LAYER_PROPS, position, TILESET_PROPS, new Vector2I(7, 6));
					}
					else if (propRandomizer < 30)
					{
						// jug
						_tileMap.SetCell(LAYER_PROPS, position, TILESET_PROPS, new Vector2I(5, 6));
						position.Y++;
						_tileMap.SetCell(LAYER_PROPS, position, TILESET_PROPS, new Vector2I(5, 7));
					}
					else if (propRandomizer < 50)
					{
						//PlaceForest(1, position);
					}
					else
					{
						// random rock
						_tileMap.SetCell(LAYER_PROPS, position, TILESET_PROPS, new Vector2I(random.Next(0, 7), 15));
					}
				}

				_lastTilePosition.X = position.X;
				_lastTilePosition.Y = position.Y;
			}
		}
	}

	private void GeneratePath()
	{
		var path = AStarAlgorithm.Compute((Vector2I)_startingPosition / TILE_SIZE, new Vector2I(110, 105), _tileMap);

		var random = new Random();

		while (path is not null)
		{
			_tileMap.SetCell(LAYER_TERRAIN, new Vector2I(path.X, path.Y), TILESET_GRASS, new Vector2I(random.Next(0, 2), random.Next(4, 7)));
			path = path.Parent;
		}
	}

	// Big problem with these methods: each scene also brings its own tilemap, which is bad
	// We end up with multiple tilemaps spanning multiple objects
	// This also breaks the path generation 
	// I should probably do away with adding scenes like this as map components altogether
	private void PlaceBuilding(int buildingId, Vector2I position)
	{
		var building = ResourceLoader.Load<PackedScene>($"res://levels/components/buildings/building_{buildingId}.tscn").Instantiate<Node2D>();
		// We loop using Vector2I coordinates, but to add a scene we need a regular Vector2, so multiply back by tile size
		building.Position = (Vector2)position * TILE_SIZE;
		AddChild(building);

		var buildingMap = building.GetNode<TileMap>("TileMap");
		foreach(var test in buildingMap.GetUsedCells(LAYER_TERRAIN))
		{
			// copy tiles?
		}
    }

	private void PlaceForest(int forestId, Vector2I position)
	{
		var forest = ResourceLoader.Load<PackedScene>($"res://levels/components/forests/forest_{forestId}.tscn").Instantiate<Node2D>();
		// We loop using Vector2I coordinates, but to add a scene we need a regular Vector2, so multiply back by tile size
		forest.Position = (Vector2)position * TILE_SIZE;
		AddChild(forest);
    }
}
