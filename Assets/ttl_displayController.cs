using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ttl_displayController : MonoBehaviour {

	//public

	//private
	//const
	//desctiption message table
	string[] descriptionStringTable = new string[]{
		//																									//<--ここまで
		"私、栗山イク子!高校1年生です。\nまわりからはクリちゃんって呼ばれてます!\n",
		"実は私、\n内緒のアルバイトをしているんです・・・",
		"なんと、時給10万円ももらえちゃうんです!",
		"その内容は・・・\n\u3000ヒ\u3000ア\u3000リ\u3000を\n生け捕りするアルバイトなんです!",
		"しかも、1匹生け捕りすると、\n時給100万円アップしちゃうんです!",
		"バイト代を貯めて、\n今使ってるポケベルを\nスマフォに機種変するのが夢なんです!",
		"スマフォを手に入れて、\n趣味の○○○○○(自主規制)画像を\nインスタにアップしたりしたいんです!",
		"どうか、私のお手伝いをしてくれませんか!!",
		"(ところで、上の私を\n何回もタップしないでくださいね・・・\n絶対ですよ!)",
	};

	//local
	//component cash
	Transform cashTransform;
	GameObject mainCtr;
	ttl_mainController mc;
	//main message
	GameObject mainMsg;
	Text cashText_mainMsg;
	GameObject mainMsg_s;
	Text cashText_mainMsg_s;
	//ver
	GameObject verDisp;
	Text verDispText;
	//license
	GameObject licButtonTxt;
	Text licButtonTxtTxt;
	GameObject licDisp;
	GameObject licDispContent;

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

	//license disp
	bool license;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash
		//transform cash
		cashTransform = transform;

		//maincontroller
		mainCtr = GameObject.Find ("ttl_mainController");
		mc = mainCtr.GetComponent<ttl_mainController> ();

		//sub message
		mainMsg = GameObject.Find("ttl_mainMessage");
		mainMsg_s = GameObject.Find("ttl_mainMessage_shadow");
		cashText_mainMsg = mainMsg.GetComponent<Text> ();
		cashText_mainMsg_s = mainMsg_s.GetComponent<Text> ();

		//ver
		verDisp = GameObject.Find ("ttl_verDisp");
		verDispText = verDisp.GetComponent<Text> ();

		//license
		licButtonTxt = GameObject.Find("ttl_gameStartButtonText");
		licButtonTxtTxt = licButtonTxt.GetComponent<Text> ();
		licDispContent = GameObject.Find ("Content");
		string tmpstr = "CREDITS\n\n\nGAME DESIGNER\nPROGRAMMER\nSOUNDS COMPOSER\n<color=#FFFFFF>KOICHI 'JON' NISHIYAMA</color>\n\n\n" +
		                "SPECIAL ADVISER\n<color=#FFFFFF>Inochin\nIPC-BOSS\nIPC-UME\nJUNKO\nQ-CHAN\nshirokuma\nYUKO.S\nYutaka Otake</color>\n\n" +
		                "3D MODEL & TEXTURE\n<color=#ffffff>Game Asset Studio\n(Satomi Character Pack)\n(Taichi Character Pack)\n" +
		                "Ben Houston\n(Fire Ant)\nUnluck Software\n(Fireball Prefabs)\nrik4000\n(UK Terraced Houses Pack FREE)\n" +
						"MyxerMan\n(Simple Cars Pack)\nVertex Studio\n(Free Smartphone)\nYGS Assets\n(Cola Can)</color>\n\n" +
						"VOICE\n<color=#ffffff>Sound Effect Lab</color>\n\n" +
						"FONT\n<color=#ffffff>patrick h. lauke\nHiroki-Chan\n(moji-waku.com)\nM+ FONTS\n\n" + 
						"I appreciate\nmany other\nfree materials.</color>\n\n\n" +
		                "SPECIAL THANKS\n<color=#ffffff>HARUMI\nHIDEKI ASAI\nKOKOCHAN\n(LOP EAR RABBIT)\nRIKA\nTSUGUMI</color>";
		licDispContent.GetComponent<Text> ().text = tmpstr;
		licDisp = GameObject.Find ("ttl_license");
		licDisp.SetActive (false);

		//local
		//title description
		dscIndex = 0;
		dscDisplay = false;
		dscStr = "";
		dscStrDispCnt = 0;
		dscStrDispIntCnt = 0;
		dscWait = 0;
		dscWaitCnt = 0;

		//license
		license = false;

		//generate title display
		this.generateTitleDisplay();
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

	//generate title display
	private void generateTitleDisplay(){
		if (cmn_staticData.Instance.debugMode == true) {
			verDispText.text = "ver" + Application.version + " debugmode:on";
		}
	}


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
		if (dscIndex >= (descriptionStringTable.Length)) {
			dscIndex = 0;
		}
		this.setDescriptionText ();
	}


	//public
	//license disp on/off
	public void setLicenseOnOff(){
		if (license == false) {
			licDisp.SetActive (true);
			license = true;
			licButtonTxtTxt.text = "ライセンスを閉じる";
		} else {
			licDisp.SetActive (false);
			license = false;
			licButtonTxtTxt.text = "ライセンス";
		}
	}

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

	//tap description
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

	public bool getLicenseDisp(){
		return this.license;
	}

}
