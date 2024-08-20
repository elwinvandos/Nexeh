using Godot;
using Nexeh.levels;
using System;

public partial class RandomLevel : GameLevel
{
	private TileMap _tileMap;

	// Formula: divide intended aspect ratio by tile size multiplied by camera zoom.
	// Then multiply by how big you want to scale the level
	private const int INITIAL_WIDTH = (1920 / 32 * 1) * 2;
	private const int INITIAL_HEIGHT = (1080 / 32 * 1) * 2;

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
	private const int LAYER_PROPS = 1;

	public override Vector2 _startingPosition => new Vector2(200, 200);

	public override void _Ready()
	{
		_tileMap = GetNode<TileMap>("TileMap");

		GenerateTerrain();
		GenerateBorder();

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
				var random = new Random();
				var randomX = random.Next(0, 8);
				var randomY = random.Next(0, 4);

				_tileMap.SetCell(LAYER_TERRAIN, new Vector2I(x, y), TILESET_GRASS, new Vector2I(randomX, randomY));
			}
		}
	}

	private void GenerateBorder()
	{
		for (int x = 0; x < _width; x++)
		{
			for (int y = 0; y < _height; y++)
			{
				if (x == 0 || x == _width - 1 || y == 0 || y == _height - 1)
				{
					_tileMap.SetCell(LAYER_PROPS, new Vector2I(x, y), TILESET_PLANT, new Vector2I(1, 6));
				}
			}
		}
	}
}
