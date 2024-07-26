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
		if (_entity.Health <= 0)
		{
			AnimateDeath();
		}
		else
		{
			AnimateWalking(_entity.Velocity);
		}

		base._Process(delta);
	}
}
