using Godot;
using Nexeh.entities.items.inventory;

public partial class HealthPotionPickup : CharacterBody2D
{
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

            if (collider is Player player)
            {
                player.AddToInventory(new HealthPotion());
                QueueFree();
                break;
            }
            break;
        }

        base._PhysicsProcess(delta);
    }
}
