using UnityEngine;
using System.Collections;

public class miniWolf : Enemy
{
    public GameObject _enemy;
    public GameObject defaultHowling;
    public GameObject howling;
    public GameObject defaultScart;
    public GameObject scart;

    public Player_Move user;
    public Enemy_Move com;

    float scartSpeed_x;
    float scartSpeed_y;
    Vector3 scartDirection;
    float scartAngle;

    public override void setCool()
    {
		com = _enemy.GetComponent<Enemy_Move>();
        user = com._player.GetComponent<Player_Move>();
        howling = (GameObject)Instantiate(defaultHowling, new Vector3(0, -3, 0), Quaternion.identity);

        skillWaitTime[1] = 50000; // 만월 / 적월
        skillWaitTime[2] = 50000; // 영역표시
        skillWaitTime[3] = 550; // 할퀴기
        skillWaitTime[4] = 1000; // 하울링
        skillWaitTime[5] = 50000;
        skillWaitTime[0] = 50000;

        com.skillWaitTime[1] = skillWaitTime[1];
        com.skillWaitTime[2] = skillWaitTime[2];
        com.skillWaitTime[3] = skillWaitTime[3];
        com.skillWaitTime[4] = skillWaitTime[4];
        com.skillWaitTime[0] = skillWaitTime[0];
        com.skillWaitTime[5] = skillWaitTime[5];

        com.skillCool[1] = Random.Range(0, skillWaitTime[1]);
        com.skillCool[2] = Random.Range(0, skillWaitTime[2]);
        com.skillCool[3] = Random.Range(0, skillWaitTime[3]);
        com.skillCool[4] = Random.Range(0, skillWaitTime[4]);
        com.skillCool[0] = Random.Range(0, skillWaitTime[0]);
        com.skillCool[5] = skillWaitTime[5];

        com.defaultMaxSpeed = 0.225f;
        com.defaultAccel = 0.012f;
        com.drfaultFricForce = 0.002f;
        if (com._level == 0)
        {
            com.defaultMaxSpeed *= 0.8f;
            com.defaultAccel *= 0.8f;
            com.drfaultFricForce *= 0.8f;
        }
        else if (com._level == 2)
        {
            com.defaultMaxSpeed *= 1.3f;
            com.defaultAccel *= 1.3f;
            com.drfaultFricForce *= 1.3f;
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
    public void DefaultAct(bool pasSw)
    {
        // 패시브 스킬 능력치 상승
        if (pasSw)
        {
            if (com.maxSpeed < com.defaultMaxSpeed * 1.5f)
            {
                com.maxSpeed = com.maxSpeed * 1.01f;
                com.accel = com.accel * 1.01f;
            }
        }

        com.direction = com._player.transform.position - this._enemy.transform.position;

        com.angle = Mathf.Atan2(com.direction.z, com.direction.x);
        com.speed_x += com.accel * Mathf.Cos(com.angle);
        com.speed_z += com.accel * Mathf.Sin(com.angle);

        if (com.speed_x > 0)
        {
            com.speed_x -= com.fricForce;
        }
        else if (com.speed_x < 0)
        {
            com.speed_x += com.fricForce;
        }
        if (com.speed_z > 0)
        {
            com.speed_z -= com.fricForce;
        }
        else if (com.speed_z < 0)
        {
            com.speed_z += com.fricForce;
        }
        if (Mathf.Abs(com.speed_x) > Mathf.Abs(com.maxSpeed * Mathf.Cos(com.angle)))
        {
            com.speed_x -= com.accel * Mathf.Cos(com.angle);
        }
        if (Mathf.Abs(com.speed_z) > Mathf.Abs(com.maxSpeed * Mathf.Sin(com.angle)))
        {
            com.speed_z -= com.accel * Mathf.Sin(com.angle);
        }
        com.transform.rotation = Quaternion.Euler(0.0f, -1 * Mathf.Atan2(com.speed_z, com.speed_x) * Mathf.Rad2Deg, 0.0f);
        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y, com.transform.position.z + com.speed_z);
    }

    // 보름달
    public void ActSkill1()
    {
        Debug.Log("스킬1");
        com.skillSw = 0;
        skillCount[1] = -1;
        DefaultAct(false);
    }

    // 영역 표시
    public void ActSkill2()
    {
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
            scart = (GameObject)Instantiate(defaultScart, new Vector3(com.transform.position.x + scartSpeed_x * 2f, 0,
                com.transform.position.z + scartSpeed_y * 2f), Quaternion.identity);
        }
		else if (skillCount[3] == 90)
        {
            Destroy(scart);
            com.skillSw = 0;
            skillCount[3] = -1;
        }
        DefaultAct(false);
    }

    // 하울링
    public void ActSkill4()
    {
		if (skillCount[4] < 300)
        {
            Debug.Log("스킬4");
            howling.transform.position = _enemy.transform.position;
        }
		else if (skillCount[4] == 300)
        {
            howling.transform.position = new Vector3(0, -3, 0);
            com.skillSw = 0;
            skillCount[4] = -1;
        }
        DefaultAct(false);
    }

    // 리더 스킬 미니 늑대 소환 (늑대 소환)
    public void LeaderSkill()
    {
        com.skillSw = 0;
		skillCount[5] = -1;
        DefaultAct(false);
    }
}