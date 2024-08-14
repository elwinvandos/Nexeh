using Godot;
using GungeonClone.entities;
using Nexeh.entities;

public partial class GreenDemonSprite2D : AnimatedEntitySprite
{
	public override void _Process(double delta)
	{
		// We could also do this as a signal, but accessing parent node is easier
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
