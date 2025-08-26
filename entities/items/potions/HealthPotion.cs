using Godot;

public partial class HealthPotion : CharacterBody2D
{
    [Export]
    public int HealAmount { get; set; } = 20;

    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

        base._Ready();
    }

    public override void _Process(double delta)
    {
        _animationPlayer.Play("float");

        base._Process(delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        var collision = MoveAndCollide(Velocity * (float)delta);

        while (collision is not null)
        {
            var collider = collision.GetCollider();

            if (collider is Player player && player.Health < player.MaxHealth)
            {
                player.Heal(HealAmount);
                QueueFree();
                break;
            }
            break;
        }

        base._PhysicsProcess(delta);
    }
}