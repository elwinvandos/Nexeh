using Godot;
using System;

public partial class Fireball : CharacterBody2D
{
	private Vector2 _velocity;
	private double _damage = 10.0f;

	public readonly float Speed = 300.0f;

	public override void _Ready()
	{
		Velocity = _velocity;

		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		//Position += Transform.X * _speed * (float)delta;

		var collision = MoveAndCollide(Velocity * (float)delta);

		while (collision is not null)
		{
			var collider = collision.GetCollider();

			if (collider is Player)
			{
				// Don't hit ourselves (for now)
				break;
			}
			else if (collider is GreenDemon enemy)
			{
				enemy.TakeDamage(25);
				QueueFree();
				break;
			}
			else
			{
				QueueFree();
				break;

				// Bounce sample
				//var normal = collision.GetNormal();
				//var remainder = collision.GetRemainder();

				//Velocity = Velocity.Bounce(normal);
				//remainder = remainder.Bounce(normal);

				//collisionCount++;
				//collision = MoveAndCollide(remainder);
			}
		}

		base._PhysicsProcess(delta);
	}
}
