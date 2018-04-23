using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttl_playerController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
//	GameObject mainCtr;
//	ttl_mainController mc;
	GameObject kuriCtr;
	GameObject kuriCtr_g;
	GameObject kuriCtr_l;
	GameObject kuriCtr_b;

	//local
	int kuriState;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//main controller
//		mainCtr = GameObject.Find ("ttl_mainController");
//		mc = mainCtr.GetComponent<ttl_mainController> ();

		//kuri controller
		kuriCtr = GameObject.Find ("f05_blazer_020_h");
		kuriCtr_g = GameObject.Find ("f05_gymclothes_00_h");
		kuriCtr_l = GameObject.Find ("f05_leotard_00_h");
		kuriCtr_b = GameObject.Find ("f05_bathtowel_00_h");

		//kuri disable
		kuriCtr_g.SetActive( false );
		kuriCtr_l.SetActive( false );
		kuriCtr_b.SetActive( false );

		//local
		kuriState = 0;
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
	public void change(){
		switch (kuriState) {
		case 0:
			//normal -> g
			kuriState++;
			//next enable
			kuriCtr_g.SetActive (true);
			//old disable
			kuriCtr.SetActive (false);
			//sound
			cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_lse);
			break;
		case 1:
			//g -> l
			kuriState++;
			//next enable
			kuriCtr_l.SetActive (true);
			//old disable
			kuriCtr_g.SetActive (false);
			//sound
			cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_lse);
			break;
		case 2:
			//l -> b
			kuriState++;
			//next enable
			kuriCtr_b.SetActive (true);
			//old disable
			kuriCtr_l.SetActive (false);
			//sound
			cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_wea);
			break;
		case 3:
			//b -> normal
			kuriState = 0;
			//next enable
			kuriCtr.SetActive (true);
			//old disable
			kuriCtr_b.SetActive (false);
			//sound
			cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ttl);
			break;
		default:
			break;
		}
	}

}
