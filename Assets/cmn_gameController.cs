using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cmn_gameController : MonoBehaviour {

	//public

	//system public
	//system const
	public bool sw_fpsDisp = true;

	//private
	//system local
	int intervalCnt;	//interval count

	//component cash
	GameObject fpsDisp;
	Text fpsDispText;

	//local
	//for fps count
	float delta;
	float fpscnt;

	//Awake
	void Awake() {
		QualitySettings.vSyncCount = 2; //vsync
		Application.targetFrameRate = 30;	//framerate

		fpscnt = 0.0f;	//fps cnt init
	}

	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//none

		//disable multitouch
		Input.multiTouchEnabled = false;

		//ftp disp init
		fpsDisp = GameObject.Find ("fpsDisp");
		fpsDispText = fpsDisp.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		////always process

		//fps cnt disp
		if (sw_fpsDisp == true) {
			fpscnt++;
			delta = delta + Time.deltaTime;
			if (delta >= 1.0f) {
				delta = 0.0f;
				//fps
				fpsDispText.text = "fps:" + fpscnt.ToString("F1");	//todo:結果整数だけだがひとまずF
				fpscnt = 0;
			}
		} else {
			//no fps disp
			//fpsDispText.text = "";
		}

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 1) {
			intervalCnt = 0;
			//nop
		}
	}

}
