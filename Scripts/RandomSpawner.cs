using Godot;
using System;

public partial class RandomSpawner : Node3D
{
	[Export] PackedScene scene;
	[Export] int maxSpawn = 40;
	[Export] float radius = 100.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RandomlySpawn();
	}

	public void RandomlySpawn(){
		for(int i = 0; i < maxSpawn; i++){
			var instance = scene.Instantiate<Node3D>();
			//GD.Print("Instance created: ", instance);
			//GD.Print("first Instance added to tree: ", instance.IsInsideTree());
			AddChild(instance);
			//GD.Print("Instance added to tree: ", instance.IsInsideTree());

			// Remember to only move position after adding to the scene tree
			instance.GlobalPosition = new Vector3((float)GD.RandRange(-radius, radius), 0.0f, (float)GD.RandRange(-radius, radius));
			//GD.Print("Instance position set: ", instance.GlobalPosition);
		}
	}
}
