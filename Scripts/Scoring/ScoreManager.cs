using Godot;
using System;

public partial class ScoreManager : Node
{
	[Export] Label profitLabel;
	[Export] Label savedLabel;
	[Export] Label deathsLabel;

	private int _profit = 0;
	private int _saved = 0;
	private int _deaths = 0;

	public void IncrementSaved(){
		_saved++;
		savedLabel.Text = "SAVED:\t" + _saved.ToString();
	}

	public void IncrementProfit(int amount){
		_profit += amount;
		profitLabel.Text = "PROFIT:\t$" + _profit.ToString();
	}

	public void IncrementDeaths(){
		_deaths++;
		deathsLabel.Text = "DEATHS:\t" + _deaths.ToString();
	}
}
