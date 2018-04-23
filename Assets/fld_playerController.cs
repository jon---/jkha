using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fld_playerController : MonoBehaviour {
	//public

	//private
	//local const
	//move seq const
	const int movwakeup = 0;
	const int movidle = 1;
	const int movmove = 2;
	const int movmoverelease = 3;
	const int movtap = 4;
	const int movwaitstart = 100;
	const int movwaithcoll = 110;
	//mov speed
	const float movdd = 1.0f;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	Rigidbody cashRigidbody;
	GameObject mainCtr;
	fld_mainController mc;
	GameObject kuriCtr;
	fld_kuriController krc;
	Animation anim;
	GameObject fld_targetMark;
	Text targetMarkTxt;
	RectTransform cashRectTransform_targetMarkTxt;

	//local
	//input
	bool duringInput;
	bool duringLongTap;
	int tapCnt;
	Vector2 startMpos;
	//state
	bool inputFirst;
	//collision
	bool hiariColl;

	//move
	int movseq;
	int movcnt;

	//sound
	int sdcnt1;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//rigidbody cash
		cashRigidbody = GetComponent<Rigidbody>();

		//main controller
		mainCtr = GameObject.Find ("fld_mainController");
		mc = mainCtr.GetComponent<fld_mainController> ();

		//jk controller
		kuriCtr = GameObject.Find ("fld_f05_blazer_020_h");
		krc = kuriCtr.GetComponent<fld_kuriController> ();
		anim = kuriCtr.GetComponent<Animation> ();

		//target mark
		fld_targetMark = GameObject.Find("fld_targetMark");
		targetMarkTxt = fld_targetMark.GetComponent<Text> ();
		cashRectTransform_targetMarkTxt = targetMarkTxt.GetComponent<RectTransform> ();

		//input
		duringInput = false;
		duringLongTap = false;
		tapCnt = 0;
		startMpos = new Vector2 (0, 0);
		//state
//		inputFirst = true;
		inputFirst = false;
		//collision
		hiariColl = false;

		//move
		movseq = movwakeup;
		movcnt = 0;
		//action
		anim.CrossFade ("idle_11", 0);

		//sound
		sdcnt1 = 0;

	}
	
	// Update is called once per frame
	void Update () {
		//fix input
		bool inputIdle = false;
		bool inputRight = false;
		bool inputLeft = false;
		//
		bool inputMove = false;
		bool inputMoveRelease = false;
		bool inputTap = false;
		bool inputHiariColl = false;
		//fix hiari collision
		if (hiariColl == true) {
			hiariColl = false;
			//input
			inputHiariColl = true;
			//input cancel
			duringInput = false;
			duringLongTap = false;
			tapCnt = 0;
		}
		Vector2 mpos = new Vector2 (0, 0);	//(fix mouse position)
		if ((movseq == movwakeup) || (movseq == movwaitstart) || (movseq == movwaithcoll)) {
			//input unavailable
			//input cancel
			duringInput = false;
			duringLongTap = false;
			tapCnt = 0;
		} else {
			//input available
			//key input for unity editor only
			#if UNITY_EDITOR
			if (Input.anyKeyDown == true) {
				//move
				if (Input.GetKeyDown (KeyCode.RightArrow) == true) {
					inputRight = true;
				}
				if (Input.GetKeyDown (KeyCode.LeftArrow) == true) {
					inputLeft = true;
				}
			}
			#endif
			//touch/tap process
			if (Input.GetMouseButtonDown (0)) {
				if (inputFirst == false) {	//(for first start tap)
					//mouse input start
					if (duringInput == false) {
						if ((tapCnt == 0) && (duringLongTap == false)) {
							duringInput = true;
							duringLongTap = false;
							tapCnt = 0;
						}
					}
				} else {
					inputFirst = false;
				}
			}
			if (Input.GetMouseButton (0)) {
				if (duringInput == true) {
					tapCnt++;
					if (tapCnt >= 5) {
						//fix touch
						duringLongTap = true;
						//fix move
						inputMove = true;
						mpos = Input.mousePosition;
					}
				}
			}
			if (Input.GetMouseButtonUp (0)) {
				if (duringInput == true) {
					if (duringLongTap == true) {
						//fix move release
						inputMoveRelease = true;
						mpos = Input.mousePosition;
					} else {
						//fix tap
						inputTap = true;
						mpos = Input.mousePosition;
					}
					duringInput = false;
					duringLongTap = false;
					tapCnt = 0;
				}
			}
		}


		//fix seq
		//move release
		if (inputMoveRelease == true) {
			if( (movseq != movwakeup) && (movseq != movwaithcoll) && (movseq != movwaitstart) ) {
				//seq
				movseq = movidle;
				movcnt = 0;
				//action
				anim.CrossFade("idle_10");
			}
		}
		//move
		if (inputMove == true) {
			if (movseq != movmove) {
				//seq
				movseq = movmove;
				movcnt = 0;
				//action
				anim.CrossFade("sprint_00");
			}
		}
		//tap
		if (inputTap == true) {
			//atack
			Ray ray = Camera.main.ScreenPointToRay (mpos);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity) == true) {
//				Debug.Log (hit.collider.tag);	//debug
				//target mark disp
				cashRectTransform_targetMarkTxt.position = mpos;
				//target?
				if( ( (hit.collider.tag == "fld_car1") || (hit.collider.tag == "fld_car2") ||
					  (hit.collider.tag == "fld_car3") || (hit.collider.tag == "fld_car4") ) &&
					( Mathf.Abs(hit.collider.transform.position.x - cashTransform.position.x) <= 16.0f ) &&
					( Mathf.Abs(hit.collider.transform.position.z - cashTransform.position.z) <= 16.0f )
				){
					//car atack
					mc.setAtack ( (hit.collider.gameObject) );
					//seq
					movseq = movtap;
					movcnt = 0;
					//action
					anim.CrossFade("kick_21");
					//sound
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_p_atck_sn);
					if (sdcnt1 == 0) {
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s100);
					} else if (sdcnt1 == 1) {
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s110);
					} else if (sdcnt1 == 2) {
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_vo_p_atck_s120);
					}
					sdcnt1++;
					if (sdcnt1 >= 3) {
						sdcnt1 = 0;
					}
				}
			}
		}
		//hiari coll
		if (inputHiariColl == true) {
			//seq
			movseq = movwaithcoll;
			movcnt = 0;
			//action
			anim.CrossFade("nod_01");
		}

		//mov seq
		switch (movseq) {
		case movwakeup:
			//wakeup
			//nop
			break;
		case movidle:
			//idle
			//stop
			cashRigidbody.velocity = Vector3.zero;
			break;
		case movmove:
			//move
			Ray ray = Camera.main.ScreenPointToRay (mpos);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, Mathf.Infinity) == true) {
				float xdistance, zdistance, xzdistance;
				float direction;
				Vector3 ppos;
				//target mark disp
				cashRectTransform_targetMarkTxt.position = mpos;
				//atack dir
				//direction to tap pos from player
				//get to tap pos dir
				ppos = cashTransform.position;
				xdistance = (hit.point.x) - (ppos.x);	//tap pos,player x distance
				zdistance = (hit.point.z) - (ppos.z);	//tap pos,player z distance
				xzdistance = Mathf.Sqrt (Mathf.Pow (xdistance, 2) + Mathf.Pow (zdistance, 2));	//tap pos, player xz distance
				if (xdistance == 0) {	//for zero exception
					xdistance = 0.0001f;
				}
				direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
				float atcdir = direction;
				if (atcdir < 0) {
					atcdir = atcdir + 360.0f;
				}
				//get x,y,z speed to tap pos
				const float pbspd = 0.85f;
				float xx = Mathf.Cos (atcdir * Mathf.Deg2Rad) * pbspd;
				float zz = Mathf.Sin (atcdir * Mathf.Deg2Rad) * pbspd;
				Vector3 movv = new Vector3 (xx, 00, zz);
				cashTransform.Translate (movv);
				//for kuri controller
				float tmpdir = (360 - atcdir) + 90;
				if (tmpdir < 0) {
					tmpdir = tmpdir + 360;
				}
				if (tmpdir >= 360) {
					tmpdir = tmpdir - 360;
				}
				krc.setKuriRotate (tmpdir);
			}
			//sound
			if (movcnt % 10 == 0) {
				cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_fld_walk);
			}
			movcnt++;
			break;
		case movtap:
			//tap
			movcnt++;
			if( movcnt >= 35 ){
				//action
				anim.CrossFade ("idle_11");
				//mov
				movseq = movidle;
				movcnt = 0;
			}
			break;
		case movwaitstart:
			//wait start
			movcnt++;
			if (movcnt == 10) {
				//action
				anim.CrossFade ("greet_03");
			}
			if (movcnt == 60) {
				//action
				anim.CrossFade ("idle_10");
			}
			break;
		case movwaithcoll:
			//wait hiari encount
			krc.setKuriRotate (180.0f);
			//stop
			cashRigidbody.velocity = Vector3.zero;
//			if (movcnt == 100) {
//				anim.CrossFade ("idle_11");
//			}
			if (movcnt == 56) {
				anim.Stop ();
				anim.CrossFade ("greet_00");
			}
			if (movcnt == 156) {
				anim.Stop ();
			}
			movcnt++;
			break;
		default:
			break;
		}
		
	}

	//public
	//collision
	public void OnCollisionEnter( Collision coll ){
		if (coll.gameObject.tag == "enemy") {
			coll.gameObject.tag = "unavailableEnemy";	//for safe
			if (hiariColl == false) {	//1回だけ有効
				//player collision
				hiariColl = true;
				//set encount
				mc.setPlayerEncount ();
				//target mark disp
				cashRectTransform_targetMarkTxt.position = new Vector2(0, -300);
			}
		}
	}


	//private


	//public
	//get pos
	public Vector3 getPlayerPos(){
		return cashTransform.position;
	}

	//term wakeup
	public void termWakeup(){
		//mov
		movseq = movwaitstart;
		movcnt = 0;
	}

	//set field start
	public void setFieldStart(){
		movseq = movidle;
		movcnt = 0;
		//action
		anim.CrossFade("idle_10");
	}


}
