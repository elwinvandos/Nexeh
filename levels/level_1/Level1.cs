using Godot;

public partial class Level1 : Node2D
{
	private static readonly Vector2 _startingPosition = new(200, 200);
	//private List<GreenDemon> _demons = new();

	public override void _Ready()
	{
		var player = ResourceLoader.Load<PackedScene>("res://entities/player/player.tscn").Instantiate<CharacterBody2D>();
		player.Position = _startingPosition;

		var spawn1 = GetNode<Marker2D>("Spawn1");
		var spawn2 = GetNode<Marker2D>("Spawn2");

		var demon = ResourceLoader.Load<PackedScene>("res://entities/enemies/green_demon/green_demon.tscn").Instantiate<GreenDemon>();
		demon.Position = spawn1.Position;

		var demon2 = ResourceLoader.Load<PackedScene>("res://entities/enemies/green_demon/green_demon.tscn").Instantiate<GreenDemon>();
		demon2.Position = spawn2.Position;

		AddChild(player);
		AddChild(demon);
		AddChild(demon2);
	}
}
