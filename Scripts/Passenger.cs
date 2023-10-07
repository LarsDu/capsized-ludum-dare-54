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
	[Export] bool setRandomPassengerType = false;
	[Export] bool randomizeColor = true;
	[Export] float hueRange = 0.4f;
	[Export] StateMachine stateMachine;
	[Export] AnimationPlayer animationPlayer;
	[Export] MeshInstance3D mesh;

	[ExportCategory("States")]
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
	

	[ExportCategory("Meshes")]
	[Export] protected Node3D lifeRingMesh;

	protected Seat lastSeat;
	public override void _Ready()
	{

		PopulatePassengerStateMap();
		if(animationPlayer == null){
			animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		}
	
		if(mesh != null && randomizeColor){
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
