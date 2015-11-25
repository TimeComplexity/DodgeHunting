using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy_Move : MonoBehaviour {

	public GameObject _player;
	public GameObject _EnemyHp;
	public GameObject _boar;
	public GameObject _wolf;
	public GameObject _miniWolf;
	public GameObject _suri;
	public GameObject _kan;
	public GameObject _cloneKan;
	public GameObject _bubble;
	public Enemy com;
	public Enemy_Hp eHP;
	/*NavMeshAgent agent;
	public Transform _target;*/
	public float defaultAccel=0.006f;
	public float defaultMaxSpeed=0.187f;
	public float drfaultFricForce=0.003f;
	public int _level;

	public float angle;
	public float preAngle;
	public float speed_x;
	public float speed_y;
	public float maxSpeed;
	public float accel;
	public float fricForce;
	public int skillSw=0;
	public int cantmoveCount = 0;
	public Vector3 direction;
	public int[] skillWaitTime = new int[6]; // 0 패시브, 1~4 액티브, 5 리더
	public int[] skillCool = new int[6]; // 0 패시브, 1~4 액티브, 5 리더
	public bool fogSw=false;

	private bool sturnSw = false;
	private bool mineSw = false;
	private float mineHeight;
	private bool cycloneSw = false;
	private float cycloneHeight;
	private bool wbSw = false;
	private bool wispSw = false;
	private bool bubbleSw = false;

	public bool fearSw = false;
	public int fearCount = 0;
	public bool icefieldSw = false;
	public bool icesturnSw = false;

	void Start () {
		_player = GameObject.Find ("/Player");
		eHP = _EnemyHp.GetComponent<Enemy_Hp>();
		if (Variables.enemySwitch == 0)
		{
			Debug.Log("맷돼지 시작");
			_boar.SetActive(true);
			com = GetComponent<wildBoar>();
			com.setCool();
		}
		else if (Variables.enemySwitch == 1)
		{
			Debug.Log("늑대 시작");
			_wolf.SetActive(true);
			com = GetComponent<wolf>();
			com.setCool();
		}
		else if (Variables.enemySwitch == 3)
		{
			Debug.Log("수리 시작");
			_suri.SetActive(true);
			com = GetComponent<Suri>();	
			com.setCool();
		}
		else if (Variables.enemySwitch == 101)
		{
			_wolf.SetActive(false);
			_miniWolf.SetActive(true);
			com = GetComponent<miniWolf>();
			com.setCool();
			Variables.enemySwitch=1;
		}
		else if (Variables.enemySwitch == 2)
		{
			_kan.SetActive(true);
			com = GetComponent<kangaroo>();
			com.setCool();
		}
		else if (Variables.enemySwitch == 102)
		{
			_cloneKan.SetActive(true);
			com = GetComponent<cloneKangaroo>();
			com.setCool();
			// 캥거루는 자기를 복제하기때문에 변수.적스위치에 클론캥거루가 마지막으로 남아있으므로 일반 캥거루로 초기화
			Variables.enemySwitch = 2;
		}
		
		//agent = GetComponent ("NavMeshAgent") as NavMeshAgent;
	}	
	void OnCollisionEnter(Collision coll)
	{
		if(coll.gameObject.tag == "IceShadow")
		{
			_player=GameObject.Find ("Player");
			Debug.Log (_player);
			Destroy(coll.gameObject);
			_player=GameObject.Find ("Player");
			Debug.Log (_player);
		}
		if(coll.gameObject.tag=="Player" && wbSw==false)
		{
			sturnSw=true;
			cantmoveCount = 180;
		}
		if (coll.gameObject.tag == "Wall" && skillSw==1 && wbSw==false && Variables.enemySwitch==0) 
		{
			Debug.Log ("!!");
			speed_x=0;
			speed_y=0;
			cantmoveCount=90;
			skillSw=0;
			com.skillCount[1]=-1;
			sturnSw=true;
		}
		if(coll.gameObject.tag == "IceWall")
		{
			if(speed_x>maxSpeed || speed_y>maxSpeed)
			{
				Destroy (coll.gameObject);
			}
			else
			{
				speed_x=0;
				speed_y=0;
				cantmoveCount=120;
				//skillSw=0;
				sturnSw=true;
			}
		}
		if(coll.gameObject.tag == "Mine")
		{
			eHP.hpBar.value-=eHP.maxHp*0.05f;
			Destroy (coll.gameObject);
			mineHeight=1.0f;
			mineSw=true;
		}
		if(mineSw==true && coll.gameObject.name=="Floor")
		{
			mineSw=false;
			sturnSw=true;
			cantmoveCount = 180;
		}
		if(cycloneSw == true && coll.gameObject.name=="Floor")
		{
			speed_x=0;
			speed_y=0;
			cycloneSw=false;
		}
/*		if(coll.gameObject.tag=="Evidence")
		{
			maxSpeed-=_maxSpeed*0.05f;
			++_player.GetComponent<Fire>().fairyEnemy;
			Destroy (coll.gameObject);
		}*/
		if(icefieldSw==true && coll.gameObject.tag=="Wall")
		{
			icesturnSw=true;
			icefieldSw=false;
			sturnSw=true;
			cantmoveCount=120;
		}
		if (coll.gameObject.tag == "FireWisp") 
		{
			Destroy (coll.gameObject);
			wispSw=true;
			cantmoveCount=120;
			speed_x=0;
			speed_y=0;
		}
		if(coll.gameObject.tag =="Bubble")
		{
			_bubble=coll.gameObject;
			bubbleSw=true;
			Vector3 pos = coll.transform.position;
			pos.y=1;
			transform.position = pos;
			GetComponent<Rigidbody>().useGravity=false;
			cantmoveCount=60;
		}
		if(bubbleSw && coll.gameObject.name=="Floor")
		{
			bubbleSw=false;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Blaze")
		{
			eHP.damage = eHP._damage*2.0f;
			maxSpeed = defaultMaxSpeed*1.5f;
		}
		if(col.gameObject.tag == "Cyclone" && cycloneSw==false)
		{
			cycloneSw=true;
			cycloneHeight = 1.0f;
		}
		if(col.gameObject.tag=="Wind Barrier" && wbSw==false)
		{
			Destroy(col.gameObject);
			Player_Move com = _player.GetComponent<Player_Move>();
			com.invincibleSw=false;
			wbSw=true;
			cantmoveCount = 10;
		}
		if(col.gameObject.tag=="FirePillar")
		{
			Enemy_Hp eHP = _EnemyHp.GetComponent<Enemy_Hp>();
			eHP.hpBar.value-=eHP.maxHp*0.05f;
			skillCool[1] -= skillWaitTime[1]/10;
			skillCool[2] -= skillWaitTime[2]/10;
			skillCool[3] -= skillWaitTime[3]/10;
			skillCool[5] -= skillWaitTime[5]/10;
			skillCool[0] -= skillWaitTime[0]/10;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag=="Blaze")
		{
			Enemy_Hp eHP = _EnemyHp.GetComponent<Enemy_Hp>();
			eHP.damage = eHP._damage;
			maxSpeed = defaultMaxSpeed;
		}
		if(col.gameObject.tag=="FirePillar")
		{
			maxSpeed = defaultMaxSpeed;
			accel = defaultAccel;
		}
		if(col.gameObject.tag == "IceFog")
		{
			fogSw=false;
		}
		if(col.gameObject.tag == "IceField")
		{
			icesturnSw=false;
			icefieldSw=false;
		}
	}

	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.tag == "IceFog")
		{
			fogSw=true;
		}
		if(col.gameObject.tag == "Blaze")
		{
			Enemy_Hp eHP = _EnemyHp.GetComponent<Enemy_Hp>();
			eHP.damage = eHP._damage*1.1f;
			maxSpeed = defaultMaxSpeed*1.5f;
		}
		if(col.gameObject.tag=="FirePillar")
		{
			Enemy_Hp eHP = _EnemyHp.GetComponent<Enemy_Hp>();
			eHP.hpBar.value-=eHP.maxHp*0.001f;
			maxSpeed = defaultMaxSpeed*1.05f;
			accel = defaultAccel*1.05f;
		}
		if(col.gameObject.tag == "Cyclone" && cycloneSw==false)
		{
			cycloneSw=true;
			cycloneHeight = 1.0f;
		}
		if(col.gameObject.tag == "IceField" && icesturnSw==false)
		{
			icefieldSw=true;
		}
	}

	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;

		for (int i=0; i<6; ++i)
			--skillCool [i];
		if(sturnSw==true)
		{
			--cantmoveCount;
			if(cantmoveCount==0)
			{
				//skillSw=0;
				maxSpeed=defaultMaxSpeed;
				accel=defaultAccel;
				fricForce=drfaultFricForce;
				speed_x=0;
				speed_y=0;
				sturnSw=false;
				wispSw=false;
			}
		}
		else if(bubbleSw==true)
		{
			--cantmoveCount;
			if(cantmoveCount>0)
			{
				transform.position = _bubble.transform.position;
			}
			else if(cantmoveCount==0)
			{
				transform.GetComponent<Rigidbody>().useGravity=true;
			}
		}
		else if(mineSw==true)
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
			transform.Translate (Vector3.left*0.8f);
			--cantmoveCount;
			if(cantmoveCount==0)
			{
				wbSw=false;
				speed_x=0;
				speed_y=0;
			}
		}
		else if(fearSw==true)
		{
			Debug.Log(fearSw);
			transform.Translate(Vector3.left*Mathf.Sqrt(speed_x*speed_x+speed_y*speed_y));
			--fearCount;
			if(fearCount==0)
			{
				fearSw=false;
			}
		}
		else if(wispSw)
		{
			if(cantmoveCount%30 == 0)
			{
				transform.Rotate (0,(Random.Range (0,7)*45),0);
			}
			transform.Translate (Vector3.right*maxSpeed);
			--cantmoveCount;
			if(cantmoveCount==0)
			{
				wispSw=false;
			}
		}
		else
		{
			for(int i=5;i>0;--i)
				if(skillSw==0)
				{
					if(skillCool[i]<=0)
					{
						if(i==5 && _level == 2)
							skillSw=-1;
						else if(i!=5)
							skillSw=i;
						skillCool[i]=skillWaitTime[i];
					}
				}
			com.ActSkill(skillSw);
		}
        //agent.destination = _target.position;
	}

/*	void DefaultAct(bool pasSw)
	{
		direction = _player.transform.position - transform.position;
		angle = Mathf.Atan2 (direction.z, direction.x);
		if (pasSw==true) 
		{
			if(Mathf.Abs (preAngle*Mathf.Rad2Deg - angle*Mathf.Rad2Deg)>60)
			{
				Debug.Log ("Drift");
				speed_x/=2;
				speed_y/=2;
			}
		}
		speed_x += _accel*Mathf.Cos (angle);
		speed_y += _accel*Mathf.Sin (angle);
		if(speed_x>0)
		{
			speed_x-=_fricForce;
		}
		else if(speed_x<0)
		{
			speed_x+=_fricForce;
		}
		if(speed_y>0)
		{
			speed_y-=_fricForce;
		}
		else if(speed_y<0)
		{
			speed_y+=_fricForce;
		}
		if (Mathf.Abs (speed_x) > Mathf.Abs(maxSpeed*Mathf.Cos (angle))) 
		{
			speed_x -= _accel*Mathf.Cos (angle);
		}
		if (Mathf.Abs (speed_y) > Mathf.Abs (maxSpeed*Mathf.Sin (angle))) 
		{
			speed_y -= _accel*Mathf.Sin (angle);
		}
		preAngle = angle;
	}
	void ActSkill1()
	{
		--count;
		if(count==0)
		{
			skillSw=0;
			speed_x=maxSpeed*Mathf.Cos (angle);
			speed_y=maxSpeed*Mathf.Sin (angle);
		}
	}
	void ActSkill2()
	{
		--count;
		if(count==0)
		{
			skillSw=0;
			maxSpeed=_maxSpeed;
		}
		DefaultAct ();
	}
	void LeaderSkill()
	{

	}*/
}
