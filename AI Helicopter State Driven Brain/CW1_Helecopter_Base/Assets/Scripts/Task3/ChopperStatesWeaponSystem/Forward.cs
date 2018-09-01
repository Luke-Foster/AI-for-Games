using UnityEngine;
using System.Collections;

// The forward state is for moving the Helicopter from one point to another across the sky

// The Forward class inherits from the AIState script
public class Forward<T> : AIState<T> {

	// Sets up FSM
	public Forward(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	// This float variable sets speed's initial value to 15.0
	public float speed = 20.0f;

	public override void OnEnter() 
	{
		base.OnEnter();
	}

	public override void OnLeave() 
	{
		base.OnLeave();
		// When the chopper leaves this state it will reset the delay
		brain.delay = 6.0f;
	}

	public override void Act()
	{
		// The move float is the maxDistanceDelta for the transform
		// speed * Time.deltaTime is how many frames the helicopter moves per second
		float move = speed * Time.deltaTime;
		// This transforms the helicopter to the AttackLocation1 GameObject via move which adds the element of time
		brain.transform.position = Vector3.MoveTowards (brain.transform.position, brain.AttackLocation1.position, move);
		// This line transforms the helicopters rotation to match the AttackLocation1 GameObject, the float determines the speed of rotation
		brain.transform.rotation = Quaternion.RotateTowards (brain.transform.rotation, brain.AttackLocation1.rotation, 0.4f);
	}
}
