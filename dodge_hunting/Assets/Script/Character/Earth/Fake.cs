using UnityEngine;
using System.Collections;

public class Fake : MonoBehaviour {

	private int count=0;
	// Use this for initialization
	void Start () {
		name = "Fake";
	}

	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0)
		{
			return;
		}
		++count;
		if (count == 60) 
		{
			Destroy(gameObject);
		}
	}
}
