using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_playerBulletController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject fireball;
	GameObject hit_particles;
	ParticleSystem cashParticleSystem_hit;

	//local
	//bullet type
	int bulletType;

	//move speed
	float xx;
	float yy;
	float zz;

	//state
	int deleteCnt;
	bool unavailable;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//bullet type
		//(set from parent objects)
//		bulletType = 0;

		//player fire ball controller (child)
		//(set from parent objects)
//		if (bulletType == 0) {
//			fireball = gameObject.transform.Find("Fireball 04 Shuriken Mobile").gameObject;
//		} else if (bulletType == 1) {
//			fireball = gameObject.transform.Find("Fireball 04 Shuriken Mobile_L").gameObject;
//		} else if (bulletType == 2) {
//			fireball = gameObject.transform.Find("m01_blazer_000_h").gameObject;
//		} else if (bulletType == 3) {
//			fireball = gameObject.transform.Find("btl_pants100").gameObject;
//		}

		//hit particles
		if ( bulletType == 3 ) {
			hit_particles = gameObject.transform.Find ("btl_hitparticles").gameObject;
			cashParticleSystem_hit = hit_particles.GetComponent<ParticleSystem> ();
		}

		//move
		//(set from parent objects)
//		xx = 0.0f;
//		yy = 0.0f;
//		zz = 0.0f;

		//state
		deleteCnt = 0;
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

		//move
		cashTransform.Translate (xx, yy, zz);

		//max check
		if (deleteCnt == 0) {
			if ((cashTransform.position.x >= 10) || (cashTransform.position.x <= -10) ||
			   (cashTransform.position.y >= 10) || (cashTransform.position.y <= -10) ||
			   (cashTransform.position.z >= 10) || (cashTransform.position.z <= -10)) {
				//delete this
				GameObject.Destroy (gameObject);
			}
		}

		//delete process
		if (deleteCnt > 0) {
			deleteCnt = deleteCnt - 1;
			if (deleteCnt <= 0) {
				//delete this (+child fire ball)
				GameObject.Destroy (gameObject);
			}
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


	//private


	//public
	//set hit (for child fire ball)
	public void setHit( Collider coll){
		if (coll.tag == "enemy") {
//			this.tag = "playerUnavailableBullet";
			unavailable = true;
			if (bulletType == 2) {
				deleteCnt = 70;
			}else if (bulletType == 3){
				deleteCnt = 80;
				//effects
				cashParticleSystem_hit.Play ();
			} else {
				deleteCnt = 100;
			}
		}
	}

	//set init
	public void setInitState( int bulletType, Vector3 pos, float xx, float yy, float zz, float atcdir){
		//bullet type
		this.bulletType = bulletType;

		//player bullet controller
		if (bulletType == 0) {
			fireball = gameObject.transform.Find("Fireball 04 Shuriken Mobile").gameObject;
		} else if (bulletType == 1) {
			fireball = gameObject.transform.Find("Fireball 04 Shuriken Mobile_L").gameObject;
		} else if (bulletType == 2) {
			fireball = gameObject.transform.Find("m01_blazer_000_h").gameObject;
			fireball.GetComponent<btl_playerFireBallController> ().setInitState (bulletType, atcdir);
		} else if (bulletType == 3) {
			fireball = gameObject.transform.Find("btl_pants100").gameObject;
			fireball.GetComponent<btl_playerFireBallController> ().setInitState (bulletType, atcdir);
		}

		//pos init
		transform.position = pos;

		//particles
		if ( (bulletType == 0) || (bulletType == 1) ) {
			fireball.GetComponent<ParticleSystem> ().Play ();
		}

		//move init
		this.xx = xx;
		this.yy = yy;
		this.zz = zz;
	}

}
