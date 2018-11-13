using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {
	public Text txtScore;
	private int totScore = 0;
	public GameObject gm;
	public GameObject sniper;
	public GameObject gmEnd1;
	public GameObject gmEnd2;
	public GameObject targetPoint;
	public Camera MainCamera;
	public Text txtRank;
	public Text me;

	public Slider hpslider;

	
	public GameObject terrain1;
	public GameObject terrain2;
	public GameObject terrain3;
	public GameObject terrain3Ladder;
	public Material skybox1;
	public Material skybox2;
	public GameObject light1;
	public GameObject light2;
	public GameObject light3;
	public AudioSource aud;

	void Awake()
	{
		aud.Play ();
	}

	void Start () {
		DispScore(0);
	}
	
	void Update () {
	
	}

	public void DispScore(int score)
	{
		totScore += score;
		txtScore.text = "SCORE <color=#ff0000>" + totScore.ToString () + "</color>";
	}

	public void hpfunction(int hp)
	{
		hpslider.value=hp*0.01f;
	}

	public void gameEnd()
	{
		PhotonNetwork.Disconnect();
		gm.SetActive(true);
		targetPoint.SetActive (false);
		gmEnd1.SetActive (true);
		gmEnd2.SetActive (true);
	}

	public void SniperMode()
	{
		targetPoint.SetActive (false);
		sniper.SetActive (true);
		MainCamera.fieldOfView = 16;
	}

	public void NSniperMode()
	{
		targetPoint.SetActive (true);
		sniper.SetActive(false);
		MainCamera.fieldOfView = 60;
	}

	public void Rank(string str)
	{
		txtRank.text = str;
	}

	public void showId(string id)
	{
		me.text = "USER : " + id;
	}

	public void mapOpen(int number)
	{
		Debug.Log ("map : " + number.ToString());
		if(number == 1)
		{
			terrain1.SetActive(true);
			RenderSettings.skybox = skybox1;
		}
		else if(number == 2)
		{
			terrain2.SetActive(true);
			RenderSettings.skybox = skybox2;
			light1.SetActive (true);
		}
		else if(number == 3)
		{
			terrain3.SetActive(true);
			terrain3Ladder.SetActive (true);
			RenderSettings.skybox = skybox2;
			light3.SetActive (true);
		}
	}
}
