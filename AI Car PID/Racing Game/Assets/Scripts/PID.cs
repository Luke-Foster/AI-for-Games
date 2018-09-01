using UnityEngine;
using System.Collections;

public class PID 
{
	//This script works out the error values needed to work with physics on the car

	public float pFactor, iFactor, dFactor;
	private float kp,ki,kd;
	private float lastError;

	public PID(float pFactor, float iFactor, float dFactor) 
	{
		this.pFactor = pFactor;
		this.iFactor = iFactor;
		this.dFactor = dFactor;
		lastError = 0f;
		ki = 0f;
	}

	public float Update(float R, float Y, float timeFrame) 
	{
		float e = R - Y;
		kp = e * pFactor;
		ki = ki + e * Time.deltaTime * iFactor;
		kd = ((e - lastError) / Time.deltaTime) * dFactor;
		lastError = e;
		return kp + ki + kd;
	}

}