using Godot;
using System;

public partial class Billboard : Node3D
{
	[Export] protected string targetGroup = "Player";
	private Node3D _target;
	

    public override void _Ready()
    {
        base._Ready();
		if(_target == null){
			_target = GetGroups().Contains(targetGroup) ? GetTree().GetNodesInGroup(targetGroup)[0] as Node3D : null;
		}
    }
    /*
	public override void _Process(double delta)
	{
		if(_target==null){
			GD.Print("Billboard: Target not found");
			return;
		}
	
		this.LookAt(_target.GlobalTransform.Origin, Vector3.Up);
		
	}*/
}
