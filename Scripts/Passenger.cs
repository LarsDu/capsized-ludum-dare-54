using Godot;
using System.Collections.Generic;
public enum PassengerType{
	Empty,
	Sailor,
	Businessman,
	Lady,
	Moneybags
}

public enum PassengerState{
	Sitting,
	Flailing,
	Ejecting,
	Leaving,
	Seeking,
	Cheering,
	Climbing
}

public partial class Passenger : CharacterBody3D
{
	[ExportCategory("Wealth")]
	[Export] int minWealth = 1;
	[Export] int maxWealth = 20;
	public int wealth = 0;
	[Export] protected Label3D moneyLabel;

	[ExportCategory("Visuals")]
	[Export] AnimationPlayer animationPlayer;
	[Export] MeshInstance3D mesh;
	[Export] protected Node3D lifeRingMesh;
	[Export] bool randomizeColor = true;
	[Export] float hueRange = 0.4f;

	[ExportCategory("States")]
	[Export] StateMachine stateMachine;

	[Export] private PassengerState _passengerState = PassengerState.Flailing;
	public PassengerState PassengerState{
		get => _passengerState;
		set{
			_passengerState = value;
			if (passengerStateMap.ContainsKey(_passengerState))
			{
				GD.Print("Set passenger state: " + _passengerState.ToString());
				stateMachine.CurrentState = passengerStateMap[_passengerState];
			} else {
				// Which state did we try to set?
				GD.Print("PassengerState: Tried to set invalid state: " + _passengerState.ToString());

			}
		}
	}

	[Export] protected BaseState flailingState;
	[Export] protected BaseState sittingState;
	[Export] protected BaseState ejectingState;
	[Export] protected BaseState leavingState;
	[Export] protected BaseState seekingState;
	[Export] protected BaseState cheeringState;
	[Export] protected ClimbState climbingState;

	protected Dictionary<PassengerState, BaseState> passengerStateMap = new Dictionary<PassengerState, BaseState>();


	protected Seat lastSeat;
	public override void _Ready()
	{

		PopulatePassengerStateMap();

		animationPlayer ??= GetNode<AnimationPlayer>("%AnimationPlayer");
		

		if(mesh != null && randomizeColor){
			RandomizeColor();
		}

		DetermineWealth();
		PassengerState = _passengerState;
	}

	protected void PopulatePassengerStateMap(){
		passengerStateMap = new Dictionary<PassengerState, BaseState>(){
			{PassengerState.Flailing, flailingState},
			{PassengerState.Sitting, sittingState},
			{PassengerState.Ejecting, ejectingState},
			{PassengerState.Leaving, leavingState},
			{PassengerState.Seeking, seekingState},
			{PassengerState.Cheering, cheeringState},
			{PassengerState.Climbing, climbingState},

		};
	}

	protected void DetermineWealth(){
		wealth = GD.RandRange(minWealth, maxWealth);
		wealth = 5 * Mathf.Max(1, (wealth/5));
		if(moneyLabel == null){
			moneyLabel = GetNode<Label3D>("%MoneyLabel");
			GD.Print("MoneyLabel: " + moneyLabel);
		}
		// Set color to be more red when poor and more green when wealthier
		/*
		moneyLabel.AddColorOverride("font_color", new Color(
			(float) Mathf.Clamp( 1.0f - (wealth / (float) maxWealth), 0.0, 1.0f),
			(float) Mathf.Clamp( wealth / (float) maxWealth, 0.0, 1.0f),
			0.0f,
			1.0f
		));
		*/
		moneyLabel.Text = "$" + wealth.ToString();
	}

	protected void RandomizeColor(){
		// https://www.reddit.com/r/godot/comments/15qvust/albedo_color_isnt_a_property_of_material_godot_41/
		// Override material with a random albedo color
		//var overrideMaterial = new StandardMaterial3D();
		var activeMaterial = mesh.GetActiveMaterial(0);
		var overrideMaterial = activeMaterial.Duplicate() as StandardMaterial3D;
		overrideMaterial.AlbedoColor = new Color(
			(float) Mathf.Clamp( overrideMaterial.AlbedoColor.R +  GD.RandRange(-hueRange, hueRange), 0.0, 1.0f),
			(float) Mathf.Clamp( overrideMaterial.AlbedoColor.G +  GD.RandRange(-hueRange, hueRange), 0.0, 1.0f),
			(float) Mathf.Clamp( overrideMaterial.AlbedoColor.B +  GD.RandRange(-hueRange, hueRange), 0.0, 1.0f),
			1.0f
		);
		mesh.SetSurfaceOverrideMaterial(0, overrideMaterial);
	}



	public void SetLastSeat(
		Seat seat
	){
		if(lastSeat != null){
			// Detach from any old seat and remote transforms
			lastSeat.Passenger = null;
		}
		// Replace the memory of the lastSeat
		lastSeat = seat;
	}

	public void SetClimingDistance(float distance){
		if(climbingState == null)
			GD.PushError("climbingState is null");
		climbingState.climbDistance = distance;
	}

}
