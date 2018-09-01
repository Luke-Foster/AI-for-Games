using UnityEngine;
using System.Collections;

// The Hover state is for when the Helicopter needs to maintain standby flying 

// The Hover class inherits from the AIState script
public class Hover<T> : AIState<T> {

	// Sets up FSM
	public Hover(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

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
		// This line transforms the helicopters rotation to match the RotationPoint GameObject, the float determines the speed of rotation
		brain.transform.rotation = Quaternion.RotateTowards (brain.transform.rotation, brain.RotationPoint.rotation, 0.4f);
	}
}
