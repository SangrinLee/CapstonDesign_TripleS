using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneMove : MonoBehaviour {

	public string SceneName;

	public void onMouseDown()
	{
		Application.LoadLevel (SceneName);
	}
}
