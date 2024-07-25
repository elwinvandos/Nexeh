using Godot;
using System;

public partial class Fireball : Area2D
{
	private float _speed = 200.0f;

	public override void _PhysicsProcess(double delta)
	{
		Position += Transform.X * _speed * (float)delta;

		base._PhysicsProcess(delta);
	}
}
