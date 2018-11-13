using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneMove3 : MonoBehaviour {
	private static SceneMove3 instance = null;
	public static int map;
	public int mapid;

	public string SceneName;
	public AudioSource aud;

	void Awake()
	{
		aud.Play ();
		instance = this;
	}
	
	public static SceneMove3 Instance
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
		if(mapid == 1)
			map = 1;
		else if(mapid == 2)
			map = 2;
		else if(mapid == 3)
			map = 3;
		Application.LoadLevel (SceneName);
	}
}
