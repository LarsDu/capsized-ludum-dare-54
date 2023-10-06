using Godot;
using System;

[GlobalClass]
[Tool]
public partial class PassengerTypeToMeshInfo : Resource {
    [Export] public PassengerType passengerType;
    [Export] public Node3D mesh;
}