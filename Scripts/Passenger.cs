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
}

public partial class Passenger : CharacterBody3D
{
	[Export] bool setRandomPassengerType = false;
	[Export] StateMachine stateMachine;
	[Export] AnimationPlayer animationPlayer;
	[Export] protected PassengerType _passengerType = PassengerType.Empty;
	public PassengerType PassengerType {
		get => _passengerType;
		set {
			_passengerType = value;
			if (_passengerType == PassengerType.Empty)
			{
				DisableAllPeopleVisuals();
			}
			else
			{
				// Hide all meshes except the one we want
				foreach ( KeyValuePair<PassengerType, Node3D> pair in passengerEnumToMeshSelection)
				{
					pair.Value.Visible = pair.Key == _passengerType;
				}
			}
		}
	}





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

	protected Dictionary<PassengerState, BaseState> passengerStateMap = new Dictionary<PassengerState, BaseState>();
	


	Godot.Collections.Dictionary<PassengerType, Node3D> passengerEnumToMeshSelection = new Godot.Collections.Dictionary<PassengerType, Node3D>();
	//[Export] protected PassengerTypeToMeshInfo pInfo; // testing export note the use of GlobalClass


	[ExportCategory("Meshes")]
	[Export] protected Node3D lifeRingMesh;
	// Ideally, these would be specified by resources and not hardcoded :(
	[Export] protected Node3D buisnessmanMesh;
	[Export] protected Node3D ladyMesh;
	[Export] protected Node3D moneybagsMesh;
	[Export] protected Node3D sailorMesh;


	public RemoteTransform3D lastSeatAttachmentTransform;
	public override void _Ready()
	{
		// Y lock
		//AxisLockLinearY = true;

		// Populate the dictionary
		PopulateEnumToMesh();
		DisableAllPeopleVisuals();

		PopulatePassengerStateMap();

		if (setRandomPassengerType){
			RandomPassengerType();
		} else {
			PassengerType = _passengerType;
		}

		if(animationPlayer == null){
			animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		}
		//animationPlayer?.Stop();
		if(PassengerType == PassengerType.Sailor){
			_passengerState = PassengerState.Seeking;
		}
		PassengerState = _passengerState;
		
	}

	protected void PopulateEnumToMesh(){
		passengerEnumToMeshSelection.Add(PassengerType.Sailor, sailorMesh);
		passengerEnumToMeshSelection.Add(PassengerType.Businessman, buisnessmanMesh);
		passengerEnumToMeshSelection.Add(PassengerType.Lady, ladyMesh);
		passengerEnumToMeshSelection.Add(PassengerType.Moneybags, moneybagsMesh);
	}

	protected void PopulatePassengerStateMap(){
		passengerStateMap.Add(PassengerState.Flailing, flailingState);
		passengerStateMap.Add(PassengerState.Sitting, sittingState);
		passengerStateMap.Add(PassengerState.Ejecting, ejectingState);
		passengerStateMap.Add(PassengerState.Leaving, leavingState);
		passengerStateMap.Add(PassengerState.Seeking, seekingState);
	}

	protected void RandomPassengerType(){
		// Randomly select a passenger type
		int randomPassengerType = (int)GD.RandRange(1, passengerEnumToMeshSelection.Count);
		PassengerType = (PassengerType)randomPassengerType;
	}

	public void DisableAllPeopleVisuals(){
		// Disable all meshes
		foreach (var mesh in passengerEnumToMeshSelection.Values)
		{
			mesh.Visible = false;
		}
		//lifeRingMesh.Visible = false;

		// Turn off animation armature
		//animationPlayer.Stop();
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


	public void ChangePassengerType(PassengerType newPassengerType){
		PassengerType = newPassengerType;
	}
}
