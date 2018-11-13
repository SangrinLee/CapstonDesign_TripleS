using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhotonInit : MonoBehaviour {
	public string version = "v1.0";
	public InputField userId;

	void Awake() {
		PhotonNetwork.ConnectUsingSettings(version);
	}
	
	void OnJoinedLobby()
	{
		Debug.Log("Entered Lobby !");
		PhotonNetwork.JoinRandomRoom();
		userId.text = GetUserId ();
	}

	string GetUserId()
	{
		string userId = PlayerPrefs.GetString ("USER_ID");

		if(string.IsNullOrEmpty (userId))
		{
			userId = "USER_" + Random.Range (0, 999);
		}
		return userId;
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("No rooms !");
		PhotonNetwork.CreateRoom("MyRoom", true, true, 20);
	}

	void OnJoinedRoom()
	{
		Debug.Log("Enter Room");
		StartCoroutine(this.CreateTank());
	//	StartCoroutine (this.LoadBattleField());
	}

	public void OnClickJoinRandomRoom()
	{
		PhotonNetwork.player.name = userId.text;
		PlayerPrefs.SetString ("USER_ID", userId.text);
		PhotonNetwork.JoinRandomRoom ();
	}

	IEnumerator CreateTank()
	{
		float pos = Random.Range(-25.0f, 25.0f);
		if(SceneMove3.map == 1)
		{
			Debug.Log ("map = 1 in photon");
			PhotonNetwork.Instantiate("Player2", new Vector3(pos, 10.0f, pos), Quaternion.identity, 0);
		}
		else
		{
			Debug.Log ("map = 2,3 in photon");
			PhotonNetwork.Instantiate("Player1", new Vector3(pos, 10.0f, pos), Quaternion.identity, 0);
		}
		yield return null;
	}

	/*
	IEnumerator LoadBattleField()
	{
	//	PhotonNetwork.isMessageQueueRunning = false;
		Application.LoadLevel ("scPlay");
		yield return null;
	}
	*/

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}
}
