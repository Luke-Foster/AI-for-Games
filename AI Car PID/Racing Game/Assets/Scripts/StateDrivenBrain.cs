using UnityEngine;
using System.Collections;

public class StateDrivenBrain : BasicAIController
{
	// Declares the constant state names 
	public enum CarStates { Idle, Launch, Drive, Finish };
	// Sets up state machine variable
	public FSM<CarStates> carStateMachine;
	// Puts a 0.4s interval on the state machine
	protected float thinkInterval = 0.4f;
	// Initialises carStateActive to true 
	public bool carStateActive = true;
	//Creates a variable that can be used to refer to other scripts
	public Engine engine;
	public Node node;
	public Node2 node2;
	// Initialises End to true 
	public bool End = false;
	public float countdown = 5.0f;
	public GameObject RedCD1;
	public GameObject RedCD2;
	public GameObject RedCD3;
	public GameObject GreenCD1;
	public GameObject GreenCD2;
	public GameObject GreenCD3;
	public GameObject Text1;
	public GameObject Text2;

	protected void Awake()
	{
		//Finds these scripts to allow use of their properties 
		engine = GetComponent<Engine> ();
		node = GetComponent<Node> ();
		node2 = GetComponent<Node2> ();

		// Sets up a new FSM 
		carStateMachine = new FSM<CarStates>();
		// This adds in all the new states into the state machine using variables declared
		carStateMachine.AddState(new Idle<CarStates>(CarStates.Idle, this, 3.0f));
		carStateMachine.AddState(new Launch<CarStates>(CarStates.Launch, this, 0f));
		carStateMachine.AddState(new Drive<CarStates>(CarStates.Drive, this, 0f));
		carStateMachine.AddState(new Finish<CarStates>(CarStates.Finish, this, 0f));
		// Sets Idle as first state to be in once plays been activated
		carStateMachine.SetInitialState (CarStates.Idle);

		// These are all the transitions of the states I will require within this program
		carStateMachine.AddTransition (CarStates.Idle, CarStates.Launch);
		carStateMachine.AddTransition (CarStates.Launch, CarStates.Drive);
		carStateMachine.AddTransition (CarStates.Drive, CarStates.Finish);
		carStateMachine.AddTransition (CarStates.Finish, CarStates.Idle);
	}

	// These Guards determine the conditions states need to meet to be initialised 
	public bool GuardIdleToLaunch(State<CarStates> currentState)
	{
		return (End == false);
	}
	public bool GuardLaunchToDrive(State<CarStates> currentState)
	{
		return (countdown <= 0.0f);
	}
	public bool GuardDriveToFinish(State<CarStates> currentState)
	{
		return (node.currentNode == (node.routeNodes.Length - 1) || node2.currentNode == (node2.routeNodes.Length - 1));
	}
	public bool GuardFinishToIdle(State<CarStates> currentState)
	{
		return (End == true);
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
		if (carStateActive) 
		{
			carStateMachine.CurrentState.Act ();
		}
	}

	protected IEnumerator Think()
	{
		yield return new WaitForSeconds(thinkInterval);
		// Checks state machine 
		carStateMachine.Check();
		// Calls to Think function
		StartCoroutine(Think());
	}

	public void Race()
	{
		//Activates engine script
		engine.enabled = true;
	}
}
