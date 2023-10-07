using Godot;
using System;

public partial class FinalReadout : RichTextLabel
{
	[Export] private ScoreManager _scoreManager;

	public void UpdateReadout(){
		_scoreManager = GetNode<ScoreManager>("%ScoreManager");
		// Show saved, profit and deaths
		this.Text = "SAVED:\t" + _scoreManager.Saved.ToString() + "\n" + 
					"PROFIT:\t$" + _scoreManager.Profit.ToString() + "\n" + 
					"DEATHS:\t" + _scoreManager.Deaths.ToString() + "\n\n" +
					"RATING:\t" + GetRating(_scoreManager);
	}


	public string GetRating(ScoreManager scoreManager){
		// Give sily names based on how many people died and how much money you made
		// Nested ifs are yuck
		if(scoreManager.Saved > 100){
			return "SAVIOR OF THE SEAS";
		}


		if (scoreManager.Deaths > 50){
			if (scoreManager.Profit > 2000){
				return "EL CHAPO";
			} else if (scoreManager.Profit > 1000){
				return "CAPTAIN AHAB";
			}
			if(scoreManager.Saved > 50){
				return "CAPTAIN KIRK";
			}
			return "CAPTAIN HOOK";
		}
		else if (scoreManager.Deaths > 40){
			if (scoreManager.Profit > 1000){
				return "JAMES CAMERON";
			}
			return "MASS MURDERER";
		} else if (scoreManager.Deaths > 20){
			if(scoreManager.Profit > 1000){
				return "CAPITALIST PIG";
			} else {
				return "FIRST DAY ON THE JOB";
			}
		} else if(scoreManager.Deaths > 10 ){
			if(scoreManager.Saved > 20 && scoreManager.Profit > 300){
				return "CAPITALIST";
			} else if (scoreManager.Saved > 0){
				return "TUGBOAT";
			}
		} else if (scoreManager.Deaths == 4){
			if (scoreManager.Profit > 500){
				return "OCEANGATE";
			} else {
				return "NAMOR THE SUBMARINER";
			}
		} else if (scoreManager.Deaths > 0 ){
			if (scoreManager.Profit > 500){
				return "NAVIGANTE";
			} else if (scoreManager.Saved < 10){
				return "INCOMPETENT";
			}
			return "LIFESAVER";
		} else if (scoreManager.Deaths == 0){
			if (scoreManager.Saved > 0 && scoreManager.Profit > 500){
				return "JAMES CAMERON";
			} else if (scoreManager.Profit > 0){
				return "SMALL BUSINESS OWNER";
			} else if (scoreManager.Saved > 0){
				return "LIFEBOAT";
			} 
			return "SLACKER";
		}
		return "ROW BOAT";
	}

	
}
