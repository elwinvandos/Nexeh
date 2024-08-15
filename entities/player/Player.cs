using Godot;
using Nexeh.entities;
using System.Linq;

public partial class Player : LivingEntity
{
	[Signal]
	public delegate void PlayerPositionEventHandler(Vector2 position);

	private float _speed = 7.5f;
	private Hud _hud;

	public override int Health { get; set; } = 100;

	public override void _Ready()
	{
		// Necessary so other scenes can find player
		AddToGroup("Player");

		_hud = GetTree().GetNodesInGroup("HUD").First() as Hud;
		_hud.UpdatePlayerHealth(Health);

		base._Ready();
	}

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

        if (Input.IsActionJustPressed("mouse_click_left"))
        {
            Shoot();
        }

        base._Process(delta);
	}

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
		_hud.UpdatePlayerHealth(Health);
    }

    private void Shoot()
	{
		var fireBall = ResourceLoader.Load<PackedScene>("res://entities/spells/fireball.tscn").Instantiate<Fireball>();
		// Adds fireball as child of root (the level)
		GetTree().Root.AddChild(fireBall);
		var hand = GetNode<Marker2D>("Hand");
		fireBall.Velocity = hand.GlobalTransform.Origin.DirectionTo(GetGlobalMousePosition()) * fireBall.Speed;
		fireBall.Transform = hand.GlobalTransform;
	}
}
