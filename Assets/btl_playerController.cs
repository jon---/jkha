using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btl_playerController : MonoBehaviour {
	//public

	//private
	//local const
	//base hit point
	const int basehitpoint = 200;
//	const int basehitpoint = 10;	//for debug
	//move seq const
	const int movwakeup = 0;
	const int movidle = 1;
	const int movright = 2;
	const int movleft = 3;
	const int movjump = 4;
	const int movatack = 5;
	const int movatacknear = 6;
	const int movtame = 7;
	const int movlongatack = 8;
	const int movlongatacknear = 9;
	const int movsmalldamage = 10;
	const int movbigdamage = 11;
	const int movwaitstart = 100;
	const int movwaithko = 110;
	const int movwaitpko = 120;
	//mov speed
	const float movdd = 1.5f;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	btl_mainController mc;
	//kuri 1
	GameObject kuriCtr;
	CapsuleCollider cashCapsuleCollider_kuriCtr;
	SkinnedMeshRenderer cashSkinnedMeshRenderer_kuriCtr;
	btl_kuriController krc;
	GameObject kuriRnd;
	//kuri 2
	GameObject kuriCtr2;
	CapsuleCollider cashCapsuleCollider_kuriCtr2;
	SkinnedMeshRenderer cashSkinnedMeshRenderer_kuriCtr2;
	btl_kuriController krc2;
	GameObject kuriRnd2;
	//kuri
	Animation anim;
	GameObject hiariCtr;
	btl_hiariController hac;
	//targetmark
	GameObject targetMark;
	Text targetMarkTxt;
	RectTransform cashRectTransform_targetMarkTxt;
	//particles
	GameObject tameParticles;
	ParticleSystem cashParticleSystem_tameParticles;
	GameObject nearParticles;
	ParticleSystem cashParticleSystem_nearParticles;

	//local
	//input
	bool duringInput;
	bool duringLongTap;
	int tapCnt;
	Vector2 startMpos;
	//for kuri Controller
	bool smallDamage;
	bool bigDamage;
	//state
	bool inputFirst;
	Vector2 tameMpos;

	//move
	int movseq;
	int movcnt;
	float movdir;	//dir for pos
	float rtcnt;
	float plydir;	//dir for player rotate
	//translate
	float xx;
	float yy;
	float zz;
	//state
	bool downShake;
	bool startTurn;
	bool pko;
	bool hko;
	int unavailableCnt;
	//rotate
	float pdir;

	//state
	bool changekuri;
	int blinkCnt;
	bool blink;

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
	int sdcnt1;	//for step
	int sdcnt2;	//for atack s
	int sdcnt3; //for tame voice
	int sdcnt4;	//for damage l


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//main controller
		mainCtr = GameObject.Find ("btl_mainController");
		mc = mainCtr.GetComponent<btl_mainController> ();

		//kuri controller 1
		kuriCtr = GameObject.Find ("btl_f05_blazer_020_h");
		cashCapsuleCollider_kuriCtr = kuriCtr.GetComponent<CapsuleCollider> ();
		krc = kuriCtr.GetComponent<btl_kuriController> ();
		anim = kuriCtr.GetComponent<Animation> ();
		kuriRnd = GameObject.Find ("f05_blazer_020_h");
		cashSkinnedMeshRenderer_kuriCtr = kuriRnd.GetComponent<SkinnedMeshRenderer> ();
		kuriCtr.SetActive (true);

		//kuri controller 2
		kuriCtr2 = GameObject.Find ("btl_f05_blazer_120_h");
		cashCapsuleCollider_kuriCtr2 = kuriCtr2.GetComponent<CapsuleCollider> ();
		krc2 = kuriCtr2.GetComponent<btl_kuriController> ();
		kuriRnd2 = GameObject.Find ("f05_blazer_120_h");
		cashSkinnedMeshRenderer_kuriCtr2 = kuriRnd2.GetComponent<SkinnedMeshRenderer> ();
		kuriCtr2.SetActive (false);

		//hiari controller
		hiariCtr = GameObject.Find ("btl_hiariController");
		hac = hiariCtr.GetComponent<btl_hiariController> ();

		//target mark
		targetMark = GameObject.Find("targetMark");
		targetMarkTxt = targetMark.GetComponent<Text> ();
		cashRectTransform_targetMarkTxt = targetMarkTxt.GetComponent<RectTransform> ();

		//player tame particles
		tameParticles = GameObject.Find("tameParticles");
		cashParticleSystem_tameParticles = tameParticles.GetComponent<ParticleSystem> ();

		//player near atack particles
		nearParticles = GameObject.Find("nearParticles");
		cashParticleSystem_nearParticles = nearParticles.GetComponent<ParticleSystem> ();

		//input
		duringInput = false;
		duringLongTap = false;
		tapCnt = 0;
		startMpos = new Vector2 (0, 0);
		smallDamage = false;
		bigDamage = false;
		//state
		inputFirst = true;
		tameMpos = new Vector2 (0, 0);

		//translate
		xx = 0.0f;
		yy = 0.0f;
		zz = 0.0f;

		//move
		movseq = movwakeup;
		movcnt = 0;
		movdir = 270.0f;
		rtcnt = 0.0f;
		anim.CrossFade ("idle_01", 0);
		//state
		downShake = false;
		startTurn = true;
		pko = false;
		hko = false;
		unavailableCnt = 0;

		//player pos init
		float px = Mathf.Cos (movdir * Mathf.Deg2Rad) * 3.0f;
		float pz = Mathf.Sin (movdir * Mathf.Deg2Rad) * 3.0f;
		Vector3 pos = cashTransform.position;
		pos.x = px;
		pos.z = pz;
		cashTransform.position = pos;

		//state
		changekuri = false;
		blinkCnt = 0;
		blink = false;

		//bullet pattern cnt
		nbpcnt = 0;
		lbpcnt = 0;

		//player atack adjust
		if (cmn_staticData.Instance.atackplus >= 9) {
			cmn_staticData.Instance.atackplus = 9;
		}

		//player defence adjust
		if (cmn_staticData.Instance.defenceplus >= 5) {
			cmn_staticData.Instance.defenceplus = 5;
		}

		//debug -->
//		cmn_staticData.Instance.hpplus = 5;
//		cmn_staticData.Instance.atackplus = 3;
//		cmn_staticData.Instance.defenceplus = 3;
		//debug <--

		//enemy inital hitpoint
		hpIntial = basehitpoint + cmn_staticData.Instance.hpplus;

		//enemy hitpoint
		hp = 0;//hpIntial;

		//hit point start update
		hpStart = true;

		//sound
		sdcnt1 = 0;	//for step
		sdcnt2 = 0;	//for atack s
		sdcnt3 = 0; //for tame voice
		sdcnt4 = 0; //for damage l

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


		//player process
		playerProcess();


		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}

	}


	//private

	//player process
	private void playerProcess(){
		//fix input
		bool inputIdle = false;
		bool inputRight = false;
		bool inputLeft = false;
		bool inputJump = false;
		bool inputAtack = false;
		bool inputTame = false;
		bool inputLongAtack = false;
		bool inputSmallDamage = false;
		bool inputBigDamage = false;
		Vector2 mpos = new Vector2 (0, 0);	//(fix mouse position)
		if ((movseq == movwaitstart) || (movseq == movwaithko) || (movseq == movwaitpko)) {
			//input unavailable
			//input cancel
			duringInput = false;
			duringLongTap = false;
			tapCnt = 0;
			inputFirst = true;
			//effect
			cashParticleSystem_tameParticles.Stop ();
			//tame gauge
			mc.tameGaugeOff();
		} else {
			//input available
			//key input for unity editor only
			#if UNITY_EDITOR || UNITY_WEBGL
			if (Input.anyKeyDown == true) {
				//move
				if (Input.GetKeyDown (KeyCode.RightArrow) == true) {
					inputRight = true;
				}
				if (Input.GetKeyDown (KeyCode.LeftArrow) == true) {
					inputLeft = true;
				}
//				if (Input.GetKeyDown (KeyCode.UpArrow) == true) {
//					inputJump = true;
//				}
			}
			#endif
			//player / hiari ko
			if ( (pko == true) || (hko == true) ) {
				//input cancel
				duringInput = false;
				duringLongTap = false;
				tapCnt = 0;
			}
			//damage
			if (smallDamage == true) {
				smallDamage = false;
				inputSmallDamage = true;
				//input cancel
				duringInput = false;
				duringLongTap = false;
				tapCnt = 0;
			}
			if (bigDamage == true) {
				bigDamage = false;
				inputBigDamage = true;
				//input cancel
				duringInput = false;
				duringLongTap = false;
				tapCnt = 0;
			}
			//swipe/atack/long atack process
			if (Input.GetMouseButtonDown (0)) {
				if (inputFirst == false) {	//(for first start tap)
					//mouse input start
					if (duringInput == false) {
						if ((tapCnt == 0) && (duringLongTap == false)) {
							duringInput = true;
							duringLongTap = false;
							tapCnt = 0;
							startMpos = Input.mousePosition;
						}
					}
				} else {
					inputFirst = false;
				}
			}
			if (Input.GetMouseButton (0)) {
				//idle only
				if ( (movseq == movidle) || (movseq == movtame) ) {
					if (duringInput == true) {
						tameMpos = Input.mousePosition;
						tapCnt++;
						if (tapCnt >= 10) {
							//star tame for long atack
							duringLongTap = true;
							//fix tame
							inputTame = true;
						}
					}
				} else {
					//input cancel
//					duringInput = false;
//					duringLongTap = false;
//					tapCnt = 0;
				}
			}
			if (Input.GetMouseButtonUp (0)) {
				if (duringInput == true) {
					Vector2 cmpos = Input.mousePosition;
					if (movseq == movtame) {
						if (tapCnt >= 50) {
							//fix long atack
							inputLongAtack = true;
							mpos = cmpos;
						} else {
							//release tame
							inputIdle = true;
						}
					} else {
//						if ( (Mathf.Abs (cmpos.x - startMpos.x) >= 20) && (Mathf.Abs (cmpos.y - startMpos.y) <= 40) ) {
						if ( Mathf.Abs (cmpos.x - startMpos.x) >= 20 ) {
							//fix swipe
							if (cmpos.x > startMpos.x) {
								inputRight = true;
							} else {
								inputLeft = true;
							}
//						} else if ( (Mathf.Abs (cmpos.x - startMpos.x) <= 40) && ((cmpos.y - startMpos.y) >= 20) ){
//							//fix jump
//							inputJump = true;
						} else {
							//fix atack
							inputAtack = true;
							mpos = cmpos;
						}
					}
					duringInput = false;
					duringLongTap = false;
					tapCnt = 0;
				}
			}
		}


		//move seq fix
		//distance near fix
		bool near = false;
		Vector3 h_pos = hac.getHiariPos ();
		Vector3 p_pos = cashTransform.position;
		if ((Mathf.Abs (h_pos.x - p_pos.x) <= cmn_staticData.Instance.neardst) && (Mathf.Abs (h_pos.z - p_pos.z) <= cmn_staticData.Instance.neardst)) {
			near = true;
		}
		//unavailable time process
		if (unavailableCnt > 0) {
			unavailableCnt--;
			if (unavailableCnt <= 0) {
				unavailableCnt = 0;
				if (pko == false) {
					//tag
					this.tag = "player";
					if (changekuri == false) {
						//tag
						kuriCtr.tag = "player";
						//collider
						cashCapsuleCollider_kuriCtr.enabled = true;
					} else {
						//tag
						kuriCtr2.tag = "player";
						//collider
						cashCapsuleCollider_kuriCtr2.enabled = true;
					}
					//blink
					blinkCnt = 0;
					blink = false;
					if (changekuri == false) {
						cashSkinnedMeshRenderer_kuriCtr.enabled = true;
					} else {
						cashSkinnedMeshRenderer_kuriCtr2.enabled = true;
					}
				}
			}
		}
		//blink process
		if (unavailableCnt > 0) {
			if (pko == false) {
				blinkCnt++;
				if (blinkCnt >= 4) {
					if (blink == true) {
						if (changekuri == false) {
							cashSkinnedMeshRenderer_kuriCtr.enabled = false;
						} else {
							cashSkinnedMeshRenderer_kuriCtr2.enabled = false;
						}
						blinkCnt = 2;
					} else {
						if (changekuri == false) {
							cashSkinnedMeshRenderer_kuriCtr.enabled = true;
						} else {
							cashSkinnedMeshRenderer_kuriCtr2.enabled = true;
						}
						blinkCnt = 0;
					}
					blink = !blink;
				}
			}
		}
		//small damage
		if (inputSmallDamage == true) {
			if ((movseq != movsmalldamage) && (movseq != movbigdamage)) {
				//sound
				cmn_soundController.Instance.stopSeCh( 0x09 );
				if (pko == false) {
					//common data
					cmn_staticData.Instance.phitnum++;
					//tag
					this.tag = "unavailablePlayer";
					if (changekuri == false) {
						//tag
						kuriCtr.tag = "unavailablePlayer";
						//collider
						cashCapsuleCollider_kuriCtr.enabled = false;
					} else {
						//tag
						kuriCtr2.tag = "unavailablePlayer";
						//collider
						cashCapsuleCollider_kuriCtr2.enabled = false;
					}
					//hp
					hp = hp - this.getSmallDamage();
					if (hp <= 0) {
						//player ko
						//hp
						hp = 0;
						//state
						this.pko = true;
						//main
						mc.setPlayerKo ();
						//target mark disp
						cashRectTransform_targetMarkTxt.position = new Vector2(0, -300);
					}
					mc.setPlayerHpGauge (hpIntial, hp);
					if ( pko == false ) {
						//unavailable time
						unavailableCnt = 40;
						//seq
						movseq = movsmalldamage;
						movcnt = 0;
						//play action
						anim.Stop ();
						anim.CrossFade ("damage_25", 0);
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_ht_s);
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ht_s);
					} else {
						//for during jump small damage
						//unavailable time
						unavailableCnt = 130;
						//seq
						movseq = movbigdamage;
						movcnt = 0;
						//play action
						anim.Stop ();
						anim.CrossFade ("down_20_p", 0);
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_ht_ko100);
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ht_ko);
					}
					//effect
					cashParticleSystem_tameParticles.Stop ();
					cashParticleSystem_nearParticles.Stop ();
					//tame gauge
					mc.tameGaugeOff ();
					//stage shake xz effect
					mc.setShakeEffect_xz (6);
				}
			}
		}
		//big damage
		if (inputBigDamage == true) {
			if ((movseq != movsmalldamage) && (movseq != movbigdamage)) {
				//sound
				cmn_soundController.Instance.stopSeCh( 0x09 );
				if (pko == false) {
					//common data
					cmn_staticData.Instance.phitnum++;
					//unavailable time
					unavailableCnt = 130;
					//tag
					this.tag = "unavailablePlayer";
					if (changekuri == false) {
						//tag
						kuriCtr.tag = "unavailablePlayer";
					} else {
						//tag
						kuriCtr2.tag = "unavailablePlayer";
					}
					//hp
					hp = hp - this.getBigDamage();
					if (hp <= 0) {
						//player ko
						//hp
						hp = 0;
						//state
						this.pko = true;
						//main
						mc.setPlayerKo();
						//target mark disp
						cashRectTransform_targetMarkTxt.position = new Vector2(0, -300);
					}
					mc.setPlayerHpGauge (hpIntial, hp);
					if (pko == false) {
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_ht_l);
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ht_l);
					} else {
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_ht_ko100);
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_ht_ko);
					}
					//seq
					movseq = movbigdamage;
					movcnt = 0;
					//change kuri
					if (changekuri == false) {
						this.changeKuri1to2 ();
						changekuri = true;
						cmn_staticData.Instance.lastkuri = true;
						//tag
						kuriCtr2.tag = "unavailablePlayer";
					}
					//play action
					anim.Stop ();
					anim.CrossFade ("down_20_p", 0);
					//effect
					cashParticleSystem_tameParticles.Stop ();
					cashParticleSystem_nearParticles.Stop ();
					//tame gauge
					mc.tameGaugeOff ();
					//stage shake xz effect
					mc.setShakeEffect_xz (15);
				}
			}
		}
		//set idle
		if (inputIdle == true) {
			//sound (tame cansel)
			if (movseq == movtame) {
//				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_tmcansel100);
				cmn_soundController.Instance.stopSeCh( 0x09 );
			}
			//seq
			movseq = movidle;
			movcnt = 0;
			//play action
			anim.Stop ();
			anim.CrossFade ("idle_20", 0);
			//effect
			cashParticleSystem_tameParticles.Stop ();
			//tame gauge
			mc.tameGaugeOff();
		}
		//normal atack
		if ( (movseq == movidle) || (movseq == movright) || (movseq == movleft) || (movseq == movtame) ) {	//idle or sidemove or tame only
			if (inputAtack == true) {
				//sound
				cmn_soundController.Instance.stopSeCh( 0x09 );	//for safe
				//common data
				cmn_staticData.Instance.patacknum++;
				if (near == false) {
					//no near
					//seq
					movseq = movatack;
					movcnt = 0;
					//set atack
					int tmp = this.getNormalBulletType();
					this.setAtack ( mpos, tmp );
					//play action
					anim.Stop ();
					anim.CrossFade ("punch_21", 0);
					//sound
					if ((cmn_staticData.Instance.highvoicecnt1 == 0) &&
					    (cmn_staticData.Instance.highvoicecnt2 == 0) &&
					    (cmn_staticData.Instance.highvoicecnt3 == 0)) {
						if (tmp == 3) {	//pants?
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_atck_p);
						} else {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_atck_s110);
						}
						if (sdcnt2 == 0) {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s100);
						} else if (sdcnt2 == 1) {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s110);
						} else if (sdcnt2 == 2) {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s120);
						}
						sdcnt2++;
						if (sdcnt2 >= 3) {
							sdcnt2 = 0;
						}
					}
				} else {
					//near
					//seq
					movseq = movatacknear;
					movcnt = 0;
					//set atack near
					this.setAtackNear (mpos, 0);
					//particle
					cashParticleSystem_nearParticles.Play();
					//play action
					anim.Stop ();
					anim.CrossFade ("punch_22", 0);
					//sound
					if ((cmn_staticData.Instance.highvoicecnt1 == 0) &&
					    (cmn_staticData.Instance.highvoicecnt2 == 0) &&
					    (cmn_staticData.Instance.highvoicecnt3 == 0)) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_atck_sn);
						if (sdcnt2 == 0) {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s100);
						} else if (sdcnt2 == 1) {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s110);
						} else if (sdcnt2 == 2) {
							cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s120);
						}
						sdcnt2++;
						if (sdcnt2 >= 3) {
							sdcnt2 = 0;
						}
					}
				}
			}
		}
		//tame for long atack
		if ( movseq == movidle ) {	//idle only
			if (inputTame == true) {
				//seq
				movseq = movtame;
				movcnt = 0;
				//play action
				anim.Stop ();
				anim.CrossFade ("crouchdown_10", 0);
				//particle
				cashParticleSystem_tameParticles.Play();
				//tame gauge
				Vector3 tmppos = cashTransform.position;
				tmppos.y = tmppos.y + 0.5f;
				mc.tameGaugeOn( tmppos );
				mc.tameGaugeUpdate (50, 0);
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_tm110);
			}
		}
		//long atack
		if ( (movseq == movidle) || (movseq == movright) || (movseq == movleft) || (movseq == movtame) ) {	//idle or sidemove or tame only
			if (inputLongAtack == true) {
				//sound
				cmn_soundController.Instance.stopSeCh( 0x09 );
				//common data
				cmn_staticData.Instance.patacknum++;
				if (near == false) {
					//no near
					//seq
					movseq = movlongatack;
					movcnt = 0;
					//set atack
					int tmp = this.getLargeBulletType();
					this.setAtack ( mpos, tmp );
					//play action
					anim.Stop ();
					anim.CrossFade ("kick_25", 0);
					//particle
					cashParticleSystem_tameParticles.Stop ();
					//tame gauge
					mc.tameGaugeOff ();
					//sound
					if( tmp == 2 ){	//tachio?
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_atck_p);
					}else{
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_atck_l130);	//select 100/120/130
					}
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_l);
				} else {
					//near
					//seq
					movseq = movlongatacknear;
					movcnt = 0;
					//set atack
					this.setAtackNear (mpos, 1);
					//play action
					anim.Stop ();
					anim.CrossFade ("throw_20", 0);
					//tame gauge
					mc.tameGaugeOff ();
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_atck_l110);
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_ln);
				}
			}
		}
		//side move
		if (movseq == movidle) {	//idle only
			//right
			if (inputRight == true) {
				//mov
				movseq = movright;
				movcnt = 0;
				//play action
				anim.Stop ();
				anim.CrossFade ("sidestep_10_p", 0);
				//sound
				if ((cmn_staticData.Instance.highvoicecnt1 == 0) &&
				    (cmn_staticData.Instance.highvoicecnt2 == 0) &&
				    (cmn_staticData.Instance.highvoicecnt3 == 0)) {
					if (sdcnt1 == 0) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_stp_100);
					} else if (sdcnt1 == 1) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_stp_110);
					}
					sdcnt1++;
					if (sdcnt1 >= 2) {
						sdcnt1 = 0;
					}
				}
			}
			//left
			if (inputLeft == true) {
				//mov
				movseq = movleft;
				movcnt = 0;
				//play action
				anim.Stop ();
				anim.CrossFade ("sidestep_11_p", 0);
				//sound
				if ((cmn_staticData.Instance.highvoicecnt1 == 0) &&
				    (cmn_staticData.Instance.highvoicecnt2 == 0) &&
				    (cmn_staticData.Instance.highvoicecnt3 == 0)) {
					if (sdcnt1 == 0) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_stp_100);
					} else if (sdcnt1 == 1) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_stp_110);
					}
					sdcnt1++;
					if (sdcnt1 >= 2) {
						sdcnt1 = 0;
					}
				}
			}
		}
		//jump
		if (movseq == movidle) {	//idle only
			//jump
			if (inputJump == true) {
				//mov
				movseq = movjump;
				movcnt = 0;
				//play action
				anim.Stop ();
				anim.CrossFade ("jump_20", 0);
			}
		}

		//move seq
		switch (movseq) {
		case movwakeup:
			//wake up
			if (movcnt == 30) {
//				anim.Stop ();
			}
			movcnt++;
			break;
		case movidle:
			//idle
			if (hko == true) {
				movseq = movwaithko;
				movcnt = 0;
			}
			break;
		case movright:
			//move right
			//move
			movdir = movdir + movdd;
			if (movdir >= 360) {
				movdir = movdir - 360;
			}
			float nx = Mathf.Cos (movdir * Mathf.Deg2Rad) * 3.0f;
			float nz = Mathf.Sin (movdir * Mathf.Deg2Rad) * 3.0f;
			Vector3 pos = cashTransform.position;
			float xx = nx - pos.x;
			float zz = nz - pos.z;
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			cashTransform.Translate (xx, 0.0f, zz);
			//cnt
			movcnt++;
			if (movcnt == 10) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_btl_walk);
			}
			if (movcnt >= 17) {
				if (hko == false) {
					movseq = movidle;
					movcnt = 0;
					anim.CrossFade ("idle_20", 0);
				} else {
					movseq = movwaithko;
					movcnt = 0;
				}
			}
			break;
		case movleft:
			//move right
			//move
			movdir = movdir - movdd;
			if (movdir <= 0) {
				movdir = movdir + 360;
			}
			nx = Mathf.Cos (movdir * Mathf.Deg2Rad) * 3.0f;
			nz = Mathf.Sin (movdir * Mathf.Deg2Rad) * 3.0f;
			pos = cashTransform.position;
			xx = nx - pos.x;
			zz = nz - pos.z;
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 0.0f));
			cashTransform.Translate (xx, 0.0f, zz);
			//cnt
			movcnt++;
			if (movcnt == 10) {
				//sound
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_btl_walk);
			}
			if (movcnt >= 17) {
				if (hko == false) {
					movseq = movidle;
					movcnt = 0;
					anim.CrossFade ("idle_20", 0);
				} else {
					movseq = movwaithko;
					movcnt = 0;
				}
			}
			break;
		case movjump:
			//move jump
			if (movcnt == 5) {
				yy = 0.15f;
			}
			//translate
			if ( (cashTransform.position.y <= 0.02f) && (yy<0.0f) ) {
				Vector3 tmppos = cashTransform.position;
				tmppos.y = 0.02f;
				cashTransform.position = tmppos;
				yy = 0.0f;
			} else {
				cashTransform.Translate (0.0f, yy, 0.0f);
				yy = yy - 0.011f;
			}
			//cnt
			movcnt++;
			if (movcnt >= 42) {
				if (hko == false) {
					movseq = movidle;
					movcnt = 0;
					anim.CrossFade ("idle_20", 0);
//					mc.setShakeEffect_y (5);
				} else {
					movseq = movwaithko;
					movcnt = 0;
				}
			}
			break;
		case movatack:
			//normal atack
			//cnt
			movcnt++;
			if (movcnt >= 20) {
				if (hko == false) {
					movseq = movidle;
					movcnt = 0;
					anim.CrossFade ("idle_20", 0);
				} else {
					movseq = movwaithko;
					movcnt = 0;
				}
			}
			break;
		case movatacknear:
			//normal atack near
			//cnt
			movcnt++;
			if (movcnt == 5) {
				//particle
				cashParticleSystem_nearParticles.Stop();
			}
			if (movcnt >= 30) {
				if (hko == false) {
					//mov
					movseq = movidle;
					movcnt = 0;
					//action
					anim.CrossFade ("idle_20", 0);
				} else {
					movseq = movwaithko;
					movcnt = 0;
				}
			}
			break;
		case movtame:
			//tame for long atack
			//target mark disp
			cashRectTransform_targetMarkTxt.position = tameMpos;
			//mov
			movcnt++;
			//sound
			if (movcnt == 10) {
				if (sdcnt3 == 0) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_tm_100);
				} else if (sdcnt3 == 1) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_tm_110);
				} else if (sdcnt3 == 2) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_tm_120);
				}
				sdcnt3++;
				if (sdcnt3 >= 3) {
					sdcnt3 = 0;
				}
			}
			if (movcnt == 20) {
				anim.Stop ();
//				anim.CrossFade ("crouch_20_a", 0);
			}
			//tame gauge
			mc.tameGaugeUpdate (50, tapCnt);
			if (hko == true) {
				movseq = movwaithko;
				movcnt = 0;
			}
			break;
		case movlongatack:
			//long atack
			//cnt
			movcnt++;
			if (movcnt >= 37) {
				if (hko == false) {
					movseq = movidle;
					movcnt = 0;
					anim.CrossFade ("idle_20", 0);
				} else {
					movseq = movwaithko;
					movcnt = 0;
				}
			}
			break;
		case movlongatacknear:
			//long atack near
			//cnt
			movcnt++;
			if (movcnt >= 40) {
				if (hko == false) {
					movseq = movidle;
					movcnt = 0;
					anim.CrossFade ("idle_20", 0);
				} else {
					movseq = movwaithko;
					movcnt = 0;
				}
				//particle
				cashParticleSystem_tameParticles.Stop ();
			}
			break;
		case movsmalldamage:
			//small damage
			movcnt++;
			if (movcnt == 10) {
				//collider
				if (changekuri == false) {
					cashCapsuleCollider_kuriCtr.enabled = false;
				} else {
					cashCapsuleCollider_kuriCtr2.enabled = false;
				}
			}
			if (movcnt >= 30) {
				if (pko == false) {
//					//tag
//					this.tag = "player";
//					kuriCtr.tag = "player";
					//seq
					movseq = movidle;
					movcnt = 0;
					//action
					anim.CrossFade ("idle_20", 0);
				} else {
					if (hko == false) {
						//seq
						movseq = movwaitpko;
						movcnt = 0;
						//action
						anim.CrossFade ("idle_20", 0);
					} else {
						movseq = movwaithko;
						movcnt = 0;
					}
				}
			}
			break;
		case movbigdamage:
			if (movcnt == 0) {
				//init
				yy = 0.20f;
				downShake = false;
			}
			movcnt++;
			if (movcnt == 10) {
				//collider
				if (changekuri == false) {
					cashCapsuleCollider_kuriCtr.enabled = false;
				} else {
					cashCapsuleCollider_kuriCtr2.enabled = false;
				}
			}
			//translate
			if ((cashTransform.position.y <= 0.02f) && (yy < 0.0f)) {
				Vector3 tmppos = cashTransform.position;
				tmppos.y = 0.02f;
				cashTransform.position = tmppos;
				yy = 0.0f;
				//stage shake y effect
				if (downShake == false) {
					mc.setShakeEffect_y (18);
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_dwn110);
					downShake = true;
				}
			} else {
				cashTransform.Translate (0.0f, yy, 0.0f);
				yy = yy - 0.011f;
			}
			//action
			if (movcnt == 20) {
				//action
				anim.Stop ();
				anim.CrossFade ("down_21", 0);
			}
			if (movcnt == 50) {
				if ( (pko == false) && (hko == false) ) {
					//sound
					if (sdcnt4 == 0) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_dwn_100);
					} else if (sdcnt4 == 1) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_dwn_110);
					} else if (sdcnt4 == 2) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_dwn_120);
					} else if (sdcnt4 == 3) {
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_dwn_130);
					}
					sdcnt4++;
					if (sdcnt4 >= 4) {
						sdcnt4 = 0;
					}
				}
			}
			if (movcnt >= 110) {
//				//tag
//				this.tag = "player";
//				kuriCtr.tag = "player";
				if (hko == false) {
					//seq
					movseq = movidle;
					movcnt = 0;
					//action
					anim.CrossFade ("idle_20", 0);
				} else {
					movseq = movwaithko;
					movcnt = 0;
				}
				//state
				downShake = false;
			} else if (movcnt >= 70) {
				if (pko == false) {
					anim.CrossFade ("getup_20_p", 0);
				} else {
					//seq
					movseq = movwaitpko;
					movcnt = 0;
				}
			}
			break;
		case movwaitstart:
			//wait start
			if (movcnt == 10) {
				anim.CrossFade ("greet_01", 0);
			}
			if (movcnt == 65) {
//				anim.CrossFade ("idle_01", 0);
				anim.Stop();
			}
			if (movcnt == 80) {
				anim.CrossFade ("turn_10", 0);
			}
			if (movcnt == 100) {
				anim.CrossFade ("idle_20", 0);
				startTurn = false;
			}
			movcnt++;
			break;
		case movwaithko:
			//wait hiari ko
			if (movcnt == 0) {
				//action
				anim.CrossFade ("idle_20", 0);
			}
			if (movcnt == 50) {
				//action
//				anim.Stop ();
			}
			if (movcnt == 100) {
				//pos adjust
				Vector3 tmppos = cashTransform.position;
				tmppos.y = 0.02f;
				cashTransform.position = tmppos;
				//action
				anim.CrossFade ("sit_05", 0);
				//generate cola can
				mc.generateColaCan( this.getPlayerPos(), pdir );
			}
			movcnt++;
			break;
		case movwaitpko:
			//wait player ko

			break;
		default:
			break;
		}

		//player rotate fix
		//direction to ant from player
		//get to hiari dir
		float xdistance, ydistance, zdistance;
		float direction,direction2;
		Vector3 ppos;
		const float doffset = +90.0f;
		ppos = cashTransform.position;
		Vector3 hpos = hac.getHiariPos ();
		xdistance = (hpos.x) - (ppos.x);	//hiari,player x distance
		zdistance = (hpos.z) - (ppos.z);	//hiari,player z distance
		if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
			xdistance = 0.0001f;
		}
		direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
		float plydir = direction;
		if (plydir < 0) {
			plydir = plydir + 360.0f;
		}
		//start turn
		if (startTurn == true) {
			plydir = plydir + 200;
			if (plydir >= 360) {
				plydir = plydir - 360;
			}
		}
		//adjust dir(y reverse)
		plydir = (360 - plydir)+doffset;
		//adjust dir(hiari ko sit action)
		if ((movseq == movwaithko) && (movcnt >= 100)) {
			plydir = plydir + 150;
		}
		pdir = plydir;
		//rotate
		if (movseq != movbigdamage) {
			cashTransform.rotation = Quaternion.Euler (new Vector3 (0.0f, pdir, 0.0f));
		}


		//hp gauge start update
		if (movseq != movwakeup) {
			if (hpStart == true) {
				if (hp < hpIntial) {
					hp = hp + 2;
					if (hp >= hpIntial) {
						hp = hpIntial;
						hpStart = false;
					}
					mc.setPlayerHpGauge (hpIntial, hp);
				}
			}
		}
	}


	//set atack
	private void setAtack( Vector2 mpos, int bulletType ){
		float xdistance, ydistance, zdistance, xzdistance;
		float direction,direction2;
		Vector3 ppos;
		//tap pos to world pos
		Ray ray = Camera.main.ScreenPointToRay (mpos);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity) == true) {
			//target mark disp
			cashRectTransform_targetMarkTxt.position = mpos;
			//atack dir
			//direction to tap pos from player
			//get to tap pos dir
			ppos = cashTransform.position;
			ppos.y = ppos.y + 1.0f;
			xdistance = (hit.point.x) - (ppos.x);	//tap pos,player x distance
			ydistance = (hit.point.y) - (ppos.y);	//tap pos,player y distance
			zdistance = (hit.point.z) - (ppos.z);	//tap pos,player z distance
			xzdistance = Mathf.Sqrt( Mathf.Pow( xdistance,2 ) + Mathf.Pow( zdistance,2 ) );	//tap pos, player xz distance
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
			float pbspd = 0.35f;
			if (bulletType == 2) {	//large bullet tachio
				pbspd = 0.14f;
			}
			if (bulletType == 3) {	//normal bullet pants
				pbspd = 0.20f;
			}
			float xx = Mathf.Cos( atcdir*Mathf.Deg2Rad ) * pbspd;
			float yy = Mathf.Cos( atcdir2*Mathf.Deg2Rad ) * pbspd;
			float zz = Mathf.Sin( atcdir*Mathf.Deg2Rad ) * pbspd;
			//bullet pos init
			Vector3 bpos = ppos;
			float initadj = 2.0f;
			if (bulletType == 0) {	//normal bullet
				initadj = initadj + 0.0f;
			}
			if (bulletType == 1) {	//large bullet
				initadj = initadj + 0.0f;
			}
			if (bulletType == 2) {	//large bullet tachio
//				initadj = initadj - 1.5f;
				initadj = initadj - 0.0f;
			}
			if (bulletType == 3) {	//normal bullet pants
				initadj = initadj + 0.0f;
			}
			bpos.x = bpos.x + (xx * initadj);
			bpos.y = bpos.y + (yy * initadj);
			bpos.z = bpos.z + (zz * initadj);
			//generate player bullet
			mc.generatePlayerBullet(bulletType, bpos, xx, yy, zz, atcdir);
		}
	}

	//set atack near
	private void setAtackNear( Vector2 mpos, int atackType ){
		float xdistance, ydistance, zdistance, xzdistance;
		float direction,direction2;
		Vector3 ppos;
		//tap pos to world pos
		Ray ray = Camera.main.ScreenPointToRay (mpos);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity) == true) {
			//target mark disp
			cashRectTransform_targetMarkTxt.position = mpos;
			//hiari hit?
			if (hit.collider.tag == "enemy"){
				if (atackType == 0) {
					hac.setSmallDamage ();
				} else if (atackType == 1) {
					hac.setBigDamage ();
				}
			}
		}
	}

	//change kuri 1 to 2
	private void changeKuri1to2(){
		//kuri2 enable
		kuriCtr2.SetActive (true);
		//kuri1 disable
		kuriCtr.SetActive (false);
		//change animation
		anim = kuriCtr2.GetComponent<Animation> ();
	}

	//get normal bullet type
	private int getNormalBulletType(){
		int ptn = cmn_staticData.Instance.pbptn_n [nbpcnt];
		nbpcnt++;
		if (nbpcnt >= cmn_staticData.Instance.pbptn_n.Length) {
			nbpcnt = 0;
		}
		if( (cmn_staticData.Instance.sitagi == false) && (ptn == 3) ){	//get shitagi?
			ptn = 0;	//normal bullet
		}
		return ptn;
	}

	//get large bullet type
	private int getLargeBulletType(){
		int ptn = cmn_staticData.Instance.pbptn_l [lbpcnt];
		lbpcnt++;
		if (lbpcnt >= cmn_staticData.Instance.pbptn_l.Length) {
			lbpcnt = 0;
		}
		return ptn;
	}

	//get small damage
	private int getSmallDamage(){
		return 10 + cmn_staticData.Instance.hatackplus - cmn_staticData.Instance.defenceplus;
	}

	//get big damage
	private int getBigDamage(){
		return 28 + cmn_staticData.Instance.hatackplus - cmn_staticData.Instance.defenceplus;
	}


	//public

	//tap pause button
	public void tapPauseButton(){
		//input cancel
		duringInput = false;
		duringLongTap = false;
		tapCnt = 0;
		//effect
		cashParticleSystem_tameParticles.Stop ();
		//tame gauge
		mc.tameGaugeOff();
	}

	//term wake up
	public void termWakeup(){
		//mov
		movseq = movwaitstart;
		movcnt = 0;
	}

	//set fight start
	public void setFightStart(){
		//mov
		movseq = movidle;
		movcnt = 0;
		startTurn = false;
	}

	//set hiari ko
	public void setHiariKo(){
		//state
		this.hko = true;
		//target mark disp
		cashRectTransform_targetMarkTxt.position = new Vector2(0, -300);
	}

	//small damage player
	public void setSmallDamage(){
		if (this.tag == "player") {
			this.smallDamage = true;
		}
	}

	//big damage player
	public void setBigDamage(){
		if (this.tag == "player") {
			this.bigDamage = true;
		}
	}

	//get player pos dir
	public float getPlayerPosDir(){
		return this.movdir;
	}

	//get player pos
	public Vector3 getPlayerPos(){
		return this.cashTransform.position;
	}

	//get player movseq
	public int getPlayerMov(){
		return this.movseq;
	}

	//get player movcnt
	public int getPlayerCnt(){
		return this.movcnt;
	}

}
