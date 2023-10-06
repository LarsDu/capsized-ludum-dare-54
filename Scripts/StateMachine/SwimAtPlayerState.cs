using Godot;
using System;

public partial class SwimAtPlayerState : AnimatedState
{	
	CharacterBody3D player;
	[Export] CharacterBody3D swimmerBody;
	[Export] Node3D lifeRing;
	[Export] float MaxSpeed = 10f;
 	public override void Enter(){
		base.Enter();
		player = (CharacterBody3D)GetTree().GetNodesInGroup("Player")[0];
		
		// Move the lifering down a bit
		if(lifeRing != null)
			lifeRing.Position -= Vector3.Up * 3f;


	}
	public override void PhysicsUpdate(double delta){
		base.PhysicsUpdate(delta);
		if(player == null)
			return;
		// Move towards the player
		Vector3 direction = (player.GlobalPosition - GlobalPosition).Normalized();
		swimmerBody.Velocity = new Vector3(direction.X, swimmerBody.Velocity.Y, direction.Z) * MaxSpeed;
		swimmerBody.LookAt(player.GlobalPosition, Vector3.Up);
		swimmerBody.MoveAndSlide();
	}

	public override void Exit(){
		base.Exit();
		// Move the lifering back up
		if(lifeRing != null)
			lifeRing.Position += Vector3.Up * 3f;
	}
}
