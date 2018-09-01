using UnityEngine;
using System.Collections;

public class BulletTrigger : MonoBehaviour {

	public bool Check = false;

	public void OnTriggerEnter(Collider col)
	{
		if (col.name == ("Army 01 (1)"))
		{
			Check = true;
		}
	}
}
