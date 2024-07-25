using Godot;
using System;

public partial class Level1 : Node2D
{
	public override void _Ready()
	{
		var player = ResourceLoader.Load<PackedScene>("res://entities/player/player.tscn").Instantiate<CharacterBody2D>();
		player.Position = new Vector2(200, 200);

		var demon = ResourceLoader.Load<PackedScene>("res://entities/enemies/green_demon/green_demon.tscn").Instantiate<RigidBody2D>();
		demon.Position = new Vector2(600, 600);

		AddChild(player);
		AddChild(demon);
	}
}
