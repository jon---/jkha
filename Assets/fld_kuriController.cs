using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fld_kuriController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//component cash
	Transform cashTransform;

	//local
	//current direction
	float cdir;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//current direction
		cdir = 180;

	}


	// Update is called once per frame
	void Update () {


		////always process


		//set rotate
		cashTransform.rotation = Quaternion.Euler (0, cdir, 0);


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
	//set rotate
	public void setKuriRotate( float dir ){
		this.cdir = dir;
	}

}
