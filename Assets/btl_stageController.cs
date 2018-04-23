using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_stageController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	btl_mainController mc;

	//local
	//effects
	int shakeCnt_xz;
	int shakeCnt_y;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//maincontroller
		mainCtr = GameObject.Find ("btl_mainController");
		mc = mainCtr.GetComponent<btl_mainController> ();

		//local

		//effects
		shakeCnt_xz = 0;
		shakeCnt_y = 0;

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

		//shake xz process
		if (shakeCnt_xz > 0) {
			if (shakeCnt_xz % 2 == 0) {
				cashTransform.Translate (0.2f, 0.0f, -0.2f);
			} else if (shakeCnt_xz % 2 == 1) {
				cashTransform.Translate (-0.2f, 0.0f, 0.2f);
			}
			shakeCnt_xz--;
			if (shakeCnt_xz <= 0) {
				shakeCnt_xz = 0;
				Vector3 tmppos = cashTransform.position;
				tmppos.x = 0.0f;
				tmppos.z = 0.0f;
				cashTransform.position = tmppos;
			}
		}

		//shake y process
		if (shakeCnt_y > 0) {
			if (shakeCnt_y % 2 == 0) {
				cashTransform.Translate (0.0f, -0.2f, 0.0f);
			} else if (shakeCnt_y % 2 == 1) {
				cashTransform.Translate (0.0f, 0.2f, 0.0f);
			}
			shakeCnt_y--;
			if (shakeCnt_y <= 0) {
				shakeCnt_y = 0;
				Vector3 tmppos = cashTransform.position;
				tmppos.y = 0.0f;
				cashTransform.position = tmppos;
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

	//private


	//public
	//set xz shake effect
	public void setShakeEffect_xz( int cnt = 20 ){
		this.shakeCnt_xz = cnt;
	}

	//set y shake effect
	public void setShakeEffect_y( int cnt = 20 ){
		this.shakeCnt_y = cnt;
	}

}
