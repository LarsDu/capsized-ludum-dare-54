using Godot;
using System;

public partial class KickedOffTheBoatState : AnimatedState
{
	[Export] Passenger passengerBody;
	[Export] float Duration = 1.0f;
	[Export] float EjectionSpeed = 50.0f;
	private float _elapsed = 0.0f;
	private Vector3 gravity = new Vector3(0.0f, -1.8f, 0.0f);
	Vector3 direction = Vector3.Zero;
	[Export] BaseState nextState;
	public override void Enter()
	{
		base.Enter();
		// Unparent transforms

		// Don't collide with boat but still collide with ladders!
		passengerBody.CollisionLayer = 2;
		passengerBody.CollisionMask = 2;

		// Random Direction to yeet the passenger
		direction = new Vector3(4*(float)GD.RandRange(-1.0f, 1.0f), (float)GD.RandRange(.5, 0.8f), (float)GD.RandRange(-0.05f, 0.05f));
		passengerBody.Velocity = passengerBody.Transform.Basis * direction * EjectionSpeed;
	}

	public override void PhysicsUpdate(double delta)
	{
		base.PhysicsUpdate(delta);
		
		// Passenger is ejected for a short time
		if(passengerBody.GlobalPosition.Y < -10.0f || passengerBody.IsOnFloor()){
			Exit();
		} else if (_elapsed > Duration){
			// Rotate the passenger 
			passengerBody.RotationDegrees = new Vector3(passengerBody.RotationDegrees.X + 1f, passengerBody.RotationDegrees.Y + 3.0f, 0.0f);
			// Apply gravity
			passengerBody.Velocity += gravity;
			passengerBody.MoveAndSlide();
		}else {
			passengerBody.Velocity = passengerBody.Transform.Basis * direction * EjectionSpeed;
			passengerBody.MoveAndSlide();
		}
		_elapsed += (float)delta;
	}

	public override void Exit(){
		base.Exit();
		stateMachine.TransitionTo(nextState);
	}
}
