using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

struct SkillType
{
    public Text text;
    public int cooltime;
}

public class CooltimeManager : MonoBehaviour {

    public GameObject player;
	SkillType[] skillType = new SkillType[4];

	public GameObject[] button = new GameObject[4];

    //
    //Text fireText;
    //Text waterText;
    //Text windText;
    //Text earthText;
    //
    //int fireCooltime;
    //int waterCooltime;
    //int windCooltime;
    //int earthCooltime;

	// Use this for initialization
	void Start () {
		for(int i=0;i<4;++i)
			skillType[i].text=button[i].GetComponentInChildren<Text>();
   }
	
	// Update is called once per frame
	void Update () {
        if (Variables.skillType == "Fire") 
			for(int i=0;i<4;++i)
				skillType[i].cooltime = player.GetComponent<Fire>().cool[Variables.skillEquipped[0,i]];
		if(Variables.skillType == "Ice")
			for(int i=0;i<4;++i)
				skillType[i].cooltime = player.GetComponent<Ice>().cool[Variables.skillEquipped[1,i]];
		if(Variables.skillType == "Wind")
			for(int i=0;i<4;++i)
				skillType[i].cooltime = player.GetComponent<Wind>().cool[Variables.skillEquipped[2,i]];
		/*water.cooltime = player.GetComponent<Ice>().cool;
        wind.cooltime = player.GetComponent<Wind>().cool;
        earth.cooltime = player.GetComponent<Earth>().cool;*/

		for(int i=0;i<4;++i)
			if(skillType[i].cooltime>0)
				skillType[i].text.text=""+(skillType[i].cooltime/Time.captureFramerate+1);//cool이 30 미만이면 0이 표시되므로 +1을 했음
			else
				skillType[i].text.text="";
    }
}
