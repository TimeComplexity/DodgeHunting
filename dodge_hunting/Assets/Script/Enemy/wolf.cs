using UnityEngine;
using System.Collections;

public class wolf : Enemy {
    public GameObject _enemy;
    public GameObject miniWolf;
    public GameObject defaultMoon;
    public GameObject moon;
    public GameObject defaultHowling;
    public GameObject howling;
    public GameObject defaultScart;
    public GameObject scart;
    public GameObject defaultWolfArea;
    public GameObject wolfArea;

    public Player_Move user;
    public Enemy_Move com;

    public int redMoonCount=0;
    public string wolfSkill;
    public string moonSkill;
    public int moonCount = 0;
    public int wolfAreaCount = 0;
    private int wolfCount = 0;
    public int howlingCount = 0;

    

    float scartSpeed_x;
    float scartSpeed_y;
    Vector3 scartDirection;
    float scartAngle;

    // Update is called once per frame
    void Update()
    {
        howlingCount++;

        if (wolfSkill == "moon")
        {
            if (moonCount == 0)
            {
                moon.transform.position = new Vector3(moon.transform.position.x - 0.1f, 3, 0);
                this.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                if(moon.transform.position.x < 0)
                {
                    moonCount = 2;
                }
            }
            else if (moonCount == 2)
            {
                if (moonSkill == "redMoon")
                {
                    moon.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 100f / 255f);
                    user.silentSw = true;
                    Debug.Log("침묵");
                    redMoonCount++;
                    if (redMoonCount > 300)
                    {
                        moonCount = 1;
                        redMoonCount = 0;
                        moon.GetComponent<MeshRenderer>().material.color = new Color(10f / 255f, 10f / 255f, 10f / 255f, 150f / 255f);
						user.silentSw=false;
                        return;
                    }
                }
            }
            else if(moonCount == 1)
            {
                moon.transform.position = new Vector3(moon.transform.position.x + 0.1f, 3, 0);
                this.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
                if (moon.transform.position.x > 40f)
                {
                    moonCount = 0;
                }
            }
        }
    }

    public override void setCool()
	{
		com = _enemy.GetComponent<Enemy_Move>();
		user = com._player.GetComponent<Player_Move>();

        skillWaitTime[1] = 50000; // 만월 / 적월
		skillWaitTime[2] = 2500; // 영역표시
		skillWaitTime[3] = 300; // 할퀴기
        skillWaitTime[4] = 700; // 하울링
        skillWaitTime[5] = 2200;
		skillWaitTime[0] = 50000;
		
        if(this._enemy == Variables.enemy1)
        {
            moon = Instantiate(defaultMoon);
            this.howling = (GameObject)Instantiate(this.defaultHowling, new Vector3(0, -3, 0), Quaternion.identity);
            skillWaitTime[1] = 500;
        }
        else if (this._enemy == Variables.enemy2)
        {
            this.howling = (GameObject)Instantiate(this.defaultHowling, new Vector3(0, -3, 0), Quaternion.identity);
            skillWaitTime[1] = 50000;
            skillWaitTime[2] = 50000;
        }

		for(int i = 0; i<6;++i)
			com.skillWaitTime[i]=skillWaitTime[i];

		for (int i=0; i<5; ++i)
			com.skillCool [i] = Random.Range (0, skillWaitTime [i]);
		com.skillCool[5] = skillWaitTime[5];

        com.defaultMaxSpeed = 0.225f;
		com.defaultAccel=0.012f;
		com.drfaultFricForce=0.002f;
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

    // 기본 움직임 수정 완료
    public void DefaultAct (bool pasSw)
    {
        // 패시브 스킬 능력치 상승
        if (pasSw)
        {
            if(com.maxSpeed < com.defaultMaxSpeed*1.5f)
            {
                com.maxSpeed = com.maxSpeed * 1.01f;
                com.accel = com.accel * 1.01f;
            }
        }

        if (this._enemy == Variables.enemy1)
        {
            com.direction = com._player.transform.position - this._enemy.transform.position;
        }
        else if (this._enemy == Variables.enemy2)
        {
            com.direction.x = com._player.transform.position.x + user.speed_x * 60 - this._enemy.transform.position.x;
            com.direction.z = com._player.transform.position.z + user.speed_y * 60 - this._enemy.transform.position.z;
        }

        com.angle = Mathf.Atan2(com.direction.z, com.direction.x);
        com.speed_x += com.accel * Mathf.Cos(com.angle);
        com.speed_y += com.accel * Mathf.Sin(com.angle);

        if (com.speed_x > 0)
        {
            com.speed_x -= com.fricForce;
        }
        else if (com.speed_x < 0)
        {
            com.speed_x += com.fricForce;
        }
        if (com.speed_y > 0)
        {
            com.speed_y -= com.fricForce;
        }
        else if (com.speed_y < 0)
        {
            com.speed_y += com.fricForce;
        }
        if (Mathf.Abs(com.speed_x) > Mathf.Abs(com.maxSpeed * Mathf.Cos(com.angle)))
        {
            com.speed_x -= com.accel * Mathf.Cos(com.angle);
        }
        if (Mathf.Abs(com.speed_y) > Mathf.Abs(com.maxSpeed * Mathf.Sin(com.angle)))
        {
            com.speed_y -= com.accel * Mathf.Sin(com.angle);
        }
        com.transform.rotation = Quaternion.Euler(0.0f, -1 * Mathf.Atan2(com.speed_y, com.speed_x) * Mathf.Rad2Deg, 0.0f);
        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y, com.transform.position.z + com.speed_y);
    }

    // 보름달
    public void ActSkill1()
    {
        Debug.Log("스킬1");
        wolfSkill = "moon";
        com.skillSw = 0;
		skillCount [1] = -1;
        com.skillCool[1] = 120;
        if(moon.transform.position.x < 0)
        {
            moonSkill = "redMoon";
        }
        DefaultAct(false);
    }

    // 영역 표시
    public void ActSkill2()
    {
        if (wolfAreaCount == 0)
        {
            wolfArea = (GameObject)Instantiate(defaultWolfArea, new Vector3(16, 0, 9), Quaternion.identity);
            wolfArea.tag = "wolfArea";
            wolfArea = (GameObject)Instantiate(defaultWolfArea, new Vector3(16, 0, -9), Quaternion.identity);
            wolfArea.tag = "wolfArea";
            wolfArea = (GameObject)Instantiate(defaultWolfArea, new Vector3(-16, 0, -9), Quaternion.identity);
            wolfArea.tag = "wolfArea";
            wolfArea = (GameObject)Instantiate(defaultWolfArea, new Vector3(-16, 0, 9), Quaternion.identity);
            wolfArea.tag = "wolfArea";
            wolfAreaCount = 1;
        }
        com.skillSw = 0;
        skillCount[2] = -1;
        DefaultAct(false);
    }

    // 할퀴기 진행방향으로 발사
    public void ActSkill3()
    {
        Debug.Log("스킬3");
		if (skillCount[3] == 0)
        {
            scartDirection = com._player.transform.position - this._enemy.transform.position;
            scartAngle = Mathf.Atan2(scartDirection.z, scartDirection.x);
            scartSpeed_x = Mathf.Cos(scartAngle);
            scartSpeed_y = Mathf.Sin(scartAngle);
            scart = (GameObject) Instantiate(defaultScart, new Vector3(com.transform.position.x + scartSpeed_x*2f, 0,
                com.transform.position.z + scartSpeed_y*2f), Quaternion.identity);
        }
		else if (skillCount[3] == 90)
        {
            Destroy(scart);
            com.skillSw = 0;
			skillCount[3] = -1;
        }
        DefaultAct(false);
    }

    public void ActSkill4()
    {
        Debug.Log("스킬4");
        if(howlingCount < 250)
        {
            if (this._enemy == Variables.enemy1)
            {
                this.howling.transform.position = this._enemy.transform.position;
            }
            else if (this._enemy == Variables.enemy2)
            {
                this.howling.transform.position = this._enemy.transform.position;
            }
        }
        else if(howlingCount > 250)
        {
            if (this._enemy == Variables.enemy1)
            {
                this.howling.transform.position = new Vector3(0, -3, 0);
                this.com.skillSw = 0;
				this.skillCount[4] = -1;
                howlingCount = 0;
            }
            else if (this._enemy == Variables.enemy2)
            {
                this.howling.transform.position = new Vector3(0, -3, 0);
                this.com.skillSw = 0;
				this.skillCount[4] = -1;
                howlingCount = 0;
            }
        }
        DefaultAct(false);


    }

    // 리더 스킬 미니 늑대 소환 (늑대 소환)
    public void LeaderSkill()
    {
        Debug.Log("대장스킬");

        if (wolfCount == 0) {
            Debug.Log("LeaderSkill");
            Variables.enemySwitch = 101;
            Instantiate(miniWolf, new Vector3(com.transform.position.x, com.transform.position.y, com.transform.position.z), Quaternion.identity);
            wolfCount++;
        }
        com.skillSw = 0;
		skillCount[5] = -1;
        DefaultAct(false);
    }
}