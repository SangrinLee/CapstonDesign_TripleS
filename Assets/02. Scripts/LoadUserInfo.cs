using UnityEngine;
using System.Collections;
using SimpleJSON;

public class LoadUserInfo : MonoBehaviour {
	public TextAsset jsonData;
	public string strJsonData;

	void Start () {
		jsonData = Resources.Load<TextAsset>("user_info.json");
		strJsonData = jsonData.text;

		var N = JSON.Parse (strJsonData);
		string user_name = N["name"].ToString ();

		Debug.Log (user_name);

	}
	
	void Update () {
	
	}
}
