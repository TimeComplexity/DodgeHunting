using UnityEngine;
using System.Collections;

public class FireWisp : MonoBehaviour {
	
	private int count =0;

	float dis;

	void Update () {
		if(Time.timeScale==0)
			return;
		if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
		{
			Enemy_Move em2 = Variables.enemy2.GetComponent<Enemy_Move>();
			dis=Vector3.Distance(transform.position,em2.transform.position);
			if(dis<3.2f)
				em2.maxSpeed=em2.defaultMaxSpeed*0.7f;
		}
		Enemy_Move em1 = Variables.enemy1.GetComponent<Enemy_Move>();
		dis=Vector3.Distance(transform.position,em1.transform.position);
		if(dis<3.2f)
			em1.maxSpeed=em1.defaultMaxSpeed*0.7f;

		if(count%60 == 0)
			transform.Rotate (0,(Random.Range (0,12)*30),0);
		transform.Translate (Vector3.right*0.1f);
		if (count == 600)
		{
			if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
			{
				Enemy_Move em2 = Variables.enemy2.GetComponent<Enemy_Move>();
				em2.maxSpeed=em2.defaultMaxSpeed;
			}
			em1 = Variables.enemy1.GetComponent<Enemy_Move>();
			em1.maxSpeed=em1.defaultMaxSpeed;
			Destroy (gameObject);
		}
		++count;
	}
}
