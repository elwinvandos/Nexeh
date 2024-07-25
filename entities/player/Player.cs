using Godot;
using System;

public partial class Player : Godot.CharacterBody2D
{
	[Signal]
	public delegate void PlayerPositionEventHandler(Vector2 position);

	[Signal]
	public delegate void HealthDepletedEventHandler(int oldValue, int newValue);

	private float _speed = 5f;
	private int _health = 100;

	public override void _Ready()
	{
		// Necessary so other scenes can find player
		AddToGroup("Player");
	}

	// PhysicsProcess is a version of _Process that offers built-in gravity and collision methods
	public override void _PhysicsProcess(double delta)
	{
		EmitSignal(SignalName.PlayerPosition, Position);

		var velocity = Velocity;
		velocity.Y += (float)delta;
		velocity.X += (float)delta;

		if (Input.IsActionPressed("ui_left"))
		{
			velocity = Vector2.Left * _speed;
		}
		else if (Input.IsActionPressed("ui_right"))
		{
			velocity = Vector2.Right * _speed;
		}
		else if (Input.IsActionPressed("ui_down"))
		{
			velocity = Vector2.Down * _speed;
		}
		else if (Input.IsActionPressed("ui_up"))
		{
			velocity = Vector2.Up * _speed;
		}
		else
		{
			velocity = new Vector2(0, 0);
		}

		Velocity = velocity;

		MoveAndCollide(Velocity);

		base._Process(delta);
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			Shoot();
		}

		base._Input(@event);
	}


	private void Shoot()
	{
		var fireBall = ResourceLoader.Load<PackedScene>("res://entities/spells/fireball.tscn").Instantiate<Node2D>();
		// Adds fireball as child of root (the level)
		GetTree().Root.AddChild(fireBall);
		var hand = GetNode<Marker2D>("Hand");
		fireBall.Transform = hand.GlobalTransform;
	}

	public void TakeDamage(int amount)
	{
		int oldHealth = _health;
		_health -= amount;

		if (_health <= 0)
		{
			EmitSignal(SignalName.HealthDepleted, oldHealth, _health);
		}
	}
}
