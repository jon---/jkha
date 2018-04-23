using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rst_mainController : MonoBehaviour {
	//public

	//private
	//local const
	//game mode
	const int gmFadein = 0;
	const int gmResult = 1;
	const int gmWait = 2;
	const int gmFadeout = 3;
	const int gmLoadScene = 4;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	GameObject sceneCtr;
	cmn_sceneController scn;
	GameObject screenCtr;
	cmn_screenController scr;
	GameObject displayCtr;
	rst_displayController dsc;
	GameObject playerCtr;
	rst_playerController plc;

	//local
	//game mode
	int gameMode;
	int gmCnt;

	//bonus
	int bonus;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//scene controller
		sceneCtr = GameObject.Find ("rst_sceneController");
		scn = sceneCtr.GetComponent<cmn_sceneController> ();

		//screen controller
		screenCtr = GameObject.Find ("rst_screenController");
		scr = screenCtr.GetComponent<cmn_screenController> ();

		//display controller
		displayCtr = GameObject.Find ("rst_displayController");
		dsc = displayCtr.GetComponent<rst_displayController> ();

		//playercontroller
		playerCtr = GameObject.Find ("rst_playerController");
		plc = playerCtr.GetComponent<rst_playerController> ();

		//game mode
		gameMode = gmFadein;
		gmCnt = 0;

		//bonus
		bonus = 0;

		//result
		this.setResult();

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
				//score
				dsc.updateScore ();
				//sound
				cmn_soundController.Instance.playSound( cmn_soundController.Instance.sd_se_rststart );
			}
			gmCnt++;
			break;
		case gmResult:
			//result
			if (gmCnt == 0) {
				//sound
				if (cmn_staticData.Instance.lastresult == true) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_stt);
				}
			}
			if (gmCnt == 75) {
				//generate title
				dsc.generateTitleText ();
				//sound
				if (cmn_staticData.Instance.lastresult == true) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_sl130);
//					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_downback);
				} else {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_encount);
				}
			}
			if (gmCnt == 130) {
				//description start
				dsc.makeDescriptionText ();
				dsc.setDescriptionText ();
				//sound
				if (cmn_staticData.Instance.lastresult == true) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_bgm101);
				} else {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_bgm251);
				}
			}
			if (gmCnt >= 130) {
				if (Input.GetMouseButtonDown (0) == true) {
					dsc.skipDescriptionText ();
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_ds110);
					break;
				}
			}
			gmCnt++;
			break;
		case gmWait:
			//wait
			//tap
			if (Input.GetMouseButtonDown (0) == true) {
				//sound
				if (cmn_staticData.Instance.gameover == true) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_gamovr_110);
				} else {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_sl130);
				}
				cmn_soundController.Instance.fadeoutBgm ();
				gameMode = gmFadeout;
				gmCnt = 0;
				break;
			}
			if (gmCnt == 0) {
				//sound
				if (cmn_staticData.Instance.gameover == true) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_gameover);
				} else if (cmn_staticData.Instance.clear == true) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_clear);
				} else if (cmn_staticData.Instance.nextfield == true) {
					if (cmn_staticData.Instance.lastresult == true) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_moneyfix110);
					} else {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_gameover);
					}
				}
			}
			if (gmCnt == 50) {
				//sound
				if (cmn_staticData.Instance.gameover == true) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_gamovr_100);
				} else if (cmn_staticData.Instance.clear == true) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_h_ht);
				} else if (cmn_staticData.Instance.nextfield == true) {
					if (cmn_staticData.Instance.lastresult == true) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_h_ht);
					} else {
//						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_lse);
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_gamovr_100);
					}
				}
			}
			gmCnt++;
			break;
		case gmFadeout:
			//fade out
			if (gmCnt == 0) {
				//fade out
				scr.setFadeout();
			}
			gmCnt++;
			break;
		case gmLoadScene:
			//load scene
			if (gmCnt == 0) {
				if (cmn_staticData.Instance.gameover == true) {
					//set title scene
					scn.setTitleScene ();
				} else if (cmn_staticData.Instance.clear == true) {
					//set ending scene
					scn.setEndingScene ();
				} else if (cmn_staticData.Instance.nextfield == true) {
					scn.setFieldScene ();
				}
			}
			gmCnt++;
			break;
		default:
			break;
		}

	}

	//result
	private void setResult(){
		//debug ->
//		cmn_staticData.Instance.lastresult = true;
//		cmn_staticData.Instance.lastresult = false;
//		cmn_staticData.Instance.lastkuri = true;
//		cmn_staticData.Instance.lastkuri = false;
//		cmn_staticData.Instance.jikyu = 500;
//		cmn_staticData.Instance.jikyu = 10;
//		cmn_staticData.Instance.money = 1200;
		//debug <-

		//init
		cmn_staticData.Instance.lastjikyu = 0;

		//perfect bonus
		if (cmn_staticData.Instance.patacknum == cmn_staticData.Instance.hhitnum) {
			cmn_staticData.Instance.atackbonus = true;
			cmn_staticData.Instance.lastjikyu += 30;
			//score bonus
			bonus += 80000;
		}
		if (cmn_staticData.Instance.phitnum == 0) {
			cmn_staticData.Instance.deffencebonus = true;
			cmn_staticData.Instance.lastjikyu += 30;
			//score bonus
			bonus += 100000;
		}

		//win/lose ?
		if (cmn_staticData.Instance.lastresult == true) {
			//win
			//jikyu & money
			cmn_staticData.Instance.lastjikyu += cmn_staticData.Instance.jikyu;
			cmn_staticData.Instance.money += cmn_staticData.Instance.lastjikyu;
			cmn_staticData.Instance.jikyu += 100;
			//score bonus
			bonus += 50000;
			if (cmn_staticData.Instance.money >= cmn_staticData.Instance.targetmoney) {
				cmn_staticData.Instance.clear = true;
				//score bonus
				bonus += 500000 + ((cmn_staticData.Instance.hlevel+1)*100000);
			} else {
				cmn_staticData.Instance.clear = false;
				cmn_staticData.Instance.gameover = false;
				cmn_staticData.Instance.nextfield = true;
			}
		} else {
			//lose
			//jikyu & money
			cmn_staticData.Instance.jikyu -= 50;
			if (cmn_staticData.Instance.jikyu <= 0) {
				cmn_staticData.Instance.jikyu = 0;
				cmn_staticData.Instance.gameover = true;
				cmn_staticData.Instance.clear = false;
				cmn_staticData.Instance.nextfield = false;
			} else {
				cmn_staticData.Instance.clear = false;
				cmn_staticData.Instance.gameover = false;
				cmn_staticData.Instance.nextfield = true;
			}
		}

		//debug ->
//		cmn_staticData.Instance.gameover = true;
//		cmn_staticData.Instance.clear = true;
//		cmn_staticData.Instance.nextfield = true;
		//debug <-

	}


	//game start
	private void gameStart(){
		if (gameMode == gmResult) {
			//mode
			gameMode = gmFadeout;
			gmCnt = 0;
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
			gameMode = gmResult;
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

	//term result description text
	public void termDescriptionText(){
		//player
		if (cmn_staticData.Instance.lastresult == true) {
			//player action
			plc.setPlayerWin ();
		} else {
			if (cmn_staticData.Instance.clear == true) {
				//player action
				plc.setPlayerWin ();
			} else {
				//player action
				plc.setPlayerLose ();
			}
		}
		//score
		cmn_staticData.Instance.score += bonus;
		dsc.updateScore ();
		//display
		dsc.setSubMessage();
		//mode
		gameMode = gmWait;
		gmCnt = 0;
	}

	//update score
	public void updateScore(){
		dsc.updateScore ();
	}

}
