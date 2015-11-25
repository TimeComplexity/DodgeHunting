using UnityEngine;
using System.Collections;

public class WindBarrier : MonoBehaviour {

	public GameObject _player;
	private int count = 0;
	private Wind com;
	// Use this for initialization
	
	void Start () {
		_player = GameObject.Find ("/Player");
		com = _player.GetComponent<Wind> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Time.timeScale==0)
			return;
		++count;
		if(count>=180)
		{
			Player_Move pm = _player.GetComponent<Player_Move>();
			pm.invincibleSw=false;
			Destroy(gameObject);
		}
		this.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y+0.5f,_player.transform.position.z);
	}
}
