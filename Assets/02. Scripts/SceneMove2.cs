using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneMove2 : MonoBehaviour {

	private static SceneMove2 instance = null;
	public static string id;
	public InputField txtid;

	public string SceneName;
	public AudioSource aud;

	void Awake()
	{
		aud.Play ();
		instance = this;
	}

	public static SceneMove2 Instance
	{
		get
		{
			if(instance == null)
				Debug.Log ("instance == null");
			return instance;
		}
	}

	public void onMouseDown()
	{
		id = txtid.text.ToString ();
		Application.LoadLevel (SceneName);
	}
}
