using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wheel : MonoBehaviour 
{
    public Transform wheelModel;
    private WheelCollider wheelCollider;

    void Awake() 
	{
		//Locates the wheel collider script 
        wheelCollider = GetComponent<Collider>() as WheelCollider;
    }

    //Alligns the wheels position from the wheel collider
    private void FixedUpdate() 
	{
        Vector3 position;
        Quaternion rotation;
        //Get the collider's position and rotation
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelModel.position = new Vector3(position.x, position.y, position.z);
        wheelModel.transform.rotation = rotation;
		WheelHit hit;
		//Applies friction to wheels 
		if (wheelCollider.GetGroundHit(out hit)) {
			PhysicMaterial material = hit.collider.material;
			WheelFrictionCurve forwardCurve = wheelCollider.forwardFriction;
			forwardCurve.stiffness = material.dynamicFriction;
			wheelCollider.forwardFriction = forwardCurve;
		}
    }
}
