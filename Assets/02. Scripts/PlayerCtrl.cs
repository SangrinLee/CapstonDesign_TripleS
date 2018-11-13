using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour {
	private Transform tr;
	private CharacterController _controller;

	private Vector3 currPos = Vector3.zero;
	private Quaternion currRot = Quaternion.identity;
	
	/* 총알 */
	public GameObject bullet;
	public GameObject bullet2;
	public GameObject bullet3;
	public GameObject weapon1;
	public GameObject weapon2;
	public GameObject weapon3;
	public int b;
	public string idid;

	private GameUI gameUI;

	public Transform firePosition;

	public Transform camPivot; // added
	public PhotonView pv = null; // added

	private bool isDie = false;

	public int hp = 100;
	public int isSniper = 1;

	public DataMgr dataMgr;

	// 캐릭터 이동부분
	private float h = 0.0f;
	private float v = 0.0f;
	public float movSpeed = 70.0f;
	public float rotSpeed = 50.0f;
	private Vector3 movDir = Vector3.zero;
	public AudioSource aud;

	void Awake() {
		tr = GetComponent<Transform>();
		_controller = GetComponent<CharacterController>();
		pv = GetComponent<PhotonView>();

		pv.observed = this;

		if(pv.isMine)
			Camera.main.GetComponent<FollowCam>().target = tr;

		currPos = tr.position;
		currRot = tr.rotation;

		dataMgr = GameObject.Find ("PhotonInit").GetComponent<DataMgr>();
	}

	void Start()
	{
		idid = SceneMove2.id;
		gameUI = GameObject.Find ("GameUI").GetComponent<GameUI>();
		gameUI.showId (idid);
		StartCoroutine (dataMgr.SaveScore (idid, 0));
		gameUI.mapOpen(SceneMove3.map);
		b = 1;
	}

	void Update () 
	{
		if(pv.isMine)
		{	
			Move();
			if(Input.GetKey (KeyCode.Alpha1))
			{
				b = 1;
				weapon1.SetActive (true);
				weapon2.SetActive (false);
				weapon3.SetActive (false);
			}
			else if(Input.GetKey (KeyCode.Alpha2))
			{
				b = 2;
				weapon1.SetActive (false);
				weapon2.SetActive (true);
				weapon3.SetActive (false);
			}
			else if(Input.GetKey (KeyCode.Alpha3))
			{
				b = 3;
				weapon1.SetActive (false);
				weapon2.SetActive (false);
				weapon3.SetActive (true);
			}
			else if(Input.GetKeyDown (KeyCode.Alpha4))
			{
				if(isSniper == 1)
				{
					b = 2;
					weapon1.SetActive (false);
					weapon2.SetActive (true);
					weapon3.SetActive (false);
					gameUI.SniperMode ();
					isSniper = 2;
				}
				else
				{
					gameUI.NSniperMode();
					isSniper = 1;
				}
			}

			if(Input.GetMouseButtonDown (0))
			{
				Fire(0);
				pv.RPC ("Fire", PhotonTargets.Others, new object[] {b+100});
			}

			Vector3 localVelocity = tr.InverseTransformDirection (_controller.velocity);
			Vector3 forwardDir = new Vector3(0f, 0f, localVelocity.z);
			Vector3 rightDir = new Vector3(localVelocity.x, 0f, 0f);
		}
	}

	[RPC]
	void Fire(int temp)
	{
		StartCoroutine(this.FireBullet(temp));
	}

	IEnumerator FireBullet(int temp2)
	{
		if(pv.isMine)
		{
			if(b == 1)
				GameObject.Instantiate (bullet, firePosition.position, firePosition.rotation);
			else if(b == 2)
				GameObject.Instantiate (bullet2, firePosition.position, firePosition.rotation);
			else if(b == 3)
				GameObject.Instantiate (bullet3, firePosition.position, firePosition.rotation);
		}
		else
		{
			if(temp2 == 101)
				GameObject.Instantiate (bullet, firePosition.position, firePosition.rotation);
			else if(temp2 == 102)
				GameObject.Instantiate (bullet2, firePosition.position, firePosition.rotation);
			else if(temp2 == 103)
				GameObject.Instantiate (bullet3, firePosition.position, firePosition.rotation);
		}

		yield return null;
	}

	void OnSerializePhotonView(PhotonStream stream, PhotonMessageInfo info)
	{
		if(stream.isWriting)
		{
			Vector3 pos = tr.position;
			Quaternion rot = tr.rotation;

			stream.SendNext(pos);
			stream.SendNext(rot);
		}
		else
		{
			Vector3 revPos = Vector3.zero;
			Quaternion revRot = Quaternion.identity;

			revPos = (Vector3)stream.ReceiveNext();
			revRot = (Quaternion)stream.ReceiveNext();

			currPos = revPos;
			currRot = revRot;
		}
	}

	[RPC]
	void Move()
	{
		if(Input.anyKey && !aud.isPlaying)
			aud.Play ();

		h = Input.GetAxis ("Horizontal");
		v = Input.GetAxis ("Vertical");
		tr.Rotate (Vector3.up * Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime);
		movDir = (tr.forward * v) + (tr.right * h);
		movDir.y -= 20f * Time.deltaTime;
		_controller.Move (movDir * movSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "BULLET")
		{
			Destroy(coll.gameObject);
			if(pv.isMine)
			{
				if(hp == 0)
				{
					gameUI.gameEnd ();
					this.gameObject.SetActive(false);
					Destroy (this);
				}
			
				hp -= 10;
				gameUI.hpfunction (hp);
			
				//StartCoroutine (dataMgr.SaveScore (idid, 0));
			}
			else if(!pv.isMine)
			{
				gameUI.DispScore (50);
				StartCoroutine (dataMgr.SaveScore (idid, 1));
			}
		}
	}

	void OnCollisionEnter(Collider coll)
	{
		Debug.Log("Inside Collision!");
	}
}
