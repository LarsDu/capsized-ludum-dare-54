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



	protected void UpdateSavedLabel(){
		savedLabel.Text = "SAVED:\t" + _saved.ToString();
	}
	protected void UpdateDeathsLabel(){
		deathsLabel.Text = "DEATHS:\t" + _deaths.ToString();
	}
	protected void UpdateProfitLabel(){
		profitLabel.Text = "PROFIT:\t$" + _profit.ToString();
	}

	public void IncrementSaved(){
		_saved++;
		UpdateSavedLabel();
	}
	public void DecrementSaved(){
		_saved--;
		UpdateSavedLabel();
	}
	public void IncrementProfit(int amount){
		_profit += amount;
		UpdateProfitLabel();
	}

	public void DecrementProfit(int amount){
		_profit -= amount;
		UpdateProfitLabel();
	}

	public void IncrementDeaths(){
		_deaths++;
		UpdateDeathsLabel();
	}

	public void DecrementDeaths(){
		_deaths--;
		UpdateDeathsLabel();
	}


}
