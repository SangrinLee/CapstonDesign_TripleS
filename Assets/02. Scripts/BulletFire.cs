using UnityEngine;
using System.Collections;

public class BulletFire : MonoBehaviour {
	private float LIFETime = 2.0f;

	void Start () {
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 10000.0f);
		Destroy (gameObject, LIFETime);
	}
	
	void Update () {
	
	}
}
