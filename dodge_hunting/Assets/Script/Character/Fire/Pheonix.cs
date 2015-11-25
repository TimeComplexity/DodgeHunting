using UnityEngine;
using System.Collections;

public class Pheonix : MonoBehaviour {

	private GameObject _player;
	private int count=0;
	void Start () {
		_player = GameObject.Find ("/Player");
	}
	
	void Update () {
		if(Time.timeScale==0)
			return;
		transform.position = _player.transform.position;
		++count;
		if(count==300)
		{
			_player.GetComponent<Player_Move>().pheonixSw=false;
			Destroy(gameObject);
		}
	}
}
