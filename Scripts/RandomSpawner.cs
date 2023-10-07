using Godot;
using Godot.Collections;
using System;

public partial class RandomSpawner : Node3D
{
	[Export] Array<PackedScene> packedScenes;
	[Export] int maxSpawn = 40;
	[Export] float radius = 100.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RandomlySpawn();
	}

	public void RandomlySpawn(){
		for(int i = 0; i < maxSpawn; i++){

			int randomIndex = (int)GD.RandRange(0, packedScenes.Count-1);
			var instance = packedScenes[randomIndex]?.Instantiate<Node3D>();
			if(instance == null){
				GD.Print("RandomSpawner: Could not instantiate scene at index: " + randomIndex);
				continue;
			}
			AddChild(instance);

			Vector3 globalSpawnPosition = new Vector3((float)GD.RandRange(-radius, radius), 0.0f, (float)GD.RandRange(-radius, radius));
			instance.GlobalPosition = globalSpawnPosition;

		}
	}
}
