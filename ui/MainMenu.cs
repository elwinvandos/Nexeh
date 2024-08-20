using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	private Button _startGameButton;
	private Button _quitToDesktopButton;
	private Button _generateLevelButton;

	public override void _Ready()
	{
		_startGameButton = GetNode<Button>("StartNewGameButton");
		_quitToDesktopButton = GetNode<Button>("QuitToDesktopButton");
		_generateLevelButton = GetNode<Button>("GenerateLevelButton");

		_startGameButton.Pressed += StartGame;
		_quitToDesktopButton.Pressed += Quit;
		_generateLevelButton.Pressed += GenerateLevel;

		base._Ready();
	}

	private void GenerateLevel()
	{
		throw new NotImplementedException();
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
