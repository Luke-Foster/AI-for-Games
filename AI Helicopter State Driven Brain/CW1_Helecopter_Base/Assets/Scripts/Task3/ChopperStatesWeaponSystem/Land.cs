using UnityEngine;
using System.Collections;

// The Land state is for downwards movement of the Helicopter

// The Land class inherits from the AIState script
public class Land<T> : AIState<T> {

	// Sets up FSM
	public Land(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	// This float variable sets UpThrust's's initial value to 5.0
	public float UpThrust = 5.0f;

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
		// The move float is the maxDistanceDelta for the transform
		// UpThrust * Time.deltaTime is how many frames the helicopter moves per second
		float move = UpThrust * Time.deltaTime;
		// This transforms the helicopter to the LandingPad GameObject via move which adds the element of time
		brain.transform.position = Vector3.MoveTowards (brain.transform.position, brain.LandingPad.position, move);
		// This rotation reverts the helicopter to its original rotation
		brain.transform.rotation = Quaternion.RotateTowards (brain.transform.rotation, brain.LandingPad.rotation, 0.4f);
		// Boolean that's becomes true at the end of this state 
		brain.Landed = true;
	}
}
