using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VacummBall : MonoBehaviour {

	private bool gravitySw=false;
	private SphereCollider sCol;
	private int count=-1;
	private int gravityCount=0;
	private List<GameObject> colList;

	void Start()
	{
		colList = new List<GameObject> ();
		sCol = GetComponent<SphereCollider> ();
	}
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag != "Wall" && col.gameObject.name != "Floor") {
			sCol.isTrigger = true;
			sCol.radius = 10;
			count = 0;
			GetComponent<Renderer>().material.color=Color.red;
		}
	}

	void OnTriggerStay(Collider col)
	{
		if(count==60)
		{
			if(col.gameObject.name!="Floor" && col.gameObject.tag!="Wall")
				colList.Add(col.gameObject);
			gravitySw=true;
		}
	}

	void Update()
	{
		if(count!=-1)
		{
			++count;
		}
		if(gravitySw==true)
		{
			Debug.Log ("!!");
			for(int i=0;i<colList.Count;++i)
			{
				GameObject col = colList[i] as GameObject;
				if(col)
				{
					col.transform.LookAt (this.gameObject.transform.position);
					Debug.Log (Vector3.forward);
					col.transform.Translate(Vector3.forward*0.5f);
				}
			}
			++gravityCount;
			if(gravityCount==60)
			{
				Destroy(gameObject);
			}
		}
	}
}
