using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	public Transform target;
	public float dist = 10.0f;
	public float height = 5.0f;
	public float dampRotate = 5.0f;
	private Transform tr;
	public Transform fpos; // added

	void Start () {
		tr = GetComponent<Transform>();
	}
	
	void LateUpdate () {
		float currYAngle = Mathf.LerpAngle (tr.eulerAngles.y, target.eulerAngles.y, dampRotate * Time.deltaTime);
		Quaternion rot = Quaternion.Euler (0, currYAngle, 0);
		tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);

		tr.position = tr.position + Vector3.up*1.5f; // added
		//fpos.position = target.position;

		tr.LookAt (target);
		//tr.LookAt (fpos); // added
	}
}
