using Godot;
using System;
using Godot.Collections;

public partial class BoatController : CharacterBody3D
{
	[Export] public float RowSpeed = 15.0f;
	[Export] public float RotationSpeed = 1.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	//public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	AnimationPlayer _animationPlayer;
	//Animation _rowingAnimation;
	private const string rowingAnimationName = "rowing";


	public override void _Ready()
	{
		_animationPlayer = FindChild("AnimationPlayer") as AnimationPlayer;
		_animationPlayer.Play(rowingAnimationName);
	}

	public override void _Process(double delta)
	{
		Vector2 inputDir = Input.GetVector("Left", "Right", "Backward", "Forward");

		// Global space direction the player is trying to move in
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();

		// Foward movement
		Vector3 velocity = Velocity;
		if (direction != Vector3.Zero)
		{
			velocity = direction * RowSpeed;
		} else{
			velocity = Vector3.Zero; //Velocity.MoveToward(Vector3.Zero, (float) delta * RowSpeed);
		}
		UpdateAnimation(inputDir.Y * RowSpeed);
		// Rotation
		RotateY((float) (-inputDir.X * RotationSpeed * delta));

		// Apply forward velocity
		Velocity = velocity;
		// Alter the forward local velocity based on the input direction
		MoveAndSlide();
	}

	protected void UpdateAnimation(float currentRowSpeed){
		if(_animationPlayer == null){
			GD.PushWarning("Animation Player is null!");
			return;
		}
		_animationPlayer.SpeedScale = Mathf.Abs(currentRowSpeed / RowSpeed);
	}
}
