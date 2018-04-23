using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fld_hiariController : MonoBehaviour {
	//public

	//private
	//local const
	//move seq const
	const int movwakeup = 0;
	const int movthink = 1;
	const int movrotate = 2;
	const int movfind = 3;
	const int movwaitstart = 100;
	const int movwaitencount = 110;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	Rigidbody cashRigidbody;
	fld_cameraController cameraCtr;
	GameObject mainCtr;
	fld_mainController mc;
	GameObject antCtr;
	fld_antController atc;
	GameObject playerCtr;
	fld_playerController plc;

	//local

	//move seq
	int movseq;
	int movcnt;
	int thinkwait;	//think wait
	int rotatecnt;	//rotate cnt
	int rotatedir;	//totate direction

	//rotate
	float ytdir;
	float ycdir;

	float xdir;
	float zdir;

	//action
	int actseq;
	int actcnt;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//rigidbody cash
		cashRigidbody = GetComponent<Rigidbody>();

		//camera controller
		cameraCtr = Camera.main.GetComponent<fld_cameraController>();

		//maincontroller
		mainCtr = GameObject.Find ("fld_mainController");
		mc = mainCtr.GetComponent<fld_mainController> ();

		//ant controller
		antCtr = GameObject.Find ("fld_fire-ant");
		atc = antCtr.GetComponent<fld_antController> ();

		//playercontroller
		playerCtr = GameObject.Find ("fld_playerController");
		plc = playerCtr.GetComponent<fld_playerController> ();

		//move
		movseq = movwakeup;
		movcnt = 0;
		thinkwait = 0;
		rotatecnt = 0;
		rotatedir = 0;
		//rotate
		ytdir = 0.0f;
		ycdir = 0.0f;
		xdir = 0.0f;
		zdir = 0.0f;

		//fix initial pos
		//(set from parent)

		//action
		actseq = 0;
		actcnt = 0;

	}


	// Update is called once per frame
	void Update () {


		////always process

		//move seq
		switch (movseq) {
		case movwakeup:
			//wakeup
			//nop
			break;
		case movthink:
			//think
			//action
			if (movcnt == 0) {
				thinkwait = Random.Range (10, 50);
			}
			movcnt++;
			if (movcnt >= thinkwait) {
				//seq
				movseq = movrotate;
				movcnt = 0;
				//rotate
				int tmp = Random.Range (0, 2);
				if (tmp == 0) {
					rotatedir = 0;
				} else {
					rotatedir = 1;
				}
				movcnt = 0;
				rotatecnt = Random.Range (30, 60);
				actcnt = 0;
			}
			//stop
			cashRigidbody.velocity = Vector3.zero;
			break;
		case movrotate:
			//rotate
			const float ydd = 5.0f;
			if (rotatedir == 0) {
				ytdir = ytdir + ydd;
				ycdir = ytdir;
			} else {
				ytdir = ytdir - ydd;
				ycdir = ytdir;
			}
			//action
			const int actdir = 4;
			actcnt++;
			if (actcnt >= 3) {
				switch (actseq) {
				case 0:
					xdir = xdir - actdir;
					zdir = zdir + actdir;
					actseq++;
					break;
				case 1:
					xdir = xdir - actdir;
					zdir = zdir - actdir;
					actseq++;
					break;
				case 2:
					xdir = xdir + actdir;
					zdir = zdir - actdir;
					actseq++;
					break;
				case 3:
					xdir = xdir + actdir;
					zdir = zdir + actdir;
					actseq = 0;
					break;
				default:
					break;
				}
			}
			//movcnt
			movcnt ++;
			if (movcnt >= rotatecnt) {
				//mov
				movseq = movthink;
				movcnt = 0;
				//action
				xdir = 0;
				zdir = 0;
			}
			//stop
			cashRigidbody.velocity = Vector3.zero;
			break;
		case movfind:
			//find
			movcnt++;
			if (movcnt == 1) {
				//direction to player from ant
				//get to hiari dir
				float xdistance, ydistance, zdistance;
				float direction, direction2;
				Vector3 hpos;
				const float doffset = 90.0f;
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
				hiadir = (360 - hiadir) + doffset;	//adjust dir
				if (hiadir > 360) {
					hiadir = hiadir - 360;
				}
				ytdir = hiadir;
			}
			//current dir process
			//dir current -> target
			const float dirstepf = 2.0f;
			bool action = false;
			if ((ytdir > ycdir) && ((ytdir - ycdir) > dirstepf)) {
				if ((ytdir - ycdir) < 180) {
					ycdir = ycdir + dirstepf;
					action = true;
				} else {
					ycdir = ycdir - dirstepf;
					action = true;
				}
			} else if ((ytdir < ycdir) && ((ycdir - ytdir) > dirstepf)) {
				if ((ycdir - ytdir) < 180) {
					ycdir = ycdir - dirstepf;
					action = true;
				} else {
					ycdir = ycdir + dirstepf;
					action = true;
				}
			} else {
				ycdir = ytdir;
//				xdir = 0.0f;
//				zdir = 0.0f;
			}
			if (ycdir > 360) {
				ycdir = ycdir - 360;
			}
			if (ycdir < 0) {
				ycdir = ycdir + 360;
			}
			//action
			const float actdirf = 6.0f;
//			if (action == true) {
				actcnt++;
				if (actcnt >= 3) {
					switch (actseq) {
					case 0:
						xdir = xdir - actdirf;
						zdir = zdir + actdirf;
						actseq++;
						break;
					case 1:
						xdir = xdir - actdirf;
						zdir = zdir - actdirf;
						actseq++;
						break;
					case 2:
						xdir = xdir + actdirf;
						zdir = zdir - actdirf;
						actseq++;
						break;
					case 3:
						xdir = xdir + actdirf;
						zdir = zdir + actdirf;
						actseq = 0;
						break;
					default:
						break;
					}
				}
//			}
			//stop
			cashRigidbody.velocity = Vector3.zero;
			break;
		case movwaitstart:
			//wait for field start
			//nop
			break;
		default:
			break;
		}

		//hiari rotate fix
		//rotate
		cashTransform.rotation = Quaternion.Euler (new Vector3 (zdir, ycdir+90.0f, xdir));	//90度offsetのため x<->z


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

	//term wakeup
	public void termWakeup(){
		//mov
		movseq = movwaitstart;
		movcnt = 0;
	}

	//set start field
	public void setFieldStart(){
		//mov
		movseq = movthink;
		movcnt = 0;
	}

	//set encount
	public void setEncount(){
		//mov
		movseq = movfind;
		movcnt = 0;
	}

	//get hiari pos
	public Vector3 getHiariPos(){
		return this.cashTransform.position;
	}

	public void setInitState( float x, float z ){
		Vector3 hpos = transform.position;
		hpos.x = x;
		hpos.y = 0.0f;
		hpos.z = z;
		transform.position = hpos;
	}

}
