using UnityEngine;
using System.Collections;

public class StateDrivenBrain0 : BasicAIController0{
	// Declares the constant state names 
	public enum ChopperStates { Idle0, TakeOff0, Hover0, Forward0, Land0 };
	// Sets up state machine variable
	public FSM<ChopperStates> chopperStateMachine;
	// Puts a 0.4s interval on the state machine
	protected float thinkInterval = 0.4f;
	// Initialises chopperStateActive to true 
	public bool chopperStateActive = true;
	// Sets up transform variables for these GameObjects
	public Transform HoverLocation1;
	public Transform HoverLocation2;
	public Transform LandingPad;
	public Transform RotationPoint;

	// Initialises two boolean variables to false
	public bool Landed = false;
	public bool HoverPosition = false;

	protected void Awake()
	{
		// Sets up a new FSM 
		chopperStateMachine = new FSM<ChopperStates>();
		// This adds in all the new states into the state machine using variables declared
		chopperStateMachine.AddState(new Idle0<ChopperStates>(ChopperStates.Idle0, this, 0f));
		chopperStateMachine.AddState(new TakeOff0<ChopperStates>(ChopperStates.TakeOff0, this, 0f));
		chopperStateMachine.AddState(new Hover0<ChopperStates>(ChopperStates.Hover0, this, 0f));
		chopperStateMachine.AddState(new Forward0<ChopperStates>(ChopperStates.Forward0, this, 0f));
		chopperStateMachine.AddState(new Land0<ChopperStates>(ChopperStates.Land0, this, 0f));
		// Sets Idle as first state to be in once plays been activated
		chopperStateMachine.SetInitialState (ChopperStates.Idle0);

		// These are all the transitions of the states I will require within this program
		chopperStateMachine.AddTransition (ChopperStates.Idle0, ChopperStates.TakeOff0);
		chopperStateMachine.AddTransition (ChopperStates.TakeOff0, ChopperStates.Hover0);
		chopperStateMachine.AddTransition (ChopperStates.Hover0, ChopperStates.Forward0);
		chopperStateMachine.AddTransition (ChopperStates.Forward0, ChopperStates.Hover0);
		chopperStateMachine.AddTransition (ChopperStates.Hover0, ChopperStates.Land0);
		chopperStateMachine.AddTransition (ChopperStates.Land0, ChopperStates.Idle0);
	}

	// These Guards determine the conditions states need to meet to be initialised 
	public bool GuardIdle0ToTakeOff0(State<ChopperStates> currentState)
	{
		// Instantiates once delay ends and if landed is false
		return (delay <= 0.0f && Landed == false);
	}
	public bool GuardTakeOff0ToHover0(State<ChopperStates> currentState)
	{
		// Changes state when helicopter reaches GameObjects Y position
		return (transform.position.y >= HoverLocation1.position.y);
	}
	public bool GuardHover0ToForward0(State<ChopperStates> currentState)
	{
		// Instantiates once delay ends and when helicopters Z position is not equal to the GameObject
		return (delay <= 0.0f && transform.position.z != HoverLocation2.position.z);
	}
	public bool GuardForward0ToHover0(State<ChopperStates> currentState)
	{
		// Instantiates when the boolean is true 
		return (HoverPosition == true && transform.position.Equals(HoverLocation2.position));
	}
	public bool GuardHover0ToLand0(State<ChopperStates> currentState)
	{
		// Instantiates once delay ends
		return (delay <= 0.0f);
	}
	public bool GuardLand0ToIdle0(State<ChopperStates> currentState)
	{
		// Changes state when the helicopters Y coordinate is the same as the GameObjects
		return (transform.position.y <= LandingPad.position.y);
	}

	public void Start()
	{
		// Calls to Think function
		StartCoroutine(Think());
	}

	public void Update()
	{
		base.Update ();
		// Refers to all act callbacks within other scripts
		if (chopperStateActive) 
		{
			chopperStateMachine.CurrentState.Act ();
		}
	}

	protected IEnumerator Think()
	{
		yield return new WaitForSeconds(thinkInterval);
		// Checks state machine 
		chopperStateMachine.Check();
		// Calls to Think function
		StartCoroutine(Think());
	}

	void OnTriggerEnter(Collider col)
	{
		// Once box collider has been entered set HoverPosition to true 
		HoverPosition = true;
	}
}
