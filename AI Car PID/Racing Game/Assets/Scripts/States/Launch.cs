using UnityEngine;
using System.Collections;


public class Launch<T> : AIState<T> 
{
	// Sets up FSM
	public Launch(T stateName, StateDrivenBrain controller, float minDuration) : base(stateName, controller, minDuration) { }

	public override void OnEnter() 
	{
		base.OnEnter();
		brain.RedCD1.SetActive (true);
		brain.RedCD2.SetActive (true);
		brain.RedCD3.SetActive (true);
	}

	public override void OnLeave() 
	{
		base.OnLeave();
		brain.GreenCD1.SetActive (false);
		brain.GreenCD2.SetActive (false);
		brain.GreenCD3.SetActive (false);
	}

	public override void Act()
	{
		brain.countdown -= Time.deltaTime;

		if (brain.countdown <= 4.0f) 
		{
			brain.RedCD1.SetActive (false);
		}
		if (brain.countdown <= 3.0f) 
		{
			brain.RedCD2.SetActive (false);
		}
		if (brain.countdown <= 2.0f) 
		{
			brain.RedCD3.SetActive (false);
		}
		if (brain.countdown <= 1.0f) 
		{
			brain.GreenCD1.SetActive (true);
			brain.GreenCD2.SetActive (true);
			brain.GreenCD3.SetActive (true);
		}
	}
}
