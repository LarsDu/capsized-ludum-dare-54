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
	[Export] StateMachine stateMachine;
	[Export] AnimationPlayer animationPlayer;


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
	[Export] protected BaseState climbingState;

	protected Dictionary<PassengerState, BaseState> passengerStateMap = new Dictionary<PassengerState, BaseState>();
	

	[ExportCategory("Meshes")]
	[Export] protected Node3D lifeRingMesh;

	public RemoteTransform3D lastSeatAttachmentTransform;
	public override void _Ready()
	{

		PopulatePassengerStateMap();
		if(animationPlayer == null){
			animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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



	public void SetSeatAttachmentTransform(
		RemoteTransform3D seatAttachmentTransform
	){
		if(lastSeatAttachmentTransform != null){
			// Detach from any old seat
			lastSeatAttachmentTransform.RemotePath = null;
		}
		lastSeatAttachmentTransform = seatAttachmentTransform;
	}

}
