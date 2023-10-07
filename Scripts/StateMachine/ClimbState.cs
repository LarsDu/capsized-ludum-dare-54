using Godot;
using System;
using System.Linq.Expressions;

public partial class ClimbState : AnimatedState
{
    [Export] protected Passenger passenger;
    [Export] protected BaseState nextState;
    [Export] protected DrowningState drowningState;
    [Export] public float speed = 3.0f;
    public float climbDistance = 40.0f;

   private Vector3 _startPosition = Vector3.Zero;

    public override void Enter(){
        base.Enter();
        _startPosition = passenger.GlobalPosition;
    }

    public override void PhysicsUpdate(double delta)
    {
        base.PhysicsUpdate(delta);
        if(passenger.GlobalPosition.Y < -7.0f && drowningState != null){
            GD.Print("ClimbState: Drowning");
            stateMachine.TransitionTo(drowningState);
        }

        passenger.Velocity = new Vector3(0.0f, speed, 0.0f);
        float distanceTraveled = (passenger.GlobalPosition - _startPosition).Length();
        if(distanceTraveled > climbDistance){
            Exit();
        }
        passenger.MoveAndSlide();
    }

    public override void Exit(){
        base.Exit();
        if(nextState != null)
            stateMachine.TransitionTo(nextState);
    }
}
