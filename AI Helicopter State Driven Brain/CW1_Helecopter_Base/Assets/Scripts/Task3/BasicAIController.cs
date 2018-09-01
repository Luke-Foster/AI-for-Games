using UnityEngine;
using System.Collections;

// This script sets up the delay that the chopper waits for before entering it's next state

public class BasicAIController : MonoBehaviour {

	// This float variable sets delay's initial value to 2.5
	public float delay = 2.5f;

	void Start () 
	{
	
	}

	// Time.deltaTime needs to be updated constantly and also as it's inherited in other scripts
	public void Update () 
	{
		// Subtracting Time.deltaTime from delay starts a countdown in seconds 
		delay -= Time.deltaTime;
	}
}
