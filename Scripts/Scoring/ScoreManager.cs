using Godot;
using System;

public partial class ScoreManager : Node
{
	[Export] Label profitLabel;
	[Export] Label savedLabel;
	[Export] Label deathsLabel;

	private int _score = 0;
	private int _deaths = 0;

	public void IncrementScore(){
		_score++;
		savedLabel.Text = "MONEY:\t$" + _score.ToString();
	}

	public void IncrementDeaths(){
		_deaths++;
		deathsLabel.Text = "DEATHS:\t" + _deaths.ToString();
	}
}
