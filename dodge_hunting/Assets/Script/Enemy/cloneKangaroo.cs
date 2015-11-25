using UnityEngine;
using System.Collections;

public class cloneKangaroo : Enemy
{
    public GameObject _enemy;
    public Player_Move user;
    public Enemy_Move com;
    public kangaroo kang;
    public GameObject dropCircle;
    public GameObject _dropCircle;

    public int count = 0;
    public int kangCount = 0;
    public string kangSkill = "non";

    bool jumpSw = false;
    bool jumpFirstSw = true;

    public override void setCool()
    {
        com = _enemy.GetComponent<Enemy_Move>();
        kang = GetComponent<kangaroo>();
        user = com._player.GetComponent<Player_Move>();

        skillWaitTime[1] = 500000;
        skillWaitTime[2] = 1200;
        skillWaitTime[3] = 720;
		skillWaitTime[4] = 100000;
        skillWaitTime[5] = 500000;
        skillWaitTime[0] = 500000;

        _dropCircle = (GameObject)Instantiate(dropCircle, new Vector3(-1, -1, -1), Quaternion.identity);

        com.skillWaitTime[1] = skillWaitTime[1];
        com.skillWaitTime[2] = skillWaitTime[2];
        com.skillWaitTime[3] = skillWaitTime[3];
		com.skillWaitTime[4] = skillWaitTime[4];
        com.skillWaitTime[0] = skillWaitTime[0];
        com.skillWaitTime[5] = skillWaitTime[5];

        com.skillCool[1] = Random.Range(skillWaitTime[1]/3, skillWaitTime[1]);
        com.skillCool[2] = Random.Range(skillWaitTime[2]/3, skillWaitTime[2]);
        com.skillCool[3] = Random.Range(skillWaitTime[3]/3, skillWaitTime[3]);
		com.skillCool[4] = skillWaitTime[4];
        com.skillCool[0] = Random.Range(skillWaitTime[0]/3, skillWaitTime[0]);
        com.skillCool[5] = skillWaitTime[5];

        // 기본능력치
        com.defaultMaxSpeed = 0.187f;
        com.defaultAccel = 0.006f;
        com.drfaultFricForce = 0.003f;

        if (com._level == 0) // child 능력치 80%
        {
            com.defaultMaxSpeed *= 0.8f;
            com.defaultAccel *= 0.8f;
            com.drfaultFricForce *= 0.8f;
        }
        else if (com._level == 2) // adult 능력치 130%
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

    public void DefaultAct(bool pasSw)
    {
        // 안개, 아이스필드 에서의 움직임
        /*
        if (com.fogSw == 0 && com.icefieldSw == false)
        {
            if (this._enemy == Variables.enemy1)
            {
                com.direction = com._player.transform.position - Variables.enemy1.transform.position;
            }
            if (this._enemy == Variables.enemy2)
            {
                com.direction.x = com._player.transform.position.x + user.speed_x * 60 - Variables.enemy2.transform.position.x;
                com.direction.z = com._player.transform.position.z + user.speed_y * 60 - Variables.enemy2.transform.position.z;
            }
        }
        */

        // 캥거루 기본 매커니즘
        // 총 60 프레임 1초 점프
        if (count < 60)
        {
            jumpSw = true;
        }
        else if (count > 60)
        {
            if (kangSkill != "kung")
            {
                jumpSw = false;
                count = 0;
            }
            else if (kangSkill == "kung" && count > 125) // count == 120
            {
                jumpSw = false;
                count = 0;
            }
        }

        // 일반 및 스킬 사용시 캥거루 점프
        if (jumpSw == false && count == 0) // 점프 하기 직전, 카운트 0 에서 방향과 속도 설정
        {
            KangVector();
        }
        else if (jumpSw == true)
        {
            if (kangSkill == "kick")
            {
                if (count < 30)
                {
                    KangVector();

                    if (com.transform.position.y < 5)
                    {
                        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y + 0.2f, com.transform.position.z + com.speed_y);
                    }
                }
                if (count > 30 && count < 35)
                {
                    com.direction = com._player.transform.position - this._enemy.transform.position;
                    com.angle = Mathf.Atan2(com.direction.z, com.direction.x);
                    com.transform.rotation = Quaternion.Euler(0.0f, -1 * com.angle * Mathf.Rad2Deg, 0.0f);
                }
                else if (30 < count && count < 60) // 내려오는 점프
                {
                    com.speed_x = 1.2f * Mathf.Cos(com.angle);
                    com.speed_y = 1.2f * Mathf.Sin(com.angle);
                    if (com.transform.position.y > 1)
                    {
                        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y - 0.3f, com.transform.position.z + com.speed_y);
                    }
                    if (count > 55)
                    {
                        kangSkill = "non";
                        com.skillSw = 0;
                        skillCount[2] = -1;
                    }
                }
                ++count;
            }
            else if (kangSkill == "kung")
            {
                if (count < 30) // 올라가는 스킬
                {
                    KangVector();
                    if (com.transform.position.y < 35)
                    {
                        com.transform.position = new Vector3(com.transform.position.x + com.speed_x * 2f, com.transform.position.y + 3f, com.transform.position.z + com.speed_y * 2f);
                    }
                    else if (com.transform.position.y > 35)
                    {
                        com.transform.position = new Vector3(com.transform.position.x + com.speed_x * 2f, com.transform.position.y, com.transform.position.z + com.speed_y * 2f);
                    }
                }
                if (count > 30)
                {
                    _dropCircle.transform.position = new Vector3(com.transform.position.x, 0, com.transform.position.z);
                }
                if (count > 30 && count < 60)
                {
                    if (com.transform.position.y > 2)
                    {
                        com.transform.position = new Vector3(com.transform.position.x, com.transform.position.y - 2f, com.transform.position.z);
                    }
                    else if (com.transform.position.y < 2)
                    {
                        com.transform.position = new Vector3(com.transform.position.x, 1, com.transform.position.z);
                    }

                    if (count > 55)
                    {
                        _dropCircle.tag = "circleEnemy";
                    }
                }
                else if (count > 115)
                {
                    _dropCircle.tag = "non";
                    kangSkill = "non";
                    com.skillSw = 0;
                    skillCount[3] = -1;
                    _dropCircle.transform.position = new Vector3(-1, -1, -1);
                }
                ++count;
            }
            else //(kangSkill == "nun")
            {
                com.direction = com._player.transform.position - this._enemy.transform.position;
                com.angle = Mathf.Atan2(com.direction.z, com.direction.x);
                com.transform.rotation = Quaternion.Euler(0.0f, -1 * com.angle * Mathf.Rad2Deg, 0.0f);
                if (count < 30) // 올라가는 스킬
                {
                    if (com.transform.position.y < 5)
                    {
                        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y + 0.2f, com.transform.position.z + com.speed_y);
                    }
                }
                else if (count > 30 && count < 60)
                {
                    if (com.transform.position.y > 1)
                    {
                        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y - 0.3f, com.transform.position.z + com.speed_y);
                    }
                }
                ++count;
            }
        }
    }

    public void KangVector()
    {
        com.direction = com._player.transform.position - this._enemy.transform.position;
        com.angle = Mathf.Atan2(com.direction.z, com.direction.x);
        com.speed_x = 0.2f * Mathf.Cos(com.angle);
        com.speed_y = 0.2f * Mathf.Sin(com.angle);
        com.transform.rotation = Quaternion.Euler(0.0f, -1 * Mathf.Atan2(com.speed_y, com.speed_x) * Mathf.Rad2Deg, 0.0f);
    }

    public void ActSkill1()
    {
        Debug.Log("클론 스킬1 " + count);
        kangSkill = "non";
        com.skillSw = 0;
        skillCount[1] = -1;
        DefaultAct(false);
    }

    // 킥복싱
    public void ActSkill2()
    {
        Debug.Log("클론 스킬2 " + count);
        if (kangSkill == "nonKick")
        {
            kangSkill = "kick";
            count = 0;
        }
        DefaultAct(false);
    }

    public void ActSkill3()
    {
        Debug.Log("클론 스킬3 " + count);
        // 피격 범위 확대
        if (kangSkill == "non")
        {
            kangSkill = "kung";
            count = 0;
        }
        DefaultAct(false);
    }

	public void ActSkill4()
	{
		DefaultAct (false);
		com.skillSw = 0;
		skillCount [4] = -1;
	}

    public void LeaderSkill()
    {
        Debug.Log("클론 대장스킬 " + count);
        kangSkill = "non";
        com.skillSw = 0;
		skillCount [5] = -1;
        DefaultAct(false);
    }
}