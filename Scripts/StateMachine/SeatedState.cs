using Godot;
using System;

public partial class SeatedState : AnimatedState
{
	[Export] CharacterBody3D passenger;
	[Export] Node3D lifeRing;

	public override void Enter()
	{
		base.Enter();
		passenger.CollisionLayer = 2; // Collidable with rescue zone
		passenger.CollisionMask = 2;
		if(lifeRing != null)
			lifeRing.Visible = false;
	}

	public override void Exit(){
		
		//passenger.CollisionLayer = 1;
		//passenger.CollisionMask = 1;
		//if(lifeRing != null)
		//	lifeRing.Visible = false;
		base.Exit();
	}
}
