using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class btl_displayController : MonoBehaviour {

	//public

	//private
	//const
	const int modewakeup = 0;
	const int modefight = 1;

	//local
	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	btl_mainController mc;
	//score
	GameObject score;
	Text scoretxt;
	GameObject score_s;
	Text scoretxt_s;
	//sub message
	GameObject subMsg;
	Text cashText_subMsg;
	GameObject subMsg_s;
	Text cashText_subMsg_s;
	//hiari name
	GameObject hName;
	Text hNameTxt;
	GameObject hName_s;
	Text hNameTxt_s;
	//hiari hp gauge
	GameObject hhpc;
	RectTransform cashRectTransform_hhpc;
	GameObject hhp;
	RectTransform cashRectTransform_hhp;
	//player hp gauge
	GameObject phpc;
	RectTransform cashRectTransform_phpc;
	GameObject php;
	RectTransform cashRectTransform_php;
	//hiari level
	GameObject hLevelDisp;
	Text hLevelDispTxt;
	GameObject hLevelDisp_s;
	Text hLevelDispTxt_s;
	//tame gauge
	GameObject tgBasef;
	GameObject tgBase;
	GameObject tg;
	RectTransform cashRectTransform_tgBasef;
	RectTransform cashRectTransform_tg;
	CanvasRenderer cashCanvasRenderer_tg;
	//earth
	GameObject earth;
	GameObject earthExp;
	RectTransform cashRectTransform_earth;
	RectTransform cashRectTransform_earthExp;
	//pause disp
	GameObject pauseDisp;
	GameObject pauseDisp_s;

	//system local
	int intervalCnt;	//interval count

	//local
	//display mode
	int dpmode;

	//hirari hp gauge
	float hHpMax;
	float hHpCurrent;
	float hHp;

	//player hp gauge
	float pHpMax;
	float pHpCurrent;
	float pHp;

	//tama gauge
	bool tameGaugeDisp;
	int tameGaugeMaxCnt;
	int tameGaugeCnt;
	int tameGaugeColCnt;

	//earth explosion
	bool earthExpMode;
	int earthExpCnt;

	//pause disp
	bool pause;
	int pauseCnt;


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

		//score
		score = GameObject.Find("scoreDisp");
		score_s = GameObject.Find("scoreDisp_shadow");
		scoretxt = score.GetComponent<Text> ();
		scoretxt_s = score_s.GetComponent<Text> ();

		//sub message
		subMsg = GameObject.Find("subMessage");
		subMsg_s = GameObject.Find("subMessage_shadow");
		cashText_subMsg = subMsg.GetComponent<Text> ();
		cashText_subMsg_s = subMsg_s.GetComponent<Text> ();

		//hiari name
		hName = GameObject.Find("hName");
		hNameTxt = hName.GetComponent<Text> ();;
		hName_s = GameObject.Find("hName_shadow");
		hNameTxt_s = hName_s.GetComponent<Text> ();;

		//hiaru hp gauge
		hhpc = GameObject.Find("hhpGauge_back");
		hhp = GameObject.Find("hhpGauge");
		cashRectTransform_hhpc = hhpc.GetComponent<RectTransform> ();
		cashRectTransform_hhp = hhp.GetComponent<RectTransform> ();

		//player hp gauge
		phpc = GameObject.Find("phpGauge_back");
		php = GameObject.Find("phpGauge");
		cashRectTransform_phpc = phpc.GetComponent<RectTransform> ();
		cashRectTransform_php = php.GetComponent<RectTransform> ();

		//hiari level
		hLevelDisp = GameObject.Find("hLevelDisp");
		hLevelDispTxt =  hLevelDisp.GetComponent<Text> ();
		hLevelDisp_s = GameObject.Find("hLevelDisp_shadow");
		hLevelDispTxt_s =  hLevelDisp_s.GetComponent<Text> ();

		//tame gauge img
		tgBasef = GameObject.Find ("tameGaugeBaseFrame");
		tgBase = GameObject.Find ("tameGaugeBase");
		tg = GameObject.Find ("tameGauge");
		cashRectTransform_tgBasef = tgBasef.GetComponent<RectTransform> ();
		cashRectTransform_tg = tg.GetComponent<RectTransform> ();
		cashCanvasRenderer_tg = tg.GetComponent<CanvasRenderer> ();

		//earth
		earth = GameObject.Find ("btl_earth");
		earthExp = GameObject.Find ("btl_earthExplosion");
		cashRectTransform_earth = earth.GetComponent<RectTransform> ();
		cashRectTransform_earthExp = earthExp.GetComponent<RectTransform> ();

		//pause disp
		pauseDisp = GameObject.Find ("btl_pauseDisp");
		pauseDisp_s = GameObject.Find ("btl_pauseDisp_shadow");
		pauseDisp.SetActive (false);
		pauseDisp_s.SetActive (false);


		//local

		//display mode
		dpmode = modewakeup;

		//hiari hp gauge
		hHpMax = 100;	//(for zero exception)
		hHpCurrent = 0;
		hHp = 0;

		//player hp gauge
		pHpMax = 100;	//(for zero exception)
		pHpCurrent = 0;
		pHp = 0;

		//tame gauge
		tameGaugeDisp = false;
		tameGaugeMaxCnt = 1;	//(for zero exception)
		tameGaugeCnt = 0;
		tameGaugeColCnt = 0;

		//earth explosion
		earthExpMode = false;
		earthExpCnt = 0;

		//pause disp
		pause = false;
		pauseCnt = 0;

	}
	

	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {
		////always process

		//// no wait and pause process

		//pause process
		pauseProcess();

		//hiari hp gauge process
		hiariHpGaugeProcess();

		//player hp gauge process
		playerHpGaugeProcess();


		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}

		//wait and pause process


		//tame gauge process
		tameGaugeProcess();

		//earth explosoion process
		earthExplosionProcess();


		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}
	}

	//private

	//pause disp
	private void pauseProcess(){
		if (pause == true) {
			if (pauseCnt % 20 == 0) {
				pauseDisp.SetActive (true);
				pauseDisp_s.SetActive (true);
			} else if (pauseCnt % 20 == 10) {
				pauseDisp.SetActive (false);
				pauseDisp_s.SetActive (false);
			}
			pauseCnt++;
		}
	}

	//hiari hp gauge
	private void hiariHpGaugeProcess(){
		//wake up?
		if (dpmode == modewakeup) {
			return;
		}
		//hp update
		float tmphp = hHp / hHpMax;
		cashRectTransform_hhp.localScale = new Vector3( tmphp, 1.0f, 1.0f );
		//hp back update
		if (hHpCurrent > hHp) {
			hHpCurrent = hHpCurrent - ((0.4f/200.0f)*hHpMax);
			if (hHpCurrent <= hHp) {
				hHpCurrent = hHp;
			}
			tmphp = hHpCurrent / hHpMax;
			cashRectTransform_hhpc.localScale = new Vector3 (tmphp, 1.0f, 1.0f);
		} else if (hHpCurrent < hHp) {
			//for start
			hHpCurrent = hHp;
		}
	}

	//player hp gauge
	private void playerHpGaugeProcess(){
		//wake up?
		if (dpmode == modewakeup) {
			return;
		}
		//hp update
		float tmphp = pHp / pHpMax;
		cashRectTransform_php.localScale = new Vector3( tmphp, 1.0f, 1.0f );
		//hp back update
		if (pHpCurrent > pHp) {
			pHpCurrent = pHpCurrent - 0.4f;
			if (pHpCurrent <= pHp) {
				pHpCurrent = pHp;
			}
			tmphp = pHpCurrent / pHpMax;
			cashRectTransform_phpc.localScale = new Vector3 (tmphp, 1.0f, 1.0f);
		} else if (pHpCurrent < pHp) {
			//for start
			pHpCurrent = pHp;
		}
	}

	//tame gauge process
	private void tameGaugeProcess(){
		if( tameGaugeDisp == true ){
			//scale
			float tmpmax = (float)tameGaugeMaxCnt;
			float tmpcnt = (float)tameGaugeCnt;
			float tmpval = (tmpcnt / tmpmax);
			if (tmpval >= 1.0f) {
				tmpval = 1.0f;
			}
			cashRectTransform_tg.localScale = new Vector3 (tmpval, 1.0f, 1.0f);
			//color
			if (tameGaugeCnt >= tameGaugeMaxCnt) {
				if (tameGaugeColCnt % 3 == 0) {
					cashCanvasRenderer_tg.SetColor (new Color (255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f));	//wt
				} else if (tameGaugeColCnt % 3 == 1){
					cashCanvasRenderer_tg.SetColor (new Color (255.0f / 255.0f, 0.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f));	//rd
//				} else if (tameGaugeColCnt % 4 == 2){
//					cashCanvasRenderer_tg.SetColor (new Color (236.0f / 255.0f, 210.0f / 255.0f, 231.0f / 255.0f, 255.0f / 255.0f));	//pink
				} else if (tameGaugeColCnt % 3 == 2){
					cashCanvasRenderer_tg.SetColor (new Color (255.0f / 255.0f, 255.0f / 255.0f, 0.0f / 255.0f, 255.0f / 255.0f));	//ye
				}
				tameGaugeColCnt++;
			}
		}
	}

	//earth explosion process
	private void earthExplosionProcess(){
		if (earthExpMode == true) {
			//sound
			if (earthExpCnt % 32 == 0) {
				//sound
				if (mc.getGameMode () == 4) {
					cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_hiariexp_s);
				}
			}
			//explosion
			Vector3 tmpscl =  cashRectTransform_earthExp.localScale;
			if (earthExpCnt <= 40) {
				tmpscl.x = tmpscl.x + 0.01f;
				tmpscl.y = tmpscl.y + 0.01f;
			} else {
				if (earthExpCnt <= 80) {
					tmpscl.x = tmpscl.x - 0.01f;
					tmpscl.y = tmpscl.y - 0.01f;
				}
			}
			cashRectTransform_earthExp.localScale = tmpscl;
			earthExpCnt++;
		}
	}



	//public

	//update score
	public void updateScore(){
		scoretxt.text = "SCORE " + cmn_staticData.Instance.score.ToString("D8");
		scoretxt_s.text = "SCORE " + cmn_staticData.Instance.score.ToString("D8");
	}

	//pause on
	public void pauseOn(){
		pause = true;
		pauseCnt = 0;
	}

	public void pauseOff(){
		pause = false;
		pauseDisp.SetActive (false);
		pauseDisp_s.SetActive (false);
	}

	//term wake up
	public void termWakeup(){
		//mode
		dpmode = modefight;
	}

	//set sub message
	public void setSubmessage( string str ){
		cashText_subMsg.text = str;
		cashText_subMsg_s.text = str;
	}

	//set hiari name
	public void setHiariName( int level ){
		string tmp = "";
		tmp = cmn_staticData.Instance.hiariName [level, (Random.Range (0, 3))];
		this.hNameTxt.text = tmp;
		this.hNameTxt_s.text = tmp;
	}

	//set hiari hp gauge
	public void setHiariHpGauge( int initHp, int hp ){
		this.hHpMax = (float)initHp;
		this.hHp = (float)hp;
	}

	//set player hp gauge
	public void setPlayerHpGauge( int initHp, int hp ){
		this.pHpMax = (float)initHp;
		this.pHp = (float)hp;
	}

	//set hiari level
	public void setHiariLevel( int level ){
		//hiari level disp
		hLevelDispTxt.text = "LEVEL:"+((level+1).ToString());
		hLevelDispTxt_s.text = "LEVEL:"+((level+1).ToString());
	}

	//tame gauge display on
	public void tameGaugeOn( Vector3 wpos ){
		//parent (canvas)
		GameObject parent = GameObject.Find ("Canvas");
		float srx = parent.GetComponent<CanvasScaler>().referenceResolution.x;
		float sry = parent.GetComponent<CanvasScaler>().referenceResolution.y;
		//pos convert
		Vector3 spos = Camera.main.WorldToScreenPoint( wpos );
		spos.x = spos.x/Camera.main.pixelWidth * srx;
		spos.y = spos.y/Camera.main.pixelHeight * sry;
		spos.x = spos.x - (srx/2);
		spos.y = spos.y - (sry/2);
//		Vector3 tmppos = cashRectTransform_tgBasef.localPosition;
//		tmppos.y = -100;
		cashRectTransform_tgBasef.localPosition = spos;
		//val
		tameGaugeDisp = true;
		tameGaugeMaxCnt = 1;	//(for zero exception)
		tameGaugeCnt = 0;
		//color
		cashCanvasRenderer_tg.SetColor (new Color (225.0f / 255.0f, 149.0f / 255.0f, 210.0f / 255.0f, 255.0f / 255.0f));
	}

	//tame gauge display off
	public void tameGaugeOff(){
		//pos
		Vector3 tmppos = cashRectTransform_tgBasef.localPosition;
		tmppos.y = -300;
		cashRectTransform_tgBasef.localPosition = tmppos;
		//val
		tameGaugeDisp = false;
		tameGaugeMaxCnt = 1;	//(for zero exception)
		tameGaugeCnt = 0;
		//color
//		cashCanvasRenderer_tg.SetColor (new Color (225.0f / 255.0f, 149.0f / 255.0f, 210.0f / 255.0f, 255.0f / 255.0f));
	}

	//tame gauge display update
	public void tameGaugeUpdate( int maxcnt, int cnt ){
		this.tameGaugeMaxCnt = maxcnt;
		this.tameGaugeCnt = cnt;
	}

	//set earth explosion
	public void setEarthExplosion(){
		//mode
		earthExpMode = true;
		earthExpCnt = 0;
		//display
		Vector3 tmppos;
		//earth
		tmppos = cashRectTransform_earth.localPosition;
		tmppos.y = tmppos.y - 600;
		cashRectTransform_earth.localPosition = tmppos;
		//explosion
		tmppos = cashRectTransform_earthExp.localPosition;
		tmppos.y = tmppos.y - 600;
		cashRectTransform_earthExp.localPosition = tmppos;
		tmppos = cashRectTransform_earthExp.localScale;
		tmppos.x = 0.05f;
		tmppos.y = 0.05f;
		cashRectTransform_earthExp.localScale = tmppos;
	}

	//term earth explosion
	public void termEarthExplosion(){
		//mode
		earthExpMode = false;
		earthExpCnt = 0;
		//display
		Vector3 tmppos;
		//earth
		tmppos = cashRectTransform_earth.localPosition;
		tmppos.y = tmppos.y + 600;
		cashRectTransform_earth.localPosition = tmppos;
		//explosion
		tmppos = cashRectTransform_earthExp.localPosition;
		tmppos.y = tmppos.y + 600;
		cashRectTransform_earthExp.localPosition = tmppos;
	}


}
