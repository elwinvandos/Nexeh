using Godot;
using System;

public partial class Hud : CanvasLayer
{
	private Label _healthLabel;

	public override void _Ready()
	{
		AddToGroup("HUD");
		_healthLabel = GetNode<Label>("HealthLabel");
	}

	public void UpdatePlayerHealth(int health)
	{
		_healthLabel.Text = $"Health: {health}";
	}
}
