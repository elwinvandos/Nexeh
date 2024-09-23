using Godot;
using GungeonClone.entities;

public partial class PlayerSprite2D : AnimatedEntitySprite
{
	private Player _player;
	private Vector2 _mousePosition;

	public override void _Ready()
	{
		_player = GetParent() as Player;

        _player.PlayerHasCastSpell += () => AnimateCastSpell();

        base._Ready();
	} 

	public override void _Process(double delta)
	{
		_mousePosition = GetGlobalMousePosition();

		if (!_player.IsCasting)
		{
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

		base._Process(delta);
	}

    public void AnimateCastSpell()
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

        AnimationFinished += () =>
        {
            _player.IsCasting = false;
        };
    }
}
