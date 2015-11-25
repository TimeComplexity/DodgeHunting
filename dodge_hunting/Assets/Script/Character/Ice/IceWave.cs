using UnityEngine;
using System.Collections;

public class IceWave : MonoBehaviour {

	private bool scaleSw = false;
	private Ice com;
	private int count = -1;

	void Start()
	{
		com = GameObject.Find ("Player").GetComponent<Ice> ();
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.name == "Floor")
			scaleSw=true;
		if(coll.gameObject.tag=="Wall")
		{
			Debug.Log (com.beStrongSw[3]);
			if(com.beStrongSw[3]==false)
				Destroy(gameObject);
			else
			{
				BoxCollider col = GetComponent<BoxCollider>();
				col.isTrigger=true;
				col.size = new Vector3(0.9f,1,0.9f);
				Vector3 pos = this.transform.position;
				pos.y=-0.35f;
				this.transform.position=pos;
				count = 0;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 0)
			return;
		if(!scaleSw)
			transform.Translate (0, -0.05f, 0);
		else if(scaleSw==true && count==-1)
			transform.localScale+=new Vector3(0.64f,0,0.36f);
		if(count!=-1)
		{
			++count;
			if(count==180)
				this.transform.Translate (0,-10f,0);
			else if(count==190)
				Destroy(gameObject);
		}
	}
}
