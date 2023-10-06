using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class BoatArea3D : Area3D
{
	[Export] Array<Seat> seats;

    public override void _Ready()
    {
        base._Ready();
		if(seats == null || seats.Count == 0){
			var seatsArray = GetChildren().OfType<Seat>().ToArray();
			seats = new Godot.Collections.Array<Seat>();
			seats.AddRange(seatsArray);
		}

		BodyEntered += OnPassengerEnteredBoatArea3D;
    }

    

	public void OnPassengerEnteredBoatArea3D(Node3D body)
	{
		GD.Print("BoatArea3D: OnBoatArea3DBodyEntered");
		if (body.IsInGroup("Passenger")) // How do I avoid using magic strings?
		{
			// Get the passenger
			//Passenger passenger = body.GetNode<Passenger>("Passenger");
			Passenger passenger = body as Passenger; // Passenger is a Character3d > PhysicsBody3d
			//bool passengerBoarded = false;
			if (passenger != null)
			{
				BringPassengerOnBoard(passenger);
			}
			//if (passengerBoarded)
			//{
				//Disable the passenger's collision
				
			//}
			
		}
	}

	/// <summary>
	/// Bring a passenger on board, but eject the first passenger on the boat if the boat
	/// is full. Return true if passenger successfully boarded
	/// </summary>
	protected void BringPassengerOnBoard(Passenger newPassenger){
		bool isBoatFull = true;
		foreach (var seat in seats)
		{
			if(!seat.isOccupied){
				//newPassenger.GetNode<CollisionShape3D>("CollisionShape3D").Disabled = true;
				seat.Passenger = newPassenger;
				seat.Passenger.PassengerState = PassengerState.Sitting;
				isBoatFull = false;
				break;
			}
		}
		if(isBoatFull){
			// Select the nearest seat to the passenger
			Node3D nearest = FindNearest(newPassenger, seats);
			Seat nearestSeat = nearest as Seat;
			nearestSeat.Passenger.PassengerState = PassengerState.Ejecting;
			nearestSeat.Passenger = newPassenger;
			nearestSeat.Passenger.PassengerState = PassengerState.Sitting;
		}
		// Note: may add a false condition
		//return true;
	}

	protected static Node3D FindNearest(Node3D query, System.Collections.Generic.IEnumerable<Node3D> candidates){
		//Find the nearest seat to the passenger
		var nearest = candidates.First();
		float nearestDistance = (query.GlobalPosition - nearest.GlobalPosition).Length();
		foreach (var candidate in candidates){
			float distance = (query.GlobalPosition - candidate.GlobalPosition).Length();
			if(distance < nearestDistance){
				nearest = candidate;
			}
		}
		return nearest;
	}
}
