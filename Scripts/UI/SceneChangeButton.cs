using Godot;
using System;

public partial class SceneChangeButton : Button
{
	[Export] PackedScene gameScene;
	public void OnStartButtonPressed()
	{
		if( gameScene == null ){
			GD.PushError("StartButton: OnStartButtonPressed: gameScene is null");
			return;
		}
		GD.Print("StartButton: OnStartButtonPressed");
		//GetTree().ChangeSceneToFile("res://Scenes/game.tscn");
		SceneTree tree = GetTree();
		tree.Paused = false;
		tree.ChangeSceneToPacked(gameScene);
		
	}
}
