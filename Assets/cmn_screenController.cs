using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cmn_screenController : MonoBehaviour {
	//public

	//private
	//const
	//screen mode
	const int scrmd_idle = 0;
	const int scrmd_fadein = 1;
	const int scrmd_fadeout = 2;
	//fade in / out speed
	const float fadespd = 5.0f;

	//local
	//component cash
	GameObject mainCtr;
	ttl_mainController mcttl;
	fld_mainController mcfld;
	btl_mainController mcbtl;
	rst_mainController mcrst;
	edg_mainController mcedg;
	//screen mask
	GameObject scrMask;
//	RectTransform cashRectTransform_scrMask;
	Image cashImage_scrMask;

	//system local
	int intervalCnt;	//interval count

	//local
	//scene name
	string sceneName;

	//mode
	int scrmode;

	//fade in / out
	float fadecnt;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//maincontroller
		mcttl = null;
		mcfld = null;
		mcbtl = null;
		mcrst = null;
		mcedg = null;
		sceneName = SceneManager.GetActiveScene().name;
		if (sceneName == "titleScene") {
			mainCtr = GameObject.Find ("ttl_mainController");
			mcttl = mainCtr.GetComponent<ttl_mainController> ();
		} else if (sceneName == "fieldScene") {
			mainCtr = GameObject.Find ("fld_mainController");
			mcfld = mainCtr.GetComponent<fld_mainController> ();
		} else if (sceneName == "battleScene") {
			mainCtr = GameObject.Find ("btl_mainController");
			mcbtl = mainCtr.GetComponent<btl_mainController> ();
		} else if (sceneName == "resultScene") {
			mainCtr = GameObject.Find ("rst_mainController");
			mcrst = mainCtr.GetComponent<rst_mainController> ();
		} else if (sceneName == "endingScene") {
			mainCtr = GameObject.Find ("edg_mainController");
			mcedg = mainCtr.GetComponent<edg_mainController> ();
		}

		//screen mask
		scrMask = GameObject.Find ("screenMask");
//		cashRectTransform_scrMask = scrMask.GetComponent<RectTransform> ();
		cashImage_scrMask = scrMask.GetComponent<Image> ();

		//local

		//mode
		scrmode = scrmd_idle;

		//fade in / out
		fadecnt = 255.0f;

		//init screen color
		cashImage_scrMask.color = (new Color (0.0f, 0.0f, 0.0f, (fadecnt / 255.0f) ) );


	}
	
	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {
		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}

		////always process

		//screen process
		this.screenProcess();


		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}

	}

	//private
	//screen process
	private void screenProcess(){
		switch (scrmode) {
		case scrmd_idle:
			//idle
			//nop
			break;
		case scrmd_fadein:
			//fade in
			if (fadecnt > 0.0f) {
				fadecnt = fadecnt - fadespd;
				if (fadecnt <= 0.0f) {
					fadecnt = 0.0f;
					scrmode = scrmd_idle;
					this.termFadeIn ();
				}
				cashImage_scrMask.color = ( new Color (0.0f, 0.0f, 0.0f, (fadecnt / 255.0f) ) );
			}
			break;
		case scrmd_fadeout:
			//fade out
			if (fadecnt < 255.0f) {
				fadecnt = fadecnt + fadespd;
				if (fadecnt >= 255.0f) {
					fadecnt = 255.0f;
					scrmode = scrmd_idle;
					this.termFadeOut ();
				}
				cashImage_scrMask.color = ( new Color (0.0f, 0.0f, 0.0f, (fadecnt / 255.0f) ) );
			}
			break;
		default:
			break;
		}
	}

	//term fadein
	private void termFadeIn(){
		if (sceneName == "titleScene") {
			mcttl.termFadeIn ();
		} else if (sceneName == "fieldScene") {
			mcfld.termFadeIn ();
		} else if (sceneName == "battleScene") {
			mcbtl.termFadeIn ();
		} else if (sceneName == "resultScene") {
			mcrst.termFadeIn ();
		} else if (sceneName == "endingScene") {
			mcedg.termFadeIn ();
		}
	}

	//term fadeout
	private void termFadeOut(){
		if (sceneName == "titleScene") {
			mcttl.termFadeOut ();
		} else if (sceneName == "fieldScene") {
			mcfld.termFadeOut ();
		} else if (sceneName == "battleScene") {
			mcbtl.termFadeOut ();
		} else if (sceneName == "resultScene") {
			mcrst.termFadeOut ();
		} else if (sceneName == "endingScene") {
			mcedg.termFadeOut ();
		}
	}


	//public

	//set fadein
	public void setFadein(){
		if (fadecnt <= 0.0f) {
			this.termFadeIn ();
		} else {
			scrmode = scrmd_fadein;
		}
	}

	//set fadeout
	public void setFadeout(){
		if (fadecnt >= 255.0f) {
			this.termFadeOut ();
		} else {
			scrmode = scrmd_fadeout;
		}
	}

}
