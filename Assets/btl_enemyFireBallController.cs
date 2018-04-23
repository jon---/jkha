using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_enemyFireBallController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject enemyBulletCtr;
	btl_enemyBulletController ebc;

	//local
	//state
	bool unavailable;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//enemy bullet controller (parent)
		enemyBulletCtr = gameObject.transform.root.gameObject;
		ebc = enemyBulletCtr.GetComponent<btl_enemyBulletController> ();

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
			this.tag = "enemyUnavailableBullet";
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
		//enemy fire ball hit
		this.hitProcess (coll);
	}


	//private

	//hit process
	private void hitProcess( Collider coll ){
		if (coll.tag == "player") {
			unavailable = true;
		}
		//for parent enemy bullet
		ebc.setHit (coll);
	}


	//public

	//set init
	public void setInitState( int bulletType, float atcdir ){
		float bdir = (360-atcdir)+270;
		if ( bulletType == 2 ) {	//big bullet hyoshiki
			transform.localRotation = Quaternion.Euler (new Vector3 (0.0f, bdir, 0.0f));
		}
		if ( bulletType == 3 ) {	//normal bullet smartphone
			transform.localRotation = Quaternion.Euler (new Vector3 (90.0f, bdir, 180.0f));
		}
	}

}
