using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_playerFireBallController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject playerBulletCtr;
	btl_playerBulletController pbc;


	//local
	//move speed
	float xx;
	float yy;
	float zz;

	//state
	bool unavailable;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//player bullet controller (parent)
		playerBulletCtr = gameObject.transform.root.gameObject;
		pbc = playerBulletCtr.GetComponent<btl_playerBulletController> ();

		//state
		unavailable = false;
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


		//tag process
		if (unavailable == true) {
			unavailable = false;
			this.tag = "playerUnavailableBullet";
		}


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
		//fire ball hit
		this.hitProcess (coll);
	}


	//private
	//hit process
	private void hitProcess( Collider coll ){
		if (coll.tag == "enemy") {
			unavailable = true;
		}
		//for parent player bullet
		pbc.setHit (coll);
	}


	//public

	//set init state
	public void setInitState( int bulletType, float atcdir ){
		float bdir = (360-atcdir)+90;
		if ( bulletType == 3 ) {	//normal bullet pants
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, bdir, 0.0f));
		}
	}

}
