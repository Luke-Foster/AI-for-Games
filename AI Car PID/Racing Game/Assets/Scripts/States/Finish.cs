using UnityEngine;
using System.Collections;


public class Finish<T> : AIState<T> 
{

	// Sets up FSM
	public Finish(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	public override void OnEnter() 
	{
		base.OnEnter();
		//Converts 'End' boolean to true from within the StateDrivenBrain
		brain.End = true;
	}

	public override void OnLeave() 
	{
		base.OnLeave();
	}

	public override void Act()
	{

	}
}