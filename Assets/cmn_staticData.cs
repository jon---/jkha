using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cmn_staticData {

	//public

	//generate static instance
	public readonly static cmn_staticData Instance = new cmn_staticData();

	//global

	//const
	//system
	public readonly bool debugMode = true;	//debug mode

	//battle near distance
	public readonly float neardst = 3.0f;

	//jk state
	public readonly int targetmoney = 1000;	//target money

	//jk normal bullet pattern
	public readonly int[] pbptn_n = new int[]{0, 0, 0, 3};

	//jk large bullet pattern
	public readonly int[] pbptn_l = new int[]{1, 1, 1, 2};

	//hiari normal bullet pattern
	public readonly int[] hbptn_n = new int[]{0, 0, 0, 3};

	//hiari large bullet pattern
	public readonly int[] hbptn_l = new int[]{1, 1, 2};

	//hiari name
	public readonly string[,] hiariName = new string[,]{
		//level 1
		{	"珍歩 松五郎",
		 	"デビッド・S.M",
		 	"満古 一義",	},
		//level 2
		{ 	"陳歩 立三",
		  	"満古 好子",
		  	"亜奈流 滑留",	},
		//level 3
		{ 	"レオナルド・大介の与一",
			"スティーブ・虎二郎の与一",
			"ポール・城太郎の与一",	},
		//level 4
		{ 	"モーリー・新宿三丁目",
			"馬喰横山のヒーロー",
			"神保町の帝王",	},
		//level 5
		{ 	"キング・オブ・西葛西",
			"キング・オブ・新小岩",
			"キング・オブ・市川市",	},
	};



	//common
	//game level
	//hiari level
	public int hlevel = 0;	//hiari level

	//jk state
	public int lastjikyu = 10;	//last jukyu
	public int jikyu = 10;	//jikyu
	public int money = 0;	//money
	public int moneys = 0;	//money
	public int hiariwin = 0;	//hiari win
	public int hiarilose = 0;	//hiari lose

	//field
	public int timebonus = 0;	//time bonus

	//field and battle
	public int hpplus = 0;	//hp plus
	public int atackplus = 0; //atack plus
	public int defenceplus = 0;	//defence plus
	public bool sitagi = false;	//shitagi plius

	//battle
	public bool lastresult = true;	//last battle result
	public bool gameover = false;	//game over
	public bool nextfield = false;	//next field
	public bool clear = false;	//game clear
	public int patacknum = 0;	//player atack num
	public int hhitnum = 0;	//hiari hit num
	public int phitnum = 0;	//player hit num
	public bool atackbonus = false;	//atack bobus
	public bool deffencebonus = false;	//deffence bobus
	public bool lastkuri = false;	//kuri wear
	public int highvoicecnt1 = 0;	//high priority voice cnt1
	public int highvoicecnt2 = 0;	//high priority voice cnt2
	public int highvoicecnt3 = 0;	//high priority voice cnt3
	public int hatackplus = 0;	//hiari atack plus
	public int hdefenceplus = 0;	//hiari defence plus

	//common state
	public int score = 0;	//score
	public bool kodemo = true;	//battle ko demo no skip
	public int pko_vo_cnt = 0;	//player ko voice cnt


	//process

	//title init
	public void initCmnData(){
		//hiari level
		hlevel = 0;

		//jk state
		lastjikyu = 10;
		jikyu = 10;
		money = 0;
		moneys = 0;
		hiariwin = 0;
		hiarilose = 0;

		//battle
		lastresult = true;
		gameover = false;
		nextfield = false;
		clear = false;

		//common state
		score = 0;
		kodemo = true;
		pko_vo_cnt = 0;
	}

	//field init
	public void fld_initCmnData(){
		//field
		timebonus = 0;
		//field and battle
		hpplus = 0;
		atackplus = 0;
		defenceplus = 0;
		sitagi = false;
	}

	//battle init
	public void btl_initCmnData(){
		//battle
		patacknum = 0;
		hhitnum = 0;
		phitnum = 0;
		atackbonus = false;
		deffencebonus = false;
		lastkuri = false;
		highvoicecnt1 = 0;
		highvoicecnt2 = 0;
		highvoicecnt3 = 0;
	}

}
