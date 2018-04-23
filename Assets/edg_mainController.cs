﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edg_mainController : MonoBehaviour {
	//public

	//private
	//local const
	//game mode
	const int gmFadein = 0;
	const int gmEnding = 1;
	const int gmFadeout = 2;
	const int gmLoadScene = 3;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	GameObject sceneCtr;
	cmn_sceneController scn;
	GameObject screenCtr;
	cmn_screenController scr;
	GameObject displayCtr;
	edg_displayController dsc;
	GameObject playerCtr;
	edg_playerController plc;

	//local
	//game mode
	int gameMode;
	int gmCnt;

	//jk tap
	int tapcnt;
	bool tapDesc;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//scene controller
		sceneCtr = GameObject.Find ("edg_sceneController");
		scn = sceneCtr.GetComponent<cmn_sceneController> ();

		//screen controller
		screenCtr = GameObject.Find ("edg_screenController");
		scr = screenCtr.GetComponent<cmn_screenController> ();

		//display controller
		displayCtr = GameObject.Find ("edg_displayController");
		dsc = displayCtr.GetComponent<edg_displayController> ();

		//player controller
		playerCtr = GameObject.Find ("edg_playerController");
		plc = playerCtr.GetComponent<edg_playerController> ();

		//game mode
		gameMode = gmFadein;
		gmCnt = 0;

		//jk tap
		tapcnt = 0;
		tapDesc = false;

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
		case gmFadein:
			//fadein
			if (gmCnt == 0) {
				//screen fade in
				scr.setFadein ();	
			}
			gmCnt++;
			break;
		case gmEnding:
			//title
			if (gmCnt == 0) {
				//sound
				cmn_soundController.Instance.playSound( cmn_soundController.Instance.sd_bgm201 );
			}
			if (gmCnt == 5) {
				//description start
				dsc.setDescriptionText ();
			}
			if (gmCnt == 30) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_mnyfix);
			}
			//jk tap process
			Vector2 mpos;
			if (Input.GetMouseButtonUp (0)) {
				mpos = Input.mousePosition;
				Ray ray = Camera.main.ScreenPointToRay (mpos);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit, Mathf.Infinity) == true) {
					if (hit.collider.tag == "player") {
						if (tapDesc == true) {
							tapDesc = false;
						} else {
							//sound
							if (tapcnt <= 50) {
								cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_ds110);
							}
							//tap process
							tapcnt++;
							if (tapcnt == 10) {
								//naked process
								plc.naked ();
								dsc.setMask ();
								//sound
								cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_naked);
							}
							if (tapcnt == 50) {
								//mask off process
								dsc.setMaskOff ();
								//sound
								cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_wea);
							}
						}
					}
				}

			}
			gmCnt++;
			break;
		case gmFadeout:
			//fade out
			if (gmCnt == 0) {
				//stop description
				dsc.setDescriptionTextDisable();
				//fade out
				scr.setFadeout();
			}
			gmCnt++;
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


	//public

	//get game mode
	public int getGmMode(){
		return gameMode;
	}

	//term fade in
	public void termFadeIn(){
		if (gameMode == gmFadein) {
			gameMode = gmEnding;
			gmCnt = 0;
		}
	}

	//term fade out
	public void termFadeOut(){
		if (gameMode == gmFadeout) {
			gameMode = gmLoadScene;
			gmCnt = 0;
		}
	}

	//term description
	public void termDescription(){
		if (gameMode == gmEnding) {
			gameMode = gmFadeout;
			gmCnt = 0;
			//sound
			cmn_soundController.Instance.fadeoutBgm();
			cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_gamovr_110);
		}
	}

	//tap decription
	public void tapDescription(){
		this.tapDesc = true;
	}

}
