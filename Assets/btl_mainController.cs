using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_mainController : MonoBehaviour {
	//public
	//player normal bullet
	public GameObject btl_playerBulletControllerPrefab;
	//player large bullet
	public GameObject btl_playerBulletController_LPrefab;
	//player tachio bullet
	public GameObject btl_playerBulletController_TPrefab;
	//player pants bullet
	public GameObject btl_playerBulletController_PPrefab;
	//enemy normal bullet
	public GameObject btl_enemyBulletControllerPrefab;
	//enemy large bullet
	public GameObject btl_enemyBulletController_LPrefab;
	//enemy hyoshiki bullet
	public GameObject btl_enemyBulletController_HPrefab;
	//enemy smartphone bullet
	public GameObject btl_enemyBulletController_PPrefab;
	//hiari parts
	public GameObject btl_hiariPartsPrefab;
	//cola can
	public GameObject btl_colaCanPrefab;


	//private
	//local const
	//game mode
	const int gmFadein = 0;
	const int gmReady = 1;
	const int gmWaitStart = 2;
	const int gmFight = 3;
	const int gmHiariKo = 4;
	const int gmPlayerKo = 5;
	const int gmFadeout = 6;
	const int gmLoadScene = 7;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	GameObject displayCtr;
	btl_cameraController cameraCtr;
	GameObject sceneCtr;
	cmn_sceneController scn;
	GameObject screenCtr;
	cmn_screenController scr;
	btl_displayController dsc = null;	//(for start call)
	GameObject stageCtr;
	btl_stageController stc;
	GameObject playerCtr;
	btl_playerController plc;
	GameObject hiariCtr;
	btl_hiariController hac;
	Animator animt;

	//local
	//game mode
	int gameMode;
	int gmCnt;

	//result
	bool result;

	//pause
	bool pause;
	float backTimeScale;
	bool tapCansel;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//camera controller
		cameraCtr = Camera.main.GetComponent<btl_cameraController>();

		//display controller
		displayCtr = GameObject.Find ("btl_displayController");
		dsc = displayCtr.GetComponent<btl_displayController> ();

		//scene controller
		sceneCtr = GameObject.Find ("btl_sceneController");
		scn = sceneCtr.GetComponent<cmn_sceneController> ();

		//screen controller
		screenCtr = GameObject.Find ("btl_screenController");
		scr = screenCtr.GetComponent<cmn_screenController> ();

		//stage controller
		stageCtr = GameObject.Find ("btl_stageController");
		stc = stageCtr.GetComponent<btl_stageController> ();

		//playercontroller
		playerCtr = GameObject.Find ("btl_playerController");
		plc = playerCtr.GetComponent<btl_playerController> ();

		//hiari controller
		hiariCtr = GameObject.Find ("btl_hiariController");
		hac = hiariCtr.GetComponent<btl_hiariController> ();

		//game mode
		gameMode = gmFadein;
		gmCnt = 0;

		//result
		result = false;

		//pause
		pause = false;
		backTimeScale = 1.0f;
		tapCansel = false;

		//cmn data init
		cmn_staticData.Instance.btl_initCmnData();
	}


	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {

		////always process


		//no wait and pause process

		//nop

		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}

		//wait and pause process

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
		//game mode process
		switch (gameMode) {
		case gmFadein:
			//fade in
			if (gmCnt == 0) {
				//screen fade in
				scr.setFadein ();
				dsc.setHiariName (hac.getHiariLevel ());
				dsc.setHiariLevel (hac.getHiariLevel ());
				dsc.updateScore ();
			}
			gmCnt++;
			break;
		case gmReady:
			//ready
			if (gmCnt == 0) {
				//display
				dsc.termWakeup ();
				dsc.setSubmessage ("READY...");
				//player
				plc.termWakeup ();
				//hiari
				hac.termWakeup ();
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_btlstart);
			}
			if (gmCnt == 4) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_stt_110);
			}
			if (gmCnt == 65) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ready);
			}
			if (gmCnt >= 120) {
				//mode
				gameMode = gmWaitStart;
				gmCnt = 0;
			} else {
				gmCnt++;
			}
			break;
		case gmWaitStart:
			//wait start
			if (gmCnt == 0) {
				//display
				dsc.setSubmessage ("TAP TO START!");
			}
			//input start
			bool inputstart = false;
			//for unity editor only
			#if UNITY_EDITOR
			if (Input.anyKey == true) {
				if (Input.GetKeyDown( KeyCode.Space)) {
					inputstart = true;
				}
			}
			#endif
			//tap
			if (Input.GetMouseButtonDown (0) == true) {
				inputstart = true;
			}
			//fight start
			if (inputstart == true) {
				gameMode = gmFight;
				gmCnt = 0;
				//display
				dsc.setSubmessage ("");
				//player
				plc.setFightStart ();
				//hiari
				hac.setFightStart ();
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_bgm401);
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_go);
			} else {
				gmCnt++;
			}
			break;
		case gmFight:
			//fight
			//high priority voice cnt process
			cmn_staticData.Instance.highvoicecnt1--;
			if (cmn_staticData.Instance.highvoicecnt1 <= 0) {
				cmn_staticData.Instance.highvoicecnt1 = 0;
			}
			cmn_staticData.Instance.highvoicecnt2--;
			if (cmn_staticData.Instance.highvoicecnt2 <= 0) {
				cmn_staticData.Instance.highvoicecnt2 = 0;
			}
			cmn_staticData.Instance.highvoicecnt3--;
			if (cmn_staticData.Instance.highvoicecnt3 <= 0) {
				cmn_staticData.Instance.highvoicecnt3 = 0;
			}
			break;
		case gmHiariKo:
			//hiari ko
			if (gmCnt == 0) {
				//slow 1
				Time.timeScale = 0.02f;
			}
			if (gmCnt == 1) {
				//slow 2
				Time.timeScale = 0.2f;
			}
			if (gmCnt == 15) {
				//normal speed
				Time.timeScale = 1.0f;
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_wn100);
			}
			if (gmCnt == 76) {
				//camera
				cameraCtr.setHiariKo ();
				//display
				dsc.setSubmessage ("生け捕り成功!!");
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_hkoback);	//(loop)
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_ko_back);
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_hkoback_e);
			}
			if ((gmCnt >= 76) && (gmCnt <= 455)) {
				if (gmCnt % 65 == 0) {
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_hkoback);
				}
				if (gmCnt % 2 == 0) {
					if (Random.Range (0, 7) == 0) {
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_exp);
					}
				}
			}
			if (gmCnt >= 76) {
				//tap
				if (cmn_staticData.Instance.kodemo == false) {
					if (Input.GetMouseButtonUp (0) == true) {
						if (pause == false) {
							if (tapCansel == false) {
								gameMode = gmFadeout;
								gmCnt = 0;
								//state
								cmn_staticData.Instance.kodemo = false;
								//sound
								cmn_soundController.Instance.fadeoutBgm ();
								cmn_soundController.Instance.stopSe ();
								cmn_soundController.Instance.stopVoice ();
								break;
							} else {
								tapCansel = false;
							}
						}
					}
				}
			}
			if (gmCnt == 90) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_h_ht);
			}
			if (gmCnt == 110) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_bgm451);
			}
			if (gmCnt == 200) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_hkoback_e);
			}
			if (gmCnt == 456) {
				//display
				dsc.setEarthExplosion ();
				//sound
				//(stop)
//				cmn_soundController.Instance.stopBgm();
				cmn_soundController.Instance.pauseBgm();
				cmn_soundController.Instance.stopSe ();
//				cmn_soundController.Instance.stopLoopSe ();
				cmn_soundController.Instance.stopVoice ();
				//(space)
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_space100);
			}
			if (gmCnt == 536) {
				hac.termHiari ();
			}
			if (gmCnt == 546) {
				//display
				dsc.termEarthExplosion ();
				//sound
				//(stop)
				cmn_soundController.Instance.stopSe ();
//				cmn_soundController.Instance.stopLoopSe ();
				//(hko back)
//				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_bgm451);
				cmn_soundController.Instance.resumeBgm();
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_hkoback);	//(loop)
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_hkoback_e);
			}
			if (gmCnt == 584) {
				//hiari particles
				hac.termHiariParticles ();
			}
			if (gmCnt == 765) {
				//sound
				cmn_soundController.Instance.stopLoopSe ();
			}
			if (gmCnt == 797) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_wn);
			}
			if (gmCnt >= 886) {
				gameMode = gmFadeout;
				gmCnt = 0;
				//state
				cmn_staticData.Instance.kodemo = false;
				//sound
				cmn_soundController.Instance.fadeoutBgm();
				cmn_soundController.Instance.stopSe ();
				break;
			} else {
				gmCnt++;
			}
			break;
		case gmPlayerKo:
			//player ko
			if (gmCnt == 0) {
				//slow 1
				Time.timeScale = 0.02f;
			}
			if (gmCnt == 1) {
				//slow 2
				Time.timeScale = 0.2f;
			}
			if (gmCnt == 15) {
				//normal speed
				Time.timeScale = 1.0f;
			}
			if (gmCnt == 75) {
				//camera
				cameraCtr.setPlayerKo ();
				//display
				dsc.setSubmessage ("生け捕り失敗...");
			}
			if (gmCnt >= 75) {
				//tap
				if (cmn_staticData.Instance.kodemo == false) {
					if (Input.GetMouseButtonUp (0) == true) {
						if (pause == false) {
							if (tapCansel == false) {
								gameMode = gmFadeout;
								gmCnt = 0;
								//state
								cmn_staticData.Instance.kodemo = false;
								//sound
								cmn_soundController.Instance.fadeoutBgm();
								cmn_soundController.Instance.stopSe ();
								cmn_soundController.Instance.stopVoice ();
								break;
							} else {
								tapCansel = false;
							}
						}
					}
				}
			}
			if (gmCnt == 90) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_los110);
			}
			if (gmCnt == 110) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_bgm351);
			}
			if (gmCnt == 160) {
				//sound
				if (cmn_staticData.Instance.pko_vo_cnt == 0) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ko_110);
				} else if(cmn_staticData.Instance.pko_vo_cnt == 1) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ko_100);
				}
				cmn_staticData.Instance.pko_vo_cnt++;
				if (cmn_staticData.Instance.pko_vo_cnt >= 2) {
					cmn_staticData.Instance.pko_vo_cnt = 0;
				}
			}
			if (gmCnt >= 485) {
				gameMode = gmFadeout;
				gmCnt = 0;
				//state
				cmn_staticData.Instance.kodemo = false;
				//sound
				cmn_soundController.Instance.fadeoutBgm();
				cmn_soundController.Instance.stopSe ();
				cmn_soundController.Instance.stopVoice ();
				break;
			} else {
				gmCnt++;
			}
			break;
		case gmFadeout:
			//fade out
			if (gmCnt == 0) {
				scr.setFadeout ();
			}
			gmCnt++;
			break;
		case gmLoadScene:
			//load scene
			if (gmCnt == 0) {
				//static data
				if (result == true) {
					//hiari level up
					cmn_staticData.Instance.hlevel++;
					if (cmn_staticData.Instance.hlevel >= 5) {
						cmn_staticData.Instance.hlevel = 4;
					}
					//hiari win num
					cmn_staticData.Instance.hiariwin ++;
				} else {
					//hiari lose num
					cmn_staticData.Instance.hiarilose ++;
				}
				//load scene
				scn.setResultScene ();
			}
			gmCnt++;
			break;
		default:
			break;
		}
	}

	private void pauseProcess(){
		if (pause == false) {	//pause?
			//no pause
			if ((gameMode != gmFadein) && (gameMode != gmFadeout)) {
				//pause
				pause = true;
				//time scale
				backTimeScale = Time.timeScale;
				Time.timeScale = 0.0f;
				//player input cansel
				plc.tapPauseButton ();
				//display
				dsc.pauseOn ();
				//tap cansel
				tapCansel = true;
				//sound
				cmn_soundController.Instance.playSound( cmn_soundController.Instance.sd_se_sl130 );
				cmn_soundController.Instance.pauseBgm ();
				cmn_soundController.Instance.pauseSe ();
				cmn_soundController.Instance.pauseVoice ();
			}
		} else {
			//pause
			pause = false;
			//time scale
			Time.timeScale = backTimeScale;
			//display
			dsc.pauseOff();
			//tap cansel
//			tapCansel = false;
			//sound
			cmn_soundController.Instance.playSound( cmn_soundController.Instance.sd_se_sl130 );
			cmn_soundController.Instance.resumeBgm ();
			cmn_soundController.Instance.resumeSe ();
			cmn_soundController.Instance.resumeVoice ();
		}
	}


	//public

	//pause button on
	public void tapPauseButton(){
		//pause process
		this.pauseProcess();
	}

	//term fade in
	public void termFadeIn(){
		if (gameMode == gmFadein) {
			gameMode = gmReady;
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

	//set player ko
	public void setPlayerKo(){
		//mode
		gameMode = gmPlayerKo;
		gmCnt = 0;
		//result
		result = false;
		//display
		dsc.setSubmessage("YOU LOSE...");
		//hiari
		hac.setPlayerKo();
		//common
		cmn_staticData.Instance.lastresult = false;
		//sound
		cmn_soundController.Instance.stopBgm();
	}

	//set hiari ko
	public void setHiariKo(){
		//mode
		gameMode = gmHiariKo;
		gmCnt = 0;
		//result
		result = true;
		//display
		dsc.setSubmessage("YOU WIN!");
		//player
		plc.setHiariKo();
		//common
		cmn_staticData.Instance.lastresult = true;
		//sound
		cmn_soundController.Instance.stopBgm();
	}

	//generate player bullet
	public void generatePlayerBullet( int bulletType, Vector3 bpos, float xx, float yy, float zz, float atcdir ){
		if (bulletType == 0) {	//normal bullet
			//genarate
			GameObject go = Instantiate (btl_playerBulletControllerPrefab) as GameObject;
			go.GetComponent<btl_playerBulletController> ().setInitState (bulletType, bpos, xx, yy, zz, atcdir);
		} else if (bulletType == 1) {	//large bullet
			//genarate
			GameObject go = Instantiate (btl_playerBulletController_LPrefab) as GameObject;
			go.GetComponent<btl_playerBulletController> ().setInitState (bulletType, bpos, xx, yy, zz, atcdir);
		} else if (bulletType == 2) {	//large bullet tachio
			//genarate
			GameObject go = Instantiate (btl_playerBulletController_TPrefab) as GameObject;
			go.GetComponent<btl_playerBulletController> ().setInitState (bulletType, bpos, xx, yy, zz, atcdir);
		} else if (bulletType == 3) {	//normal bullet pants
			//genarate
			GameObject go = Instantiate (btl_playerBulletController_PPrefab) as GameObject;
			go.GetComponent<btl_playerBulletController> ().setInitState (bulletType, bpos, xx, yy, zz, atcdir);
		}
	}

	//generate enemy bullet
	public void generateEnemyBullet( int bulletType, Vector3 bpos, float xx, float yy, float zz, float atcdir ){
		if (bulletType == 0) {	//normal bullet
			//genarate
			GameObject go = Instantiate (btl_enemyBulletControllerPrefab) as GameObject;
			go.GetComponent<btl_enemyBulletController> ().setInitState (bulletType, bpos, xx, yy, zz, atcdir);
		} else if (bulletType == 1) {	//large bullet
			//genarate
			GameObject go = Instantiate (btl_enemyBulletController_LPrefab) as GameObject;
			go.GetComponent<btl_enemyBulletController> ().setInitState (bulletType, bpos, xx, yy, zz, atcdir);
		} else if (bulletType == 2) {	//hyoshiki bullet
			//genarate
			GameObject go = Instantiate (btl_enemyBulletController_HPrefab) as GameObject;
			go.GetComponent<btl_enemyBulletController> ().setInitState (bulletType, bpos, xx, yy, zz, atcdir);
		} else if (bulletType == 3) {	//smartphone bullet
			//genarate
			GameObject go = Instantiate (btl_enemyBulletController_PPrefab) as GameObject;
			go.GetComponent<btl_enemyBulletController> ().setInitState (bulletType, bpos, xx, yy, zz, atcdir);
		}
	}

	//generate hiari parts
	public void generateHiariParts( Vector3 hpos ){
		for (int i = 0; i < 50; i++) {
			//genarate
			GameObject go = Instantiate (btl_hiariPartsPrefab) as GameObject;
			//set
			float dir = Random.Range(0,360.0f);
			float dist = Random.Range (0.5f, 4.2f);
			float rx = Random.Range(0,360.0f);
			float ry = Random.Range(0,360.0f);
			float rz = Random.Range(0,360.0f);
			float sx = Random.Range(0.2f,3.0f);
			float sy = Random.Range(0.2f,3.0f);
			float sz = Random.Range(0.2f,3.0f);
			float posy = Random.Range(0.1f,0.3f);
			float tmpx = hpos.x + (Mathf.Cos (dir * Mathf.Deg2Rad) * dist);
			float tmpz = hpos.z + (Mathf.Sin (dir * Mathf.Deg2Rad) * dist);
			go.transform.position = new Vector3 (tmpx, posy, tmpz);
			go.transform.rotation = Quaternion.Euler ( new Vector3 (rx, ry, rz) );
			go.transform.localScale = new Vector3 (sx, sy, sz);
		}
	}

	//generate cola can
	public void generateColaCan( Vector3 ppos, float dir ){
		//genarate
		GameObject go = Instantiate (btl_colaCanPrefab) as GameObject;
		dir = (360 - dir) + 90;
		dir = dir + 30;
		float tmpx = ppos.x + (Mathf.Cos (dir * Mathf.Deg2Rad) * 0.4f);
		float tmpz = ppos.z + (Mathf.Sin (dir * Mathf.Deg2Rad) * 0.4f);
		Vector3 tmppos =  go.transform.position;
		tmppos.x = tmpx;
		tmppos.y = 0.0f;
		tmppos.z = tmpz;
		go.transform.position = tmppos;
//		go.transform.Rotate (new Vector3 (0.0f, 10.0f, 0.0f));
	}

	//set hiari hp gauge
	public void setHiariHpGauge( int initHp, int hp ){
		if (dsc == null) {
			//display controller
			displayCtr = GameObject.Find ("btl_displayController");
			dsc = displayCtr.GetComponent<btl_displayController> ();
		}
		dsc.setHiariHpGauge (initHp, hp);
	}

	//set player hp gauge
	public void setPlayerHpGauge( int initHp, int hp ){
		if (dsc == null) {
			//display controller
			displayCtr = GameObject.Find ("btl_displayController");
			dsc = displayCtr.GetComponent<btl_displayController> ();
		}
		dsc.setPlayerHpGauge (initHp, hp);
	}

	//tame gauge display on
	public void tameGaugeOn( Vector3 wpos ){
		dsc.tameGaugeOn (wpos);
	}

	//tame gauge display off
	public void tameGaugeOff(){
		dsc.tameGaugeOff ();
	}

	//tame gauge update
	public void tameGaugeUpdate( int maxcnt, int cnt ){
		dsc.tameGaugeUpdate (maxcnt, cnt);
	}

	//set shake xz effect
	public void setShakeEffect_xz( int cnt = 20 ){
		stc.setShakeEffect_xz (cnt);
	}

	//set shake y effect
	public void setShakeEffect_y( int cnt = 20 ){
		stc.setShakeEffect_y (cnt);
	}

	//update score
	public void updateScore(){
		dsc.updateScore ();
	}

	//get gamemode
	public int getGameMode(){
		return this.gameMode;
	}

}
