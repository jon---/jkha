using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fld_mainController : MonoBehaviour {
	//public
	//house1Prefab
	public GameObject house1Prefab;
	//house2Prefab
	public GameObject house2Prefab;
	//house3Prefab
	public GameObject house3Prefab;
	//house4Prefab
	public GameObject house4Prefab;
	//kanban1Preafb
	public GameObject kanban1Prefab;
	//kanban2Preafb
	public GameObject kanban2Prefab;
	//kanban3Preafb
	public GameObject kanban3Prefab;
	//kanban4Preafb
	public GameObject kanban4Prefab;
	//kanban5Preafb
	public GameObject kanban5Prefab;
	//car1Prefab
	public GameObject car1Prefab;
	//car2Prefab
	public GameObject car2Prefab;
	//car3Prefab
	public GameObject car3Prefab;
	//car4Prefab
	public GameObject car4Prefab;
	//car atack particles
	public GameObject carAtackPrefab;

	//private
	//local const
	//game mode
	const int gmFadein = 0;
	const int gmReady = 1;
	const int gmWaitStart = 2;
	const int gmField = 3;
	const int gmEncount = 4;
	const int gmFadeout = 5;
	const int gmLoadScene = 6;


	//system local
	int intervalCnt;	//interval counter

	//component cash
	fld_cameraController cameraCtr;
	GameObject sceneCtr;
	cmn_sceneController scn;
	GameObject screenCtr;
	cmn_screenController scr;
	GameObject displayCtr;
	fld_displayController dsc;
	GameObject stageCtr;
	fld_stageController stc;
	GameObject playerCtr;
	fld_playerController plc;
	GameObject fld_hiariCtr;
	fld_hiariController hac;
	Animator animt;

	//local
	//game mode
	int gameMode;
	int gmCnt;

	//move seq
	int mvseq;
	int mvcnt;
	float rtcnt;

	//field map
	int[,] fldmap;

	//encount score
	float enstime;

	//sound
	bool pokesnd;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//camera controller
		cameraCtr = Camera.main.GetComponent<fld_cameraController>();

		//scene controller
		sceneCtr = GameObject.Find ("fld_sceneController");
		scn = sceneCtr.GetComponent<cmn_sceneController> ();

		//screen controller
		screenCtr = GameObject.Find ("fld_screenController");
		scr = screenCtr.GetComponent<cmn_screenController> ();

		//display controller
		displayCtr = GameObject.Find ("fld_displayController");
		dsc = displayCtr.GetComponent<fld_displayController> ();

		//stage controller
		stageCtr = GameObject.Find ("fld_stageController");
		stc = stageCtr.GetComponent<fld_stageController> ();

		//playercontroller
		playerCtr = GameObject.Find ("fld_playerController");
		plc = playerCtr.GetComponent<fld_playerController> ();

		//hiari controller
		fld_hiariCtr = GameObject.Find ("fld_hiariController");
		hac = fld_hiariCtr.GetComponent<fld_hiariController> ();

		//game mode
		gameMode = gmFadein;
		gmCnt = 0;

		//encount score
		enstime = 0.0f;

		//sound
		pokesnd = false;

		//field map
		fldmap = new int[23,13];

		//generate field map
		generateFieldMap();

		//set car map
		setCarMap();

		//generate field object
		generateFieldObject();

		//hiari set
		setHiari();

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
			//fade in
			if (gmCnt == 0) {
				//screen fade in
				scr.setFadein ();
				//score
				dsc.updateScore();
			}
			gmCnt++;
			break;
		case gmReady:
			//ready
			if (gmCnt == 0) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_bgm301);
			}
			if (gmCnt == 25) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_start);
			}
			gmCnt++;
			if (gmCnt == 1) {
				//camera
				cameraCtr.zoomUp ();
				//display
				dsc.setSubmessage ("READY...");
				//player
				plc.termWakeup();
				//hiari
				hac.termWakeup();
			}
			if (gmCnt == 60) {
				gameMode = gmWaitStart;
				gmCnt = 0;
				cameraCtr.zoomOut ();
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ready);
			}
			break;
		case gmWaitStart:
			//wait start
			gmCnt++;
			if (gmCnt == 1) {
//				dsc.setSubmessage ("TAP TO START!");
			}
			if (gmCnt == 50) {
				//disp tap to start
				dsc.setSubmessage ("TAP TO START!");
			}
			if (gmCnt >= 50) {
				//tap input to field mode
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
					gameMode = gmField;
					gmCnt = 0;
					//camera
					cameraCtr.zoomOut ();
					//display
					dsc.setSubmessage ("");
					//player
					plc.setFieldStart ();
					//hiari
					hac.setFieldStart ();
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_go);
				}
			}
			break;
		case gmField:
			//field
			//encount score process
			enstime = enstime + Time.deltaTime;
			if (enstime >= 3.0f) {
				enstime = 0.0f;
				cmn_staticData.Instance.timebonus -= 80;
				if (cmn_staticData.Instance.timebonus <= 0) {
					cmn_staticData.Instance.timebonus = 0;
				} else {
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_ds100);
				}
				dsc.setStatus ();
			}
			//position message
			Vector3 ppos = plc.getPlayerPos ();
			Vector3 hpos = hac.getHiariPos ();
			//near?
			if ((Mathf.Abs (ppos.x - hpos.x) <= 50) &&
			    (Mathf.Abs (ppos.z - hpos.z) <= 50)) {
				//message
				dsc.setAlertmessage ("ポケベルにメッセージ!\n「ヒアリ　チカクニイル」");
				dsc.setPokebellOn ();
				//sound
				if (pokesnd == false) {
					pokesnd = true;
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_pokebell);
				}
			} else {
				//message
				dsc.setAlertmessage ("");
				dsc.setPokebellOff ();
				//sound
				pokesnd = false;
				cmn_soundController.Instance.stopLoopSe();
			}
			break;
		case gmEncount:
			//encount
			if (gmCnt == 0) {
				//sound
				cmn_soundController.Instance.stopBgm();
				pokesnd = false;
				cmn_soundController.Instance.stopLoopSe ();
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_encount);
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_encount);
			}
			if (gmCnt == 45) {
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_encount);
			}
			gmCnt++;
			if (gmCnt == 1) {
				//camera
				cameraCtr.zoomUpEncount ();
				//message
				dsc.setAlertmessage ("");
				dsc.setPokebellOff ();
//				dsc.setSubmessage ("ヒアリ発見!!\n");
				dsc.setSubmessage ("ヒアリ発見!!");
				//hiari
				hac.setEncount ();
			}
//			if (gmCnt == 50) {
//				//message
//				dsc.setSubmessage ("ヒアリ発見!!\n(TAP TO SKIP)");
//			}
			if (gmCnt == 120) {
				//message
//				dsc.setSubmessage ("生け捕り開始!!\n(TAP TO SKIP)");
				dsc.setSubmessage ("生け捕り開始!!");
			}
			if (gmCnt == 160) {
				//mov
				gameMode = gmFadeout;
				gmCnt = 0;
			}
			if (gmCnt >= 50) {
				if (Input.GetMouseButtonDown (0) == true) {
					//mov
					gameMode = gmFadeout;
					gmCnt = 0;
				}
			}
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
				//set battle scene
				scn.setBattleScene ();
			}
			gmCnt++;
			break;
		default:
			break;
		}

	}


	//generate field map
	private void generateFieldMap(){
		//init
		for (int x = 0; x < 23; x++) {
			for (int y = 0; y < 13; y++) {
				fldmap [x, y] = 0;
			}
		}

		//side wall
		for (int x = 0; x < 23; x++) {
			fldmap [x, 0] = 1;
			fldmap [x, 12] = 1;
		}
		for (int y = 0; y < 13; y++) {
			fldmap [0, y] = 1;
			fldmap [22, y] = 1;
		}

		//inside wall
		for (int x = 2; x < 21; x++) {
			for (int y = 0; y < 11; y++) {
				if (x % 2 == 0) {
					if (y % 2 == 0) {
						fldmap [x, y] = 1;
					}
				}
			}
		}

		//wall phase1
		for (int x = 2; x < 21; x++) {
			if (x % 2 == 0) {
				int wtmp = 10 + (4 - cmn_staticData.Instance.hlevel);
				if (Random.Range (0, wtmp) <= 7) {	//no wall random 8-
					int tmp = Random.Range (0, 4);
					if (tmp == 0) {
						if (fldmap [x, 2 - 1] == 0) {
							fldmap [x, 2 - 1] = 1;
						} else {
							tmp++;
						}
					}
					if (tmp == 1) {
						if (fldmap [x + 1, 2] == 0) {
							fldmap [x + 1, 2] = 1;
						} else {
							tmp++;
						}
					}
					if (tmp == 2) {
						if (fldmap [x, 2 + 1] == 0) {
							fldmap [x, 2 + 1] = 1;
						} else {
							tmp++;
						}
					}
					if (tmp == 3) {
						if (fldmap [x - 1, 2] == 0) {
							fldmap [x - 1, 2] = 1;
						} else {
							//nop
						}
					}
				}
			}
		}

		//wall phase2
		for (int y = 4; y < 11; y++) {
			for (int x = 2; x < 21; x++) {
				if ( (x % 2 == 0) && (y % 2 == 0)) {
					int wtmp = 10 + (4 - cmn_staticData.Instance.hlevel);
					if (Random.Range (0, wtmp) <= 7) {	//no wall random 8
						int tmp = Random.Range (0, 3);
						tmp = tmp + 1;
						if (tmp == 1) {
							if (fldmap [x + 1, y] == 0) {
								fldmap [x + 1, y] = 1;
							} else {
								tmp++;
							}
						}
						if (tmp == 2) {
							if (fldmap [x, y + 1] == 0) {
								fldmap [x, y + 1] = 1;
							} else {
								tmp++;
							}
						}
						if (tmp == 3) {
							if (fldmap [x - 1, y] == 0) {
								fldmap [x - 1, y] = 1;
							} else {
								//nop
							}
						}
					}
				}
			}
		}

	}

	//generate field object
	private void generateFieldObject(){
		for (int x = 0; x < 23; x++) {
			for (int y = 0; y < 13; y++) {
				if (fldmap [x, y] == 1) {
					//genarate house
					GameObject go = null;
					int tmp = 0;
					//kanban or house
					if ( (Random.Range (0, 5) <= 0) && (x != 0) && (x != 22) && (y != 0) && (y != 12) ) {	//kanban?
						tmp = (10 + Random.Range (0, 5));	//0-4 kanban random
					}else{
						tmp = Random.Range (0, 4);	//0-3 house random
					}
					if (tmp == 0) {
						go = Instantiate (house1Prefab) as GameObject;
					} else if (tmp == 1) {
						go = Instantiate (house2Prefab) as GameObject;
					} else if (tmp == 2) {
						go = Instantiate (house3Prefab) as GameObject;
					} else if (tmp == 3) {
						go = Instantiate (house4Prefab) as GameObject;
					} else if (tmp == 10) {
						go = Instantiate (kanban1Prefab) as GameObject;
					} else if (tmp == 11) {
						go = Instantiate (kanban2Prefab) as GameObject;
					} else if (tmp == 12) {
						go = Instantiate (kanban3Prefab) as GameObject;
					} else if (tmp == 13) {
						go = Instantiate (kanban4Prefab) as GameObject;
					} else if (tmp == 14) {
						go = Instantiate (kanban5Prefab) as GameObject;
					}
					Vector3 tmppos;
					tmppos.x = ((x - 11) * 10);
					tmppos.y = 0.0f;
					tmppos.z = (((12 - y) - 6) * 10);
					go.transform.position = tmppos;
				} else if ( (fldmap[x,y] >= 20) && (fldmap[x,y] <= 23) ){
					//genarate car
					GameObject go = null;
					switch (fldmap [x, y]) {
					case 20:
						go = Instantiate (car1Prefab) as GameObject;
						break;
					case 21:
						go = Instantiate (car2Prefab) as GameObject;
						break;
					case 22:
						go = Instantiate (car3Prefab) as GameObject;
						break;
					case 23:
						go = Instantiate (car4Prefab) as GameObject;
						break;
					default:
						break;
					}
					if (go != null) {
						Vector3 tmppos;
						tmppos.x = ((x - 11) * 10);
						tmppos.y = 2;
						tmppos.z = (((12 - y) - 6) * 10);
						go.transform.position = tmppos;
						float tmpd = 30 + (float)((int)Random.Range (0, 4) * 90);
						go.transform.rotation = Quaternion.Euler ( new Vector3 (0.0f, tmpd, 0.0f) );
					}
				}
			}
		}
	}

	//set car to fieldmap
	private void setCarMap(){
		for (int x = 3; x < 20; x=x+2) {
			for (int y = 3; y < 10; y=y+2) {
				if ((x != 11) || (y != 5)) {
					if (Random.Range (0, 10) <= 3) {	//car generate random 0-3
//					if (Random.Range (0, 10) <= 9) {	//car generate random all
						fldmap [x, y] = (20 + Random.Range (0, 4));
					}
				}
			}
		}
	}

	//set hiari
	private void setHiari(){
		//xline or zline
		int x = 1;
		int z = 1;
		int tmp = Random.Range(0,2);	//x or z
		int tmp2 = Random.Range (0, 2);	//front/left or back/right
		if (tmp == 0) {
			//xline
			if (Random.Range (0, 2) == 0) {
				x = Random.Range (0, 3);
			} else {
				x = Random.Range (8, 11);
			}
			x = (x * 2) + 1;
			if (tmp2 == 0) {
				z = 1;	
			} else {
				z = 11;
			}
		} else {
			//zline
			z = Random.Range(0,6);
			z = (z * 2) + 1;
			if (tmp2 == 0) {
				x = 1;	
			} else {
				x = 21;
			}
		}
		//fix world pos
		x = ((x - 11 ) * 10 );
		z = (((12 - z) - 6) * 10);
		//hiari set
		hac.setInitState( x, z );
	}



	//public

	//get game mode
	public int getGmMode(){
		return gameMode;
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

	//set player encount
	public void setPlayerEncount(){
		//mode
		this.gameMode = gmEncount;
		gmCnt = 0;
		//message
		dsc.setSubmessageTimeReset();
		//score
		cmn_staticData.Instance.score += cmn_staticData.Instance.timebonus;
		this.updateScore ();
	}

	//set atack
	public void setAtack( GameObject car ){
		//generate particles
		GameObject go = Instantiate (carAtackPrefab) as GameObject;
		Vector3 tmppos = go.transform.position;
		tmppos.x = car.transform.position.x;
		tmppos.z = car.transform.position.z;
		go.transform.position = tmppos;
		go.GetComponent<ParticleSystem> ().Play ();

		//bonus
		if (car.tag == "fld_car1") {
			//message
			dsc.setSubmessage ("ヤフ○オクで売って\n1000円ゲット!\n攻撃力+1!", 100);
			//money
			cmn_staticData.Instance.moneys += 10;
			//atack up
			cmn_staticData.Instance.atackplus += 1;
			//score
			cmn_staticData.Instance.score += 540;
			this.updateScore ();
		} else if (car.tag == "fld_car2") {
			//message
			dsc.setSubmessage ("ヤフ○オクで売って\n1000円ゲット!\nHP+1!", 100);
			//money
			cmn_staticData.Instance.moneys += 10;
			//hp up
			cmn_staticData.Instance.hpplus += 1;
			//score
			cmn_staticData.Instance.score += 540;
			this.updateScore ();
		} else if (car.tag == "fld_car3") {
			//message
			dsc.setSubmessage ("ヤフ○オクで売って\n1000円ゲット!\n防御力+1!", 100);
			//money
			cmn_staticData.Instance.moneys += 10;
			//defence up
			cmn_staticData.Instance.defenceplus += 1;
			//score
			cmn_staticData.Instance.score += 540;
			this.updateScore ();
		} else if (car.tag == "fld_car4") {
			//message
			int tmp = Random.Range( 0, 3);
			if (tmp == 0) {
				dsc.setSubmessage ("ヤフ○オクで売って\n1500円ゲット!\n勝負下着ゲット!\n攻撃力+2!", 100);
				//atack up
				cmn_staticData.Instance.atackplus += 2;
			} else if (tmp == 1) {
				dsc.setSubmessage ("ヤフ○オクで売って\n1500円ゲット!\n勝負下着ゲット!\nHP+2!", 100);
				//hp up
				cmn_staticData.Instance.hpplus += 2;
			} else if (tmp == 2) {
				dsc.setSubmessage ("ヤフ○オクで売って\n1500円ゲット!\n勝負下着ゲット!\n防御力+2!", 100);
				//defence up
				cmn_staticData.Instance.defenceplus += 2;
			}
			//money
			cmn_staticData.Instance.moneys += 15;
			//sitagi up
			cmn_staticData.Instance.sitagi = true;
			//score
			cmn_staticData.Instance.score += 680;
			this.updateScore ();
		}
		//shake effect
		stc.setShakeEffect_y();
		//small money process
		if (cmn_staticData.Instance.moneys >= 100) {
			int tmp = (cmn_staticData.Instance.moneys / 100);
			cmn_staticData.Instance.money += tmp;
			cmn_staticData.Instance.moneys -= 100;
		}
		//status message
		dsc.setStatus ();

		//car destroy
		GameObject.Destroy (car);

		//sound
		cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_carexp);
	}

	//update score
	public void updateScore(){
		dsc.updateScore ();
	}

}
