using Godot;
using System;

public partial class GameMenu : CanvasLayer
{
	private Button _resumeButton;
	private Button _quitButton;
	
	public override void _Ready()
	{
		_resumeButton = GetNode<Button>("ResumeButton");
		_quitButton = GetNode<Button>("QuitButton");

		_resumeButton.Pressed += Resume;
		_quitButton.Pressed += Quit;
	}

	private void Quit()
	{
		GetTree().ChangeSceneToFile("res://ui/main_menu.tscn");
	}

	private void Resume()
	{
		GetTree().Paused = false;
		Hide();
	}
}
