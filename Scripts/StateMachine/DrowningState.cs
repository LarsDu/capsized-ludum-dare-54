using Godot;
using System;

public partial class DrowningState : AnimatedState
{
    [Export] Passenger passenger;
    [Export] float Duration = 3.0f;
    private float _elapsed = 0.0f;
    [Export] GpuParticles3D drowningParticles;
    //[Export] GpuParticles3D splashParticles;

    private Vector3 waterGravity = new Vector3(0.0f, -0.15f, 0.0f);

    [Signal] public delegate void OnDrownEventHandler();
    [Signal] public delegate void OnDeathEventHandler();
    public override void Enter(){
        base.Enter();

        // Connect the death signal to the ScoreManager which is tagged with the group "ScoreManager"
        ScoreManager scoreManager = (ScoreManager)GetTree().GetNodesInGroup("ScoreManager")[0];
        if(scoreManager != null)
            OnDeath += scoreManager.IncrementDeaths;
        //if(splashParticles != null)
        //    splashParticles.Emitting = true;

        if(drowningParticles != null)
            drowningParticles.Emitting = true;
        EmitSignal(SignalName.OnDrown);
        passenger.Velocity = Vector3.Zero;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        passenger.Velocity = passenger.Velocity + waterGravity;
        // Pull the passenger down

        passenger.MoveAndSlide();
    }

    public override void Update(double delta){
        base.Update(delta);
        if(_elapsed > Duration){
            if(passenger != null){
                EmitSignal(SignalName.OnDeath);
                passenger?.QueueFree();
            }
        }
        _elapsed += (float)delta;
    }
}
