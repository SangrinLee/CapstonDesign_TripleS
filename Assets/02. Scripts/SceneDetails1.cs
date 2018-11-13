using UnityEngine;
using System.Collections;

public class SceneDetails1 : MonoBehaviour {
	public GameObject ob1;
	public GameObject ob2;

	public AudioSource aud;

	public DataMgr2 dataMgr2;

	public void onMouseDown()
	{
		dataMgr2 = GameObject.Find ("PhotonInit2").GetComponent<DataMgr2>();
		aud.Play ();
		StartCoroutine (dataMgr2.SaveScore ("idid", 0));
		ob1.SetActive(false);
		ob2.SetActive (true);
	}
}
