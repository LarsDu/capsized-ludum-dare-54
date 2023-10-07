using Godot;
using System;

public partial class LadderArea3D : Area3D
{
    [Export] protected float height = 30.0f;

    public override void _Ready(){
        BodyEntered += OnPassengerEnteredLadderArea3D;
    }
	public void OnPassengerEnteredLadderArea3D(Node3D body){
        if(body is Passenger passenger){
            passenger.SetLastSeat(null); // Detatch from any seats
            passenger.GlobalPosition = GlobalTransform.Origin;
            passenger.GlobalRotation = GlobalTransform.Basis.GetEuler();
            passenger.SetClimingDistance(height);
            passenger.PassengerState = PassengerState.Climbing;
        }
    }
}
