using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {
	
	public GameObject _player;
	//public GameObject _Blaze;
	public GameObject _Mine;
	public GameObject _FirePillar;
	//public GameObject _Fairy;
	public GameObject _Wisp;
	public GameObject _pheonix;
	public Player_Move com;
//	public int resetCount = 0;
	private int awakingCount=-1;
//	private int blazestepCount = -1;
//	private int fairyCount = -1;
//	public int fairyPlayer = 0;
//	public int fairyEnemy = 0;
	public int[] cool;
	private int[] cooltime;
	//private int touch = -1;
	private bool pheonixSw = false;


	// Use this for initialization
	void Start () {
		cool = new int[4];
		for(int i=0;i<4;++i)
			cool [i] = 0;
		cooltime = new int[4];
		cooltime [0] = 20;
		cooltime [1] = 60;
		cooltime [2] = 40;
		cooltime [3] = 60;
//		cooltime [4] = 40;
//		cooltime [5] = 60;
	}

	public void SkillCheck(int sw)
	{
		switch(sw)
		{
		case 0:
			Mine ();
			break;
		case 1:
			Awaking ();
			break;
		case 2:
			FirePillar();
			break;
		case 3:
			Wisp ();
			break;
/*		case 4:
			FirePillar();
			break;
		case 5:
			Wisp ();
			break;*/
		}
	}

	// Update is called once per frame
    void Update()
    {
		for(int i=0;i<4;++i)
			--cool[i];
		/*if(blazestepCount!=-1)
		{
			++blazestepCount;
			BlazeStep();
		}*/
		if (awakingCount != -1)
		{
			++awakingCount;
			Awaking();
		}
	}

	public void Pheonix()
	{
		if(pheonixSw==false)
		{
			Instantiate (_pheonix,_player.transform.position,Quaternion.identity);
			com.pheonixSw=true;
			for(int i=0;i<4;++i)
				cool[i]=cooltime[i]*Time.captureFramerate;
			pheonixSw=true;
		}
	}

	void Mine()
	{
		if (cool[0] <= 0)
		{
			Vector3 currentPos = _player.transform.position;
			float angle = com.angle;
			currentPos.x -= 1 * Mathf.Cos(angle);
			currentPos.y = 0;
			currentPos.z -= 1 * Mathf.Sin(angle);
			Instantiate(_Mine, currentPos, com.transform.rotation);
			
			cool[0]=cooltime[0]*Time.captureFramerate;
		}
	}
	
	void Awaking()
	{
		if (awakingCount == -1 && cool[1]<0)
		{
			cool[1] = cooltime[1] * Time.captureFramerate; 
			com.maxSpeed *= 2;
			//			com.accel *= 2;
			//			com.fricForce *= 2;
			awakingCount = 1;
		}
		else if (awakingCount == 600)
			com.maxSpeed = com.defaultMaxSpeed / 2.0f;
		//			com.accel = com.defaultAccel / 2.0f;
		//			com.fricForce = com.defaultFricForce / 2.0f;
		else if (awakingCount == 900)
			com.maxSpeed = com.defaultMaxSpeed;
		//			com.accel = com.defaultAccel;
		//			com.fricForce = com.defaultFricForce;
		else if (awakingCount >= cooltime[1] * Time.captureFramerate) 
			awakingCount = -1;
	}
	
	void FirePillar()
	{
		if (cool[2] <= 0)
		{
			for(int i=0;i<3;++i)
			{
				float x, z;
				x=Random.Range(-15.0f,15.0f);
				z=Random.Range(-8.0f,8.0f);
				Vector3 currentPos = new Vector3(x,-2.2f,z);
				Instantiate(_FirePillar, currentPos, com.transform.rotation);
			}
			cool[2]=cooltime[2]*Time.captureFramerate;
		}
	}
	
	void Wisp()
	{
		if(cool[3]<0)
		{
			Instantiate (_Wisp,new Vector3(Random.Range(-15f,15f),1,Random.Range(-8f,8f)),Quaternion.identity);
			Instantiate (_Wisp,new Vector3(Random.Range(-15f,15f),1,Random.Range(-8f,8f)),Quaternion.identity);
			Instantiate (_Wisp,new Vector3(Random.Range(-15f,15f),1,Random.Range(-8f,8f)),Quaternion.identity);
			cool[3]=cooltime[3]*Time.captureFramerate;
		}
	}
/*	void BlazeStep()
	{
		if(blazestepCount==-1 && cool[1]<0)
		{
			cool[1]=cooltime[1]*Time.captureFramerate;
			blazestepCount = 0;
		}
		if(blazestepCount%2==0 && blazestepCount<=300/(resetCount+1))
		{
			Vector3 currentPos = _player.transform.position;
			currentPos.y = 0;
			Instantiate(_Blaze, currentPos, com.transform.rotation);
		}
		if(blazestepCount>=cooltime[1]*Time.captureFramerate)
			blazestepCount=-1;
			//touch = -1;
		/*if(touch == -1)
		{
			for(int i=0;i<Input.touchCount;++i)
			{
				if (Input.GetTouch (i).phase == TouchPhase.Began) 
				{
					touch = i;
					touchposX = Input.GetTouch (i).position.x;
					touchposY = Input.GetTouch (i).position.y;
				}
			}
		}
		if(Input.GetTouch (touch).phase == TouchPhase.Ended)
		{
			float posX = Input.GetTouch (touch).position.x;
			float posY = Input.GetTouch (touch).position.y;
			float angle =  Mathf.Atan2 (touchposY-posY, touchposX-posX);
			com.rollingAngle = angle;
			com.rollingCount = 10;
			touch = -2;
		}*/
//	}
	
	/*public void Fairy()
	{
		if(fairyCount == -1)
		{
			for(int i=0;i<5;++i)
			{
				Instantiate(_Fairy, new Vector3(Random.Range(-15.5f,15.5f),Random.Range(6.0f,7.0f),Random.Range (-8.5f,8.5f)), com.transform.rotation);
			}
			cool[4] = cooltime[4] * Time.captureFramerate;
			fairyCount=0;
		}
		else if(fairyCount==600/(resetCount+1))
		{
			com.maxSpeed=com._maxSpeed;
			if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
			{
				Enemy_Move em2 = Variables.enemy2.GetComponent<Enemy_Move>();
				em2.maxSpeed=em2._maxSpeed;
			}
			Enemy_Move em1 = Variables.enemy1.GetComponent<Enemy_Move>();
			em1.maxSpeed=em1._maxSpeed;
			
			if(fairyEnemy + fairyPlayer !=5)
			{
				com.maxSpeed -= (com._maxSpeed*0.05f)*fairyPlayer;
			}
			else
			{
				com.maxSpeed += (com._maxSpeed*0.05f)*fairyPlayer*2;
			}
		}
		else if(fairyCount==900/(resetCount+1))
		{
			com.maxSpeed=com._maxSpeed;
		}
		else if(fairyCount >= cooltime[4] * Time.captureFramerate)
		{
			fairyCount = -1;
			fairyEnemy=0;
			fairyPlayer=0;
		}
	}*/

	/*public void Reset()
	{
		if(resetCount==0)
		{
			resetCount=1;
			for(int i=0;i<6;++i)
			{
				cool[i]=0;
				cooltime[i]*=2;
			}
			com.maxSpeed = com.defaultMaxSpeed;
//			com.accel = com.defaultAccel;
//			com.fricForce = com.defaultFricForce;
			blazestepCount = cooltime[1]*Time.captureFramerate;
			awakingCount = cooltime[4]*Time.captureFramerate;
//			fairyCount = cooltime[4]*Time.captureFramerate;
		}
	}*/

}
