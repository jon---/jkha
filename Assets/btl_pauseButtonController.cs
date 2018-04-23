using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btl_pauseButtonController : MonoBehaviour {
	//public

	//private
	//component cash
	GameObject mainCtr;
	btl_mainController mc;

	// Use this for initialization
	void Start () {
		//cash
		//maincontroller
		mainCtr = GameObject.Find ("btl_mainController");
		mc = mainCtr.GetComponent<btl_mainController> ();

	}

	// Update is called once per frame
	void Update () {
		//nop		
	}

	//pause button down
	public void OnPauseButtonDown(){
		//pause button process
		mc.tapPauseButton();
	}

}
