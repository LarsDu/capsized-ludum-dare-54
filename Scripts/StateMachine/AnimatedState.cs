using Godot;

public partial class AnimatedState: BaseState{

    [Export] protected AnimationPlayer animationPlayer;
    [Export] protected string animationName = "help";

    public override void Enter(){
        base.Enter();
        animationPlayer.Play(animationName);
    }

    public override void Exit(){
        base.Exit();
        animationPlayer.Stop();
    }
}