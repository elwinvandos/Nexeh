using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	private Button _startLevel1Button;
	private Button _startLevel2Button;
	private Button _quitToDesktopButton;
	private Button _generateLevelButton;

	public override void _Ready()
	{
		_startLevel1Button = GetNode<Button>("PlayLevel1Button");
		_startLevel2Button = GetNode<Button>("PlayLevel2Button");
		_quitToDesktopButton = GetNode<Button>("QuitToDesktopButton");
		_generateLevelButton = GetNode<Button>("GenerateLevelButton");

		_startLevel1Button.Pressed += () => StartGame(1);
		_startLevel2Button.Pressed += () => StartGame(2);
		_generateLevelButton.Pressed += () => StartGame();

		_quitToDesktopButton.Pressed += Quit;

		base._Ready();
	}

	private void StartGame(int level = 0)
	{
        // Scene transition via SceneTree.change_scene? bad. Don't use it. Make a Level autoload that handles it.
        if (level == 0) GetTree().ChangeSceneToFile("res://levels/level_random/random_level.tscn");
		else GetTree().ChangeSceneToFile($"res://levels/level_{level}/level_{level}.tscn");
	}

	private void Quit()
	{
		GetTree().Quit();
	}
}
