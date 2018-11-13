using UnityEngine;
using System.Collections;

public class SceneMove4 : MonoBehaviour {

	public void onMouseDown()
	{
		PhotonNetwork.Disconnect();
		Application.LoadLevel ("scMain");
	}
}
