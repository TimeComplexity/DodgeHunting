using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

	public GameObject _player;
//	public GameObject _enemy1;
//	public GameObject _enemy2;
//	public GameObject _Cyclone;
	public GameObject _VacummBall;
	public GameObject _WindBarrier;
	public GameObject _mirage;
	public Player_Move com;

//	public float windSkill=1.0f;

//	private int windCount = 0;
//  private int jumpCount = -1;
//	private float jumpHeight = 0;
//	private int howlingCount = -1;
	private int mirageCount=-1;
	private GameObject mirage1;
	private GameObject mirage2;
	private bool isWind=false;
	private int windCount = 0;

	public int[] cool;
	private int[] cooltime;

	// Use this for initialization
	void Start () {
		cool = new int[4];
		for (int i=0; i<4; ++i)
			cool [i] = 0;
		cooltime = new int[4];
		cooltime [0] = 30;
		cooltime [1] = 12;
		cooltime [2] = 15;
		cooltime [3] = 50;
		//cooltime [4] = 15;
		//cooltime [5] = 10;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0; i<4; ++i)
			--cool [i];
		if(mirageCount!=-1)
		{
			++mirageCount;
			Mirage ();
		}
		/*if(howlingCount!=-1)
		{
			++howlingCount;
			Howling();
		}
		if(jumpCount!=-1)
		{
			++jumpCount;
			Jump ();
		}*/
		if(isWind==true)
		{
			++windCount;
			com.maxSpeed*=1.0001f;
		}
		else if(isWind==false && windCount >0)
		{
			--windCount;
			if(com.maxSpeed>com.defaultMaxSpeed)
			{
				com.maxSpeed*=0.999f;
				if(com.maxSpeed<com.defaultMaxSpeed)
					com.maxSpeed=com.defaultMaxSpeed;
			}
		}
	}

	public void SkillCheck(int sw)
	{
		if(isWind==true)
			return;
		switch(sw)
		{
		case 0:
			VacummBall();
			break;
		case 1:
			Flash ();
			break;
		case 2:
			WindBarrier();
			break;
		case 3:
			Mirage();
			break;
		/*case 4:
			Howling();
			break;
		case 5:
			Jump();
			break;*/
		}
	}

	public void SyungSyung()
	{
		if(isWind==false && windCount ==0)
		{
			isWind=true;
		}
		else if(isWind==true)
		{
			isWind=false;
		}
	}

	void VacummBall()
	{
		if(cool[0]<=0)
		{
			Vector3 currentPos = _player.transform.position;
			float angle = com.angle;
			currentPos.x -= 1 * Mathf.Cos(angle);
			currentPos.z -= 1 * Mathf.Sin(angle);
			Instantiate(_VacummBall, currentPos, com.transform.rotation);
			//Instantiate (_VacummBall,
			cool[0]=cooltime[0]*Time.captureFramerate;
		}
	}

    void Flash()
    {
        if (cool[1] <= 0)//쿨타임 확인
        {
			//++windCount;
			//reiteration();
			float angle = com.angle;
            Vector3 currentPos = com.transform.position;
            currentPos.x += 5.33f * Mathf.Cos(angle);
            currentPos.z += 5.33f * Mathf.Sin(angle);
            if (Mathf.Abs(currentPos.x) > 15)
                if (currentPos.x > 0)
                    currentPos.x = 14.5f;
                else
                    currentPos.x = -14.5f;
            
			if (Mathf.Abs(currentPos.z) > 8)
                if (currentPos.z > 0)
                    currentPos.z = 8.5f;
                else
                    currentPos.z = -8.5f;
            com.transform.position = currentPos;

            cool[1] = cooltime[1] * Time.captureFramerate; //쿨타임 초기화
        }
    }

	void WindBarrier()
	{
		if (cool[2] <= 0) 
		{
			com.invincibleSw = true;
			//++windCount;
			//reiteration();
			Instantiate (_WindBarrier,new Vector3(_player.transform.position.x,1.0f,_player.transform.position.z),com.transform.rotation);
			cool[2] = cooltime[2] * Time.captureFramerate;
		}
	}

	void Mirage()
	{
		if(mirageCount==-1 && cool[3]<=0)
		{
			mirage1=(Instantiate(_mirage, com.transform.position+new Vector3(1,0,0), com.transform.rotation)as GameObject);
			mirage2=(Instantiate(_mirage, com.transform.position+new Vector3(-1,0,0), com.transform.rotation)as GameObject);
			mirage1.GetComponent<Mirage>().maxSpeed=com.defaultMaxSpeed/2;
			mirage2.GetComponent<Mirage>().maxSpeed=com.defaultMaxSpeed/2;
			mirageCount = 0;
			cool[3]=cooltime[3]*Time.captureFramerate;
		}
		//Debug.Log (shadowCount);
		if(mirageCount>= 0 && mirageCount<cooltime[3]*Time.captureFramerate)
		{
			float dis0=1000;
			float dis1=1000;
			float dis2=1000;
			if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
			{
				Enemy_Move em2 = Variables.enemy2.GetComponent<Enemy_Move>();
				dis0=Vector3.Distance(com.transform.position,em2.transform.position);
				if(mirage1)
				{
					dis1=Vector3.Distance(mirage1.transform.position,em2.transform.position);
				}
				if(mirage2)
				{
					dis2=Vector3.Distance(mirage2.transform.position,em2.transform.position);
				}
				if(Mathf.Min (dis0,dis1,dis2)==dis0)
				{
					em2._player = _player;
				}
				else if(Mathf.Min (dis0,dis1,dis2)==dis1)
				{
					em2._player = mirage1;
				}
				else if(Mathf.Min (dis0,dis1,dis2)==dis2)
				{
					em2._player = mirage2;
				}
			}
			Enemy_Move em1 = Variables.enemy1.GetComponent<Enemy_Move>();
			dis0=Vector3.Distance(com.transform.position,em1.transform.position);
			if(mirage1)
			{
				dis1=Vector3.Distance(mirage1.transform.position,em1.transform.position);
			}
			if(mirage2)
			{
				dis2=Vector3.Distance(mirage2.transform.position,em1.transform.position);
			}
			if(Mathf.Min (dis0,dis1,dis2)==dis0)
			{
				em1._player = _player;
			}
			else if(Mathf.Min (dis0,dis1,dis2)==dis1)
			{
				em1._player = mirage1;
			}
			else if(Mathf.Min (dis0,dis1,dis2)==dis2)
			{
				em1._player = mirage2;
			}
		}
		if(mirageCount>=cooltime[3]*Time.captureFramerate)
		{
			if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
			{
				Variables.enemy2.GetComponent<Enemy_Move>()._player=_player;
			}
			Variables.enemy1.GetComponent<Enemy_Move>()._player=_player;
			mirageCount = -1;
		}
	}
	/*void Cyclone()
	{
		if (cool[2] <= 0)
		{
			++windCount;
			reiteration();
			for(int i=0;i <4; ++i)
				Instantiate(_Cyclone,new Vector3(Random.Range (-14.0f,14.0f),-4.2f,Random.Range (-7.0f,7.0f)), com.transform.rotation);	
			cool[2] = cooltime[2] * Time.captureFramerate; //쿨타임 초기화
		}
	}*/



	/*void Howling()
	{
		if(howlingCount==-1)
		{
			++windCount;
			reiteration();
			float ex = _enemy1.transform.position.x;
			float ey = _enemy1.transform.position.y;
			float px = _player.transform.position.x;
			float py = _player.transform.position.y;
			if(ex>px-5.33f && ex<px+5.33f && ey>py-5.33f && ey<py+5.33f)
			{
				Enemy_Move enemy_move = _enemy1.GetComponent<Enemy_Move>();
				enemy_move.maxSpeed *=0.6f;
				if(Random.Range (1,100)<=30)
				{
					enemy_move.fearSw= true;
					enemy_move.fearCount = 60;
				}
			}
			if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
			{
				float ex2 = _enemy2.transform.position.x;
				float ey2 = _enemy2.transform.position.y;
				if(ex2>px-5.33f && ex2<px+5.33f && ey2>py-5.33f && ey2<py+5.33f)
				{
					Enemy_Move enemy_move = _enemy2.GetComponent<Enemy_Move>();
					enemy_move.maxSpeed *=0.6f;
					if(Random.Range (1,100)<=50)
					{
						enemy_move.fearSw= true;
						enemy_move.fearCount = 60;
					}
				}
			}
			howlingCount = 0;
			cool[4] = cooltime[4] * Time.captureFramerate;
		}
		if(howlingCount ==300*windSkill)
		{
			Enemy_Move enemy_move = _enemy1.GetComponent<Enemy_Move>();
			enemy_move.maxSpeed = enemy_move._maxSpeed;
			if(Variables.enemyLevel==2 || Variables.enemyLevel==3)
			{
				enemy_move = _enemy2.GetComponent<Enemy_Move>();
				enemy_move.maxSpeed = enemy_move._maxSpeed;
			}
		}
		if(howlingCount == cooltime[4]*Time.captureFramerate)
			howlingCount = -1;
	}

	void Jump()
	{
		if(jumpCount==-1)
		{
			++windCount;
			reiteration();
			jumpCount = 0;
			jumpHeight=0.5f;
			cool[5]=cooltime[5]*Time.captureFramerate;
		}
		if(jumpCount<10)
		{
			_player.transform.Translate (0.0f,jumpHeight,0.0f);
			jumpHeight-=0.05f;

		}
		if(jumpCount==cooltime[5]*Time.captureFramerate)
			jumpCount=-1;
	}*/

	/*void reiteration()
	{
		if(windCount == 3)
			com.defaultMaxSpeed*=1.2f;*/
/*		else if(windCount == 6)
		{
//			com.defaultAccel *=1.2f;
		}*/
		/*else if(windCount == 9)
			windSkill=1.2f;
		else if(windCount == 12)
		{
			float cooldown = 0.4f;
			for(int i=0;i<6;++i)
				cooltime[i]=System.Convert.ToInt32(cooltime[i]*cooldown);
		}
/*		else if(windCount==15)
		{

		}*/
	//}
}
