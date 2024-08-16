using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	private Button _startGameButton;
	private Button _quitToDesktopButton;

	public override void _Ready()
	{
		_startGameButton = GetNode<Button>("StartNewGameButton");
		_quitToDesktopButton = GetNode<Button>("QuitToDesktopButton");

		_startGameButton.Pressed += StartGame;
		_quitToDesktopButton.Pressed += Quit;

		base._Ready();
	}

	private void Quit()
	{
		GetTree().Quit();
	}

	private void StartGame()
	{
		GetTree().ChangeSceneToFile("res://levels/level_1/level_1.tscn");
	}
}
