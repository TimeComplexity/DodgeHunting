using UnityEngine;
using System.Collections;

public class FirePillar : MonoBehaviour {

	public GameObject _player;
	private int count=0;
	void Start () {
		_player = GameObject.Find ("/Player");
	}
	
	void Update () {
		if(Time.timeScale==0)
			return;
		if (count < 16) 
			transform.Translate (0.0f, 0.2f, 0.0f);
		++count;

		if(count==16)
			tag="FirePillar";
		else if(count==600)
			Destroy(gameObject);
	}
}
