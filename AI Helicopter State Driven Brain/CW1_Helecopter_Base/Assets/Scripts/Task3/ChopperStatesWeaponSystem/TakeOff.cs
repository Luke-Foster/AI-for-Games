using UnityEngine;
using System.Collections;

// The TakeOff state is for upwards movement of the Helicopter 

// The TakeOff class inherits from the AIState script
public class TakeOff<T> : AIState<T> {

	// Sets up FSM
	public TakeOff(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	// This float variable sets UpThrust's initial value to 5.0
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
		// This transforms the helicopter to the HoverLocation1 GameObject via move which adds the element of time
		brain.transform.position = Vector3.MoveTowards (brain.transform.position, brain.HoverLocation1.position, move);
		// resets delay to 5.0 for next state
		brain.delay = 5.0f;
	}
}
