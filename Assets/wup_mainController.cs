using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wup_mainController : MonoBehaviour {
	//public

	//private
	//local const
	//game mode
	const int gmWakeup = 0;
	const int gmLoadScene = 1;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	GameObject sceneCtr;
	cmn_sceneController scn;

	//local
	//game mode
	int gameMode;
	int gmCnt;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//scene controller
		sceneCtr = GameObject.Find ("wup_sceneController");
		scn = sceneCtr.GetComponent<cmn_sceneController> ();

		//game mode
		gameMode = gmWakeup;
		gmCnt = 0;

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

		//game mode process
		gameModeProcess();


		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}
	}

	//private

	//game mode process
	private void gameModeProcess(){
		switch (gameMode) {
		case gmWakeup:
			//wake up
			if (gmCnt == 0) {
				gameMode = gmLoadScene;
				gmCnt = 0;
			} else {
				gmCnt++;
			}
			break;
		case gmLoadScene:
			//load scene
			if (gmCnt == 0) {
				//set title scene
				scn.setTitleScene ();
			}
			gmCnt++;
			break;
		default:
			break;
		}

	}

	//private


	//public

	//get game mode
	public int getGmMode(){
		return gameMode;
	}

}
