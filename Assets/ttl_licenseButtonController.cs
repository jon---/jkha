using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttl_licenseButtonController : MonoBehaviour {
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

	//license button down
	public void OnLicenseButtonDown(){
		//license button process
		mc.tapLicenseButton();
	}

}
