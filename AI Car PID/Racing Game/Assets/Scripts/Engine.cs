using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour 
{
	//Sets wheel collider array and gameobjects 
	public WheelCollider[] wheels;
	public WheelCollider fl;
	public WheelCollider fr;
	public WheelCollider rl;
	public WheelCollider rr;
	//Sets value for torque
	private float torque = -400f; 
	//Shows in inspector whats the next target node for the car
	public Transform targetNodes;
	[HideInInspector]
	//Car physics values, can be changed to adjust car performance
	public float turnAngle;
	private float currentAngle = 0;
	private float turningSpeed = 25.0f; 
	private float currentMaxTurnAngle = 35.0f; 
	private bool stop = false;
	public float brakeForce = 20000f;
	public PID pid;
	public float TargetVelocity = 15.0f; 
	private Rigidbody rb;

	void Awake() 
	{

		turnAngle = 30f;
		//Place the wheel colliders in an array for easier processing
		wheels = new WheelCollider[4];
		wheels[0] = fr;
		wheels[1] = fl;
		wheels[2] = rr;
		wheels[3] = rl;
		//These values can be changed to tune the car 
		pid = new PID (50.0f, 15.0f, 15.0f); 
		rb = GetComponent<Rigidbody>();
	}

	void Update() 
	{
		float Velocity = 0.0f;
		//This calculation determines the car speed
		Velocity = (rb.velocity.magnitude) * 1.4f; 
		//Uses PID to set up torque on the vehicle 
		torque = -(pid.Update(TargetVelocity, Velocity, Time.deltaTime));

		//This if statement will move the car and steer is in refernece to a target (node)
		if (!stop) 
		{
			Vector3 moveDirection = (targetNodes.position - transform.position).normalized;
			//Underneath a ray is drawn demonstrating a vision model (senses)
			Debug.DrawRay(transform.position, moveDirection * 150, Color.cyan);
			Vector3 localTarget = transform.InverseTransformPoint(targetNodes.position);
			localTarget = localTarget * -1;
			float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
			if (currentAngle < targetAngle) 
			{
				currentAngle = currentAngle + (Time.deltaTime * turningSpeed);
				if (currentAngle > targetAngle) 
				{
					currentAngle = targetAngle;
				}
			}
			else if (currentAngle > targetAngle) 
			{
				currentAngle = currentAngle - (Time.deltaTime * turningSpeed);
				if (currentAngle < targetAngle) 
				{
					currentAngle = targetAngle;
				}
			}
			turnAngle = Mathf.Clamp(currentAngle, (-1) * currentMaxTurnAngle, currentMaxTurnAngle);
		}
	}

	void FixedUpdate() 
	{
		//This if statement stops the car using torque and break force 
		if (stop) {
			for (int n = 0; n < 4; n++) 
			{
				// Apply brakes
				wheels[n].brakeTorque = brakeForce;
				wheels[n].motorTorque = 0;
			}
		}
		else 
		{
			for (int n = 0; n < 4; n++) 
			{
				// Apply torque
				wheels[n].motorTorque = torque;
			}
			fl.steerAngle = turnAngle;
			fr.steerAngle = turnAngle;
		}
	}

	public void Stop() 
	{
		stop = true;
	}
}
