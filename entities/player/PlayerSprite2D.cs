using Godot;

//Maybe rename this, basically this class governs animation state of player
public partial class PlayerSprite2D : Godot.AnimatedSprite2D
{
	private Node2D _player = new();
	private Vector2 _mousePosition = new();

	public override void _Ready()
	{
		// seems hacky, is this the way to do this?
		_player = GetTree().GetNodesInGroup("CharacterBody2D")[0] as Node2D;
	}

	public override void _Process(double delta)
	{
		_mousePosition = GetLocalMousePosition().Normalized();

		if (Input.IsActionPressed("ui_left"))
		{
			Play("walk_left");
		}
		else if (Input.IsActionPressed("ui_right"))
		{
			Play("walk_right");
		}
		else if (Input.IsActionPressed("ui_down"))
		{
			Play("walk_down");
		}
		else if (Input.IsActionPressed("ui_up"))
		{
			Play("walk_up");
		}
		else
		{
            if (_mousePosition.X > _player.Position.X && _mousePosition.Y > _player.Position.Y)
            {
                Play("idle_down");
            }
            else if (_mousePosition.X < _player.Position.X && _mousePosition.Y > _player.Position.Y)
            {
                Play("idle_left");
            }
            else if (_mousePosition.X > _player.Position.X && _mousePosition.Y < _player.Position.Y)
            {
                Play("idle_right");
            }
            else if (_mousePosition.X < _player.Position.X && _mousePosition.Y < _player.Position.Y)
            {
                Play("idle_up");
            }
        }
	}

	public override void _Input(InputEvent @event)
	{
		// todo: currently gets overriden by _process animations
		if (@event is InputEventMouseButton eventMouseButton)
		{
            if (_mousePosition.X > _player.Position.X && _mousePosition.Y > _player.Position.Y)
            {
                Play("cast_spell_down");
            }
            else if (_mousePosition.X < _player.Position.X && _mousePosition.Y > _player.Position.Y)
            {
                Play("cast_spell_left");
            }
            else if (_mousePosition.X > _player.Position.X && _mousePosition.Y < _player.Position.Y)
            {
                Play("cast_spell_right");
            }
            else if (_mousePosition.X < _player.Position.X && _mousePosition.Y < _player.Position.Y)
            {
                Play("cast_spell_up");
            }
        }

		base._Input(@event);
	}

	private void CalculateFaceDirection()
	{
		// todo: extract the logic here
	}
}
