using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fld_displayController : MonoBehaviour {

	//public

	//private
	//const

	//local
	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	fld_mainController mc;
	//score
	GameObject score;
	Text scoretxt;
	GameObject score_s;
	Text scoretxt_s;
	//status
	GameObject statusMsg;
	Text cashText_statusMsg;
	GameObject statusMsg_s;
	Text cashText_statusMsg_s;
	//sub message
	GameObject subMsg;
	Text cashText_subMsg;
	GameObject subMsg_s;
	Text cashText_subMsg_s;
	//alert message
	GameObject altMsg;
	Text cashText_altMsg;
	GameObject altMsg_s;
	Text cashText_altMsg_s;
	//pokebell image
	GameObject pokeImg;

	//system local
	int intervalCnt;	//interval count

	//local
	//pokebell blink
	bool pblink;
	int pblinkCnt;

	//sub message disp time
	int submsgtime;


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

		//score
		score = GameObject.Find("scoreDisp");
		score_s = GameObject.Find("scoreDisp_shadow");
		scoretxt = score.GetComponent<Text> ();
		scoretxt_s = score_s.GetComponent<Text> ();

		//status message
		statusMsg = GameObject.Find("fld_status");
		statusMsg_s = GameObject.Find("fld_status_shadow");
		cashText_statusMsg = statusMsg.GetComponent<Text> ();
		cashText_statusMsg_s = statusMsg_s.GetComponent<Text> ();

		//sub message
		subMsg = GameObject.Find("subMessage");
		subMsg_s = GameObject.Find("subMessage_shadow");
		cashText_subMsg = subMsg.GetComponent<Text> ();
		cashText_subMsg_s = subMsg_s.GetComponent<Text> ();

		//sub message
		altMsg = GameObject.Find("alertMessage");
		altMsg_s = GameObject.Find("alertMessage_shadow");
		cashText_altMsg = altMsg.GetComponent<Text> ();
		cashText_altMsg_s = altMsg_s.GetComponent<Text> ();

		//pokebell img
		pokeImg = GameObject.Find("fld_pokeimg");
		pokeImg.SetActive (false);

		//pokebell blink count
		pblink = false;
		pblinkCnt = 0;

		//sub message disp time
		submsgtime = 0;

		//field and battle data init
		cmn_staticData.Instance.fld_initCmnData();
		//field data init
		cmn_staticData.Instance.timebonus = 10000;

		//set status message
		this.setStatusMessage();

	}


	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {
		////always process

		//// no wait and pause process


		//poke bell image blink process
		this.pokeBlinkProcess ();

		//sub message time clear
		this.subMessageClear ();

		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}

		//// wait and pause process

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

	//set pokebell blink
	private void pokeBlinkProcess(){
		if (pblink == true) {
			if ((pblinkCnt % 10) == 0) {
				pokeImg.SetActive (true);
			} else if ((pblinkCnt % 10) == 7) {
				pokeImg.SetActive (false);
			}
			pblinkCnt++;
		}
	}

	//sub message clear process
	private void subMessageClear(){
		if (submsgtime > 0) {
			submsgtime--;
			if (submsgtime <= 0) {
				submsgtime = 0;
				cashText_subMsg.text = "";
				cashText_subMsg_s.text = "";
			}
		}
	}

	//set status message
	private void setStatusMessage(){
		string tmpstr = "";
		tmpstr = "タイムボーナス:" + cmn_staticData.Instance.timebonus.ToString() + "\n";
		tmpstr += "HP +" + cmn_staticData.Instance.hpplus.ToString () + " ";
		tmpstr += "攻撃力 +" + cmn_staticData.Instance.atackplus.ToString () + " ";
		tmpstr += "防御力 +" + cmn_staticData.Instance.defenceplus.ToString () + "\n";
		tmpstr += "貯金:" + cmn_staticData.Instance.money.ToString () + "万円\n";
		tmpstr += "時給:" + cmn_staticData.Instance.jikyu.ToString () + "万円\n";
		tmpstr += "ヒアリ生け捕り数:" + cmn_staticData.Instance.hiariwin.ToString() + "匹(失敗:" + cmn_staticData.Instance.hiarilose + "匹)\n";
		int tmp = cmn_staticData.Instance.targetmoney - cmn_staticData.Instance.money;
		if (tmp <= 0) {
			tmp = 0;
		}
		tmpstr += "スマフォ購入まであと:" + tmp.ToString() + "万円";
		cashText_statusMsg.text = tmpstr;
		cashText_statusMsg_s.text = cashText_statusMsg.text;
	}


	//public

	//update score
	public void updateScore(){
		scoretxt.text = "SCORE " + cmn_staticData.Instance.score.ToString("D8");
		scoretxt_s.text = "SCORE " + cmn_staticData.Instance.score.ToString("D8");
	}

	//set sub message
	public void setSubmessage( string str, int time = 0 ){
		//message 
		cashText_subMsg.text = str;
		cashText_subMsg_s.text = str;
		//clear time
		submsgtime = time;
	}

	//set sub message clear time disable
	public void setSubmessageTimeReset(){
		//clear time
		submsgtime = 0;
	}

	//set alert message
	public void setAlertmessage( string str ){
		cashText_altMsg.text = str;
		cashText_altMsg_s.text = str;
	}

	//set pokebell on
	public void setPokebellOn(){
		pblink = true;
//		pblinkCnt = 0;
//		pokeImg.SetActive (true);
	}

	//set pokebell on
	public void setPokebellOff(){
		pblink = false;
		pblinkCnt = 0;
		pokeImg.SetActive (false);
	}

	//set status message
	public void setStatus(){
		this.setStatusMessage ();
	}

}
