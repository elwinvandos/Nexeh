using Godot;

public partial class Cursor : Node2D
{
	public override void _Ready()
	{
		var cursor = ResourceLoader.Load("res://assets/UI/Cursor1.png");

		Input.SetCustomMouseCursor(cursor);
	}
}
