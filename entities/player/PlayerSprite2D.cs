using Godot;
using GungeonClone.entities;

public partial class PlayerSprite2D : AnimatedEntitySprite
{
	private CharacterBody2D _player = new();
	private Vector2 _mousePosition = new();

	public override void _Ready()
	{
		_player = GetParent() as CharacterBody2D;
	}

	public override void _Process(double delta)
	{
		_mousePosition = GetLocalMousePosition().Normalized();

        if (_player.Velocity.Length() > 0.0)
        {
            AnimateWalking(_player.Velocity);
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
