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

    private bool _isDrowning = false;
    public override void Enter(){
        base.Enter();

        // Connect the death signal to the ScoreManager which is tagged with the group "ScoreManager"
        ScoreManager scoreManager = (ScoreManager)GetTree().GetNodesInGroup("ScoreManager")[0]; // How to get rid of this magic string?
        if(scoreManager != null)
            OnDeath += scoreManager.IncrementDeaths;
        //if(splashParticles != null)
        //    splashParticles.Emitting = true;


        passenger.Velocity = Vector3.Zero;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        passenger.Velocity = passenger.Velocity + waterGravity;
        // Pull the passenger down
        passenger.MoveAndSlide();

        if (passenger.GlobalPosition.Y < -2.0f){
            // Only trigger drowning once
            if(!_isDrowning){
                _isDrowning = true;
                drowningParticles.Emitting = true;
                EmitSignal(SignalName.OnDrown);
            }
        }
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
