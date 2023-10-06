using Godot;

public partial class BaseState: Node3D{

    public StateMachine stateMachine;
    public virtual void Enter(){
    }

    public virtual void Exit(){

    }

    public virtual void Update(double delta){


    }

    public virtual void PhysicsUpdate(double delta){

    }
}