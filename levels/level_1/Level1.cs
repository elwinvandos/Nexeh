using Godot;
using Nexeh.levels;
using System;
using System.Collections.Generic;

public partial class Level1 : GameLevel
{
	private List<GreenDemon> _demons = new();
	private Marker2D _spawn1;
	private Marker2D _spawn2;

	public override Vector2 _startingPosition => new Vector2(200, 180);

	public override void _Ready()
	{
		base._Ready();

		_spawn1 = GetNode<Marker2D>("Spawn1");
		_spawn2 = GetNode<Marker2D>("Spawn2");

		var demon1 = ResourceLoader.Load<PackedScene>("res://entities/enemies/green_demon/green_demon.tscn").Instantiate<GreenDemon>();
		demon1.Position = _spawn1.Position;

		var demon2 = ResourceLoader.Load<PackedScene>("res://entities/enemies/green_demon/green_demon.tscn").Instantiate<GreenDemon>();
		demon2.Position = _spawn2.Position;

		_demons.Add(demon1);
		_demons.Add(demon2);

		AddChild(demon1);
		AddChild(demon2);
	}

	public override void _Process(double delta)
	{
		foreach (var demon in _demons)
		{
			if (demon.Health <= 0)
			{
				_demons.Remove(demon);
			}
		}

		if (_demons.Count < 2)
		{
			var newDemon = ResourceLoader.Load<PackedScene>("res://entities/enemies/green_demon/green_demon.tscn").Instantiate<GreenDemon>();

			var random = new Random();
			int whichSpawn = random.Next(2);

			if (whichSpawn == 0)
			{
				newDemon.Position = _spawn1.Position;
			}
			else
			{
				newDemon.Position = _spawn2.Position;
			}
			_demons.Add(newDemon);
			AddChild(newDemon);
		}

		base._Process(delta);
	}
}
