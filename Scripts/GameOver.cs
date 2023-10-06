using Godot;
using System.Linq;

public partial class GameOver : Node
{
	[Export] Countdown countdown;
	private Control _canvas;
	//[Export] PackedScene gameScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		countdown.TimeUp += OnTimeUp;
		_canvas = GetChildren().OfType<Control>().FirstOrDefault();
		_canvas.Visible = false;
	}

	public void OnTimeUp(){

		// Pause game
		GetTree().Paused = true;
		// Show game over screen
		
		_canvas.Visible = true;
	}	
}
