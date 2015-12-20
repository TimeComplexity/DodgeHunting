using UnityEngine;
using System.Collections;

public class wildBoar : Enemy {

	public GameObject _player;
	public GameObject prefab;
	public GameObject _enemy;
	public Enemy_Move com;
	public Player_Move user;


	public override void setCool()
	{
		skillWaitTime[0] = 300;
		skillWaitTime[1] = 900;
		skillWaitTime[2] = 1200;
		skillWaitTime[3] = 100000;
		skillWaitTime[4] = 100000;
		skillWaitTime[5] = 1200;

		com = _enemy.GetComponent<Enemy_Move>();
		
		for(int i = 0; i<6;++i)
			com.skillWaitTime[i]=skillWaitTime[i];
		
		for (int i=0; i<5; ++i)
			com.skillCool [i] = Random.Range (0, skillWaitTime [i]);
		com.skillCool[5] = skillWaitTime[5];
		user = com._player.GetComponent<Player_Move>();

		com.defaultMaxSpeed = 0.225f;
		com.defaultAccel=0.012f;
		com.drfaultFricForce=0.003f;
		if(com._level == 0)
		{
			com.defaultMaxSpeed*=0.8f;
			com.defaultAccel*=0.8f;
			com.drfaultFricForce*=0.8f;
		}
		else if(com._level == 2)
		{
			com.defaultMaxSpeed*=1.3f;
			com.defaultAccel*=1.3f;
			com.drfaultFricForce*=1.3f;
		}
		com.maxSpeed = com.defaultMaxSpeed;
		com.accel = com.defaultAccel;
		com.fricForce = com.drfaultFricForce;
	}

	public override void ActSkill(int skillSw)
	{
		//Debug.Log (skillSw);
		switch (skillSw) {
		case -1:
			++skillCount [5];
			LeaderSkill ();
			break;
		case 1:
			++skillCount [1];
			ActSkill1 ();
			break;
		case 2:
			++skillCount [2];
			ActSkill2 ();
			break;
		case 3:
			++skillCount [3];
			ActSkill3 ();
			break;
		case 4:
			++skillCount [4];
			ActSkill4 ();
			break;
		default:
			if(com.skillCool[0]<=0)
			{
				DefaultAct (true);
				com.skillCool[0]=com.skillWaitTime[0];
			}
			else
				DefaultAct(false);
			break;
		}
	}


	public void DefaultAct( bool pasSw)
	{
		if(com.fogSw==false && com.icefieldSw==false)
		{
			if (this._enemy == Variables.enemy1)
			{
				com.direction = com._player.transform.position - Variables.enemy1.transform.position;
			}
			if (this._enemy == Variables.enemy2)
			{
				com.direction.x = com._player.transform.position.x+user.speed_x*60 - Variables.enemy2.transform.position.x;
				com.direction.z = com._player.transform.position.z+user.speed_y*60 - Variables.enemy2.transform.position.z;
			}
		}
		com.angle = Mathf.Atan2 (com.direction.z, com.direction.x);
		if (pasSw==true) 
		{
			if(Mathf.Abs (com.preAngle*Mathf.Rad2Deg - com.angle*Mathf.Rad2Deg)>60)
			{
				com.speed_x/=2;
				com.speed_z/=2;
			}
		}
		com.speed_x += com.accel*Mathf.Cos (com.angle);
		com.speed_z += com.accel*Mathf.Sin (com.angle);
		if(com.speed_x>0)
		{
			com.speed_x-=com.fricForce;
		}
		else if(com.speed_x<0)
		{
			com.speed_x+=com.fricForce;
		}
		if(com.speed_z>0)
		{
			com.speed_z-=com.fricForce;
		}
		else if(com.speed_z<0)
		{
			com.speed_z+=com.fricForce;
		}
		if (Mathf.Abs (com.speed_x) > Mathf.Abs(com.maxSpeed*Mathf.Cos (com.angle))) 
		{
			com.speed_x -= com.accel*Mathf.Cos (com.angle);
		}
		if (Mathf.Abs (com.speed_z) > Mathf.Abs (com.maxSpeed*Mathf.Sin (com.angle))) 
		{
			com.speed_z -= com.accel*Mathf.Sin (com.angle);
		} //기본
		/*com.speed_x = com.maxSpeed * Mathf.Cos (com.angle);
		com.speed_y = com.maxSpeed * Mathf.Sin (com.angle);*/ // 가속도,마찰력X
		com.preAngle = com.angle;
		com.transform.rotation = Quaternion.Euler(0.0f, -1 * Mathf.Atan2(com.speed_z, com.speed_x) * Mathf.Rad2Deg, 0.0f);
		com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y, com.transform.position.z + com.speed_z);
	}
	public void ActSkill1()
	{
		if(skillCount[1]==0){
			//com.speed_x = com.defaultMaxSpeed * 2 * Mathf.Cos (com.angle);
			//com.speed_y = com.defaultMaxSpeed * 2 * Mathf.Sin (com.angle);
			com.transform.LookAt(com._player.transform.position);
			com.transform.Rotate (0,-90,0);
		}
		else if(skillCount[1]==60)
		{
			com.skillSw=0;
			com.speed_x=0;
			com.speed_z=0;
			//com.speed_x=com.maxSpeed*Mathf.Cos (com.angle);
			//com.speed_y=com.maxSpeed*Mathf.Sin (com.angle);
			skillCount[1]=-1;
		}
	/*	com.transform.rotation = Quaternion.Euler(0.0f, -1 * Mathf.Atan2(com.speed_y, com.speed_x) * Mathf.Rad2Deg, 0.0f);
		com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y, com.transform.position.z + com.speed_y);*/
		com.transform.Translate (Vector3.right * com.defaultMaxSpeed * 2);
	}
	public void ActSkill2()
	{
		if(skillCount[2]==0)
		{
			com.maxSpeed*=1.1f;
		}
		bool pasSw;
		if(com.skillCool[0]<=0)
		{
			pasSw=true;
			com.skillCool[0]=com.skillWaitTime[0];
		}
		else
		{
			pasSw=false;
		}
		DefaultAct (pasSw);
		if(skillCount[2]==600)
		{
			com.skillSw=0;
			com.maxSpeed=com.defaultMaxSpeed;
			skillCount[2]=-1;
		}
	}

	public void ActSkill3()
	{
		DefaultAct (false);
		com.skillSw = 0;
		skillCount [3] = -1;
	}

	public void ActSkill4()
	{
		DefaultAct (false);
		com.skillSw = 0;
		skillCount [4] = -1;
	}

	public void LeaderSkill()
	{
		com.skillSw = 0;
		skillCount [5] = -1;
		for (int i=0; i<Random.Range(10,20); ++i) 
		{
			Instantiate(prefab,new Vector3(Random.Range (-14.0f,14.0f),0.5f,Random.Range (-7.0f,7.0f)),Quaternion.identity);
			/*int sw = Random.Range (1,4);
			if(sw==1)
			{
				Instantiate(prefab,new Vector3(-14.0f,0.5f,Random.Range (-7.0f,7.0f)),Quaternion.identity);
			}
			else if(sw==2)
			{
				Instantiate(prefab,new Vector3(14.0f,0.5f,Random.Range (-7.0f,7.0f)),Quaternion.identity);
			}
			else if (sw==3)
			{
				Instantiate(prefab,new Vector3(Random.Range(-14.0f,14.0f),0.5f,-7.0f),Quaternion.identity);
			}
			else if(sw==4)
			{
				Instantiate(prefab,new Vector3(Random.Range(-14.0f,14.0f),0.5f,7.0f),Quaternion.identity);
			}*/
		}
		com.transform.rotation = Quaternion.Euler(0.0f, -1 * Mathf.Atan2(com.speed_z, com.speed_x) * Mathf.Rad2Deg, 0.0f);
		com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y, com.transform.position.z + com.speed_z);
	}
}
