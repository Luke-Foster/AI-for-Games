using UnityEngine;
using System.Collections;

// The forward state is for moving the Helicopter from one point to another across the sky

// The Forward class inherits from the AIState script
public class Forward0<T> : AIState0<T> {

	// Sets up FSM
	public Forward0(T stateName, StateDrivenBrain0 controller, float minDuration) : base(stateName, controller, minDuration) { }

	// This float variable sets speed's initial value to 15.0
	public float speed = 10.0f;

	public override void OnEnter() 
	{
		base.OnEnter();
	}

	public override void OnLeave() 
	{
		base.OnLeave();
		brain.delay = 5.0f;
	}

	public override void Act()
	{
		// The move float is the maxDistanceDelta for the transform
		// speed * Time.deltaTime is how many frames the helicopter moves per second
		float move = speed * Time.deltaTime;
		// This transforms the helicopter to the HoverLocation2 GameObject via move which adds the element of time
		brain.transform.position = Vector3.MoveTowards (brain.transform.position, brain.HoverLocation2.position, move);
		// This line transforms the helicopters rotation to match the HoverLocation2 GameObject, the float determines the speed of rotation
		brain.transform.rotation = Quaternion.RotateTowards (brain.transform.rotation, brain.HoverLocation2.rotation, 0.4f);
	}
}
