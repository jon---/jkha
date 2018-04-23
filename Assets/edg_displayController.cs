using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class edg_displayController : MonoBehaviour {

	//public

	//private
	//const
	//desctiption message table
	string[] descriptionStringTable = new string[]{
		//																									//<--ここまで
		"私のために、\nお手伝いありがとうございました!",
		"私、念願のスマフォを\n手に入れる事ができました!",
		"少しお金が余ったので・・・",
		"今、\u3000オ\u3000マ\u3000ー\u3000ン\u3000に来ています!\n人生初の海外旅行です!",
		"そして、ヌーディストビーチで海水浴です!",
		"え?\nヌーディストビーチなのに\nなぜ水着を着てるのかって?",
		"(私の事を\n何回もタップしないでくださいね!\n絶対ですよ!)",
		"まあ、それはともかくとして。",
		"それでは、\n泳ぎに行ってきまーす!",
		"本当に\nありがとうございました!!!",
		"〜THE END〜\n\n(タップしてタイトル画面へ戻る)",
	};

	//local
	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	edg_mainController mc;
	//main message
	GameObject mainMsg;
	Text cashText_mainMsg;
	GameObject mainMsg_s;
	Text cashText_mainMsg_s;
	//mask
	GameObject mask1;
	RectTransform cashRectTransform_mask1;
	GameObject mask2;
	RectTransform cashRectTransform_mask2;


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
		mainCtr = GameObject.Find ("edg_mainController");
		mc = mainCtr.GetComponent<edg_mainController> ();

		//sub message
		mainMsg = GameObject.Find("edg_mainMessage");
		mainMsg_s = GameObject.Find("edg_mainMessage_shadow");
		cashText_mainMsg = mainMsg.GetComponent<Text> ();
		cashText_mainMsg_s = mainMsg_s.GetComponent<Text> ();

		//mask
		mask1 = GameObject.Find("mask1");
		cashRectTransform_mask1 = mask1.GetComponent<RectTransform> ();
		mask2 = GameObject.Find("mask2");
		cashRectTransform_mask2 = mask2.GetComponent<RectTransform> ();

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
						cashText_mainMsg.text = st;
						cashText_mainMsg_s.text = st;
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
								dscWait = 6;
							} else if (tmpstr == "。") {
								dscWait = 10;
							} else if (tmpstr == "!") {
								dscWait = 10;
							} else if (tmpstr == "\u3000") {
								dscWait = 24;
							} else if (tmpstr == "・") {
								dscWait = 10;
							} else {
								dscWait = 2;
							}
						}
						//sound
						cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_ds100);
					}
				} else {
					//string
					cashText_mainMsg.text = dscStr;
					cashText_mainMsg_s.text = dscStr;
//					dscDisplay = false;
				}
			} else {
				dscWaitCnt++;
			}
		}
	}

	//select title description arrow right process
	private void setNextDescription(){
		dscIndex++;
		if (dscIndex == (descriptionStringTable.Length - 1)) {
			cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_clear);
		}
		if (dscIndex >= (descriptionStringTable.Length)) {
			dscIndex = 0;
			//term description
			mc.termDescription();
		}
		this.setDescriptionText ();
	}


	//public
	//set title description display
	public void setDescriptionText(){
		dscStr = descriptionStringTable [dscIndex];
		dscStrDispCnt = 0;
		dscStrDispIntCnt = 0;
		dscDisplay = true;
		dscWait = 0;
		dscWaitCnt = 0;
	}

	//set title description display stop 
	public void setDescriptionTextDisable(){
		dscDisplay = false;
	}

	//set mask on
	public void setMask(){
		//mask1
		Vector3 tmppos = cashRectTransform_mask1.localPosition;
		tmppos.x = tmppos.x - 300;
		cashRectTransform_mask1.localPosition = tmppos;
		//mask2
		tmppos = cashRectTransform_mask2.localPosition;
		tmppos.x = tmppos.x - 300;
		cashRectTransform_mask2.localPosition = tmppos;
	}

	//set mask off
	public void setMaskOff(){
		//mask1
		Vector3 tmppos = cashRectTransform_mask1.localPosition;
		tmppos.x = tmppos.x + 300;
		cashRectTransform_mask1.localPosition = tmppos;
		//mask2
		tmppos = cashRectTransform_mask2.localPosition;
		tmppos.x = tmppos.x + 300;
		cashRectTransform_mask2.localPosition = tmppos;
	}

	//select description
	public void selectDescription( BaseEventData eventData ){
		//sound
		cmn_soundController.Instance.playSound (cmn_soundController.Instance.sd_se_ds110);
		//all display or next display
		if (dscDisplay == true) {
			if (dscStrDispCnt < dscStr.Length) {
				dscStrDispCnt = dscStr.Length;
				dscWaitCnt = dscWait;
			} else {
				this.setNextDescription ();
			}
		}
		//main
		mc.tapDescription();
	}

}
