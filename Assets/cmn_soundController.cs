using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cmn_soundController : SingletonMonoBehaviour<cmn_soundController> {
	//public
	//bgm clip
	//(result)
	//public audio bgm bgm101
	public AudioClip bgm101;
	//public audio bgm bgm201
	public AudioClip bgm201;
	//public audio bgm bgm251
	public AudioClip bgm251;
	//public audio bgm bgm301
	public AudioClip bgm301;
	//public audio bgm bgm351
	public AudioClip bgm351;
	//public audio bgm bgm401
	public AudioClip bgm401;
	//public audio bgm bgm451
	public AudioClip bgm451;
	//public audio bgm bgm501
	public AudioClip bgm501;
	//voice clip
	//(ending)
	//public audio se voice ending naked
	public AudioClip vo_edg_p_naked;
	//(field)
	//public audio se voice field encount
	public AudioClip vo_fld_p_encount;
	//public audio se voice field go
	public AudioClip vo_fld_p_go;
	//public audio se voice field ready
	public AudioClip vo_fld_p_ready;
	//public audio se voice field start
	public AudioClip vo_fld_p_start;
	//(battle)
	//public audio se voice battle hiari atack l 100
	public AudioClip vo_h_atack_l100;
	//public audio se voice battle hiari atack s 100
	public AudioClip vo_h_atack_s100;
	//public audio se voice battle hiari atack s 110
	public AudioClip vo_h_atack_s110;
	//public audio se voice battle hiari hit ko
	public AudioClip vo_h_hit_ko;
	//public audio se voice battle hiari hit l
	public AudioClip vo_h_hit_l;
	//public audio se voice battle hiari hit s
	public AudioClip vo_h_hit_s;
	//public audio se voice battle hiari ko
	public AudioClip vo_h_ko;
	//public audio se voice battle hiari tame 100
	public AudioClip vo_h_tame100;
	//public audio se voice battle player abunai
	public AudioClip vo_p_abunai;
	//public audio se voice battle player atack l
	public AudioClip vo_p_atack_l;
	//public audio se voice battle player atack l near
	public AudioClip vo_p_atack_ln;
	//public audio se voice battle player atack s 100
	public AudioClip vo_p_atack_s100;
	//public audio se voice battle player atack s 110
	public AudioClip vo_p_atack_s110;
	//public audio se voice battle player atack s 120
	public AudioClip vo_p_atack_s120;
	//public audio se voice battle player atocyoto
	public AudioClip vo_p_atocyoto;
	//public audio se voice battle player down 100
	public AudioClip vo_p_down100;
	//public audio se voice battle player down 110
	public AudioClip vo_p_down110;
	//public audio se voice battle player down 120
	public AudioClip vo_p_down120;
	//public audio se voice battle player down 130
	public AudioClip vo_p_down130;
	//public audio se voice battle player hiari atack pattern
	public AudioClip vo_p_h_atack_p;
	//public audio se voice battle player hiari hit
	public AudioClip vo_p_h_hit;
	//public audio se voice battle player hit ko
	public AudioClip vo_p_hit_ko;
	//public audio se voice battle player hit l
	public AudioClip vo_p_hit_l;
	//public audio se voice battle player hit s
	public AudioClip vo_p_hit_s;
	//public audio se voice battle player ko 100
	public AudioClip vo_p_ko100;
	//public audio se voice battle player ko 110
	public AudioClip vo_p_ko110;
	//public audio se voice battle player start 100
	public AudioClip vo_p_start100;
	//public audio se voice battle player start 110
	public AudioClip vo_p_start110;
	//public audio se voice battle player step 100
	public AudioClip vo_p_step100;
	//public audio se voice battle player step 110
	public AudioClip vo_p_step110;
	//public audio se voice battle player tame 100
	public AudioClip vo_p_tame100;
	//public audio se voice battle player tame 110
	public AudioClip vo_p_tame110;
	//public audio se voice battle player tame 120
	public AudioClip vo_p_tame120;
	//(title)
	//public audio se voice title player title
	public AudioClip vo_p_title;
	//(common)
	//public audio se voice player wait
	public AudioClip vo_p_wait;
	//(battle)
	//public audio se voice player win
	public AudioClip vo_p_win;
	//(result)
	//public audio se voice result money fix
	public AudioClip vo_rst_moneyfix;
	//public audio se voice result gameover 100
	public AudioClip vo_rst_p_gameover100;
	//public audio se voice result gameover 110
	public AudioClip vo_rst_p_gameover110;
	//public audio se voice result lose
	public AudioClip vo_rst_p_lose;
	//public audio se voice result start
	public AudioClip vo_rst_p_start;
	//(title)
	//public audio se voice title wear 100
	public AudioClip vo_ttl_p_wear100;
	//se clip
	//(battle)
	//public audio se battle explosion space 100
	public AudioClip se_btl_exp_space100;
	//public audio se battle explosion space 100
	public AudioClip se_btl_hko_back100;
	//public audio se battle space 100
	public AudioClip se_btl_space100;
	//public audio se battle space 110
	public AudioClip se_btl_space110;
	//public audio se battle start
	public AudioClip se_btl_start;
	//(field)
	//public audio se field explosion car
	public AudioClip se_fld_expcar100;
	//public audio se field walk
	public AudioClip se_fld_p_walk;
	//public audio se field pokebell
	public AudioClip se_fld_pokebell100;
	//(battle)
	//public audio se battle hiari atack l
	public AudioClip se_h_atack_l;
	//public audio se battle hiari atack l near 100
	public AudioClip se_h_atack_ln100;
	//public audio se battle hiari atack l near 110
	public AudioClip se_h_atack_ln110;
	//public audio se battle hiari atack s
	public AudioClip se_h_atack_s;
	//public audio se battle hiari atack s near 100
	public AudioClip se_h_atack_sn100;
	//public audio se battle hiari atack s near 110
	public AudioClip se_h_atack_sn110;
	//public audio se battle hiari down 100
	public AudioClip se_h_down_100;
	//public audio se battle hiari down 110
	public AudioClip se_h_down_110;
	//public audio se battle hiari down 120
	public AudioClip se_h_down_120;
	//public audio se battle hiari down back
	public AudioClip se_h_down_back;
	//public audio se battle hiari explosion
	public AudioClip se_h_exp100;
	//public audio se battle hiari hit l 100
	public AudioClip se_h_hit_l100;
	//public audio se battle hiari hit l near
	public AudioClip se_h_hit_ln;
	//public audio se battle hiari hit s
	public AudioClip se_h_hit_s;
	//public audio se battle hiari hit move 100
	public AudioClip se_h_move100;
	//public audio se battle hiari tame 100
	public AudioClip se_h_tame100;
	//public audio se battle player atack l 100
	public AudioClip se_p_atack_l100;
	//public audio se battle player atack l 110
	public AudioClip se_p_atack_l110;
	//public audio se battle player atack l 120
	public AudioClip se_p_atack_l120;
	//public audio se battle player atack l 130
	public AudioClip se_p_atack_l130;
	//public audio se battle player atack pattern
	public AudioClip se_p_atack_p;
	//public audio se battle player atack s 100
	public AudioClip se_p_atack_s100;
	//public audio se battle player atack s 110
	public AudioClip se_p_atack_s110;
	//public audio se battle player atack s near
	public AudioClip se_p_atack_sn;
	//public audio se battle player down 100
	public AudioClip se_p_down100;
	//public audio se battle player down 110
	public AudioClip se_p_down110;
	//public audio se battle player lose 100
	public AudioClip se_p_lose100;
	//public audio se battle player lose 110
	public AudioClip se_p_lose110;
	//public audio se battle player tame cansel 100
	public AudioClip se_p_tame_cansel100;
	//public audio se battle player tame 100
	public AudioClip se_p_tame100;
	//public audio se battle player tame 110
	public AudioClip se_p_tame110;
	//public audio se battle player win 100
	public AudioClip se_p_win100;
	//(result)
	//public audio se result clear
	public AudioClip se_rst_clear;
	//public audio se result gameover
	public AudioClip se_rst_gameover;
	//public audio se result moneyfix 100
	public AudioClip se_rst_moneyfix100;
	//public audio se result moneyfix 110
	public AudioClip se_rst_moneyfix110;
	//public audio se result start
	public AudioClip se_rst_start;
	//(title)
	//public audio se title chime
	public AudioClip se_ttl_chime;
	//public audio se title escription 100
	public AudioClip se_ttl_des100;
	//public audio se title description 110
	public AudioClip se_ttl_des110;
	//public audio se select 100
	public AudioClip se_ttl_sel100;
	//public audio se select 110
	public AudioClip se_ttl_sel110;
	//public audio se select 120
	public AudioClip se_ttl_sel120;
	//public audio se select 130
	public AudioClip se_ttl_sel130;
	//public audio se select 140
	public AudioClip se_ttl_sel140;
	//public audio se select 150
	public AudioClip se_ttl_sel150;


	//system public
	//system const
	//public bgm
	public int sd_bgm101 = 0x00;
	public int sd_bgm201 = 0x01;
	public int sd_bgm251 = 0x02;
	public int sd_bgm301 = 0x03;
	public int sd_bgm351 = 0x04;
	public int sd_bgm401 = 0x05;
	public int sd_bgm451 = 0x06;
	public int sd_bgm501 = 0x07;

	//public voice
	//(ending)
	public int sd_vo_p_naked = 0x40;	//player naked
	//(field)
	public int sd_vo_p_encount = 0x41;	//player encount
	public int sd_vo_p_go = 0x42;	//player go
	public int sd_vo_p_ready = 0x43;	//player ready
	public int sd_vo_p_start = 0x44;	//player start
	//(battle)
	public int sd_vo_h_atck_l100 = 0x45;	//hiari atack l 100
	public int sd_vo_h_atck_s100 = 0x46;	//hiari atack s 100
	public int sd_vo_h_atck_s110 = 0x47;	//hiari atack s 110
	public int sd_vo_h_htko = 0x48;	//hiari hit ko
	public int sd_vo_h_ht_l = 0x49;	//hiari hit l
	public int sd_vo_h_ht_s = 0x4a;	//hiari hit s
	public int sd_vo_h_ko_back = 0x4b;	//hiari ko
	public int sd_vo_h_tm100 = 0x4c;	//hiari tame 100
	public int sd_vo_p_abnai = 0x4d;	//player abunai
	public int sd_vo_p_atck_l = 0x4e;	//player atack l

	public int sd_vo_p_atck_ln = 0x4f;	//player atack l near
	public int sd_vo_p_atck_s100 = 0x50;	//player atack s 100
	public int sd_vo_p_atck_s110 = 0x51;	//player atack s 110
	public int sd_vo_p_atck_s120 = 0x52;	//player atack s 120
	public int sd_vo_p_atchot = 0x53;	//player ato choto
	public int sd_vo_p_dwn_100 = 0x54;	//player down 100
	public int sd_vo_p_dwn_110 = 0x55;	//player down 110
	public int sd_vo_p_dwn_120 = 0x56;	//player down 120
	public int sd_vo_p_dwn_130 = 0x57;	//player down 130
	public int sd_vo_p_h_atck_p = 0x58;	//player hiari atack pattern

	public int sd_vo_p_h_ht = 0x59;	//player hiari hit (yattane)
	public int sd_vo_p_ht_ko = 0x5a;	//player hit ko
	public int sd_vo_p_ht_l = 0x5b;	//player hit l
	public int sd_vo_p_ht_s = 0x5c;	//player hit s
	public int sd_vo_p_ko_100 = 0x5d;	//player ko 100
	public int sd_vo_p_ko_110 = 0x5e;	//player ko 110
	public int sd_vo_p_stt_100 = 0x5f;	//player ko 100
	public int sd_vo_p_stt_110 = 0x60;	//player ko 110
	public int sd_vo_p_stp_100 = 0x61;	//player step 100
	public int sd_vo_p_stp_110 = 0x62;	//player step 110

	public int sd_vo_p_tm_100 = 0x63;	//player tame 100
	public int sd_vo_p_tm_110 = 0x64;	//player tame 110
	public int sd_vo_p_tm_120 = 0x65;	//player tame 120
	//(title)
	public int sd_vo_p_ttl = 0x66;	//player title
	//(common)
	public int sd_vo_p_wat = 0x67;	//player wait
	//(battle)
	public int sd_vo_p_wn = 0x68;	//player win
	//(result)
	public int sd_vo_p_mnyfix = 0x69;	//player money fix
	public int sd_vo_p_gamovr_100 = 0x6a;	//player game over 100
	public int sd_vo_p_gamovr_110 = 0x6b;	//player game over 110
	public int sd_vo_p_lse = 0x6c;	//player lose
	public int sd_vo_p_stt = 0x6d;	//player start
	//(title)
	public int sd_vo_p_wea = 0x6e;	//player wear
	//(field)
	public int sd_vo_h_encount = 0x6f;	//hiari encount

	//public se
	//(battle)
	public int sd_se_hiariexp_s = 0xb0;	//hiari explosion space
	public int sd_se_hkoback = 0xb1;	//hko back
	public int sd_se_space100 = 0xb2;	//space 100
	public int sd_se_space110 = 0xb3;	//space 110
	public int sd_se_btlstart = 0xb4;	//battle start dora
	//(field)
	public int sd_se_carexp = 0xb5;	//car explosion
	public int sd_se_fld_walk = 0xb6;	//walk
	public int sd_se_pokebell = 0xb7;	//pokebell
	//(battle)
	public int sd_se_h_atck_l = 0xb8;	//hiari atack l
	public int sd_se_h_atck_ln100 = 0xb9;	//hiari atack l near 100
	public int sd_se_h_atck_ln110 = 0xba;	//hiari atack l near 110
	public int sd_se_h_atck_s = 0xbb;	//hiari atack s
	public int sd_se_h_atck_sn100 = 0xbc;	//hiari atack s near 100
	public int sd_se_h_atck_sn110 = 0xbd;	//hiari atack s near 110
	public int sd_se_h_down100 = 0xbe;	//hiari down 100
	public int sd_se_h_down110 = 0xbf;	//hiari down 110
	public int sd_se_h_down120 = 0xc0;	//hiari down 120
	public int sd_se_h_downback = 0xc1;	//hiari down back

	public int sd_se_h_exp = 0xc2;	//hiari explosion(hko)
	public int sd_se_h_ht_l = 0xc3;	//hiari hit l
	public int sd_se_h_ht_ln = 0xc4;	//hiari hit l near
	public int sd_se_h_ht_s = 0xc5;	//hiari hit s
	public int sd_se_h_mov = 0xc6;	//hiari move
	public int sd_se_h_tame = 0xc7;	//hiari tame
	public int sd_se_p_atck_l100 = 0xc8;	//player atack l 100
	public int sd_se_p_atck_l110 = 0xc9;	//player atack l 110
	public int sd_se_p_atck_l120 = 0xca;	//player atack l 120
	public int sd_se_p_atck_l130 = 0xcb;	//player atack l 130

	public int sd_se_p_atck_p = 0xcc;	//player atack pattern
	public int sd_se_p_atck_s100 = 0xcd;	//player atack s 100
	public int sd_se_p_atck_s110 = 0xce;	//player atack s 110
	public int sd_se_p_atck_sn = 0xcf;	//player atack s near
	public int sd_se_p_dwn100 = 0xd0;	//player down 100
	public int sd_se_p_dwn110 = 0xd1;	//player down 110
	public int sd_se_p_ht_ko100 = 0xd2;	//player ko 100
	public int sd_se_p_los110 = 0xd3;	//player lose 110
	public int sd_se_p_tmcansel100 = 0xd4;	//player tamecansel 100
	public int sd_se_p_tm100 = 0xd5;	//player tame 100

	public int sd_se_p_tm110 = 0xd6;	//player tame 110
	public int sd_se_p_wn100 = 0xd7;	//player win 100
	//(result)
	public int sd_se_clear = 0xd8;	//claer
	public int sd_se_gameover = 0xd9;	//game over
	public int sd_se_moneyfix100 = 0xda;	//money fix 100
	public int sd_se_moneyfix110 = 0xdb;	//money fix 110
	public int sd_se_rststart = 0xdc;	//result start
	//(title)
	public int sd_se_chime = 0xdd;	//title chime
	public int sd_se_ds100 = 0xde;	//description
	public int sd_se_ds110 = 0xdf;	//description skip
	public int sd_se_sl100 = 0xe0;	//select
	public int sd_se_sl110 = 0xe1;	//select
	public int sd_se_sl120 = 0xe2;	//select
	public int sd_se_sl130 = 0xe3;	//select
	public int sd_se_sl140 = 0xe4;	//select
	public int sd_se_sl150 = 0xe5;	//select
	//(battle)
	public int sd_se_h_jumps = 0xe6;	//jump start
	public int sd_se_h_jumpe = 0xe7;	//jump end
	public int sd_se_h_hkoback_e = 0xe8;	//hiari explosion electric
	public int sd_se_p_ht_s = 0xe9;	//player hit s
	public int sd_se_p_ht_l = 0xea;	//player hit l
	public int sd_se_h_ht_ko = 0xeb;	//player hit lose
	public int sd_se_btl_walk = 0xec;	//walk
	public int sd_se_h_atck_p = 0xed;	//hiari atack l pattern
	//(field)
	public int sd_se_h_encount = 0xee;	//hiari encount



	//private
	//local const
	//audio ch
	const int ch0 = 0x00;	//[loop]bgm
	const int ch1 = 0x01;	//voice player
	const int ch2 = 0x02;	//voice hiari
	const int ch3 = 0x03;	//event se 0
	const int ch4 = 0x04;	//event se 1
	const int ch5 = 0x05;	//event se 2
	const int ch6 = 0x06;	//[loop]event se 3 loop se
	const int ch7 = 0x07;	//player se 0
	const int ch8 = 0x08;	//player se 1 
	const int ch9 = 0x09;	//player se 2 
	const int ch10 = 0x0a;	//player se 3 
	const int ch11 = 0x0b;	//hiari se 0
	const int ch12 = 0x0c;	//hiari se 1
	const int ch13 = 0x0d;	//hiari se 2
	const int ch14 = 0x0e;	//hiari se 3


	//system local
	int intervalCnt;	//interval counter

	//component cash
	AudioSource[] aud;

	//local

	//index min/max
	int bgmmin;
	int bgmmax;
	int voicemin;
	int voicemax;
	int semin;
	int semax;

	//bgm fadeout
	bool bgmfadeout;
	int bgmfadeoutcnt;

	//loop se fadeout
	bool loopsefadeout;
	int loopsefadeoutcnt;

	//sound info struct
	struct soundInfo{
		public AudioClip audioClip;
		public float volume;
		public int ch;
		public soundInfo(AudioClip a, float v, int c ){
			audioClip = a;
			volume = v;
			ch = c;
		}
	}
	//sound clip index,volume,ch table
	soundInfo[] soundInfoTbl;


	// Use this for initialization
	void Start () {
		//system init
		intervalCnt = 0;

		//cash

		//audio
		aud = GetComponents<AudioSource>();

		//local

		//index min/max
		bgmmin = sd_bgm101;
		bgmmax = sd_bgm501;
		voicemin = sd_vo_p_naked;
		voicemax = sd_vo_h_encount;
		semin = sd_se_hiariexp_s;
		semax = sd_se_h_encount;

		//bgm fadeout
		bgmfadeout = false;
		bgmfadeoutcnt = 0;

		//loop se fadeout
		loopsefadeout = false;
		loopsefadeoutcnt = 0;

		//sound info table
		soundInfoTbl = new soundInfo[]{	//audio clip, volume, audio ch
			//bgm[loop]
			new soundInfo( bgm101, 0.70f, ch0),	//bgm result win	//00
			new soundInfo( bgm201, 0.70f, ch0),	//bgm ending	//01
			new soundInfo( bgm251, 0.70f, ch0),	//bgm result lose	//02
			new soundInfo( bgm301, 0.70f, ch0),	//bgm field	//03
			new soundInfo( bgm351, 1.00f, ch0),	//bgm pko	//04
			new soundInfo( bgm401, 1.00f, ch0),	//bgm battle	//05
			new soundInfo( bgm451, 0.65f, ch0),	//bgm hko	//06
			new soundInfo( bgm501, 0.70f, ch0),	//bgm title	//07
			//voice
			//(ending)
			new soundInfo( vo_edg_p_naked, 0.90f, ch1),	//player naked	//40
			//(field)
			new soundInfo( vo_fld_p_encount, 9.50f, ch1),	//player encount	//41
			new soundInfo( vo_fld_p_go, 0.90f, ch1),	//player go	//42
			new soundInfo( vo_fld_p_ready, 0.90f, ch1),	//player ready	//43
			new soundInfo( vo_fld_p_start, 0.90f, ch1),	//player start	//44
			//(battle)
			new soundInfo( vo_h_atack_l100, 0.70f, ch2),	//player atack l 100	//45
			new soundInfo( vo_h_atack_s100, 0.70f, ch2),	//hiari atack s 100	//46
			new soundInfo( vo_h_atack_s110, 0.70f, ch2),	//hiari atack s 110	//47
			new soundInfo( vo_h_hit_ko, 0.70f, ch2),	//hiari hit ko	//48
			new soundInfo( vo_h_hit_l, 0.70f, ch2),	//hiari hit l	//49
			new soundInfo( vo_h_hit_s, 0.70f, ch2),	//hiari hit s	//4a
			new soundInfo( vo_h_ko, 0.90f, ch2),	//hiari ko	//4b
			new soundInfo( vo_h_tame100, 0.70f, ch2),	//hiari tame 100	//4c
			new soundInfo( vo_p_abunai, 1.00f, ch1),	//player abunai	//4d
			new soundInfo( vo_p_atack_l, 0.90f, ch1),	//player atack l	//4e

			new soundInfo( vo_p_atack_ln, 1.00f, ch1),	//player atack ln	//4f
			new soundInfo( vo_p_atack_s100, 0.90f, ch1),	//player atack s 100	//50
			new soundInfo( vo_p_atack_s110, 0.90f, ch1),	//player atack s 110	//51
			new soundInfo( vo_p_atack_s120, 0.90f, ch1),	//player atack s 120	//52
			new soundInfo( vo_p_atocyoto, 0.90f, ch1),	//player ato choto	//53
			new soundInfo( vo_p_down100, 1.00f, ch1),	//player down 100	//54
			new soundInfo( vo_p_down110, 1.00f, ch1),	//player down 110	//55
			new soundInfo( vo_p_down120, 1.00f, ch1),	//player down 120	//56
			new soundInfo( vo_p_down130, 1.00f, ch1),	//player down 130	//57
			new soundInfo( vo_p_h_atack_p, 1.00f, ch1),	//player hiari atack pattern	//58

			new soundInfo( vo_p_h_hit, 1.00f, ch1),	//player hiari hit	//59
			new soundInfo( vo_p_hit_ko, 0.90f, ch1),	//player hit ko	//5a
			new soundInfo( vo_p_hit_l, 0.90f, ch1),	//player hit l	//5b
			new soundInfo( vo_p_hit_s, 0.90f, ch1),	//player hit s	//5c
			new soundInfo( vo_p_ko100, 1.00f, ch1),	//player ko 100	//5d
			new soundInfo( vo_p_ko110, 1.00f, ch1),	//player ko 110	//5e
			new soundInfo( vo_p_start100, 0.90f, ch1),	//player start 100	//5f
			new soundInfo( vo_p_start110, 0.90f, ch1),	//player start 110	//60
			new soundInfo( vo_p_step100, 0.90f, ch1),	//player step 100	//61
			new soundInfo( vo_p_step110, 0.90f, ch1),	//player step 110	//62

			new soundInfo( vo_p_tame100, 0.90f, ch1),	//player tame 100	//63
			new soundInfo( vo_p_tame110, 0.90f, ch1),	//player tame 110	//64
			new soundInfo( vo_p_tame120, 0.90f, ch1),	//player tame 120	//65
			//(title)
			new soundInfo( vo_p_title, 0.90f, ch1),	//player title	//66
			//(common)
			new soundInfo( vo_p_wait, 0.90f, ch1),	//player wait	//67
			//(battle)
			new soundInfo( vo_p_win, 1.00f, ch1),	//player win	//68
			//(result)
			new soundInfo( vo_rst_moneyfix, 0.90f, ch1),	//player money fix	//69
			new soundInfo( vo_rst_p_gameover100, 0.90f, ch1),	//player gameover 100	//6a
			new soundInfo( vo_rst_p_gameover110, 0.90f, ch1),	//player gameover 110	//6b
			new soundInfo( vo_rst_p_lose, 0.90f, ch1),	//player lose	//6c
			new soundInfo( vo_rst_p_start, 0.90f, ch1),	//player result start	//6d
			//(title)
			new soundInfo( vo_ttl_p_wear100, 0.90f, ch1),	//player wear	//6e
			//(field)
			new soundInfo( vo_h_atack_l100, 0.55f, ch2),	//hiari encount	//6f

			//se
			//(battle)
			new soundInfo( se_btl_exp_space100, 0.50f, ch4),	//explosion space	//b0
			new soundInfo( se_btl_hko_back100, 0.90f, ch5),	//hiari hko  demo back	//b1
			new soundInfo( se_btl_space100, 0.90f, ch6),	//space back 100	//b2
			new soundInfo( se_btl_space110, 0.90f, ch6),	//space back 110	//b3
			new soundInfo( se_btl_start, 0.90f, ch4),	//start dora	//b4
			//(field)
			new soundInfo( se_fld_expcar100, 0.90f, ch4),	//explosion car	//b5
			new soundInfo( se_fld_p_walk, 0.70f, ch7),	//field walk	//b6
			new soundInfo( se_fld_pokebell100, 0.50f, ch6),	//pokebell	//b7
			//(battle)
			new soundInfo( se_h_atack_l, 0.80f, ch12),	//hiari atack l	//b8
			new soundInfo( se_h_atack_ln100, 0.80f, ch12),	//hiari atack l near 100	//b9
			new soundInfo( se_h_atack_ln110, 0.90f, ch6),	//hiari atack l near 110	//ba
			new soundInfo( se_h_atack_s, 0.80f, ch12),	//hiari atack s	//bb
			new soundInfo( se_h_atack_sn100, 0.80f, ch12),	//hiari atack s near 100	//bc
			new soundInfo( se_h_atack_sn110, 0.80f, ch12),	//hiari atack s near 110	//bd
			new soundInfo( se_h_down_100, 0.90f, ch11),	//hiari down 100	//be
			new soundInfo( se_h_down_110, 0.90f, ch11),	//hiari down 110	//bf
			new soundInfo( se_h_down_120, 0.90f, ch11),	//hiari down 120	//c0	//(使用)
			new soundInfo( se_h_down_back, 0.95f, ch4),	//hiari down back	//c1

			new soundInfo( se_h_exp100, 0.45f, ch4),	//hiari hko demo explosion	//c2
			new soundInfo( se_h_hit_l100, 0.80f, ch14),	//hiari hit l 100	//c3
			new soundInfo( se_h_hit_ln, 0.80f, ch14),	//hiari hit l near	//c4
			new soundInfo( se_h_hit_s, 0.80f, ch14),	//hiari hit s	//c5
			new soundInfo( se_h_move100, 0.80f, ch11),	//hiari move 100	//c6
			new soundInfo( se_h_tame100, 0.80f, ch13),	//hiari tame 100	//c7
			new soundInfo( se_p_atack_l100, 0.90f, ch8),	//player tame 100	//c8
			new soundInfo( se_p_atack_l110, 0.90f, ch8),	//player tame 110	//c9
			new soundInfo( se_p_atack_l120, 0.90f, ch8),	//player tame 120	//ca
			new soundInfo( se_p_atack_l130, 0.90f, ch8),	//player tame 130	//cb	//(使用)

			new soundInfo( se_p_atack_p, 0.90f, ch8),	//player atack pattern	//cc
			new soundInfo( se_p_atack_s100, 0.90f, ch8),	//player atack s 100	//cd
			new soundInfo( se_p_atack_s110, 0.90f, ch8),	//player atack s 110	//ce
			new soundInfo( se_p_atack_sn, 0.90f, ch8),	//player atack s near	//cf
			new soundInfo( se_p_down100, 1.00f, ch7),	//player down 100	//d0
			new soundInfo( se_p_down110, 1.00f, ch7),	//player down 110	//d1
			new soundInfo( se_p_lose100, 0.90f, ch10),	//player hit ko 100	//d2
			new soundInfo( se_p_lose110, 0.90f, ch4),	//player lose 110	//d3
			new soundInfo( se_p_tame_cansel100, 0.90f, ch9),	//player tame cansel 100	//d4
			new soundInfo( se_p_tame100, 0.90f, ch9),	//player tame 100	//d5

			new soundInfo( se_p_tame110, 0.90f, ch9),	//player tame 110	//d6
			new soundInfo( se_p_win100, 1.00f, ch4),	//player win 100	//d7
			//(result)
			new soundInfo( se_rst_clear, 0.80f, ch4),	//clear	//d8
			new soundInfo( se_rst_gameover, 0.80f, ch4),	//gameover	//d9
			new soundInfo( se_rst_moneyfix100, 0.70f, ch4),	//money fix 100	//da
			new soundInfo( se_rst_moneyfix110, 0.80f, ch4),	//money fix 110	//db
			new soundInfo( se_rst_start, 0.90f, ch4),	//start	//dc
			//(title)
			new soundInfo( se_ttl_chime, 0.60f, ch4),	//chime	//dd
			new soundInfo( se_ttl_des100, 0.95f, ch9),	//description	//de
			new soundInfo( se_ttl_des110, 0.75f, ch5),	//description skip	//df
			new soundInfo( se_ttl_sel100, 0.90f, ch3),	//select	//e0
			new soundInfo( se_ttl_sel110, 0.90f, ch3),	//select	//e1
			new soundInfo( se_ttl_sel120, 0.90f, ch3),	//select	//e2
			new soundInfo( se_ttl_sel130, 0.90f, ch3),	//select	//e3
			new soundInfo( se_ttl_sel140, 0.90f, ch3),	//select	//e4
			new soundInfo( se_ttl_sel150, 0.90f, ch3),	//select	//e5
			//(battle)
			new soundInfo( se_h_atack_ln100, 0.80f, ch11),	//hiari jump start	//e6
			new soundInfo( se_h_atack_sn100, 0.80f, ch11),	//hiari jump end	//e7
			new soundInfo( se_h_atack_ln110, 0.45f, ch13),	//hiari explosion electric	//e8
			new soundInfo( se_p_atack_s100, 1.00f, ch10),	//player hit s	//e9
			new soundInfo( se_p_atack_l120, 0.90f, ch10),	//player hit l	//ea
			new soundInfo( se_p_lose100, 0.90f, ch14),	//hiari hit ko 100	//eb
			new soundInfo( se_fld_p_walk, 0.95f, ch7),	//battle walk	//ec
			new soundInfo( se_p_atack_p, 0.90f, ch12),	//hiari atack pattern	//ed
			//(field)
			new soundInfo( se_h_atack_l, 0.45f, ch12),	//hiari encount	//ee
		};

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
		//nop

		////interval process
		//interval count
		intervalCnt++;
		if (intervalCnt >= 2) {
			intervalCnt = 0;

			//fadeout bgm
			if (bgmfadeout == true) {
				bgmfadeoutcnt++;
				if (bgmfadeoutcnt % 2 == 0) {
					float vo = aud [ch0].volume;
					vo = vo - 0.084f;
					if (vo <= 0) {
						vo = 0.0f;
						bgmfadeout = false;
						bgmfadeoutcnt = 0;
						aud [ch0].clip = null;
					}
					aud [ch0].volume = vo;
				}
			}

			//fadeout loop se
			if (loopsefadeout == true) {
				loopsefadeoutcnt++;
				if (loopsefadeoutcnt % 2 == 0) {
					float vo = aud [ch6].volume;
					vo = vo - 0.075f;
					if (vo <= 0) {
						vo = 0.0f;
						loopsefadeout = false;
						loopsefadeoutcnt = 0;
						aud [ch6].clip = null;
					}
					aud [ch6].volume = vo;
				}
			}

		}
	}


	//private

	//start bgm fadeout
	private void startBgmFadeout(){
		bgmfadeout = true;
		bgmfadeoutcnt = 0;
	}

	private void stopBgmFadeout(){
		bgmfadeout = false;
		bgmfadeoutcnt = 0;
		aud [ch0].clip = null;
	}

	//start loop se fadeout
	private void startLoopSeFadeout(){
		loopsefadeout = true;
		loopsefadeoutcnt = 0;
	}

	private void stopLoopSeFadeout(){
		loopsefadeout = false;
		loopsefadeoutcnt = 0;
		aud [ch6].clip = null;
	}


	//public

	//play sound
	public void playSound( int sound, bool playForce = true ){
		//fix index
		if (sound >= semin) {	//se?
			sound = bgmmax + (voicemax - voicemin + 1) + (sound - semin + 1);
		} else if (sound >= voicemin) {	//voice?
			sound = bgmmax + (sound - voicemin + 1);
		}
		//play no force (same sound no play)
		if (playForce == false) {
			if ( aud [soundInfoTbl [sound].ch].isPlaying == true ) {
				if (aud [soundInfoTbl [sound].ch].clip == soundInfoTbl [sound].audioClip) {
					return;
				}
			}
		}
		//stop fadeout
		if (soundInfoTbl [sound].ch == ch0) {
			this.stopBgmFadeout ();
		}
		if (soundInfoTbl [sound].ch == ch6) {
			this.stopLoopSeFadeout ();
		}
		//sound play
		aud [soundInfoTbl [sound].ch].clip = soundInfoTbl [sound].audioClip;	//clip
		aud [soundInfoTbl [sound].ch].volume = soundInfoTbl [sound].volume;	//volume
		aud [soundInfoTbl [sound].ch].Play ();
	}

	//stop bgm
	public void stopBgm(){
		aud [ch0].Stop ();
		aud [ch0].clip = null;
	}

	//fadeout bgm
	public void fadeoutBgm(){
		if (aud [ch0].clip != null) {
			this.startBgmFadeout ();
		}
	}

	//pause bgm
	public void pauseBgm(){
		if (aud [ch0].clip != null) {
			if (aud [ch0].isPlaying == true) {
				aud [ch0].Pause ();
			} else {
				aud [ch0].clip = null;
			}
		}
	}

	//resume bgm
	public void resumeBgm(){
		if (aud [ch0].clip != null) {
			aud [ch0].Play ();
		}
	}

	//stop voice
	public void stopVoice(){
		for (int i = ch1; i <= ch2; i++) {
			aud [i].Stop ();
			aud [i].clip = null;
		}
	}

	//fadeout voice
	public void fadeoutVoice(){	//(非対応)
		for (int i = ch1; i <= ch2; i++) {
			aud [i].Stop ();
			aud [i].clip = null;
		}
	}

	//pause voice
	public void pauseVoice(){
		for (int i = ch1; i <= ch2; i++) {
			if (aud [i].clip != null) {
				if (aud [i].isPlaying == true) {
					aud [i].Pause ();
				} else {
					aud [i].clip = null;
				}
			}
		}
	}

	//resume voice
	public void resumeVoice(){
		for (int i = ch1; i <= ch2; i++) {
			if (aud [i].clip != null) {
				aud [i].Play ();
			}
		}
	}

	//stop se
	public void stopSe(){
		for (int i = ch3; i <= ch14; i++) {
			aud [i].Stop ();
			aud [i].clip = null;
		}
	}

	//stop se ch
	public void stopSeCh( int ch){
		if ((ch >= ch3) && (ch <= ch14)) {
			aud [ch].Stop ();
			aud [ch].clip = null;
		}
	}

	//fadeout se
	public void fadeoutSe(){	//(非対応)
		for (int i = ch3; i <= ch14; i++) {
			aud [i].Stop ();
			aud [i].clip = null;
		}
	}

	//stop loop se
	public void stopLoopSe(){
		aud [ch6].Stop ();
		aud [ch6].clip = null;
	}

	//fadeout loop se
	public void fadeoutLoopSe(){
		if (aud [ch6].clip != null) {
			this.startLoopSeFadeout ();
		}
	}

	//pause se
	public void pauseSe(){
		for (int i = ch4; i <= ch14; i++) {	//ch3は除く
			if (aud [i].clip != null) {
				if (aud [i].isPlaying == true) {
					aud [i].Pause ();
				} else {
					aud [i].clip = null;
				}
			}
		}
	}

	//resume se
	public void resumeSe(){
		for (int i = ch4; i <= ch14; i++) {	//ch3は除く
			if (aud [i].clip != null) {
				aud [i].Play ();
			}
		}
	}

}
