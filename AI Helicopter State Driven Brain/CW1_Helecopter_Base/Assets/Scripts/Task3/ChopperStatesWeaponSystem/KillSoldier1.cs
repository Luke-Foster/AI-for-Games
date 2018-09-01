using UnityEngine;
using System.Collections;

public class KillSoldier1<T> : AIState<T> {

	// Sets up FSM
	public KillSoldier1(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	// These float variables set speed with an initial value
	public float speed = 15.0f;
	public float RocketSpeed = 35.0f;

	public override void OnEnter() 
	{
		base.OnEnter();
		// Locates these two game objects at the start of this state 
		brain.Rocket1 = GameObject.Find ("RedCardinalRocket").transform;
		brain.Soldier1 = GameObject.Find ("Army 01 (1)").transform;
	}

	public override void OnLeave() 
	{
		base.OnLeave();
		// When the chopper leaves this state it will reset the delay
		brain.delay = 8.0f;
		// When the chopper leaves this state it will deactivate these game objects
		brain.RocketBang.SetActive (false);
		brain.BulletRelease.SetActive (false);
	}

	public override void Act()
	{
		// The move float is the maxDistanceDelta for the transform
		// speed * Time.deltaTime is how many frames the helicopter moves per second
		float move = speed * Time.deltaTime;
		// This transforms the helicopter to the AttackLocation2 GameObject via move which adds the element of time
		brain.transform.position = Vector3.MoveTowards (brain.transform.position, brain.AttackLocation2.position, move);
		// This line transforms the helicopters rotation to match the AttackLocation2 GameObject, the float determines the speed of rotation
		brain.transform.rotation = Quaternion.RotateTowards (brain.transform.rotation, brain.AttackLocation2.rotation, 0.4f);


		// If statement that instantiates when delay is equal to or less than 2.0
		if (brain.delay <= 2.0f) 
		{
			// The move float is the maxDistanceDelta for the transform
			// speed * Time.deltaTime is how many frames the helicopter moves per second
			float move1 = RocketSpeed * Time.deltaTime;
			// This transforms the Rocket1 to the Soldier1 GameObject via move which adds the element of time
			brain.Rocket1.position = Vector3.MoveTowards (brain.Rocket1.position, brain.Soldier1.position, move1);
			// This is a specific game object with just the Bullet Release audio source on it 
			brain.BulletRelease.SetActive (true);
		}

		// This If statement is only accessable through the Bullet and Soldier meeting and setting off a trigger 
		if (brain.Rocket1.GetComponent<BulletTrigger>().Check) 
		{
			// Here a group of game objects active states are changed to accomodate effects in the scene
			brain.DestroyRocket1.SetActive (false);
			brain.SoldierA.SetActive (false);
			brain.Fire.SetActive (true);
			brain.RocketExplosion1.SetActive (true);
			brain.Smoke.SetActive (true);
			brain.RocketBang.SetActive (true);
		}
	}
}
