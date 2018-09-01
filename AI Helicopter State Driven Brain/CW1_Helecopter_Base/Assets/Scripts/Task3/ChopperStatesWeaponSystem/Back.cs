using UnityEngine;
using System.Collections;

public class Back<T> : AIState<T> {

	// Sets up FSM
	public Back(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	// This float variable sets speed's initial value to 20.0
	public float speed = 20.0f;

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
		// speed * Time.deltaTime is how many frames the helicopter moves per second
		float move2 = speed * Time.deltaTime;
		// This transforms the helicopter to the HoverLocation2 GameObject via move which adds the element of time
		brain.transform.position = Vector3.MoveTowards (brain.transform.position, brain.HoverLocation2.position, move2);
		// This line transforms the helicopters rotation to match the HoverLocation2 GameObject, the float determines the speed of rotation
		brain.transform.rotation = Quaternion.RotateTowards (brain.transform.rotation, brain.HoverLocation2.rotation, 0.6f);
	}
}
