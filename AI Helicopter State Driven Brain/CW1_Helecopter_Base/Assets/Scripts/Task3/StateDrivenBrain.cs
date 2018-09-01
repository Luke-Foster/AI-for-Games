using UnityEngine;
using System.Collections;

public class StateDrivenBrain : BasicAIController{
	// Declares the constant state names 
	public enum ChopperStates { Idle, TakeOff, Hover, Forward, KillSoldier1, KillSoldier2, DropBomb, Back, Land };
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
	public Transform AttackLocation1; 
	public Transform AttackLocation2;
	public Transform AttackLocation3;
	public Transform AttackLocation4;
	public Transform Rocket1;
	public Transform Rocket2;
	public Transform Bomb;
	public Transform Soldier1;
	public Transform Soldier2;
	public Transform BombDestination;
	public GameObject DestroyRocket1;
	public GameObject DestroyRocket2;
	public GameObject DestroyBomb;
	public GameObject SoldierA;
	public GameObject SoldierB;
	public GameObject Fire;
	public GameObject Fire1;
	public GameObject Fire2;
	public GameObject RocketExplosion1;
	public GameObject RocketExplosion2;
	public GameObject BombExplosion;
	public GameObject Smoke;
	public GameObject Smoke1;
	public GameObject Smoke2;
	public GameObject Smoke3;
	public GameObject RocketBang;
	public GameObject BombBang;
	public GameObject BulletRelease;
	public GameObject BombRelease;

	// Initialises two boolean variables to false
	public bool Landed = false;
	public bool HoverPosition = false;

	protected void Awake()
	{
		// Sets up a new FSM 
		chopperStateMachine = new FSM<ChopperStates>();
		// This adds in all the new states into the state machine using variables declared
		chopperStateMachine.AddState(new Idle<ChopperStates>(ChopperStates.Idle, this, 0f));
		chopperStateMachine.AddState(new TakeOff<ChopperStates>(ChopperStates.TakeOff, this, 0f));
		chopperStateMachine.AddState(new Hover<ChopperStates>(ChopperStates.Hover, this, 0f));
		chopperStateMachine.AddState(new Forward<ChopperStates>(ChopperStates.Forward, this, 0f));
		chopperStateMachine.AddState(new Land<ChopperStates>(ChopperStates.Land, this, 0f));
		chopperStateMachine.AddState(new KillSoldier1<ChopperStates>(ChopperStates.KillSoldier1, this, 0f));
		chopperStateMachine.AddState(new KillSoldier2<ChopperStates>(ChopperStates.KillSoldier2, this, 0f));
		chopperStateMachine.AddState(new DropBomb<ChopperStates>(ChopperStates.DropBomb, this, 0f));
		chopperStateMachine.AddState(new Back<ChopperStates>(ChopperStates.Back, this, 0f));
		// Sets Idle as first state to be in once plays been activated
		chopperStateMachine.SetInitialState (ChopperStates.Idle);

		// These are all the transitions of the states I will require within this program
		chopperStateMachine.AddTransition (ChopperStates.Idle, ChopperStates.TakeOff);
		chopperStateMachine.AddTransition (ChopperStates.TakeOff, ChopperStates.Hover);
		chopperStateMachine.AddTransition (ChopperStates.Hover, ChopperStates.Forward);
		chopperStateMachine.AddTransition (ChopperStates.Forward, ChopperStates.KillSoldier1);
		chopperStateMachine.AddTransition (ChopperStates.KillSoldier1, ChopperStates.KillSoldier2);
		chopperStateMachine.AddTransition (ChopperStates.KillSoldier2, ChopperStates.DropBomb);
		chopperStateMachine.AddTransition (ChopperStates.DropBomb, ChopperStates.Back);
		chopperStateMachine.AddTransition (ChopperStates.Back, ChopperStates.Hover);
		chopperStateMachine.AddTransition (ChopperStates.Hover, ChopperStates.Land);
		chopperStateMachine.AddTransition (ChopperStates.Land, ChopperStates.Idle);
	}

	// These Guards determine the conditions states need to meet to be initialised 
	public bool GuardIdleToTakeOff(State<ChopperStates> currentState)
	{
		// Instantiates once delay ends and if landed is false
		return (delay <= 0.0f && Landed == false);
	}
	public bool GuardTakeOffToHover(State<ChopperStates> currentState)
	{
		// Changes state when helicopter reaches GameObjects Y position
		return (transform.position.y >= HoverLocation1.position.y);
	}
	public bool GuardHoverToForward(State<ChopperStates> currentState)
	{
		// Instantiates once delay ends and when helicopters Z position is not equal to the GameObject
		return (delay <= 0.0f && transform.position.z != HoverLocation2.position.z);
	}
	public bool GuardForwardToKillSoldier1(State<ChopperStates> currentState)
	{ 
		return (delay <= 0.0f && transform.position.Equals(AttackLocation1.position));
	}
	public bool GuardKillSoldier1ToKillSoldier2(State<ChopperStates> currentState)
	{ 
		return (delay <= 0.0f && transform.position.Equals(AttackLocation2.position));
	}
	public bool GuardKillSoldier2ToDropBomb(State<ChopperStates> currentState)
	{ 
		return (delay <= 0.0f && transform.position.Equals(AttackLocation3.position));
	}
	public bool GuardDropBombToBack(State<ChopperStates> currentState)
	{ 
		return (delay <= 0.0f && transform.position.Equals(AttackLocation4.position));
	}
	public bool GuardBackToHover(State<ChopperStates> currentState)
	{
		// Instantiates when the boolean is true 
		return (HoverPosition == true && transform.position.Equals(HoverLocation2.position));
	}
	public bool GuardHoverToLand(State<ChopperStates> currentState)
	{
		// Instantiates once delay ends
		return (delay <= 0.0f);
	}
	public bool GuardLandToIdle(State<ChopperStates> currentState)
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
