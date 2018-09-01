using UnityEngine;
using System.Collections;

public class DropBomb<T> : AIState<T> {

	// Sets up FSM
	public DropBomb(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	// These float variables set speed with an initial value
	public float speed = 15.0f;
	public float RocketSpeed = 30.0f;

	public override void OnEnter() 
	{
		base.OnEnter();
	}

	public override void OnLeave() 
	{
		base.OnLeave();
		// When the chopper leaves this state it will deactivate these game objects
		brain.BombBang.SetActive (false);
		brain.BombRelease.SetActive (false);
	}

	public override void Act()
	{
		// The move float is the maxDistanceDelta for the transform
		// speed * Time.deltaTime is how many frames the helicopter moves per second
		float move2 = speed * Time.deltaTime;
		// This transforms the helicopter to the AttackLocation4 GameObject via move which adds the element of time
		brain.transform.position = Vector3.MoveTowards (brain.transform.position, brain.AttackLocation4.position, move2);
		// This line transforms the helicopters rotation to match the AttackLocation4 GameObject, the float determines the speed of rotation
		brain.transform.rotation = Quaternion.RotateTowards (brain.transform.rotation, brain.AttackLocation4.rotation, 0.6f);

		// If statement that instantiates when delay is equal to or less than 4.0
		if (brain.delay <= 4.0f) 
		{
			// The move float is the maxDistanceDelta for the transform
			// speed * Time.deltaTime is how many frames the helicopter moves per second
			float move3 = RocketSpeed * Time.deltaTime;
			// This transforms the Bomb to the BombDestination GameObject via move which adds the element of time
			brain.Bomb.position = Vector3.MoveTowards (brain.Bomb.position, brain.BombDestination.position, move3);
			// This is a specific game object with just the Bomb Release audio source on it 
			brain.BombRelease.SetActive (true);
		}

		// This If statement is only true if Bomb and BombDestination positions are equal
		if (brain.Bomb.position.Equals(brain.BombDestination.position))
		{
			// Here a group of game objects active states are changed to accomodate effects in the scene
			brain.DestroyBomb.SetActive (false);
			brain.Fire2.SetActive (true);
			brain.BombExplosion.SetActive (true);
			brain.Smoke2.SetActive (true);
			brain.Smoke3.SetActive (true);
			brain.BombBang.SetActive (true);
		}
	}
}
