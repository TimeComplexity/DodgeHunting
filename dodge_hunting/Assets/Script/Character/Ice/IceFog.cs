using UnityEngine;
using System.Collections;

public class IceFog : MonoBehaviour {

	public Ice com;
	private int count = 0;
	// Update is called once per frame
	void Start(){
		com = GameObject.Find ("Player").GetComponent<Ice> ();
		if(com.beStrongSw[2]==true)
			transform.localScale*=1.2f;
	}
	void Update () {
		if(Time.timeScale==0)
			return;
		++count;
		if(count==600 && com.beStrongSw[2]==false)
		{
			Destroy(gameObject);
			if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
				Variables.enemy2.GetComponent<Enemy_Move>().fogSw=false;
			Variables.enemy1.GetComponent<Enemy_Move>().fogSw=false;
		}
		else if(count>=720 && com.beStrongSw[2]==true)
		{
			Destroy(gameObject);
			if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
				Variables.enemy2.GetComponent<Enemy_Move>().fogSw=false;
			Variables.enemy1.GetComponent<Enemy_Move>().fogSw=false;
			com.beStrongSw[2]=false;
		}
	}
}
