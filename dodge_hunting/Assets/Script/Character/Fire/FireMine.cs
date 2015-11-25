using UnityEngine;
using System.Collections;

public class FireMine : MonoBehaviour {

	public GameObject _player;
	private int count = 0;
	void Start () {
		_player = GameObject.Find ("/Player");
	}

	void Update () {
		if(Time.timeScale==0)
			return;
		++count;
		if(count == 10)
			gameObject.tag="Mine";
		if(count == 600)
			Destroy (gameObject);
	}
}
