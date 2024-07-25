using Godot;
using GungeonClone.entities;

public partial class GreenDemonSprite2D : AnimatedEntity
{
	private CharacterBody2D _entity = new();

	public override void _Ready()
	{
		_entity = GetParent() as CharacterBody2D;
	}

	public override void _Process(double delta)
	{
		AnimateWalking(_entity.Velocity);

		base._Process(delta);
	}
}
