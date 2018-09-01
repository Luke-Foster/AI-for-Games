using UnityEngine;
using System.Collections;


public class Drive<T> : AIState<T> 
{

	// Sets up FSM
	public Drive(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }


	public override void OnEnter() 
	{
		base.OnEnter();
		//Calls to function from within StateDrivenBrain
		brain.Race ();
	}

	public override void OnLeave() 
	{
		base.OnLeave();
	}

	public override void Act()
	{

	}
}