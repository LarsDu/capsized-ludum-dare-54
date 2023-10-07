using Godot;
using System;

public partial class ScoreManager : Node
{
	[Export] Label profitLabel;
	[Export] Label savedLabel;
	[Export] Label deathsLabel;

	public int Profit{
		get;
		protected set;
	}

	public int Saved{
		get;
		protected set;
	}

	public int Deaths{
		get;
		protected set;
	}


	protected void UpdateSavedLabel(){
		savedLabel.Text = "SAVED:\t" + Saved.ToString();
	}
	protected void UpdateDeathsLabel(){
		deathsLabel.Text = "DEATHS:\t" + Deaths.ToString();
	}
	protected void UpdateProfitLabel(){
		profitLabel.Text = "PROFIT:\t$" + Profit.ToString();
	}

	public void IncrementSaved(){
		Saved++;
		UpdateSavedLabel();
	}
	public void DecrementSaved(){
		Saved--;
		UpdateSavedLabel();
	}
	public void IncrementProfit(int amount){
		Profit += amount;
		UpdateProfitLabel();
	}

	public void DecrementProfit(int amount){
		Profit -= amount;
		UpdateProfitLabel();
	}

	public void IncrementDeaths(){
		Deaths++;
		UpdateDeathsLabel();
	}

	public void DecrementDeaths(){
		Deaths--;
		UpdateDeathsLabel();
	}

	public void HideLabels(){
		profitLabel.Hide();
		savedLabel.Hide();
		deathsLabel.Hide();
	}


}
