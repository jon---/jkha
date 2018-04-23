using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class rst_displayController : MonoBehaviour {

	//public

	//private
	//const
	//desctiption message table
	string[] descriptionStringTable = new string[]{
		"",
	};

	//local
	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	rst_mainController mc;
	//score
	GameObject score;
	Text scoretxt;
	GameObject score_s;
	Text scoretxt_s;
	//title
	GameObject rsttitle;
	Text cashText_rsttitle;
	GameObject rsttitle_s;
	Text cashText_rsttitle_s;
	//main message
	GameObject rsttxtMsg;
	Text cashText_rsttxtMsg;
	GameObject rsttxtMsg_s;
	Text cashText_rsttxtMsg_s;
	//sub message
	GameObject subMsg;
	Text cashText_subMsg;
	GameObject subMsg_s;
	Text cashText_subMsg_s;

	//system local
	int intervalCnt;	//interval count

	//local
	//title description
	int dscIndex;
	bool dscDisplay;
	string dscStr;
	int dscStrDispCnt;
	int dscStrDispIntCnt;
	int dscWait;
	int dscWaitCnt;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//maincontroller
		mainCtr = GameObject.Find ("rst_mainController");
		mc = mainCtr.GetComponent<rst_mainController> ();

		//score
		score = GameObject.Find("scoreDisp");
		score_s = GameObject.Find("scoreDisp_shadow");
		scoretxt = score.GetComponent<Text> ();
		scoretxt_s = score_s.GetComponent<Text> ();

		//title txt
		rsttitle = GameObject.Find("rst_title");
		rsttitle_s = GameObject.Find("rst_title_shadow");
		cashText_rsttitle = rsttitle.GetComponent<Text> ();
		cashText_rsttitle_s = rsttitle_s.GetComponent<Text> ();

		//result txt
		rsttxtMsg = GameObject.Find("rst_resulttxt");
		rsttxtMsg_s = GameObject.Find("rst_resulttxt_shadow");
		cashText_rsttxtMsg = rsttxtMsg.GetComponent<Text> ();
		cashText_rsttxtMsg_s = rsttxtMsg_s.GetComponent<Text> ();

		//sub message
		subMsg = GameObject.Find("rst_subMessage");
		subMsg_s = GameObject.Find("rst_subMessage_shadow");
		cashText_subMsg = subMsg.GetComponent<Text> ();
		cashText_subMsg_s = subMsg_s.GetComponent<Text> ();

		//local
		//title description
		dscIndex = 0;
		dscDisplay = false;
		dscStr = "";
		dscStrDispCnt = 0;
		dscStrDispIntCnt = 0;
		dscWait = 0;
		dscWaitCnt = 0;

	}


	float cnt = 0.0f;	//time scale cnt
	// Update is called once per frame
	void Update () {

		////always process

		//title description display process
		this.dscDispProcess();


		//wait and pause
		cnt = cnt + Time.timeScale;
		if (cnt < 1.0f) {
			return;
		} else {
			cnt = cnt - 1.0f;
		}

		//// no wait and pause process


		////interval process

		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;
			//nop
		}
	}

	//private


	//title description display process
	private void dscDispProcess(){
		if (dscDisplay == true) {	//description display?
			if (dscWaitCnt >= dscWait) {
				dscWaitCnt = 0;
				if (dscStrDispCnt <= dscStr.Length) {
					//string slide
					string st = "";
					dscStrDispIntCnt++;
					if (dscStrDispIntCnt >= 1) {
						dscStrDispIntCnt = 0;
						st = dscStr.Substring (0, dscStrDispCnt);
						if (dscStrDispCnt <= (dscStr.Length - 1)) {
							st = st + "■";
						}
						for (int i = 0; i <= ((dscStr.Length) - dscStrDispCnt - 2); i++) {
							st = st + " ";
						}
						cashText_rsttxtMsg.text = st;
						cashText_rsttxtMsg_s.text = st;
						dscStrDispCnt = dscStrDispCnt + 1;
						if (dscStrDispCnt >= dscStr.Length) {
							dscStrDispCnt = dscStr.Length + 1;
						} else {
							//fix wait
							string tmpstr = "";
							if (dscStrDispCnt >= 2) {
								tmpstr = dscStr.Substring (dscStrDispCnt - 2, 1);
							}
							if (tmpstr == "、") {
								dscWait = 3;
							} else if (tmpstr == "。") {
								dscWait = 5;
							} else if (tmpstr == "!") {
								dscWait = 5;
							} else if (tmpstr == "\u3000") {
								dscWait = 12;
							} else if (tmpstr == "・") {
								dscWait = 5;
							} else if (tmpstr == "\n") {
								dscWait = 10;
							} else {
								dscWait = 1;
							}
						}
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_ds100);
					}
				} else {
					//string
					cashText_rsttxtMsg.text = dscStr;
					cashText_rsttxtMsg_s.text = dscStr;
					dscDisplay = false;
					//term
					this.termDescriptionText();
				}
			} else {
				dscWaitCnt++;
			}
		}
	}

	//select title description next process
//	private void setNextDescription(){
//		dscIndex++;
//		if (dscIndex >= (descriptionStringTable.Length)) {
//			dscIndex = 0;
//		}
//		this.setDescriptionText ();
//	}

	//generate description(result) text
	private void generateDescriptionText(){
		descriptionStringTable [0] = "●";

		//bobus
		descriptionStringTable [0] += "攻撃パーフェクトボーナス\n";
		if (cmn_staticData.Instance.atackbonus == true) {
			descriptionStringTable [0] += "30万円!\n\n";
		} else {
			descriptionStringTable [0] += "なし\n\n";
		}
		descriptionStringTable [0] += "●防御パーフェクトボーナス\n";
		if (cmn_staticData.Instance.deffencebonus == true) {
			descriptionStringTable [0] += "30万円!\n\n";
		} else {
			descriptionStringTable [0] += "なし\n\n";
		}


		//money
		if (cmn_staticData.Instance.lastresult == true) {
			descriptionStringTable [0] += "●" + (cmn_staticData.Instance.lastjikyu.ToString ()) + "万円稼いだ!";
		} else {
			descriptionStringTable [0] += "●時給をもらえなかった...";
		}
		descriptionStringTable [0] += "\n●現在の貯金\n" + (cmn_staticData.Instance.money.ToString()) + "万円\n\n●スマフォ購入まで、あと\n";
		int tmp = (cmn_staticData.Instance.targetmoney - cmn_staticData.Instance.money);
		if (tmp <= 0) {
			tmp = 0;
		}
		descriptionStringTable [0] += (tmp.ToString ()) + "万円!\n\n";

		//jikyu
		if (cmn_staticData.Instance.lastresult == true) {
			descriptionStringTable [0] += "●ヒアリ生け捕り成功したので\n時給 100万円アップ!\n●現在の時給\n";
			descriptionStringTable [0] += (cmn_staticData.Instance.jikyu.ToString()) + "万円";
		} else {
			descriptionStringTable [0] += "●ヒアリ生け捕り失敗したので\n時給 50万円ダウン...\n●現在の時給\n";
			descriptionStringTable [0] += (cmn_staticData.Instance.jikyu.ToString()) + "万円";
		}
	}

	//term result description display
	private void termDescriptionText(){
		mc.termDescriptionText ();
	}


	//public


	//update score
	public void updateScore(){
		scoretxt.text = "SCORE " + cmn_staticData.Instance.score.ToString("D8");
		scoretxt_s.text = "SCORE " + cmn_staticData.Instance.score.ToString("D8");
	}

	//generate title text
	public void generateTitleText(){
		if (cmn_staticData.Instance.lastresult == true) {
			cashText_rsttitle.text = "結果\nヒアリ生け捕り成功!";
			cashText_rsttitle_s.text = cashText_rsttitle.text;
		} else {
			cashText_rsttitle.text = "結果\nヒアリ生け捕り失敗...";
			cashText_rsttitle_s.text = cashText_rsttitle.text;
		}
	}

	//make description(result) text
	public void makeDescriptionText(){
		this.generateDescriptionText();
	}

	//set result description display
	public void setDescriptionText(){
		dscStr = descriptionStringTable [dscIndex];
		dscStrDispCnt = 0;
		dscStrDispIntCnt = 0;
		dscDisplay = true;
		dscWait = 0;
		dscWaitCnt = 0;
	}

	//skip result description display
	public void skipDescriptionText(){
		dscStrDispCnt = dscStr.Length;
	}

	//set result description display stop 
	public void setDescriptionTextDisable(){
		dscDisplay = false;
	}

	//set sub message
	public void setSubMessage(){
		if (cmn_staticData.Instance.gameover == true) {
			cashText_subMsg.text = "時給が0になったため、\nゲームオーバー...";
			cashText_subMsg_s.text = cashText_subMsg.text;
		} else if (cmn_staticData.Instance.clear == true) {
			cashText_subMsg.text = "目標達成!!\nスマフォゲット!!!!";
			cashText_subMsg_s.text = cashText_subMsg.text;
		} else if (cmn_staticData.Instance.nextfield == true) {
			if (cmn_staticData.Instance.lastresult == true) {
				cashText_subMsg.text = "もっと稼ぐぞ!\n頑張ろう!";
				cashText_subMsg_s.text = cashText_subMsg.text;
			} else {
				cashText_subMsg.text = "次は勝とう!\n頑張ろう!";
				cashText_subMsg_s.text = cashText_subMsg.text;
			}
		}
	}

	//select description
	public void selectDescription( BaseEventData eventData ){
		if (dscDisplay == true) {
//			this.setNextDescription ();
		}
	}


}
