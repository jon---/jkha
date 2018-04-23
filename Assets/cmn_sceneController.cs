using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cmn_sceneController : MonoBehaviour {
	//public

	//private
	//local const

	//system local
	int intervalCnt;	//interval count

	//component cash
	//system
	GameObject waitDisp;
	RectTransform cashRectTransform_waitDisp;
	Text waitDispText;
	GameObject waitBarFrameDisp;
	RectTransform cashRectTransform_waitBarFrameDisp;
	GameObject waitBarBaseDisp;
	RectTransform cashRectTransform_waitBarBaseDisp;
	GameObject waitBarDisp;
	RectTransform cashRectTransform_waitBarDisp;

	//local
	string sceneName = "titleScene";
	string waittxt = "PLEASE WAIT";

	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//wait txt disp
		waitDisp = GameObject.Find ("waitDisp");
		cashRectTransform_waitDisp = waitDisp.GetComponent<RectTransform> ();
		waitDispText = waitDisp.GetComponent<Text> ();

		//wait bar frame disp
		waitBarFrameDisp = GameObject.Find ("waitBarDispFrame");
		cashRectTransform_waitBarFrameDisp = waitBarFrameDisp.GetComponent<RectTransform> ();

		//wait bar base disp
		waitBarBaseDisp = GameObject.Find ("waitBarDispBase");
		cashRectTransform_waitBarBaseDisp = waitBarBaseDisp.GetComponent<RectTransform> ();

		//wait bar disp
		waitBarDisp = GameObject.Find ("waitBarDisp");
		cashRectTransform_waitBarDisp = waitBarDisp.GetComponent<RectTransform> ();

		//wait count disp
		waitDispText.text = waittxt;

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

	//load game scene  
	IEnumerator loadGameScene(){
		int waitcnt = 0;
		int dotcnt = 0;
		string dottxt = "";

		//async load scene
		AsyncOperation loadWait = Application.LoadLevelAsync(sceneName);
		loadWait.allowSceneActivation = false;

		//load wait
		while (loadWait.progress < 0.9f) {	//load term(0.9f)?

			//wait text disp process
			if (waitcnt % 6 == 0) {
				dottxt = "";
				for (int i = 0; i < dotcnt; i++) {
					dottxt = dottxt + ".";
				}
				waitDispText.text = waittxt + dottxt;	//wait dot display
				dotcnt++;
				if (dotcnt >= 4) {
					dotcnt = 0;
				}
			}

			//wait bar disp process
			waitBarDisp.transform.localScale = new Vector3(loadWait.progress, 1.0f, 1.0f);

			//wait 10.0msec(実際はフレーム以上時間)
			yield return new WaitForSeconds (0.010f);

			//wait cnt
			waitcnt++;
		}

		//load term

		//wait bar 100% disp
		waitBarDisp.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

		//wait 1.0sec
		yield return new WaitForSeconds(1.0f);

		//change scene
		loadWait.allowSceneActivation = true;
	}

	//start load scene coroutine
	private void startLoadScene(){
		//text display adjust
		Vector3 tmppos;
		tmppos =  cashRectTransform_waitDisp.localPosition;
		tmppos.x = tmppos.x - 300;
		cashRectTransform_waitDisp.localPosition = tmppos;
		//progress bar frame display adjust
		tmppos =  cashRectTransform_waitBarFrameDisp.localPosition;
		tmppos.x = tmppos.x - 300;
		cashRectTransform_waitBarFrameDisp.localPosition = tmppos;
		//progress bar base display adjust
		tmppos =  cashRectTransform_waitBarBaseDisp.localPosition;
		tmppos.x = tmppos.x - 300;
		cashRectTransform_waitBarBaseDisp.localPosition = tmppos;
		//progress bar display adjust
		tmppos =  cashRectTransform_waitBarDisp.localPosition;
		tmppos.x = tmppos.x - 300;
		cashRectTransform_waitBarDisp.localPosition = tmppos;

		//load scene coroutine
		StartCoroutine ("loadGameScene"); 
	}


	//public
	public void setTitleScene(){
		//set title scene name
		sceneName = "titleScene";
		//start load scene coroutine
		this.startLoadScene();
	}

	public void setFieldScene(){
		//set field scene name
		sceneName = "fieldScene";
		//start load scene coroutine
		this.startLoadScene();
	}

	public void setBattleScene(){
		//set battle scene name
		sceneName = "battleScene";
		//start load scene coroutine
		this.startLoadScene();
	}

	public void setResultScene(){
		//set result scene name
		sceneName = "resultScene";
		//start load scene coroutine
		this.startLoadScene();
	}

	public void setEndingScene(){
		//set ending scene name
		sceneName = "endingScene";
		//start load scene coroutine
		this.startLoadScene();
	}

}
