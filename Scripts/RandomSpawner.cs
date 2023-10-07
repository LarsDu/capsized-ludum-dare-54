using Godot;
using Godot.Collections;
using System;

public partial class RandomSpawner : Node3D
{
	[Export] Array<PackedScene> packedScenes;
	[Export] int maxSpawn = 40;
	[Export] float radius = 100.0f;
	// Called when the node enters the scene tree for the first time.

	private Node3D _playerNode;
	public override void _Ready()
	{
		RandomlySpawn();
		_playerNode = GetTree().GetNodesInGroup("Player")[0] as Node3D;
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

			Vector3 spawnRange = new Vector3((float)GD.RandRange(-radius, radius), 0.0f, (float)GD.RandRange(-radius, radius));
			Vector3 globalSpawnPosition = GlobalTransform.Origin + spawnRange;
			if(_playerNode != null){
				// Do not spawn too close to the player
				while(globalSpawnPosition.DistanceTo(_playerNode.GlobalPosition) < 20.0f){
					globalSpawnPosition = GlobalTransform.Origin + new Vector3((float)GD.RandRange(-radius, radius), 0.0f, (float)GD.RandRange(-radius, radius));
				}
			}
			instance.GlobalPosition = globalSpawnPosition;

		}
	}
}
