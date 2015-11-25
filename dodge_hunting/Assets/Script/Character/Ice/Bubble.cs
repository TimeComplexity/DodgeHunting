using UnityEngine;
using System.Collections;

public class Bubble : MonoBehaviour {

	private Vector3 direction;
	private int count = 0;
	private bool sw=false;
	// Use this for initialization
	void OnCollisionEnter(Collision coll)
	{
		if(coll.gameObject.tag=="Player")
		{
			direction=transform.position-coll.transform.position;
			direction.y=0;
		}
		if(coll.gameObject.tag=="Bubble")
		{
			direction=transform.position-coll.transform.position;
			direction.y=0;
		}
		if (coll.gameObject.tag == "Wall") 
			if(Mathf.Abs(coll.transform.position.x)==20)
				direction.x*=-1;
			else
				direction.z*=-1;
		if(coll.gameObject.tag=="Enemy")
		{
			Vector3 pos = transform.position;
			pos.y=1;
			transform.position=pos;
			sw=true;
			count=0;
			GetComponent<SphereCollider>().isTrigger=true;
			GetComponent<Rigidbody>().useGravity=false;

		}
	}

	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0)
			return;
		if(!sw)
			transform.Translate (direction*0.1f);
		else
			if(count<=10)
				transform.Translate (0,0.2f,0);
		++count;

		if(count==600 && sw ==false)
			Destroy (gameObject);
		else if(count==60 && sw == true)
			Destroy (gameObject);
	}
}
