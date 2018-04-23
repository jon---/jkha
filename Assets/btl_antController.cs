using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_antController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject hiariCtr;
	btl_hiariController hac;


	// Use this for initialization
	void Start () {

		//cash
		//transform cash
		cashTransform = transform;

		//hiari controller
		hiariCtr = GameObject.Find ("btl_hiariController");
		hac = hiariCtr.GetComponent<btl_hiariController> ();

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
	private void hitProcess(Collider coll){
		//player normal bullet hit?
		if (coll.tag == "playerNormalBullet") {
			hac.setSmallDamage ();
		}
		//player large bullet hit?
		if (coll.tag == "playerLargeBullet") {
			hac.setBigDamage ();
		}
	}

}
