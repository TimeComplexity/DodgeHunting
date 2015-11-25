using UnityEngine;
using System.Collections;

public class miniBoar : MonoBehaviour {

	private float maxSpeed;
	private float speed=0.0f;
	private float accel;
	private int count;
	private bool mineSw=false;
	private float mineHeight;
	private bool cycloneSw = false;
	private float cycloneHeight;
	private bool wbSw= false;

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Wall") 
		{
			Destroy(gameObject);
		}
		if(coll.gameObject.name == "Player")
		{
			Destroy (gameObject);
		}
		if(coll.gameObject.tag == "Mine")
		{
			Destroy (coll.gameObject);
			mineHeight=1.0f;
			mineSw=true;
		}
		if(mineSw==true && coll.gameObject.name=="Floor")
		{
			speed=0;
			mineSw=false;
		}
		if(cycloneSw == true && coll.gameObject.name=="Floor")
		{
			speed=0;
			cycloneSw=false;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Cyclone" && cycloneSw==false)
		{
			cycloneSw=true;
			cycloneHeight = 1.0f;
		}
		if(col.gameObject.tag=="Wind Barrier" && wbSw==false)
		{
			wbSw=true;
			count = 10;
		}
	}

	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.tag == "Cyclone" && cycloneSw==false)
		{
			cycloneSw=true;
			cycloneHeight = 1.0f;
		}
	}
	// Use this for initialization
	void Start () {
		maxSpeed = 0.187f;
		accel = 0.003f;
		transform.LookAt (new Vector3 (0.0f, 0.0f, 0.0f));
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.deltaTime == 0)
            return;

		++count;
		if (count == 20) 
		{
			this.tag="miniBoar";
		}
		speed += accel;
		if(mineSw==true)
		{
			if(mineHeight>0)
			{
				transform.Translate (0.0f,mineHeight,0.0f);
				mineHeight-=0.2f;
			}
		}
		else if(cycloneSw==true)
		{
			if(cycloneHeight>0)
			{
				transform.Rotate (0,720.0f * Mathf.Deg2Rad,0);
				transform.Translate (0.0f,cycloneHeight,0.0f);
				cycloneHeight-=0.017f;
				if(cycloneHeight<=0)
				{
					Vector3 currentPos = transform.position;
					currentPos.x=Random.Range(-15,15);
					currentPos.z=Random.Range(-8,8);
					transform.position = currentPos;
				}
			}
		}
		else if(wbSw==true)
		{
			Debug.Log ("!!");
			transform.Translate (Vector3.back*0.8f);
			--count;
			if(count==0)
			{
				speed=0;
			}
		}
		else
		{
			if(speed>maxSpeed)
			{
				speed=maxSpeed;
			}
        	transform.Translate(Vector3.forward * speed);
		}
	}
}
