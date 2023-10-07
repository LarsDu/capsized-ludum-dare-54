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

		// Set collision layer to 010
		passengerBody.CollisionLayer = 2;
		passengerBody.CollisionMask = 2;


		// Random Direction to yeet the passenger
		direction = new Vector3(2*(float)GD.RandRange(-1.0f, 1.0f), (float)GD.RandRange(0.8f, 1.0f), (float)GD.RandRange(-0.05f, 0.05f));
		passengerBody.Velocity = passengerBody.Transform.Basis * direction * EjectionSpeed;
	}

	public override void PhysicsUpdate(double delta)
	{
		base.PhysicsUpdate(delta);
		
		// Passenger is ejected for a short time
		if(passengerBody.GlobalPosition.Y < -10.0f || passengerBody.IsOnFloor()){
			Exit();
		} else if (_elapsed > Duration){
			// Apply gravity
			passengerBody.Velocity += gravity;
			passengerBody.MoveAndSlide();
			passengerBody.RotateX((float) delta * -10);
		}else {
			
			passengerBody.Velocity = passengerBody.Transform.Basis * direction * EjectionSpeed;
			passengerBody.MoveAndSlide();
			passengerBody.RotateX((float) delta * -3);
		}
		_elapsed += (float)delta;
	}

	public override void Exit(){
		base.Exit();
		stateMachine.TransitionTo(nextState);
	}
}
