using UnityEngine;
using System.Collections;
using SimpleJSON;

public class DataMgr : MonoBehaviour {
	private const string seqNo = "8201505274";
	private string urlSave = "http://www.Unity3dStudy.com/Tankwar/save_score.php";
	private string urlScoreList = "http://www.Unity3dStudy.com/Tankwar/get_score_list.php";
	public GameUI gameUI2;

	public IEnumerator SaveScore(string user_name, int killCount) {
		WWWForm form = new WWWForm();
		form.AddField("user_name", user_name);
		form.AddField ("kill_count", killCount);
		form.AddField ("seq_no", seqNo);

		var www = new WWW(urlSave, form);
		yield return www;

		if(string.IsNullOrEmpty (www.error)) {
			Debug.Log (www.text);
		}
		else {
			Debug.Log ("Error : " + www.error);
		}

		StartCoroutine (this.GetScoreList());
	}

	public IEnumerator GetScoreList()
	{
		WWWForm form = new WWWForm();
		form.AddField ("seq_no", seqNo);

		var www = new WWW(urlScoreList, form);
		yield return www;

		if(string.IsNullOrEmpty (www.error)) {
			Debug.Log (www.text + "@@@@@");
			DispScoreList(www.text);
		}
		else {
			Debug.Log ("Error : " + www.error);
		}
	}

	void DispScoreList(string strJsonData) {
		var N = JSON.Parse (strJsonData);
		string temp = "";

		for(int i=0; i<N.Count; i++) {
			int ranking = N[i]["ranking"].AsInt;
			string userName = N[i]["user_name"].ToString ();
			int killCount = N[i]["kill_count"].AsInt;

			Debug.Log ("Rank : " + ranking.ToString () + "Name : " + userName + "Kill : " + killCount.ToString ());
		}

		temp = "Real-Time Ranking Info.\n";
		temp += "1st. " + N[0]["user_name"].ToString () + "  Total Score : " + N[0]["kill_count"].AsInt.ToString () + "\n";
		temp += "2nd. " + N[1]["user_name"].ToString () + "  Total Score : " + N[1]["kill_count"].AsInt.ToString () + "\n";
		temp += "3rd. " + N[2]["user_name"].ToString () + "  Total Score : " + N[2]["kill_count"].AsInt.ToString () + "\n";
		temp += "4th. " + N[3]["user_name"].ToString () + "  Total Score : " + N[3]["kill_count"].AsInt.ToString () + "\n";
		temp += "5th. " + N[4]["user_name"].ToString () + "  Total Score : " + N[4]["kill_count"].AsInt.ToString () + "\n";

		//ranking.ToString () + "Name : " + userName + "Kill : " + killCount.ToString () + "\n";
		gameUI2.Rank (temp);
	}

	void Start () {
	
	}
	
	void Update () {
	
	}
}
