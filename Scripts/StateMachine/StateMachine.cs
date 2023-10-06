using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class StateMachine : Node3D
{
	[Export] private BaseState _currentState;
	public BaseState CurrentState{
		get{
			return _currentState;
		}
		set{
			BaseState prevState = _currentState;
			_currentState = value;
			if (prevState != _currentState){
				if(prevState != null){
					prevState.Exit();
				}
				if(_currentState != null){
					_currentState.Enter();
				}
			}
		}
	}

	[Signal] public delegate void TransitionedEventHandler(BaseState newState);

	//protected System.Collections.Generic.Dictionary< string, BaseState> statesMap;
	protected List<BaseState> states;
    public override void _Ready()
    {
		//statesMap = GetChildren().Cast<BaseState>().ToDictionary(x => (string) x.Name, x => x);
		states = GetChildren().OfType<BaseState>().ToList();
		foreach(var state in states){
			state.stateMachine = this;
		}
		CurrentState = _currentState;
		if(CurrentState != null){
			CurrentState.Enter();
		}
		
    }
 
	public override void _Process(double delta)
	{
		if(CurrentState == null){
			return;
		}
		CurrentState.Update(delta);
	}
	public override void _PhysicsProcess(double delta)
	{
		if(CurrentState == null){
			return;
		}
		CurrentState.PhysicsUpdate(delta);
	}

	public void TransitionTo( BaseState newState){
		if(newState == null){
			GD.Print("StateMachine: Tried to transition to null state");
			return;
		}
		CurrentState = newState;
		// Note: Remember to set vscode to Omnisharp rather than Rosalyn for SignalName to be respected!
		EmitSignal(SignalName.Transitioned, newState);
	}

}
