using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fld_antController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval counter

	//local


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

	}


	// Update is called once per frame
	void Update () {
		
		////always process

		//nop

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


}
