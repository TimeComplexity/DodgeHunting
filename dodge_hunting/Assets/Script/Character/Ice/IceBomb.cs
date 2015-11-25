using UnityEngine;
using System.Collections;

public class IceBomb : MonoBehaviour {

	private int count=0;
	void Update () {
		if(Time.timeScale==0)
			return;
		transform.Translate (0, -0.08f, 0);
		++count;
		if(count==180)
			Destroy(gameObject);
	}
}
