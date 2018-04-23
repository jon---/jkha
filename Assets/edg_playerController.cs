using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edg_playerController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
//	GameObject mainCtr;
//	edg_mainController mc;
	GameObject kuriCtr;
	GameObject kuriCtr_n;
	Animation anim;
	Animation anim_n;

	//local



	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//main controller
//		mainCtr = GameObject.Find ("edg_mainController");
//		mc = mainCtr.GetComponent<edg_mainController> ();

		//kuri controller
		kuriCtr = GameObject.Find ("edg_f05_swimwear_00_h");
		anim = kuriCtr.GetComponent<Animation> ();
		kuriCtr_n = GameObject.Find ("edg_f05_naked_00_h");
		anim_n = kuriCtr_n.GetComponent<Animation> ();

		//naked disable
		kuriCtr_n.SetActive( false );

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


	//private



	//public

	//naked process
	public void naked(){
		//naked enable
		kuriCtr_n.SetActive (true);
		//normal disable
		kuriCtr.SetActive (false);
	}

}
