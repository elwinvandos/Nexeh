using Godot;
using System;

public partial class Hand : Marker2D
{
	public override void _Process(double delta)
	{
		LookAt(GetGlobalMousePosition());
	}
}
