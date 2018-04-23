using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rst_playerController : MonoBehaviour {
	//public

	//private
	//local const
	//move seq const
	const int movwakeup = 0;
	const int movresultwin = 1;
	const int movresultlose = 2;
	const int movresultgameover = 3;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	rst_mainController mc;
	GameObject kuriCtr;
	GameObject kuriCtr2;
	Animation anim;

	//local
	int movseq;
	int movcnt;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//main controller
		mainCtr = GameObject.Find ("rst_mainController");
		mc = mainCtr.GetComponent<rst_mainController> ();

		//kuri controller
		kuriCtr = GameObject.Find ("rst_f05_blazer_020_h");

		//kuri controller2
		kuriCtr2 = GameObject.Find ("rst_f05_blazer_120_h");

		//kuri select
		anim = null;
		if (cmn_staticData.Instance.lastkuri == false) {
			kuriCtr2.SetActive (false);
			anim = kuriCtr.GetComponent<Animation> ();
		} else {
			kuriCtr.SetActive (false);
			anim = kuriCtr2.GetComponent<Animation> ();
		}

		//action
		anim.CrossFade ("idle_01", 0);

		//local
		movseq = movwakeup;
		movcnt = 0;
	}


	// Update is called once per frame
	void Update () {


		//game mode process
		switch (movseq) {
		case movwakeup:
			//wakeup
			//nop
			break;
		case movresultwin:
			//win
			//nop
			break;
		case movresultlose:
			//lose
			if (movcnt == 40) {
				anim.CrossFade ("sit_04", 0);
			}
			movcnt++;
			break;
		case movresultgameover:
			//game over
			break;
		default:
			break;
		}


	}

	//private


	//public
	//get pos
	public Vector3 getPlayerPos(){
		return cashTransform.position;
	}

	//set player win
	public void setPlayerWin(){
		//mov
		movseq = movresultwin;
		movcnt = 0;
		//action
		anim.CrossFade ("jump_11", 0);
	}

	//set player lose
	public void setPlayerLose(){
		//mov
		movseq = movresultlose;
		movcnt = 0;
		//action
		anim.CrossFade ("down_22", 0);
	}

}
