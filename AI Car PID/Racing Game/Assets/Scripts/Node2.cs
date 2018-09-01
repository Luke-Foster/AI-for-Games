using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node2 : MonoBehaviour 
{

	public enum DriveStates { Drive, Stop, OutOfControl };
	public DriveStates driveState;
	public Transform[] routeNodes;
	public int currentNode = 0;
	private Engine engine;
	private List<Collider> visitedNodes;

	protected void Awake() 
	{
		//Inherits the Engine script to allow driving force towards nodes 
		engine = GetComponent<Engine>();
		driveState = DriveStates.Drive;
		engine.targetNodes = routeNodes[currentNode];
		visitedNodes = new List<Collider>();
	}

	protected void Update() 
	{
		if (driveState == DriveStates.Stop) 
		{
			//Stops the driving force 
			engine.Stop();
		}
	}

	//This function determines when a node has been met and what its next target (node) is
	void OnTriggerEnter(Collider collider) 
	{
		if (collider.tag == "node") 
		{
			if (!visitedNodes.Exists(delegate(Collider col) { return col == collider; })) 
			{
				visitedNodes.Add(collider);

				if (currentNode == (routeNodes.Length - 1)) 
				{
					driveState = DriveStates.Stop;
				}
				else 
				{
					engine.targetNodes = routeNodes[++currentNode];
				}
			}
		}
	}
}
