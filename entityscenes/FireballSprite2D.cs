using Godot;
using System;

public partial class FireballSprite2D : AnimatedSprite2D
{
	public override void _Process(double delta)
	{
		Play("burning");

		base._Process(delta);
	}
}
