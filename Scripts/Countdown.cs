using Godot;
using System;
using System.Linq;

public partial class Countdown : Node
{

	[Export] double DurationSecs = 60.0;
	private Timer _tickTimer;
	private Label _timeLabel;

	private int _numTicks = 0;
	public int NumTicks {
		get{
			return _numTicks;
		}
		protected set{
			_numTicks = value;
		}
	}
	[Signal]
	public delegate void TimeUpEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_tickTimer = GetChildren().OfType<Timer>().FirstOrDefault();
		_timeLabel = GetChildren().OfType<Label>().FirstOrDefault();

		_tickTimer.Timeout += OnTick;
	}

	public void OnTick(){
		if(_timeLabel == null || _tickTimer == null){
			GD.PushError("TimeReadout: OnTick: _timeLabel or _tickTimer is null");
			return;
		}
		double elapsedSecs = _tickTimer.WaitTime * NumTicks;
		if(elapsedSecs >= DurationSecs){
			_timeLabel.Text = "FINISHED";
			_timeLabel.Modulate = new Color(0.0f, 1.0f, 0.0f);
			_tickTimer.Stop();
			EmitSignal(SignalName.TimeUp);
		} else {
			// Time formatted as MM:SS
			float timeLeft = (float)(DurationSecs - elapsedSecs);
			float minLeft = (float)Math.Floor(timeLeft / 60.0f);
			_timeLabel.Text = minLeft.ToString("00") + ":" + (timeLeft % 60.0f).ToString("00");
		}
		NumTicks++;
	}
	
}
