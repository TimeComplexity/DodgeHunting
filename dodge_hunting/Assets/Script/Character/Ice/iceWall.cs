using UnityEngine;
using System.Collections;

public class iceWall : MonoBehaviour {

	public Ice com;
	private int count=0;
	// Use this for initialization
	void Start () {
		com = GameObject.Find ("Player").GetComponent<Ice> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0)
			return;
		if (count < 10) 
			transform.Translate (0.0f, 0.2f, 0.0f);
		++count;
		if(count==10)
			tag="IceWall";
		else if(count==180 && com.beStrongSw[1]==false)
			Destroy(gameObject);
		else if(count>=240 && com.beStrongSw[1]==true)
		{
			Destroy(gameObject);
			com.beStrongSw[1]=false;
		}
	}
}
