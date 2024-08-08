using Godot;
using GungeonClone.entities;
using Nexeh.entities;

public partial class GreenDemonSprite2D : AnimatedEntitySprite
{
	private LivingEntity _entity;

	public override void _Ready()
	{
		_entity = GetParent() as LivingEntity;
	}

	public override void _Process(double delta)
	{
		// This is bad, I should create a signal and hook up to that.
		if (_entity.Health <= 0)
		{
			AnimateDeath();

			// This removed the sprite from the game, should implement this with a level clean up later
			// Call(Node.MethodName.QueueFree);
		}
		else
		{
			AnimateWalking(_entity.Velocity);
		}

		base._Process(delta);
	}
}
