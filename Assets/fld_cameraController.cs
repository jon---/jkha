using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fld_cameraController : MonoBehaviour {
	//public
	//

	//private
	//local const
	//move seq const
	const int movidle = 0;
	const int movright = 1;
	const int movleft = 2;
	//mov speed
	const float movdd = 1.0f;

	//camera distance
	const float cdzoomup = 20.0f;
	const float cdzoomupencount = 34.0f;
	const float cdzoomout = 52.0f;
	const float cdd = 0.5f;

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	fld_mainController mc;
	GameObject playerCtr;
	fld_playerController plc;
	GameObject hiariCtr;
	fld_hiariController hac;

	//local

	//move
	float ccdist;	//camera distance current
	float tcdist;	//camera distance target

	//move seq
	int movseq;
	int movcnt;
	float movdir;
	float rtcnt;
	float postdir;	//target camera pos dir
	float poscdir;	//current camera pos dir
	bool firstposdir;
	float tdir;	//target camera dir(y)
	float cdir;	//current camera dir(y)
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
		mainCtr = GameObject.Find ("fld_mainController");
		mc = mainCtr.GetComponent<fld_mainController> ();

		//playercontroller
		playerCtr = GameObject.Find ("fld_playerController");
		plc = playerCtr.GetComponent<fld_playerController> ();

		//hiaricontroller
//		hiariCtr = GameObject.Find ("fld_hiariController");
//		hac = hiariCtr.GetComponent<fld_hiariController> ();

		//move
		tcdist = cdzoomup;
		ccdist = tcdist;


		movseq = movidle;
		movcnt = 0;
		movdir = 270.0f;
		rtcnt = 0.0f;
		//posdir
		postdir = 109.0f;	//target camera pos dir
		poscdir = 109.0f;	//current camera pos dir
		firstposdir = true;
		//dir
		tdir = 280.0f;
		cdir = 280.0f;
		firstdir = true;

		//hiari info
		hsmove = false;

	}
	
	// Update is called once per frame
	void Update () {

		////always process

		//camera move

		//zoom current -> target
		if (tcdist > ccdist) {
			ccdist = ccdist + cdd;
			if (ccdist >= tcdist) {
				ccdist = tcdist;
			}
		}
		if (tcdist < ccdist) {
			ccdist = ccdist - cdd;
			if (ccdist <= tcdist) {
				ccdist = tcdist;
			}
		}


		//to player
//old
//		Vector3 ppos = plc.getPlayerPos();
//		Vector3 cpos = cashTransform.position;
//		cpos.x = ppos.x;
//		cpos.z = ppos.z - 29;
//		cashTransform.position = cpos;

		Vector3 ppos = plc.getPlayerPos();
		const float cpxdir = 127.0f;	//camera pos angle
		float cxdir = 53.0f;		//camera angle base
		//fix pos
		float cdist = ccdist;
		float cx = ppos.x;
		float cz = Mathf.Cos ((360-cpxdir) * Mathf.Deg2Rad) * 1.0f * cdist;
		float cy = Mathf.Sin (cpxdir * Mathf.Deg2Rad) * 1.0f * cdist;
		cz = ppos.z + cz;
		cy = ppos.y + cy - 3.0f;
		Vector3 cpos = cashTransform.position;
		cpos.x = cx;
		cpos.y = cy;
		cpos.z = cz;
		cashTransform.position = cpos;
		//fix angle
		cxdir = cxdir + (cdist-cdzoomout);	//adjust at zoom
		cashTransform.rotation = Quaternion.Euler (new Vector3 (cxdir, 0.0f, 0.0f));


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
	public void zoomUp(){
		this.tcdist = cdzoomup;
	}

	public void zoomUpEncount(){
		this.tcdist = cdzoomupencount;
	}

	public void zoomOut(){
		this.tcdist = cdzoomout;
	}


}
