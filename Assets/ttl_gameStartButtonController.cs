using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttl_gameStartButtonController : MonoBehaviour {
	//public

	//private
	//component cash
	GameObject mainCtr;
	ttl_mainController mc;

	// Use this for initialization
	void Start () {
		//cash
		//maincontroller
		mainCtr = GameObject.Find ("ttl_mainController");
		mc = mainCtr.GetComponent<ttl_mainController> ();

	}

	// Update is called once per frame
	void Update () {
		//nop		
	}

	//game start button down
	public void OnGameStartButtonDown(){
		//gmae start button process
		mc.tapGameStartButton();
	}

}
