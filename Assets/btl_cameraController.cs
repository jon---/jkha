using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_cameraController : MonoBehaviour {
	//public
	//

	//private
	//local const
	//move seq const
	const int movbattle = 0;
	const int movplayerko = 1;
	const int movhiariko = 2;
	//mov speed
	const float movdd = 1.0f;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	btl_mainController mc;
	GameObject playerCtr;
	btl_playerController plc;
	GameObject hiariCtr;
	btl_hiariController hac;

	//local
	//move seq
	int movseq;
	int movcnt;
	float postdir;	//target camera pos dir
	float poscdir;	//current camera pos dir
	bool firstposdir;
	float tdir;	//target camera angle(y)
	float cdir;	//current camera angle(y)
	bool firstdir;

	//hiari info
	bool hsmove;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//maincontroller
		mainCtr = GameObject.Find ("btl_mainController");
		mc = mainCtr.GetComponent<btl_mainController> ();

		//playercontroller
		playerCtr = GameObject.Find ("btl_playerController");
		plc = playerCtr.GetComponent<btl_playerController> ();

		//hiaricontroller
		hiariCtr = GameObject.Find ("btl_hiariController");
		hac = hiariCtr.GetComponent<btl_hiariController> ();

		//move
		movseq = movbattle;
		movcnt = 0;
		//posdir
		postdir = 0.0f;	//target camera pos dir
		poscdir = 0.0f;	//current camera pos dir
		firstposdir = true;
		//dir
		tdir = 0.0f;	//target camera angle
		cdir = 0.0f;	//current camera angle
		firstdir = true;

		//hiari info
		hsmove = false;

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

		//battle camera process
		this.battleCameraProcess();


		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}

	}


	//private

	//battle camera process
	private void battleCameraProcess(){
		switch( movseq ){
		case movbattle:
			//battle
			//camera mov
			//direction to ant from player
			float xdistance, zdistance;
			float direction;
			const float doffset = +90.0f;
			//get to hiari dir
			Vector3 ppos = plc.getPlayerPos ();
			Vector3 hpos = hac.getHiariPos ();
			xdistance = (hpos.x) - (ppos.x);	//hiari,player x distance
			zdistance = (hpos.z) - (ppos.z);	//hiari,player z distance
			if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
			float camdir = direction;;
			if (camdir < 0) {
				camdir = camdir + 360.0f;
			}
			//camera dir for pos adjust
			camdir = camdir + 180 + 20.0f;	//(player中心に antと反対側(+180)+antを常に右手(+)にpos取り)
			if (camdir > 360) {
				camdir = camdir - 360;
			}
			//target pos dir set
			postdir = camdir;
			//first dir set
			if (firstposdir == true) {
				firstposdir = false;
				postdir = camdir;
				poscdir = postdir;
			}
			//pos dir current -> target
			const float posdirstep = 10.0f;
			if (this.hsmove == false) {
				if ((postdir > poscdir) && ((postdir - poscdir) > posdirstep)) {
					if ((postdir - poscdir) < 180) {
						poscdir = poscdir + posdirstep;
					} else {
						poscdir = poscdir - posdirstep;
					}
				} else if ((postdir < poscdir) && ((poscdir - postdir) > posdirstep)) {
					if ((poscdir - postdir) < 180) {
						poscdir = poscdir - posdirstep;
					} else {
						poscdir = poscdir + posdirstep;
					}
				} else {
					poscdir = postdir;
				}
				if (poscdir > 360) {
					poscdir = poscdir - 360;
				}
				if (poscdir < 0) {
					poscdir = poscdir + 360;
				}
			}
			//fix distance camera pos to player pos
			float tmpdis = Mathf.Abs(xdistance) + Mathf.Abs(zdistance);
			float adddis = 0.0f;
			if ( tmpdis < 4.5f) {
				adddis = 4.5f - tmpdis;
			}
			//camera pos fix (from player)
			float nx = Mathf.Cos (poscdir * Mathf.Deg2Rad) * (2.35f + adddis);	//distance player
			float nz = Mathf.Sin (poscdir * Mathf.Deg2Rad) * (2.35f + adddis);	//distance player
			Vector3 pos = cashTransform.position;
			pos.x = ppos.x + nx;
			pos.z = ppos.z + nz;
			cashTransform.position = pos;

			//camera angle
			//direction to player and ant
			//get to player dir
			xdistance = (ppos.x) - (cashTransform.position.x);	//camera,player x distance
			zdistance = (ppos.z) - (cashTransform.position.z);	//camera,player z distance
			if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
			float topdir = direction;
			if (topdir < 0) {
				topdir = topdir + 360.0f;
			}
			//get to hiari dir
			xdistance = (hpos.x) - (cashTransform.position.x);	//camera,hiari x distance
			zdistance = (hpos.z) - (cashTransform.position.z);	//camera,hiari z distance
			if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
			float tohdir = direction;
			//		tohdir = tohdir - 3.5f;//	hiari fbx center adjust
			if (tohdir < 0) {
				tohdir = tohdir + 360.0f;
			}
			//fix camera angle
			float tmpdir = 0.0f;
			if (tohdir >= topdir) {
				if ((tohdir - topdir) < 180) {
					tmpdir = tohdir - topdir;
					tdir = topdir + ((tmpdir / 3) * 2);
				} else {
//					#if UNITY_EDITOR
//					Debug.Log (" reverse 1 ");
//					#endif
					tmpdir = (360 - tohdir) + topdir;
					tdir = tohdir + ((tmpdir / 3) * 2);
				}
			} else {
				if ((topdir - tohdir) < 180) {
					tmpdir = topdir - tohdir;
					tdir = tohdir + (tmpdir / 3);
				} else {
//					#if UNITY_EDITOR
//					Debug.Log (" reverse 2 ");
//					#endif
					tmpdir = (360 - topdir) + tohdir;
					tdir = topdir + ((tmpdir / 3) * 2);
				}
			}
			if (tdir > 360) {
				tdir = tdir - 360;
			}
			//first dir set
			if (firstdir == true) {
				firstdir = false;
				cdir = tdir;
			}
			//angle current -> target
			const float dirstep = 10.0f;
			if (this.hsmove == false) {
				if ((tdir > cdir) && ((tdir - cdir) > dirstep)) {
					if ((tdir - cdir) < 180) {
						cdir = cdir + dirstep;
					} else {
						cdir = cdir - dirstep;
					}
				} else if ((tdir < cdir) && ((cdir - tdir) > dirstep)) {
					if ((cdir - tdir) < 180) {
						cdir = cdir - dirstep;
					} else {
						cdir = cdir + dirstep;
					}
				} else {
					cdir = tdir;
				}
				if (cdir > 360) {
					cdir = cdir - 360;
				}
				if (cdir < 0) {
					cdir = cdir + 360;
				}
			}
			//adjust rotate
			float cmdir = (360-cdir)+90;
			//set camera dir rotate
			cashTransform.rotation = Quaternion.Euler (new Vector3 (6.0f, cmdir, 0.0f));
			break;
		case movplayerko:
			//player ko
			//player ko camera mov
			//direction to player from hiari
			//get to player dir
			hpos = hac.getHiariPos ();
			ppos = plc.getPlayerPos ();
			xdistance = (hpos.x) - (ppos.x);	//hiari,player x distance
			zdistance = (hpos.z) - (ppos.z);	//hiari,player z distance
			if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
			camdir = direction;
			if (camdir < 0) {
				camdir = camdir + 360.0f;
			}
			//target pos dir set
			postdir = camdir;
			poscdir = postdir - 80 + (((float)movcnt) * 0.5f);
			movcnt++;
			if (movcnt >= 240) {
				pos = cashTransform.position;
				pos.y = 1.1f;
				cashTransform.position = pos;

				firstposdir = true;
				firstdir = true;

				movseq = movbattle;
				movcnt = 0;
			} else {
				//fix distance camera pos to player pos
				//camera pos fix (from player)
				nx = Mathf.Cos (poscdir * Mathf.Deg2Rad) * (3.8f - (((float)movcnt) * 0.014f));	//distance player
				nz = Mathf.Sin (poscdir * Mathf.Deg2Rad) * (3.8f - (((float)movcnt) * 0.014f));	//distance player
				pos = cashTransform.position;
				pos.x = ppos.x + nx;
				pos.y = ppos.y + 0.7f;
				pos.z = ppos.z + nz;
				cashTransform.position = pos;

				//player ko camera angle
				//direction to player
				xdistance = (ppos.x) - (cashTransform.position.x);	//camera,hiari x distance
				zdistance = (ppos.z) - (cashTransform.position.z);	//camera,hiari z distance
				if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
					xdistance = 0.0001f;
				}
				direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
				topdir = direction;
				if (topdir < 0) {
					topdir = topdir + 360.0f;
				}
				//fix camera angle
				//angle current -> target
				cdir = topdir;
				//adjust rotate
				cmdir = (360 - cdir) + 90;
				//set camera dir rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (20.0f, cmdir, 0.0f));
			}
			break;
		case movhiariko:
			//hiari ko
			//hiari ko camera mov
			//direction to hiari from player
			//get to hiari dir
			ppos = plc.getPlayerPos ();
			hpos = hac.getHiariPos ();
			xdistance = (ppos.x) - (hpos.x);	//player,hiari x distance
			zdistance = (ppos.z) - (hpos.z);	//player,hiari z distance
			if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
				xdistance = 0.0001f;
			}
			direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
			camdir = direction;
			if (camdir < 0) {
				camdir = camdir + 360.0f;
			}
			//target pos dir set
			postdir = camdir;
			poscdir = postdir - 80 + (((float)movcnt) * 1.0f);
			movcnt++;
			if (movcnt >= 160) {
				pos = cashTransform.position;
				pos.y = 1.1f;
				cashTransform.position = pos;

				firstposdir = true;
				firstdir = true;

				movseq = movbattle;
				movcnt = 0;
			} else {
				//fix distance camera pos to hiari pos
				//camera pos fix (from hiari)
				nx = Mathf.Cos (poscdir * Mathf.Deg2Rad) * (7.0f - (((float)movcnt) * 0.02f));	//distance hiari
				nz = Mathf.Sin (poscdir * Mathf.Deg2Rad) * (7.0f - (((float)movcnt) * 0.02f));	//distance hiari
				pos = cashTransform.position;
				pos.x = hpos.x + nx;
				pos.y = hpos.y + 1.9f;
				pos.z = hpos.z + nz;
				cashTransform.position = pos;

				//hiari ko camera angle
				//direction to hiari
				xdistance = (hpos.x) - (cashTransform.position.x);	//camera,hiari x distance
				zdistance = (hpos.z) - (cashTransform.position.z);	//camera,hiari z distance
				if ((xdistance == 0) && (zdistance == 0)) {	//for zero exception
					xdistance = 0.0001f;
				}
				direction = Mathf.Atan2 (zdistance, xdistance) * Mathf.Rad2Deg;	//distance -> direction
				tohdir = direction;
				if (tohdir < 0) {
					tohdir = tohdir + 360.0f;
				}
				//fix camera angle
				//angle current -> target
				cdir = tohdir;
				//adjust rotate
				cmdir = (360 - cdir) + 90;
				//set camera dir rotate
				cashTransform.rotation = Quaternion.Euler (new Vector3 (22.0f, cmdir, 0.0f));
			}
			break;
		default:
			break;
		}
	}


	//public

	//set hiari ko
	public void setHiariKo(){
		//hiari ko camera mode set
		movseq = movhiariko;
		movcnt = 0;
	}

	//set hiari ko
	public void setPlayerKo(){
		//player ko camera mode set
		movseq = movplayerko;
		movcnt = 0;
	}

	//set hiari side move (for camera move)
	public void setHiariSideMove( bool hsmove ){
		this.hsmove = hsmove;
	}

}
