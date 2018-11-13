using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneDetails : MonoBehaviour {
	
	public GameObject ob1;
	public GameObject ob2;
	public AudioSource aud;

	public void onMouseDown()
	{
		aud.Play ();
		ob1.SetActive (true);
		ob2.SetActive (false);
	}
}