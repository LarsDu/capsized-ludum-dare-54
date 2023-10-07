using Godot;
using System;
//using System.Threading.Tasks;
public partial class FlailingState : AnimatedState
{
	[Export] double maxAnimationStartDelaySecs = 3f;
	
	double _elapsed = 0.0f;

	double _randomStartDelay = 3.0f;
	public override void Enter(){
		base.Enter();
		// NOTE: Maybe use a Timer instead?
		animationPlayer.Stop();
		_randomStartDelay = GD.RandRange(0, maxAnimationStartDelaySecs);
	}

	public override void Update(double delta)
	{
		base.Update(delta);
		if(_elapsed < 0.0f){
			return;
		}

	   if(_elapsed > _randomStartDelay)
	   {
		   animationPlayer?.Play(animationName);
		   _elapsed = -1.0f;
	   }
		_elapsed += (float)delta;
	}
	/*
	public override void Enter(){
		//base.Enter();
		animationPlayer.Stop();
		RandomDelayedAnimationStart();
	}

	/*public async void RandomDelayedAnimationStart()
	{
		if (stateMachine.CurrentState != this)
		{
			return;
		}
		int randomDelaySecs = 1000 * (int) GD.RandRange(0, maxAnimationStartDelaySecs);
		
		GD.Print("Random delay: ", randomDelaySecs);
		await Task.Delay(randomDelaySecs);
		GD.Print("Random delay over");
		animationPlayer?.Play(animationName);
	}*/

}
