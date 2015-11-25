using UnityEngine;
using System.Collections;

public class FirePillar : MonoBehaviour {

	private int count=0;
	void Update () {
		if(Time.timeScale==0)
			return;
		++count;
		if(count%120==0)
			transform.position = new Vector3(Random.Range(-15.0f,15.0f),-1.9f,Random.Range(-8.0f,8.0f));
		else if (count % 120>30 && count%120<=90) 
			transform.Translate (0.0f, 0.065f, 0.0f);
		else if(count%120>90 && count%120<=119)
			transform.Translate (0.0f,-0.13f,0.0f);
		if(count==16)
			tag="FirePillar";
		else if(count==1200)
			Destroy(gameObject);
	}
}
