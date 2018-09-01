using UnityEngine;
using System.Collections;

// The idle state is for when the Car has no functions to carry out 

// The Idle class inherits from the AIState script
public class Idle<T> : AIState<T> 
{
	// Sets up FSM
	public Idle(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	public override void OnEnter() 
	{
		base.OnEnter();
		//Picks a random integer value out of 0 and 1 (2 isn't counted as a value) 
		int i = Random.Range (0, 2);

		//Activates node script if random value is 0
		if (i == 0) 
		{
			Debug.Log ("AI has chosen Route 1");
			brain.node.enabled = true;
			brain.Text1.SetActive (true);
		}

		//Activates node2 script if random value is 1 or higher
		if (i >= 1) 
		{
			Debug.Log ("AI has chosen Route 2");
			brain.node2.enabled = true;
			brain.Text2.SetActive (true);
		}
	}

	public override void OnLeave() 
	{
		base.OnLeave();
		brain.Text1.SetActive (false);
		brain.Text2.SetActive (false);
	}

	public override void Act()
	{
		// No instructions are needed as the car is idle 
	}
}
