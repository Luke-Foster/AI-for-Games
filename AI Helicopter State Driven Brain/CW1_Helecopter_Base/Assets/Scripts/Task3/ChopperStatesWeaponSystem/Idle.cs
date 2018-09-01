﻿using UnityEngine;
using System.Collections;

// The idle state is for when the Helicopter has no functions to carry out 

// The Idle class inherits from the AIState script
public class Idle<T> : AIState<T> {

	// Sets up FSM
	public Idle(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	public override void OnEnter() 
	{
		base.OnEnter();
	}
		
	public override void OnLeave() 
	{
		base.OnLeave();
	}

	public override void Act()
	{
		// No instructions are needed as the helicopter is idle 
	}
}