using Godot;
using System;

public partial class Seat : Node3D
{
	public bool isOccupied {
		get{
			return Passenger != null;
		}
	}
	[Export] RemoteTransform3D seatAttachmentTransform;

	private Passenger _passenger = null;
	public Passenger Passenger{
		set{
			var oldPassenger = _passenger;
			_passenger = value;
			if(_passenger != null && oldPassenger != _passenger){
				seatAttachmentTransform.RemotePath = _passenger.GetPath(); // Do not use reparenting
			} else {
				seatAttachmentTransform.RemotePath = null;
			}
			_passenger?.SetLastSeat(this);
		
		}
		get{
			return _passenger;
		}
	}

	public override void _Ready(){
		if(seatAttachmentTransform == null)
			seatAttachmentTransform = GetNode<RemoteTransform3D>("RemoteTransform3D");
	}
}
