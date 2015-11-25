using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player_Move : MonoBehaviour {
	
	public GameObject _mainCamera;
	public GameObject button;
	private int touchState=0;
	public float defaultMaxSpeed=0.187f;

	public int playerHp=3;
	
	public float maxSpeed;
	public float tempX;
	public float tempY;
	public float speed_x;
	public float speed_y;
	public float angle = 0;
	public int touchIndex=0;
	public int hitCount = -1;
	public bool invincibleSw = false; // 무적스위치
	public bool pheonixSw = false;
	public bool silentSw = false;

	private Wind wind;
	private Fire fire;
	private Ice ice;
	private Earth earth;
	
	private float mineHeight;
	private bool mineSw=false;
	private bool cycloneSw = false;
	private bool hitSw=false;
	private bool icefieldSw = false;
//	private bool iceSturnSw = false;
	private bool wispSw = false;
	private int wispCount = 0;
	private Vector3 hitDir;
	private float hitAngle;
	private float cycloneHeight;
	
	private float posX;
	private float posY;
	
	private int sturnCount=0;

	void Start () {
		Application.targetFrameRate = 60;
		Time.captureFramerate = 60;
		maxSpeed = defaultMaxSpeed;
		speed_x = 0.0f;
		speed_y = 0.0f;
		button.transform.position = new Vector3 ((float)Screen.width + 75.0f, (float)Screen.height + 75.0f,0.0f );
		wind = GetComponent<Wind> ();
		fire = GetComponent<Fire> ();
		ice = GetComponent<Ice> ();
		earth = GetComponent<Earth> ();
	}
	
	public void ActiveSkilled(int sw)
	{
		if (silentSw == true)
			return;
		if (Variables.skillType == "Fire") 
		{
			if(sw==5)
			{
				fire.Pheonix();
				return;
			}
			fire.SkillCheck(Variables.skillEquipped[0,sw-1]);
		}
		else if(Variables.skillType=="Ice")
		{
			if(sw==5)
			{
				ice.WeatherIsRain();
				return;
			}
			ice.SkillCheck(Variables.skillEquipped[1,sw-1]);
		}
		else if(Variables.skillType=="Wind")
		{
			if(sw==5)
			{
				wind.SyungSyung();
				return;
			}
			wind.SkillCheck(Variables.skillEquipped[2,sw-1]);
		}
	}

	bool isInvincible() // 플레이어가 무적상태인지 알려주는 function
	{
		if (hitCount != -1)
			return true;
		else if (invincibleSw == true)
			return true;
		else if (cycloneSw == true)
			return true;
		else if (mineSw == true)
			return true;
		return false;
	}

	bool isMoveable() // 플레이어가 움직일 수 있는 상태인지 알려주는 function
	{
		if (hitSw == true)
			return false;
		else if (cycloneSw == true)
			return true;
		else if (mineSw == true)
			return false;
		else if (wispSw == true)
			return false;
		return true;
	}

	void OnCollisionEnter(Collision coll)
    {
        int i=0;
        if (coll.gameObject.tag == "circleEnemy" && isInvincible() == false)
        {
            Debug.Log("장판 데미지");
            if (coll.gameObject.GetComponent<DropCircle>().dropLevel == 0)
                i = 0;
            else if (coll.gameObject.GetComponent<DropCircle>().dropLevel == 1)
                i = 1;
            else if (coll.gameObject.GetComponent<DropCircle>().dropLevel == 2)
                i = 2;
            switch (i)
            {
                case 0:
                    --playerHp;
                    break;
                case 1:
                    playerHp -= 2;
                    break;
                case 2:
                    playerHp -= 3;
                    break;
            }
            hitCount = 0;
            hitDir = coll.gameObject.transform.position - transform.position;
            hitAngle = Mathf.Atan2(hitDir.z, hitDir.x);
            speed_x = 0;
            speed_y = 0;
            hitSw = true;
        }
        if (coll.gameObject.tag == "Enemy" && isInvincible() == false)
        {
            if (PlayerPrefs.GetInt("isVibrate") == 1)
                Handheld.Vibrate();
            switch (coll.gameObject.GetComponent<Enemy_Move>()._level)
            {
                case -1:
                    break;
                case 0:
                    --playerHp;
                    break;
                case 1:
                    playerHp -= 2;
                    break;
                case 2:
                    playerHp -= 3;
                    break;
            }
            hitCount = 0;
            hitDir = coll.gameObject.transform.position - transform.position;
            hitAngle = Mathf.Atan2(hitDir.z, hitDir.x);
            speed_x = 0;
            speed_y = 0;
            hitSw = true;
        }
        if (coll.gameObject.tag == "miniEnemy" && isInvincible()==false) // 미니에너미와 충돌체크 
		{
			if(PlayerPrefs.GetInt("isVibrate")==1)
				Handheld.Vibrate();
			--playerHp;
			hitCount = 0;
		}
		if(coll.gameObject.tag=="miniBoar" && isInvincible()==false)
		{
			sturnCount=90;
		}
		if(coll.gameObject.tag == "Mine" && isInvincible()==false) // 마인과 충돌체크
		{
			if(PlayerPrefs.GetInt("isVibrate")==1)
				Handheld.Vibrate();
			--playerHp;
			Destroy (coll.gameObject);
			mineHeight=1.0f;
			mineSw=true;
		}
		if(mineSw==true && coll.gameObject.name=="Floor") // 마인으로 공중에 뜬 후에 바닥과 충돌했는지 체크
		{
			speed_x=0;
			speed_y=0;
			mineSw=false;
		}
		if(cycloneSw == true && coll.gameObject.name=="Floor") // 싸이클론으로 공중에 뜬 후에 바닥과 충돌했는지 체크
		{
			speed_x=0;
			speed_y=0;
			cycloneSw=false;
		}
		if(hitSw==true && coll.gameObject.tag=="Wall") // 맞아서 날아가는 도중 벽이랑 부딪쳤는지 체크
			hitSw=false;
/*		if(icefieldSw==true && coll.gameObject.tag=="Wall") // 발얼리기 중 벽과 충돌했는지 체크
		{
			iceSturnSw=true;
			icefieldSw=false;
			speed_x=0;
			speed_y=0;
			if(ice.iceSw==5)
			{
				sturnCount=0;
				ice.iceSw=-1;
			}
			else
				sturnCount=120;
		}*/
		if (coll.gameObject.tag == "FireWisp" && isInvincible()==false) 
		{
			Destroy (coll.gameObject);
			wispSw=true;
			wispCount=120;
			speed_x=0;
			speed_y=0;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag=="FirePillar" && isInvincible()==false)
		{
			if(PlayerPrefs.GetInt("isVibrate")==1)
				Handheld.Vibrate();
			--playerHp;
			hitCount = 0;
		}
		if(col.gameObject.tag=="Scart" && isInvincible()==false)
		{
			if(PlayerPrefs.GetInt("isVibrate")==1)
				Handheld.Vibrate();
			--playerHp;
			hitCount = 0;
			hitDir = col.gameObject.transform.position - transform.position;
			hitAngle = Mathf.Atan2(hitDir.z, hitDir.x);
			speed_x = 0;
			speed_y = 0;
			hitSw = true;
		}
		if(col.gameObject.tag == "wolfArea")
		{
			if(PlayerPrefs.GetInt("isVibrate")==1)
				Handheld.Vibrate();
			hitCount = 0;
			hitDir = col.gameObject.transform.position - transform.position;
			hitAngle = Mathf.Atan2(hitDir.z, hitDir.x);
			speed_x = 0;
			speed_y = 0;
			hitSw = true;
			--playerHp;
		}
		if (col.gameObject.tag == "IceWave") 
			icefieldSw=true;
	}
	
	void OnTriggerExit(Collider col)
	{
		/*if(col.gameObject.tag == "IceField")
		{
			iceSturnSw=false;
			icefieldSw=false;
		}
		*/
		if (col.gameObject.tag == "IceWave")
			icefieldSw = false;
	}
	
	void OnTriggerStay(Collider col)
	{
		if(col.gameObject.tag == "Cyclone" && isInvincible()==false)
		{
			cycloneSw=true;
			cycloneHeight = 1.0f;
		}
		if(col.gameObject.tag=="Scart" && isInvincible() == false)
		{
			--playerHp;
			hitCount = 0;
		}
		/*if(col.gameObject.tag == "IceField" && iceSturnSw==false)
			icefieldSw=true;*/
	}
	
	void OnCollisionStay(Collision coll)
	{
		if (coll.gameObject.tag == "Enemy" && isInvincible()==false)
		{
			if(PlayerPrefs.GetInt("isVibrate")==1)
				Handheld.Vibrate();
			switch(coll.gameObject.GetComponent<Enemy_Move>()._level)
			{
			case 0:
				--playerHp;
				break;
			case 1:
				playerHp-=2;
				break;
			case 2:
				playerHp-=3;
				break;
			}
			hitCount=0;
			hitDir = coll.gameObject.transform.position-transform.position;
			hitAngle = Mathf.Atan2(hitDir.z,hitDir.x);
			speed_x=0;
			speed_y=0;
			hitSw=true;
		}
	}

	void Update () 
	{
		if(Time.timeScale==0)
			return;
		if (sturnCount > 0) 
		{
			--sturnCount;
			return;
		}
		if(hitCount!=-1 && hitSw==false)
		{
			++hitCount;
			if(hitCount==60)
				hitCount=-1;
		}
		if (touchState == 0 && icefieldSw==false) 
		{
			speed_x=0;
			speed_y=0; // 가속도 마찰력 X
		}
		if(Application.platform == RuntimePlatform.Android)
		{
			touchChecked();
			_mainCamera.transform.position = new Vector3 (transform.position.x, 8.5f+transform.position.y, transform.position.z - 5.0f);
		}
		playerMoved ();
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
		else if(hitSw==true)
		{
			++hitCount;
			transform.position = new Vector3(transform.position.x-Mathf.Cos (hitAngle)*0.7f,transform.position.y,transform.position.z-Mathf.Sin(hitAngle)*0.7f);
			if(hitCount==10)
				hitSw=false;
		}
		else if(wispSw)
		{
			if(wispCount%30 == 0)
				transform.Rotate (0,(Random.Range (0,7)*45),0);
			transform.Translate (Vector3.right*maxSpeed);
			--wispCount;
			if(wispCount==0)
				wispSw=false;
		}
		else if (Application.platform == RuntimePlatform.Android) 
		{
			posX = Input.GetTouch(touchIndex).position.x;
			posY = Input.GetTouch(touchIndex).position.y;
			if (touchState == 1 && Input.touchCount > 0) 
			{
				angle = Mathf.Atan2 (posY - tempY, posX - tempX);
				changeSpeed();
			}
		} 
		else 
		{
			posX = Input.mousePosition.x;
			posY = Input.mousePosition.y;
			if (Input.GetMouseButtonDown (0) && posX < (float)Screen.width / 2) 
			{
				tempX = posX;
				tempY = posY;
				touchState = -1;
				button.transform.position = new Vector3 (tempX, tempY, 0.0f);
			}
			else if (touchState == -1 && (posX != tempX || posY != tempY)) 
				touchState = 1;
			if (touchState == 1 && Input.GetMouseButton (0)) 
			{
				angle = Mathf.Atan2 (posY - tempY, posX - tempX);
				changeSpeed();
			}
			else 
			{
				if (touchState == 1) 
				{
					touchState = 0;
					button.transform.position = new Vector3 ((float)Screen.width + 100.0f, 0.0f, (float)Screen.height + 100.0f);
				}
			}
		}
	}
	
	void touchChecked()
	{
		if (Input.touchCount == 0) 
			return;
		for(int i=0;i<Input.touchCount;++i)
		{
			posX = Input.GetTouch (i).position.x;
			posY = Input.GetTouch (i).position.y;
			if (Input.GetTouch (i).phase == TouchPhase.Began) 
			{
				if(touchState==0 && posX < (float)Screen.width / 2)
				{
					touchState = -1;
					tempX = posX;
					tempY = posY;
					touchIndex=i;
					button.transform.position = new Vector3 (tempX, tempY, 0.0f);
					break;
				}
				else if(i<=touchIndex)
					++touchIndex;
			}
		}
		if (Input.GetTouch (touchIndex).phase == TouchPhase.Moved && touchState == -1) 
			touchState = 1;
		if (Input.GetTouch (touchIndex).phase == TouchPhase.Ended) 
		{
			if (touchState == 1 || touchState==-1) 
			{
				touchState = 0;
				button.transform.position = new Vector3 ((float)Screen.width + 75.0f, 0.0f, (float)Screen.height + 75.0f);
				touchIndex=0;
				return;
			}
		}
		for(int i=0;i<touchIndex;++i)
		{
			if(Input.GetTouch(i).phase == TouchPhase.Ended)
			{
				--touchIndex;
				return;
				--i;
			}
		}
	}
	
	void playerMoved()
	{
		if ((speed_y != 0 || speed_x != 0) && isMoveable()==true) 
		{
			transform.rotation = Quaternion.Euler(0.0f, (-1 * Mathf.Atan2(speed_y, speed_x) * Mathf.Rad2Deg), 0.0f);
			transform.position = new Vector3(transform.position.x + speed_x, transform.position.y, transform.position.z + speed_y);
		}
		_mainCamera.transform.position = new Vector3 (transform.position.x, 8.5f+transform.position.y, transform.position.z - 5.0f);
	}
	
	void changeSpeed()
	{
		if(mineSw==false && icefieldSw==false)
		{
			speed_x=maxSpeed*Mathf.Cos(angle);
			speed_y=maxSpeed*Mathf.Sin (angle); //가속도 마찰력 X
		}
	}
}