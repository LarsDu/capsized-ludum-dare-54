using Godot;
using System;

public partial class WanderState : AnimatedState
{	
	[Export] CharacterBody3D swimmerBody;
	[Export] float Duration = 5.0f;
	[Export] float MaxSpeed = 5.0f;
	private float _elapsed = 0.0f;
	private Vector3 wanderDirection = Vector3.Zero;
	public override void PhysicsUpdate(double delta){
		base.PhysicsUpdate(delta);
		if(_elapsed > Duration){
			// Pick a new direction
			wanderDirection = new Vector3((float)GD.RandRange(-1.0f, 1.0f), 0.0f, (float)GD.RandRange(-1.0f, 1.0f));
			_elapsed = 0.0f;
		}
		_elapsed += (float)delta;

		// Move in the direction
		swimmerBody.Velocity = wanderDirection * MaxSpeed;


	}
}
