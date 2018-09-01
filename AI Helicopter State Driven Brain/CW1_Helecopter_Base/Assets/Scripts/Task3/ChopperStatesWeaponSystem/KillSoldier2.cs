using UnityEngine;
using System.Collections;

public class KillSoldier2<T> : AIState<T> {

	// Sets up FSM
	public KillSoldier2(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	// These float variables set speed with an initial value
	public float speed = 15.0f;
	public float RocketSpeed = 35.0f;

	public override void OnEnter() 
	{
		base.OnEnter();
	}

	public override void OnLeave() 
	{
		base.OnLeave();
		// When the chopper leaves this state it will reset the delay
		brain.delay = 10.0f;
		// When the chopper leaves this state it will deactivate these game objects
		brain.RocketBang.SetActive (false);
		brain.BulletRelease.SetActive (false);
	}

	public override void Act()
	{
		// The move float is the maxDistanceDelta for the transform
		// speed * Time.deltaTime is how many frames the helicopter moves per second
		float move1 = speed * Time.deltaTime;
		// This transforms the helicopter to the AttackLocation3 GameObject via move which adds the element of time
		brain.transform.position = Vector3.MoveTowards (brain.transform.position, brain.AttackLocation3.position, move1);
		// This line transforms the helicopters rotation to match the AttackLocation3 GameObject, the float determines the speed of rotation
		brain.transform.rotation = Quaternion.RotateTowards (brain.transform.rotation, brain.AttackLocation3.rotation, 0.4f);

		// If statement that instantiates when delay is equal to or less than 2.0
		if (brain.delay <= 2.0f) 
		{
			// The move float is the maxDistanceDelta for the transform
			// speed * Time.deltaTime is how many frames the helicopter moves per second
			float move2 = RocketSpeed * Time.deltaTime;
			// This transforms the Rocket2 to the Soldier2 GameObject via move which adds the element of time
			brain.Rocket2.position = Vector3.MoveTowards (brain.Rocket2.position, brain.Soldier2.position, move2);
			// This is a specific game object with just the Bullet Release audio source on it 
			brain.BulletRelease.SetActive (true);
		}

		// This If statement is only true if Rocket2 and Soldier2's positions are equal
		if (brain.Rocket2.position.Equals(brain.Soldier2.position))
		{
			// Here a group of game objects active states are changed to accomodate effects in the scene
			brain.DestroyRocket2.SetActive (false);
			brain.SoldierB.SetActive (false);
			brain.Fire1.SetActive (true);
			brain.RocketExplosion2.SetActive (true);
			brain.Smoke1.SetActive (true);
			brain.RocketBang.SetActive (true);
		}
	}
}
