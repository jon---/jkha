using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btl_hiariController : MonoBehaviour {
	//public

	//private
	//local const
	//base hit point
	const int basehitpoint = 200;
//	const int basehitpoint = 10;	//for debug
	//move seq const
	const int movwakeup = 0;
	const int movthink = 1;
	const int movidlesu = 2;
	const int movidlesd = 3;
	const int movsitdown = 4;
	const int movright = 5;
	const int movleft = 6;
	const int movjump = 7;
	const int movatack = 8;
	const int movatacknear = 9;
	const int movlongatack = 10;
	const int movlongatacknear = 11;
	const int movsmalldamage = 12;
	const int movbigdamage = 13;
	const int movwaitstart = 100;
	const int movwaithko = 110;
	const int movwaitpko = 120;
	//mov speed
	const float movdd = 1.4f;
	//dir for pos offset
	const float posdiroffset = 0.0f;//16.0f;
	//dir for hiari rotate offset
	const float rttdiroffset = 90.0f;//-90.0f;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	btl_cameraController cameraCtr;
	GameObject mainCtr;
	btl_mainController mc;
	GameObject antCtr;
	CapsuleCollider cashCapsuleCollider_antCtr;
	Renderer cashRenderer_antCtr;
	btl_antController atc;
	GameObject playerCtr;
	btl_playerController plc;
	GameObject hatackParticles;
	ParticleSystem cashParticleSystem_hatackParticles;
	GameObject htameParticles;
	ParticleSystem cashParticleSystem_htameParticles;
	GameObject hkoParticles;
	ParticleSystem cashParticleSystem_hkoParticles;

	//local
	//hiari level
	int hLevel;

	//hiari behavior
	int behseq;
	int behcnt;
	int behwait;
	//for ant Controller
	bool smallDamage;
	bool bigDamage;

	//move seq
	int movseq;
	int movcnt;
	int lastmovseq;	//(for bihavior think)
	int backmovseq;	//(for small damage)
	int backmovcnt;	//(for small damage)
	float movdir;	//dir for pos
	float atackdir;	//dir hiari large atack near
	//state
	bool hko;
	bool pko;
	int unavailableCnt;
	//translate
	float xx;
	float yy;
	float zz;
	//rotate
	float xdir;
	float ydir;
	float zdir;
	//translate adjust
	float breathy;
	float jumpy;
	float atackz;

	//action
	bool duringAction;	//(for sitdown)
	int actseq;
	int actcnt;

	//state
	int blinkCnt;

	//bullet pattern cnt
	int nbpcnt;
	int lbpcnt;

	//init hitpoint
	int hpIntial;

	//hitpoint
	int hp;

	//hit point start update
	bool hpStart;

	//sound
	int sdcnt1;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//camera controller
		cameraCtr = Camera.main.GetComponent<btl_cameraController>();

		//maincontroller
		mainCtr = GameObject.Find ("btl_mainController");
		mc = mainCtr.GetComponent<btl_mainController> ();

		//ant controller
		antCtr = GameObject.Find ("fire-ant");
		cashCapsuleCollider_antCtr = antCtr.GetComponent<CapsuleCollider> ();
		cashRenderer_antCtr = antCtr.GetComponent<Renderer> ();
		atc = antCtr.GetComponent<btl_antController> ();

		//playercontroller
		playerCtr = GameObject.Find ("btl_playerController");
		plc = playerCtr.GetComponent<btl_playerController> ();

		//hiari atack particles
		hatackParticles = GameObject.Find("hatackParticles");
		cashParticleSystem_hatackParticles = hatackParticles.GetComponent<ParticleSystem> ();

		//hiari tame for atack L particles
		htameParticles = GameObject.Find("htameParticles");
		cashParticleSystem_htameParticles = htameParticles.GetComponent<ParticleSystem> ();

		//hiari ko particles
		hkoParticles = GameObject.Find("hkoParticles");
		cashParticleSystem_hkoParticles = hkoParticles.GetComponent<ParticleSystem> ();

		//hiari level
		hLevel = cmn_staticData.Instance.hlevel;
		//debug -->
//		hLevel = 2;	//(0-4)	//for debug
		//debug <--

		//behavior
		behcnt = 0;
		behwait = 0;
		smallDamage = false;
		bigDamage = false;

		//move
		movseq = movwakeup;
		movcnt = 0;
		lastmovseq = movwakeup;
		backmovseq = movwakeup;
		backmovcnt = 0;
		movdir = 90.0f;
		atackdir = 0.0f;
		//translate
		xx = 0.0f;
		yy = 0.0f;
		zz = 0.0f;
		//rotate
		xdir = 0.0f;
		ydir = 0.0f;
		zdir = 0.0f;
		//translate adjust
		breathy = 0.0f;
		atackz = 0.0f;
		//state
		hko = false;
		pko = false;
		unavailableCnt = 0;

		//state
		blinkCnt = 0;

		//fix initial pos
		float nx = Mathf.Cos ((movdir+posdiroffset) * Mathf.Deg2Rad) * 3.0f;
		float ny = 0.2f;
		float nz = Mathf.Sin ((movdir+posdiroffset) * Mathf.Deg2Rad) * 3.0f;
		Vector3 pos = new Vector3 (nx, ny, nz);
		cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
		cashTransform.position = pos;

		//action
		duringAction = false;
		actseq = 0;
		actcnt = 0;

		//bullet pattern cnt
		nbpcnt = 0;
		lbpcnt = 0;

		//atack/defence plus
		cmn_staticData.Instance.hatackplus = hLevel;
		if (hLevel >= 2) {
			cmn_staticData.Instance.hatackplus += Random.Range (0, (hLevel - 2));
		}
		cmn_staticData.Instance.hdefenceplus = hLevel;
		if (hLevel >= 3) {
			cmn_staticData.Instance.hdefenceplus += Random.Range (0, (hLevel - 2));
		}

		//enemy inital hitpoint
//		hpIntial = basehitpoint + (hLevel*35);
//		hpIntial = basehitpoint + (hLevel*45);
		hpIntial = basehitpoint + (hLevel*36);

		//enemy hitpoint
		hp = 0;

		//hp start update
		hpStart = true;

		//sound
		sdcnt1 = 0;
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
 

		//hiari process
		hiariProcess();


		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}
		
	}


	//private

	//hiari process
	private void hiariProcess(){
		//fix hiari behavior
		bool behaviorThink = false;
		bool behaviorIdleSu = false;
		bool behaviorIdleSd = false;
		bool behaviorSitDown = false;
		bool behaviorRight = false;
		bool behaviorLeft = false;
		bool behaviorJump = false;
		bool behaviorAtack = false;
		bool behaviorLongAtack = false;
		bool behaviorSmallDamage = false;
		bool behaviorBigDamage = false;
		bool behaviorWaitHko = false;
		bool behaviorWaitPko = false;
		//damage
		if (smallDamage == true) {
			smallDamage = false;
			if (hko == false) {
				behaviorSmallDamage = true;
			}
		}
		if (bigDamage == true) {
			bigDamage = false;
			if (hko == false) {
				behaviorBigDamage = true;
			}
		}
		//fix behavior process
		int hlvl = hLevel+1;	//hiari level for random
		int hlvlr = (4 - hLevel) + 1;	//hiari level reverse for random
		int tmpr = 0;	//for random val
		switch (movseq) {
		case movthink:
			//think wait
			if (behcnt == 0) {
//				behwait = ((4-hLevel) * 10);
//				behwait = ((4-hLevel) * Random.Range(3,11));
				behwait = ((4-hLevel) * Random.Range(1,6));
			}
			if (behcnt >= behwait) {
				behcnt = 0;
				if (pko == false) {
					//atack for player down
					if (plc.getPlayerMov() == 11) {	//down
						if (plc.getPlayerCnt() >= 100) {	//before rebirth
							if (Random.Range (0, 3) == 0) {
								if (Random.Range (0, (5 - hLevel)) == 0) {
									if (Random.Range (0, 2) == 0) {
										behaviorAtack = true;
									} else {
										behaviorLongAtack = true;
									}
								}
							}
						}
					}
					//jump for player bullet
					if ( (plc.getPlayerMov () == 5) || (plc.getPlayerMov () == 6) ||	//atack / atacknear
						(((plc.getPlayerMov() == 8) || (plc.getPlayerMov() == 9)) && plc.getPlayerCnt() <= 5) ) {	//big atack / big atacknear
						if (Random.Range (0, 8) == 0) {
							if (Random.Range (0, (5 - hLevel)) == 0) {
								behaviorJump = true;
							}
						}
					}
					//no high behavior fix?
					if ((behaviorAtack == false) && (behaviorLongAtack == false) && (behaviorJump == false)) {
						//think
						switch (lastmovseq) {	//last move seq
						case movthink:
							//think
							#if UNITY_EDITOR
								Debug.Log ("think -> think");
							#endif
							//continue any action
							if (xdir <= -35) {
								behaviorIdleSu = true;
							} else {
								if (duringAction == true) {
									behaviorSitDown = true;
								} else {
									if (xdir == 0) {
										behaviorIdleSd = true;
									} else {
										behaviorIdleSu = true;
									}
								}
							}
							break;
						case movidlesu:
							//idle stand up
							//atack for player down
							if (plc.getPlayerMov () == 11) {	//down
								if (plc.getPlayerCnt () >= 100) {	//before rebirth
									if (Random.Range (0, 3) == 0) {
										if (Random.Range (0, (5 - hLevel)) == 0) {
											if (Random.Range (0, 2) == 0) {
												behaviorAtack = true;
											} else {
												behaviorLongAtack = true;
											}
										}
									}
								}
							}
							//jump for player bullet
							if ((plc.getPlayerMov () == 5) || (plc.getPlayerMov () == 6) ||	//atack / atacknear
							    (((plc.getPlayerMov () == 8) || (plc.getPlayerMov () == 9)) && plc.getPlayerCnt () <= 5)) {	//big atack / big atacknear
								if (Random.Range (0, 7) == 0) {
									if (Random.Range (0, (5 - hLevel)) == 0) {
										behaviorJump = true;
									}
								}
							}
							//any action
							if ((behaviorAtack == false) && (behaviorLongAtack == false) && (behaviorJump == false)) {
								tmpr = Random.Range (0, 8);
								if (tmpr <= 2) {
									behaviorAtack = true;
								} else if (tmpr <= 5) {
									behaviorLongAtack = true;
								} else if (tmpr == 6) {
									behaviorSitDown = true;
								} else {
									behaviorJump = true;
								}
							}
							break;
						case movidlesd:
							//idle sit down
							if (Random.Range (0, hlvl) >= 1) {	//side move or action
								//side move
								if (Random.Range (0, 2) == 0) {
									behaviorLeft = true;
								} else {
									behaviorRight = true;
								}
							} else {
								//any action
								tmpr = Random.Range (0, 7);
								if (tmpr <= 2) {
									behaviorAtack = true;
								} else if (tmpr <= 5) {
									behaviorLongAtack = true;
								} else if (tmpr == 6) {
									behaviorJump = true;
								}
							}
							break;
						case movsitdown:
							//sitdown
							if (Random.Range (0, 4) >= 1) {	//side move or action
								//side move
								if (Random.Range (0, 2) == 0) {
									behaviorLeft = true;
								} else {
									behaviorRight = true;
								}
							} else {
								//any action
								tmpr = Random.Range (0, 7);
								if (tmpr <= 2) {
									behaviorAtack = true;
								} else if (tmpr <= 5) {
									behaviorLongAtack = true;
								} else if (tmpr == 6) {
									behaviorJump = true;
								}
							}
							break;
						case movright:
							//right
							//atack for player down
							if (plc.getPlayerMov () == 11) {	//down
								if (plc.getPlayerCnt () >= 100) {	//before rebirth
									if (Random.Range (0, 3) == 0) {
										if (Random.Range (0, (5 - hLevel)) == 0) {
											if (Random.Range (0, 2) == 0) {
												behaviorAtack = true;
											} else {
												behaviorLongAtack = true;
											}
										}
									}
								}
							}
							//jump for player bullet
							if ((plc.getPlayerMov () == 5) || (plc.getPlayerMov () == 6) ||	//atack / atacknear
							    (((plc.getPlayerMov () == 8) || (plc.getPlayerMov () == 9)) && plc.getPlayerCnt () <= 5)) {	//big atack / big atacknear
								if (Random.Range (0, 7) == 0) {
									if (Random.Range (0, (5 - hLevel)) == 0) {
										behaviorJump = true;
									}
								}
							}
							if ((behaviorAtack == false) && (behaviorLongAtack == false) && (behaviorJump == false)) {
								//atack or move continue or idle
								if (Random.Range (0, 2) == 0) {
									//atack
									if (Random.Range (0, 3) <= 1) {
										behaviorAtack = true;
									} else {
										behaviorLongAtack = true;
									}
								} else {
									//move continue?
									if (Random.Range (0, hlvl) >= 1) {
										//move
										//reverse?
										if (Random.Range (0, hlvl) >= 2) {
											behaviorLeft = true;
										} else {
											behaviorRight = true;
										}
									} else {
										//idle su or sd
										if (Random.Range (0, hlvl) >= 3) {
											behaviorIdleSd = true;
										} else {
											behaviorIdleSu = true;
										}
									}
								}
							}
							break;
						case movleft:
							//left
							//atack for player down
							if (plc.getPlayerMov () == 11) {	//down
								if (plc.getPlayerCnt () >= 100) {	//before rebirth
									if (Random.Range (0, 3) == 0) {
										if (Random.Range (0, (5 - hLevel)) == 0) {
											if (Random.Range (0, 2) == 0) {
												behaviorAtack = true;
											} else {
												behaviorLongAtack = true;
											}
										}
									}
								}
							}
							//jump for player bullet
							if ((plc.getPlayerMov () == 5) || (plc.getPlayerMov () == 6) ||	//atack / atacknear
							    (((plc.getPlayerMov () == 8) || (plc.getPlayerMov () == 9)) && plc.getPlayerCnt () <= 5)) {	//big atack / big atacknear
								if (Random.Range (0, 7) == 0) {
									if (Random.Range (0, (5 - hLevel)) == 0) {
										behaviorJump = true;
									}
								}
							}
							if ((behaviorAtack == false) && (behaviorLongAtack == false) && (behaviorJump == false)) {
								//atack or move continue or idle
								if (Random.Range (0, 2) == 0) {
									//atack
									if (Random.Range (0, 3) <= 1) {
										behaviorAtack = true;
									} else {
										behaviorLongAtack = true;
									}
								} else {
									//move continue?
									if (Random.Range (0, hlvl) >= 1) {
										//move
										//reverse?
										if (Random.Range (0, hlvl) >= 2) {
											behaviorRight = true;
										} else {
											behaviorLeft = true;
										}
									} else {
										//idle su or sd
										if (Random.Range (0, hlvl) >= 3) {
											behaviorIdleSd = true;
										} else {
											behaviorIdleSu = true;
										}
									}
								}
							}
							break;
						case movjump:
							//jump
							if (Random.Range (0, hlvl) <= 2) {
								//any idle continue
								if (xdir <= -35) {
									behaviorIdleSu = true;
								} else {
									if (xdir < 0) {
										if (duringAction == true) {
											behaviorSitDown = true;
										} else {
											behaviorIdleSu = true;
										}
									} else {
										behaviorIdleSd = true;
									}
								}
							} else {
								//continue?
								if (Random.Range (0, hlvl) >= 2) {
									behaviorJump = true;
								} else {
									//atack
									//normal or long?
									if (Random.Range (0, 3) <= 1) {
										behaviorAtack = true;
									} else {
										behaviorLongAtack = true;
									}
								}
							}
							break;
						case movatack:
							//atack
							//continue atack?
							if (Random.Range (0, hlvl) >= 3) {
								if (Random.Range (0, 3) >= 2) {
									behaviorLongAtack = true;
								} else {
									behaviorAtack = true;
								}
							} else {
								//idle su?
								if (xdir <= -35) {
									behaviorSitDown = true;
								} else if ((xdir < 0) && (duringAction == false)) {	//during su?
									behaviorIdleSu = true;
								} else {	//during sd?
									if (duringAction == true) {
										behaviorSitDown = true;
									} else {
										//sd
										if (Random.Range (0, 2) == 0) {
											behaviorRight = true;
										} else {
											behaviorLeft = true;
										}
									}
								}
							}
							break;
						case movlongatack:
							//atack
							//continue long atack?
							if (Random.Range (0, hlvl) >= 2) {
								if (Random.Range (0, 3) >= 1) {
									behaviorLongAtack = true;
								} else {
									behaviorAtack = true;
								}
							} else {
								//idle su?
								if (xdir <= -35) {
									behaviorSitDown = true;
								} else if ((xdir < 0) && (duringAction == false)) {	//during su?
									behaviorIdleSu = true;
								} else {	//during sd?
									if (duringAction == true) {
										behaviorSitDown = true;
									} else {
										//sd
										if (Random.Range (0, 2) == 0) {
											behaviorRight = true;
										} else {
											behaviorLeft = true;
										}
									}
								}
							}
							break;
						case movsmalldamage:
							//small damage
							#if UNITY_EDITOR
								Debug.Log ("smalldamage -> think");
							#endif
							//continue any action
							if (xdir <= -35) {
								behaviorIdleSu = true;
							} else {
								if (duringAction == true) {
									behaviorSitDown = true;
								} else {
									if (xdir == 0) {
										behaviorIdleSd = true;
									} else {
										behaviorIdleSu = true;
									}
								}
							}
							break;
						case movbigdamage:
							//big damage
							//idle sd?
							if (Random.Range (0, 5 + (hlvlr)) <= 4) {
								behaviorAtack = true;
							}else if (Random.Range (0, (hlvl+5)) <= 0) {
								behaviorIdleSd = true;
							} else {
								//side move or any action?
								if (Random.Range (0, 5) <= 3) {
									//side move
									if (Random.Range (0, 2) == 0) {
										behaviorLeft = true;
									} else {
										behaviorRight = true;
									}
								} else {
									//any action
									tmpr = Random.Range (0, 7);
									if (tmpr <= 2) {
										behaviorAtack = true;
									} else if (tmpr <= 5) {
										behaviorLongAtack = true;
									} else if (tmpr == 6) {
										behaviorJump = true;
									}
								}
							}
							break;
						case movwaitstart:
							//wait start
							//atack or sitdown
							if (Random.Range (0, hlvl) >= 2) {
								behaviorAtack = true;
							} else {
								behaviorSitDown = true;
							}
							break;
						case movwaithko:
							//hiari ko
							//nop
							break;
						case movwaitpko:
							//player ko
							//nop
							break;
						default:
							break;
						}
					}
				}
			}
			behcnt++;
			break;
		case movidlesu:
			//idle stand up
			if (behcnt >= 100) {
				behcnt = 0;
				//seq
				lastmovseq = movseq;
				movseq = movthink;
				movcnt = 0;
			}
			//atack for player down
			if (plc.getPlayerMov() == 11) {	//down
				if (plc.getPlayerCnt() >= 100) {	//before rebirth
					if (Random.Range (0, 3) == 0) {
						if (Random.Range (0, (5 - hLevel)) == 0) {
							if (Random.Range (0, 2) == 0) {
								behaviorAtack = true;
							} else {
								behaviorLongAtack = true;
							}
						}
					}
				}
			}
			//jump for player bullet
			if ( (plc.getPlayerMov () == 5) || (plc.getPlayerMov () == 6) ||	//atack / atacknear
				 (((plc.getPlayerMov() == 8) || (plc.getPlayerMov() == 9)) && plc.getPlayerCnt() <= 5) ) {	//big atack / big atacknear
				if (Random.Range (0, 7) == 0) {
					if (Random.Range (0, (5 - hLevel)) == 0) {
						behaviorJump = true;
					}
				}
			}
			behcnt++;
			break;
		case movidlesd:
			//idle sit down
			if (behcnt == 0) {
				behwait = Random.Range (0, (hlvlr * 10))+hlvlr;
			}
			if (behcnt >= behwait) {
				//seq
				lastmovseq = movseq;
				movseq = movthink;
				movcnt = 0;
			}
			//atack for player down
			if (plc.getPlayerMov() == 11) {	//down
				if (plc.getPlayerCnt() >= 100) {	//before rebirth
					if (Random.Range (0, 3) == 0) {
						if (Random.Range (0, (5 - hLevel)) == 0) {
							if (Random.Range (0, 2) == 0) {
								behaviorAtack = true;
							} else {
								behaviorLongAtack = true;
							}
						}
					}
				}
			}
			//jump for player bullet
			if ( (plc.getPlayerMov () == 5) || (plc.getPlayerMov () == 6) ||	//atack / atacknear
				(((plc.getPlayerMov() == 8) || (plc.getPlayerMov() == 9)) && plc.getPlayerCnt() <= 5) ) {	//big atack / big atacknear
				if (Random.Range (0, 7) == 0) {
					if (Random.Range (0, (5 - hLevel)) == 0) {
						behaviorJump = true;
					}
				}
			}
			behcnt++;
			break;
		case movsitdown:
			//sit down
			//action term?
			if (duringAction == false) {
				lastmovseq = movseq;
				movseq = movthink;
				movcnt = 0;
			}
			break;
		case movright:
			//right move
			//atack for player down
			if (plc.getPlayerMov() == 11) {	//down
				if (plc.getPlayerCnt() >= 100) {	//before rebirth
					if (Random.Range (0, 5) == 0) {
						if (Random.Range (0, (6 - hLevel)) == 0) {
							if (Random.Range (0, 2) == 0) {
								behaviorAtack = true;
								xx = 0.0f;
								zz = 0.0f;
								xdir = 0.0f;
								ydir = 0.0f;
								zdir = 0.0f;
							} else {
								behaviorLongAtack = true;
								xx = 0.0f;
								zz = 0.0f;
								xdir = 0.0f;
								ydir = 0.0f;
								zdir = 0.0f;
							}
						}
					}
				}
			}
			break;
		case movleft:
			//left move
			//atack for player down
			if (plc.getPlayerMov() == 11) {	//down
				if (plc.getPlayerCnt() >= 100) {	//before rebirth
					if (Random.Range (0, 5) == 0) {
						if (Random.Range (0, (6 - hLevel)) == 0) {
							if (Random.Range (0, 2) == 0) {
								behaviorAtack = true;
								xx = 0.0f;
								zz = 0.0f;
								xdir = 0.0f;
								ydir = 0.0f;
								zdir = 0.0f;
							} else {
								behaviorLongAtack = true;
								xx = 0.0f;
								zz = 0.0f;
								xdir = 0.0f;
								ydir = 0.0f;
								zdir = 0.0f;
							}
						}
					}
				}
			}
			break;
		case movjump:
			//jump
			//wait
			break;
		case movatack:
			//atack
			//wait
			break;
		case movlongatack:
			//long atack
			//wait
			break;
		case movsmalldamage:
			//small damage
			//wait
			break;
		case movbigdamage:
			//big damage
			//wait
			break;
		case movwaitstart:
			//wait for fight start
			//wait
			break;
		case movwaithko:
			//wait for hiari ko
			//wait
			break;
		case movwaitpko:
			//wait for player ko
			//wait
			break;
		default:
			break;
		}

		//move seq fix
		//distance near fix
		bool near = false;
		Vector3 p_pos = plc.getPlayerPos ();
		Vector3 h_pos = cashTransform.position;
		if ((Mathf.Abs (h_pos.x - p_pos.x) <= cmn_staticData.Instance.neardst) && (Mathf.Abs (h_pos.z - p_pos.z) <= cmn_staticData.Instance.neardst)) {
			near = true;
		}
		//unavailable time process
		if (unavailableCnt > 0) {
			unavailableCnt--;
			if (unavailableCnt <= 0) {
				unavailableCnt = 0;
				if (hko == false) {
					//tag
					this.tag = "enemy";
					antCtr.tag = "enemy";
					//collider
					cashCapsuleCollider_antCtr.enabled = true;
					//blink
					blinkCnt = 0;
					cashRenderer_antCtr.enabled = true;
				}
			}
		}
		//blink process
		if (unavailableCnt > 0) {
			if (hko == false) {
				blinkCnt++;
				if (blinkCnt >= 4) {
					cashRenderer_antCtr.enabled = !(cashRenderer_antCtr.enabled);
					if (cashRenderer_antCtr.enabled == true) {
						blinkCnt = 0;
					} else {
						blinkCnt = 2;
					}
				}
			}
		}
		//atack adjust
		if (unavailableCnt > 0) {
			if (behaviorLongAtack == true) {
				behaviorLongAtack = false;
				behaviorAtack = true;
			}
		}
		//set think
		if (behaviorThink == true) {
			//seq
			movseq = movthink;
			lastmovseq = movseq;
			movcnt = 0;
		}
		//set idle stand up
		if (behaviorIdleSu == true) {
			//seq
			movseq = movidlesu;
			lastmovseq = movseq;
			movcnt = 0;
		}
		//set idle sit down
		if (behaviorIdleSd == true) {
			//seq
			movseq = movidlesd;
			lastmovseq = movseq;
			movcnt = 0;
		}
		//set sit down
		if (behaviorSitDown == true) {
			//seq
			duringAction = true;
			movseq = movsitdown;
			lastmovseq = movseq;
			movcnt = 0;
		}
		//side move
		//right
		if (behaviorRight == true) {
			//seq
			movseq = movright;
			lastmovseq = movseq;
			movcnt = 0;
			//camera
			cameraCtr.setHiariSideMove( true );
		}
		//left
		if (behaviorLeft == true) {
			movseq = movleft;
			lastmovseq = movseq;
			movcnt = 0;
			//camera
			cameraCtr.setHiariSideMove( true );
		}
		//jump
		if (behaviorJump == true) {
			cameraCtr.setHiariSideMove( false );
			movseq = movjump;
			lastmovseq = movseq;
			movcnt = 0;
			//sound
			cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_jumps);
		}
		//normal atack
		if (behaviorAtack == true) {
			//camera
			cameraCtr.setHiariSideMove( false );
			if (near == false) {
				//no near
				//seq
				movseq = movatack;
				lastmovseq = movseq;
				movcnt = 0;
				//particles
				Vector3 tmppos = hatackParticles.transform.localPosition;
				if (xdir <= -35) {
					tmppos.y = 0.03f;
				} else {
					tmppos.y = 0.65f;
				}
				hatackParticles.transform.localPosition = tmppos;
				cashParticleSystem_hatackParticles.Play ();
				//sound
				if (sdcnt1 == 0) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_atck_s100);
				} else if (sdcnt1 == 1) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_atck_s110);
				}
				sdcnt1++;
				if (sdcnt1 >= 2) {
					sdcnt1 = 0;
				}
			} else {
				//near
				//seq
				movseq = movatacknear;
				lastmovseq = movseq;
				movcnt = 0;
				//particle
				cashParticleSystem_htameParticles.Play ();
				//sound
				if ((cmn_staticData.Instance.highvoicecnt1 == 0) && (cmn_staticData.Instance.highvoicecnt2 == 0)) {
					if (pko == false) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_abnai);
						cmn_staticData.Instance.highvoicecnt3 = 40;
					}
				}
			}
		}
		//long atack
		if (behaviorLongAtack == true) {
			//camera
			cameraCtr.setHiariSideMove( false );
			if (near == false) {
				//seq
				movseq = movlongatack;
				lastmovseq = movseq;
				movcnt = 0;
				//particle
				cashParticleSystem_htameParticles.Play ();
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_tame);
			} else {
				//seq
				movseq = movlongatacknear;
				lastmovseq = movseq;
				movcnt = 0;
				//particle
				cashParticleSystem_htameParticles.Play ();
				//sound
				if ((cmn_staticData.Instance.highvoicecnt1 == 0) && (cmn_staticData.Instance.highvoicecnt2 == 0)) {
					if (pko == false) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_abnai);
						cmn_staticData.Instance.highvoicecnt3 = 40;
					}
				}
			}
		}
		//small damage
		if (behaviorSmallDamage == true) {
			if ( (movseq != movsmalldamage) && (movseq != movbigdamage) ) {
				//common data
				cmn_staticData.Instance.hhitnum++;
				//tag
				this.tag = "unavailableEnemy";
				antCtr.tag = "unavailableEnemy";
				//hp
				hp = hp - this.getSmallDamage();
				if (hp <= 0) {
					//hp
					hp = 0;
					//state
					this.hko = true;
					//main
					mc.setHiariKo ();
				} else {
					//sound
					if (hp <= 40) {
						if (cmn_staticData.Instance.highvoicecnt1 == 0) {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atchot);
							cmn_staticData.Instance.highvoicecnt2 = 40;
						}
					}
				}
				mc.setHiariHpGauge (hpIntial, hp);
				//seq
				if (hko == false) {
					//score
					cmn_staticData.Instance.score += 980;
					mc.updateScore ();
					//unavailable time
					unavailableCnt = 40;
					//seq
					backmovseq = movseq;
					backmovcnt = movcnt;
					movseq = movsmalldamage;
//					lastmovseq = movseq;
					movcnt = 0;
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_ht_s);
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_ht_s);
				} else {
					//score
					cmn_staticData.Instance.score += 9880 + (cmn_staticData.Instance.hlevel * 2480);
					mc.updateScore ();
					//unavailable time
					unavailableCnt = 130;
					//seq
					movseq = movbigdamage;
					lastmovseq = movseq;
					movcnt = 0;
					duringAction = false;
					//particles
					cashParticleSystem_hatackParticles.Stop ();
					cashParticleSystem_htameParticles.Stop ();
					//camera
					cameraCtr.setHiariSideMove (false);
					//sound
					cmn_soundController.Instance.stopSeCh( 13 );
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_ht_ko);
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_htko);
				}
				//mov
				xx = 0.0f;
				yy = 0.0f;
				zz = 0.0f;
				//stage shake xz effect
				mc.setShakeEffect_xz( 6 );
			}
		}
		//big damage
		if (behaviorBigDamage == true) {
			if ((movseq != movsmalldamage) && (movseq != movbigdamage)) {
				//sound
				cmn_soundController.Instance.stopSeCh( 13 );
				//common data
				cmn_staticData.Instance.hhitnum++;
				//unavailable time
				unavailableCnt = 130;
				//tag
				this.tag = "unavailableEnemy";
				antCtr.tag = "unavailableEnemy";
				//hp
				hp = hp - this.getBigDamage();
				if (hp <= 0) {
					//hp
					hp = 0;
					//state
					this.hko = true;
					//main
					mc.setHiariKo ();
					//score
					cmn_staticData.Instance.score += 8980 + (cmn_staticData.Instance.hlevel * 2380);
					mc.updateScore ();
				} else {
					//score
					cmn_staticData.Instance.score += 980;
					mc.updateScore ();
					//sound
					if (hp <= 40) {
						if (cmn_staticData.Instance.highvoicecnt1 == 0) {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atchot);
							cmn_staticData.Instance.highvoicecnt2 = 40;
						}
					}
				}
				if (hko == false) {
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_ht_l);
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_ht_l);
				} else {
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_ht_ko);
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_htko);
				}
				mc.setHiariHpGauge (hpIntial, hp);
				//seq
				movseq = movbigdamage;
				lastmovseq = movseq;
				movcnt = 0;
				duringAction = false;
				//mov
				xx = 0.0f;
				yy = 0.0f;
				zz = 0.0f;
				//atack dir
				atackdir = 0;
				//particles
				cashParticleSystem_hatackParticles.Stop ();
				cashParticleSystem_htameParticles.Stop ();
				//camera
				cameraCtr.setHiariSideMove (false);
				//stage shake xz effect
				mc.setShakeEffect_xz (13);
			}
		}
		//wait hiari ko
		if (behaviorWaitHko == true) {
			//seq
			movseq = movwaithko;
			lastmovseq = movseq;
			movcnt = 0;
		}
		//wait player ko
		if (behaviorWaitPko == true) {
			//seq
			movseq = movwaitpko;
			lastmovseq = movseq;
			movcnt = 0;
		}


		//move seq
		switch (movseq) {
		case movwakeup:
			//wake up
			movcnt++;
			if (movcnt >= 4) {
				movcnt = 0;

				//breath
				if (actseq == 0) {
					yy = 0.03f;
					breathy = breathy + yy;
				} else {
					yy = -0.03f;
					breathy = breathy + yy;
				}
				actseq++;
				if (actseq >= 2) {
					actseq = 0;
				}

			} else {
				yy = 0.0f;
			}
			break;
		case movthink:
			//think
			//action
			movcnt++;
			if (movcnt >= 4) {
				movcnt = 0;

				//breath
				if (actseq == 0) {
					yy = 0.03f;
					breathy = breathy + yy;
				} else {
					yy = -0.03f;
					breathy = breathy + yy;
				}
				actseq++;
				if (actseq >= 2) {
					actseq = 0;
				}

			} else {
				yy = 0.0f;
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
			}
			break;
		case movidlesu:
			//idle stand up
			movcnt++;
			if (movcnt >= 4) {
				movcnt = 0;

				//breath
				if (actseq == 0) {
					yy = 0.03f;
					breathy = breathy + yy;
				} else {
					yy = -0.03f;
					breathy = breathy + yy;
				}
				actseq++;
				if (actseq >= 2) {
					actseq = 0;
				}

				//stand up
				if (xdir >= -35.0f) {
					xdir = xdir - 2.3f;
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_mov);
				} else {
					if (pko == true) {
						movseq = movwaitpko;
						movcnt = 0;
					}
				}

			} else {
				yy = 0.0f;
			}
			break;
		case movidlesd:
			//idle sit down
			movcnt++;
			if (movcnt >= 4) {
				movcnt = 0;

				//breath
				if (actseq == 0) {
					yy = 0.03f;
					breathy = breathy + yy;
				} else {
					yy = -0.03f;
					breathy = breathy + yy;
				}
				actseq++;
				if (actseq >= 2) {
					actseq = 0;
				}

			} else {
				yy = 0.0f;
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
			}
			break;
		case movsitdown:
			//sit down
			//mov
			movcnt++;
			if (movcnt >= 1) {
				movcnt = 0;
				//sit down
				if (xdir < 0.0f) {
					xdir = xdir + 2.0f;
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_mov);
					if (xdir >= 0) {
						xdir = 0;
						actseq = 0;
						actcnt = 0;
						duringAction = false;
						if (pko == true) {
							movseq = movwaitpko;
							movcnt = 0;
						}
					}
				}
			}
			//(for safe)
			if (xdir >= 0) {
				xdir = 0;
				duringAction = false;
			}
			break;
		case movright:
			//move right
			//sound
			if (movcnt % 5 == 0) {
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_mov);
			}
			//move
			movdir = movdir + movdd;
			if (movdir >= 360) {
				movdir = movdir - 360;
			}
			float nx = Mathf.Cos ((movdir + posdiroffset) * Mathf.Deg2Rad) * 3.0f;
			float nz = Mathf.Sin ((movdir + posdiroffset) * Mathf.Deg2Rad) * 3.0f;
			Vector3 pos = cashTransform.position;
			xx = nx - pos.x;
			zz = nz - pos.z;
			//action
			actcnt++;
			if (actcnt >= 3) {
				switch (actseq) {
				case 0:
					xdir = xdir - 2;
					zdir = zdir + 2;
					actseq++;
					break;
				case 1:
					xdir = xdir - 2;
					zdir = zdir - 2;
					actseq++;
					break;
				case 2:
					xdir = xdir + 2;
					zdir = zdir - 2;
					actseq++;
					break;
				case 3:
					xdir = xdir + 2;
					zdir = zdir + 2;
					actseq = 0;
					break;
				default:
					break;
				}
			}
			//cnt
			movcnt++;
			if (movcnt >= 15) {
				//camera
				cameraCtr.setHiariSideMove( false );
			}
			if (movcnt >= 30) {
				//behavior
				behcnt = 0;
				//mov
				lastmovseq = movseq;
				movseq = movthink;
				movcnt = 0;
				xx = 0.0f;
				yy = 0.0f;
				zz = 0.0f;
				//action
				xdir = 0;
				zdir = 0;
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
			}
			break;
		case movleft:
			//move right
			//sound
			if (movcnt % 5 == 0) {
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_mov);
			}
			//move
			movdir = movdir - movdd;
			if (movdir <= 0) {
				movdir = movdir + 360;
			}
			nx = Mathf.Cos ((movdir + posdiroffset) * Mathf.Deg2Rad) * 3.0f;
			nz = Mathf.Sin ((movdir + posdiroffset) * Mathf.Deg2Rad) * 3.0f;
			pos = cashTransform.position;
			xx = nx - pos.x;
			zz = nz - pos.z;
			//action
			actcnt++;
			if (actcnt >= 3) {
				switch (actseq) {
				case 0:
					xdir = xdir - 2;
					zdir = zdir + 2;
					actseq++;
					break;
				case 1:
					xdir = xdir - 2;
					zdir = zdir - 2;
					actseq++;
					break;
				case 2:
					xdir = xdir + 2;
					zdir = zdir - 2;
					actseq++;
					break;
				case 3:
					xdir = xdir + 2;
					zdir = zdir + 2;
					actseq = 0;
					break;
				default:
					break;
				}
			}
			//cnt
			movcnt++;
			if (movcnt >= 15) {
				//camera
				cameraCtr.setHiariSideMove( false );
			}
			if (movcnt >= 30) {
				//behavior
				behcnt = 0;
				//seq
				lastmovseq = movseq;
				movseq = movthink;
				movcnt = 0;
				xx = 0.0f;
				yy = 0.0f;
				zz = 0.0f;
				//action
				xdir = 0;
				zdir = 0;
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
			}
			break;
		case movjump:
			//jump
			if (movcnt == 0) {
				yy = 0.4f;
			}
			if ((cashTransform.position.y <= 0.2f) && (yy < 0)) {
				Vector3 tmppos = cashTransform.position;
				tmppos.y = 0.2f;
				cashTransform.position = tmppos;
				yy = 0;
				jumpy = 0;
				//behavior
				behcnt = 0;
				//seq
				lastmovseq = movseq;
				movseq = movthink;
				movcnt = 0;
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_jumpe);
			} else {
				yy = yy - 0.02f;
			}
			movcnt++;
			jumpy = jumpy + yy;
			break;
		case movatack:
			//normal atack
			//action
			if (movcnt < 10) {
				xdir = xdir - 3.0f;
			} else if (movcnt < 15) {
				xdir = xdir + 6.0f;
			}
			if (movcnt == 15) {
				//set atack
				int tmp = this.getNormalBulletType();
				this.setAtack ( tmp );
				//particles
				cashParticleSystem_hatackParticles.Stop ();
				//sound
				if (tmp == 0) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_atck_s);
				} else if (tmp == 3) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_atck_p);
				}
			}
			//cnt
			movcnt++;
			if (movcnt >= 18) {
				//behavior
				behcnt = 0;
				//seq
				lastmovseq = movseq;
				movseq = movthink;
				movcnt = 0;
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
			}
			break;
		case movatacknear:
			//normal atack near
			//action
			if (movcnt % 4 == 0) {
				xdir = xdir + 15.0f;
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_atck_sn110);
			}
			if (movcnt % 4 == 2) {
				xdir = xdir - 15.0f;
			}
			if (movcnt == 52) {
				//set atack
				this.setAtackNear ( 0, near );
				//sound
				if (sdcnt1 == 0) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_atck_s100);
				} else if (sdcnt1 == 1) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_atck_s110);
				}
				sdcnt1++;
				if (sdcnt1 >= 2) {
					sdcnt1 = 0;
				}
			}
			//cnt
			movcnt++;
			if (movcnt >= 79) {
				//action
//				zdir = 0.0f;
				//behavior
				behcnt = 0;
				//seq
//				lastmovseq = movseq;
				lastmovseq = movatack;
				movseq = movthink;
				movcnt = 0;
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
				//particles
				cashParticleSystem_htameParticles.Stop ();
			}
			break;
		case movlongatack:
			//long atack
			//sound
			if (movcnt == 15) {
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_atck_l100);
			}
			//action
			if (movcnt < 20) {
				xdir = xdir - 2.0f;
			} else if( movcnt < 40) {
				if (movcnt % 2 == 0) {
					xdir = xdir - 2.0f;
				} else {
					xdir = xdir + 2.0f;
				}
			} else if (movcnt < 50) {
				xdir = xdir + 4.0f;
			}
			if (movcnt == 50) {
				//set atack
				int tmp = this.getLargeBulletType();
				this.setAtack ( tmp );
				//particles
				cashParticleSystem_htameParticles.Stop ();
				//sound
				cmn_soundController.Instance.stopSeCh( 13 );
				if (tmp == 1) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_atck_l);
				}else if (tmp == 2) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_atck_p);
					if (pko == false) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_h_atck_p);
						cmn_staticData.Instance.highvoicecnt1 = 40;
					}
				}
			}
			//cnt
			movcnt++;
			if (movcnt >= 55) {
				//sound
				if ((cmn_staticData.Instance.highvoicecnt1 == 0) && (cmn_staticData.Instance.highvoicecnt2 == 0)) {
					if (pko == false) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_abnai);
						cmn_staticData.Instance.highvoicecnt3 = 40;
					}
				}
				//behavior
				behcnt = 0;
				//mov
				lastmovseq = movseq;
				movseq = movthink;
				movcnt = 0;
				//camera
//				cameraCtr.setHiariSideMove( false );
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
			}
			break;
		case movlongatacknear:
			//long atack near
			//sound
			if (movcnt % 8 == 0) {
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_atck_ln100);
			}
			//action
			atackdir = atackdir + 25.0f;
			if (movcnt == 60) {
				//set atack
				this.setAtackNear ( 1, near );
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_h_atck_l100);
			}
			//cnt
			movcnt++;
			if (movcnt >= 85) {
				//action
				atackdir = 0.0f;
				//behavior
				behcnt = 0;
				//mov
//				lastmovseq = movseq;
				lastmovseq = movlongatack;
				movseq = movthink;
				movcnt = 0;
				//camera
//				cameraCtr.setHiariSideMove( false );
				if (pko == true) {
					movseq = movwaitpko;
					movcnt = 0;
				}
				//particles
				cashParticleSystem_htameParticles.Stop ();
			}
			break;
		case movsmalldamage:
			//small damage
			if (movcnt == 10) {
				cashCapsuleCollider_antCtr.enabled = false;
			}
			//action
			if (movcnt % 2 == 0) {
				zdir = zdir + 3.0f;
			} else {
				zdir = zdir - 3.0f;
			}
			if (movcnt < 6) {
				xdir = xdir - 14.0f;
			}
			if (movcnt >= 6) {
				xdir = xdir + 14.0f;
			}
			movcnt++;
			if (movcnt >= 12) {
				if (hko == false) {
					//tag
//					this.tag = "enemy";
//					antCtr.tag = "enemy";
					//revert movseq
					movseq = backmovseq;
					movcnt = backmovcnt;
					if (pko == true) {
						movseq = movwaitpko;
						movcnt = 0;
					}
				} else {
					//movseq
					movseq = movwaithko;
					movcnt = 0;
				}
			}
			break;
		case movbigdamage:
			//big damage
			if (movcnt == 10) {
				cashCapsuleCollider_antCtr.enabled = false;
			}
			//action
			if (movcnt == 0) {
				//jumpy
				jumpy = 0.0f;
				//atackz
				atackz = 0.0f;
				//action init
				yy = 0.5f;
				movcnt++;
			} else if (movcnt == 1) {
				//rotate and jump
				if ((cashTransform.position.y <= 0.8f) && (yy <= 0.0f)) {
					//to down
					//pos
					Vector3 tmppos = cashTransform.position;
					tmppos.y = 0.8f;
					cashTransform.position = tmppos;
					//rotate
					xdir = 90;
					zdir = 0;
					//mov
					yy = 0.0f;
					//cnt
					movcnt++;
					//stage shake y effect
					mc.setShakeEffect_y( 25 );
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_down120);
				} else {
					//damage action
					yy = yy - 0.035f;
					xdir = xdir + 20;
					if (xdir >= 360) {
						xdir = xdir - 360;
					}
				}
			} else if (movcnt <= 24) {
				//wait
				movcnt++;
				//sound
				if (movcnt == 20) {
					if ( (hko == false) && (pko == false) ) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_downback);
					}
				}
			} else if (movcnt > 24) {
				if (hko == false) {
					//wake up
					if ((xdir <= 0) && (cashTransform.position.y <= 0.2f)) {
//						//tag
//						this.tag = "enemy";
//						antCtr.tag = "enemy";
						//rotate
						xdir = 0.0f;
						zdir = 0.0f;
						//pos
						Vector3 tmppos = cashTransform.position;
						tmppos.y = 0.2f;
						cashTransform.position = tmppos;
						//behavior
						behcnt = 0;
						//seq
						lastmovseq = movseq;
						movseq = movthink;
						movcnt = 0;
						if (pko == true) {
							movseq = movwaitpko;
							movcnt = 0;
						}
					} else {
						if (movcnt % 8 == 0) {
							zdir = zdir + 4.0f;
						}
						if (movcnt % 8 == 4) {
							zdir = zdir - 4.0f;
						}
						if (xdir > 0) {
							xdir = xdir - 2;
						}
						if (cashTransform.position.y > 0.2f) {
							yy = -0.1f;
						} else {
							yy = 0.0f;
						}
					}
				} else {
					//movseq
					movseq = movwaithko;
					movcnt = 0;
				}
				movcnt++;
			}
			break;
		case movwaitstart:
			//wait for fight start
			movcnt++;
			if (movcnt >= 4) {
				movcnt = 0;

				//breath
				if (actseq == 0) {
					yy = 0.03f;
					breathy = breathy + yy;
				} else {
					yy = -0.03f;
					breathy = breathy + yy;
				}
				actseq++;
				if (actseq >= 2) {
					actseq = 0;
				}

				//stand up
				if (xdir >= -35.0f) {
					xdir = xdir - 2.3f;
					//sound
//					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_mov);
				}

			} else {
				yy = 0.0f;
			}
			break;
		case movwaithko:
			//wait for hiari ko
			if (movcnt == 40) {
				cashParticleSystem_hkoParticles.Play ();
			}
			if (movcnt <= 240) {
				if (movcnt % 4 == 0) {
					xdir = xdir + 3.0f;
					zdir = zdir - 3.0f;
					//sound
					if (mc.getGameMode () == 4) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_h_mov);
					}
				} else {
					xdir = xdir - 1.0f;
					zdir = zdir + 1.0f;
				}
			}
			if (movcnt == 240) {
				cmn_soundController.Instance.stopVoice();
			}
			movcnt++;
			break;
		case movwaitpko:
			//wait for player ko
			movcnt++;
			if (movcnt >= 4) {
				movcnt = 0;

				//breath
				if (actseq == 0) {
					yy = 0.03f;
					breathy = breathy + yy;
				} else {
					yy = -0.03f;
					breathy = breathy + yy;
				}
				actseq++;
				if (actseq >= 2) {
					actseq = 0;
				}

			} else {
				yy = 0.0f;
			}
			break;
		default:
			break;
		}

		//translate adjust
		//translate y
		if ( (movseq != movwakeup) && (movseq != movwaitstart) && (movseq != movidlesu) && (movseq != movidlesd) && 
			(movseq != movjump) && (movseq != movbigdamage) && (movseq != movwaitpko) ) {
			if (yy != 0) {
				yy = 0.0f;
			}
		}
		//breath y
		if ( (movseq != movwakeup) && (movseq != movwaitstart) && (movseq != movidlesu) && (movseq != movidlesd) &&  
			 (movseq != movsmalldamage) && (movseq != movwaitpko) ) {
			if (breathy != 0) {
				yy = yy - breathy;
				breathy = 0;
			}
		}
		//jump y
		if ( (movseq != movjump) && (movseq != movbigdamage) ) {
			if (jumpy != 0) {
				yy = yy - jumpy;
				jumpy = 0;
			}
		}
		//atack z
//		if ( (movseq != movatack) && (movseq != movsmalldamage) ) {
//			if (atackz != 0) {
//				zz = zz - atackz;
//				atackz = 0;
//			}
//		}

		//hiari rotate fix
		//direction to player from ant
		//get to hiari dir
		float xdistance, ydistance, zdistance;
		float direction,direction2;
		Vector3 hpos;
		const float doffset = 90.0f;//+0.0f;//90.0f;
		hpos = cashTransform.position;
		Vector3 ppos = plc.getPlayerPos ();
		xdistance = (ppos.x) - (hpos.x);	//player,hiari x distance
		zdistance = (ppos.z) - (hpos.z);	//player,hiari z distance
		if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
			xdistance = 0.0001f;
		}
		direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
		float hiadir = direction;
		if (hiadir < 0) {
			hiadir = hiadir + 360.0f;
		}
		hiadir = (360 - hiadir)+doffset;	//adjust dir

		//hiari move and rotate
		//move
		cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
		cashTransform.Translate (xx, yy, zz);

		//rotate
		float hydir = ydir + hiadir + rttdiroffset + posdiroffset + atackdir;
		cashTransform.rotation = Quaternion.Euler (new Vector3 (zdir, hydir, xdir));	//90度offsetのため x<->z


		//hp gauge start update
		if (movseq != movwakeup) {
			if (hpStart == true) {
				if (hp < 200) {
					hp = hp + 2;
					if (hp >= 200) {
						hp = hpIntial;
						hpStart = false;
						mc.setHiariHpGauge (hpIntial, hp);
					} else {
						mc.setHiariHpGauge (200, hp);
					}
				}
			}
		}
	}


	//set atack
	private void setAtack( int bulletType ){
		float xdistance, ydistance, zdistance, xzdistance;
		float direction,direction2;
		Vector3 hpos;
		//atack dir
		hpos = cashTransform.position;
		if (bulletType == 0) {
			//bullet normal
			if (xdir <= -35) {
				//stand up
				hpos.y = hpos.y + 1.15f;	//org
			} else {
				//sit down
				hpos.y = hpos.y + 2.70f;
			}
		} else {
			//bullet large
			if (xdir <= -35) {
				//stand up
				hpos.y = hpos.y + 1.15f;	//org
			} else {
				//sit down
				hpos.y = hpos.y + 2.80f;
			}
		}
		Vector3 ppos = plc.getPlayerPos();
		ppos.y = ppos.y + 0.4f;	//target adjust
		xdistance = (ppos.x) - (hpos.x);	//hiari,player x distance
		ydistance = (ppos.y) - (hpos.y);	//hiari,player y distance
		zdistance = (ppos.z) - (hpos.z);	//hiari,player z distance
		xzdistance = Mathf.Sqrt( Mathf.Pow( xdistance,2 ) + Mathf.Pow( zdistance,2) );	//hiari,player xz distance
		if (xdistance == 0) {	//for zero exception
			xdistance = 0.0001f;
		}
		if (ydistance == 0) {	//for zero exception
			ydistance = 0.0001f;
		}
		direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
		direction2 = Mathf.Atan2 (xzdistance, ydistance) * Mathf.Rad2Deg;	//distance -> direction
		float atcdir = direction;
		if (atcdir < 0) {
			atcdir = atcdir + 360.0f;
		}
		float atcdir2 = direction2;
		if (atcdir2 < 0) {
			atcdir2 = atcdir2 + 360.0f;
		}
		//get x,y,z speed to tap pos
//		float ebspd = 0.28f;	//org
		float ebspd = 0.22f;
		if (bulletType == 2) {
			ebspd = 0.18f;
		} else if (bulletType == 3) {
			ebspd = 0.16f;
		}
		float xx = Mathf.Cos( atcdir*Mathf.Deg2Rad ) * ebspd;
		float yy = Mathf.Cos( atcdir2*Mathf.Deg2Rad ) * ebspd;
		float zz = Mathf.Sin( atcdir*Mathf.Deg2Rad ) * ebspd;
		//bullet pos init
		Vector3 bpos = hpos;
		float initadj = 3.5f;
		if (bulletType == 0) {
			//bullet normal
			if (xdir <= -35) {
				//stand up
				initadj = initadj + 0.5f;	//org 0.0f
			} else {
				//sit down
				initadj = initadj + 4.4f;
			}
		} else if (bulletType == 1) {
			//bullet large
			if (xdir <= -35) {
				//stand up
				initadj = initadj + 0.8f;	//org 0.0f
			} else {
				//sit down
				initadj = initadj + 5.2f;
			}
		} else if (bulletType == 2) {
			//bullet large hyoshiki
			if (xdir <= -35) {
				//stand up
				initadj = initadj + 0.8f;	//org 0.0f
			} else {
				//sit down
				initadj = initadj + 5.2f;
			}
		} else if (bulletType == 3) {
			//bullet normal smartphone
			if (xdir <= -35) {
				//stand up
				initadj = initadj + 0.5f;	//org 0.0f
			} else {
				//sit down
				initadj = initadj + 4.4f;
			}
		}
		bpos.x = bpos.x + (xx * initadj);
		bpos.y = bpos.y + (yy * initadj);
		bpos.z = bpos.z + (zz * initadj);
		//generate enemy bullet
		mc.generateEnemyBullet(bulletType, bpos, xx, yy, zz, atcdir);
	}

	//set atack near
	private void setAtackNear( int atackType, bool near ){
		if (atackType == 0) {
			if (near == true) {
				plc.setSmallDamage ();
			}
		} else if (atackType == 1) {
			if (near == true) {
				plc.setBigDamage ();
			}
		}
	}

	//get normal bullet type
	private int getNormalBulletType(){
		int ptn = cmn_staticData.Instance.hbptn_n [nbpcnt];
		nbpcnt++;
		if (nbpcnt >= cmn_staticData.Instance.hbptn_n.Length) {
			nbpcnt = 0;
		}
		return ptn;
	}

	//get large bullet type
	private int getLargeBulletType(){
		int ptn = cmn_staticData.Instance.hbptn_l [lbpcnt];
		lbpcnt++;
		if (lbpcnt >= cmn_staticData.Instance.hbptn_l.Length) {
			lbpcnt = 0;
		}
		return ptn;
	}

	//get small damage
	private int getSmallDamage(){
		return 10 + cmn_staticData.Instance.atackplus - cmn_staticData.Instance.hdefenceplus;
	}

	//get big damage
	private int getBigDamage(){
		return 28 + cmn_staticData.Instance.atackplus - cmn_staticData.Instance.hdefenceplus;
	}


	//public

	//term wake up
	public void termWakeup(){
		//mov
		lastmovseq = movseq;
		backmovseq = movseq;
		movseq = movwaitstart;
		movcnt = 0;
	}

	//set start fight
	public void setFightStart(){
		//mov
		lastmovseq = movseq;
		backmovseq = movseq;
		movseq = movthink;
		movcnt = 0;
	}

	//set player ko
	public void setPlayerKo(){
		//state
		this.pko = true;
	}

	//set hiari term
	public void termHiari(){
		//delete fire-ant
		GameObject.Destroy (antCtr);
		//generate hiari parts
		mc.generateHiariParts( this.getHiariPos() );
	}

	//term hiari term particles
	public void termHiariParticles(){
		//particles
		cashParticleSystem_hkoParticles.Stop();
	}

	//hiari small damage
	public void setSmallDamage(){
		if (this.tag == "enemy") {
			this.smallDamage = true;
		}
	}

	//hiari big damage
	public void setBigDamage(){
		if (this.tag == "enemy") {
			this.bigDamage = true;
		}
	}

	//get hiari pos
	public Vector3 getHiariPos(){
		return this.cashTransform.position;
	}

	//get hiari level
	public int getHiariLevel(){
		return this.hLevel;
	}

}
