using UnityEngine;
using System.Collections;

public class Mirage : MonoBehaviour {

	public float maxSpeed;

	private int count =0;

	void Update () {
		if(Time.timeScale==0)
			return;
		if(count%60 == 0)
			transform.Rotate (0,(Random.Range (0,12)*30),0);
		transform.Translate (Vector3.right*maxSpeed);
		if (count == 1200)
			Destroy (gameObject);
		++count;
	}
}
