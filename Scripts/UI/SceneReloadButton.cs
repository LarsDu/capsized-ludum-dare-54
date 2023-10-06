using Godot;
using System;

public partial class SceneReloadButton : Button
{
	public void OnReloadButtonPressed()
	{
		GD.Print("ReloadButton: OnReloadButtonPressed");
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene();
	}
}
