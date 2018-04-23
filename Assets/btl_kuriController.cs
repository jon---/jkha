using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_kuriController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
//	Animation anim;
//	GameObject mainCtr;
//	btl_mainController mc;
	GameObject playerCtr;
	btl_playerController plc;

	//local


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//animation
//		anim = GetComponent<Animation>();

		//maincontroller
//		mainCtr = GameObject.Find ("btl_mainController");
//		mc = mainCtr.GetComponent<btl_mainController> ();

		//playercontroller
		playerCtr = GameObject.Find ("btl_playerController");
		plc = playerCtr.GetComponent<btl_playerController> ();
	}


	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {
		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}

		////always process


		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}

	}


	//public

	//collision
	public void OnTriggerEnter(Collider coll) {
		this.hitProcess (coll);
	}


	//private

	//hit process
	private void hitProcess( Collider coll ){
		//enemy normal bullet hit?
//		if (coll.gameObject.name == "Fireball 01 Shuriken Mobile") {
		if (coll.gameObject.tag == "enemyNormalBullet") {
			plc.setSmallDamage ();
		}
		//enemy large bullet hit?
//		if (coll.gameObject.name == "Fireball 01 Shuriken Mobile_L") {
		if (coll.gameObject.tag == "enemyLargeBullet") {
			plc.setBigDamage ();
		}
	}


	//public

}
