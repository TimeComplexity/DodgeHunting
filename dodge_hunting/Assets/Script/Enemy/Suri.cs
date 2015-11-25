using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Suri : Enemy {

    public GameObject player;
    public GameObject rader;
    public GameObject defaultRader;
    public GameObject enemy;

    public Enemy_Move com;
    public Player_Move user;
    // Use this for initialization
    void Start () {
        //rader = (GameObject)Instantiate(defaultRader, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
    }

	public override void setCool()
	{
        com = enemy.GetComponent<Enemy_Move>();
        user = com._player.GetComponent<Player_Move>();

        rader = (GameObject)Instantiate(defaultRader, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        skillWaitTime[0] = 300;
        skillWaitTime[1] = 500;
        skillWaitTime[2] = 1200;
        skillWaitTime[3] = 100000;
        skillWaitTime[4] = 100000;
        skillWaitTime[5] = 1200;

        for (int i = 0; i < 6; ++i)
            com.skillWaitTime[i] = skillWaitTime[i];

        for (int i = 0; i < 5; ++i)
            com.skillCool[i] = Random.Range(0, skillWaitTime[i]);
        com.skillCool[5] = skillWaitTime[5];

        com.defaultMaxSpeed = 0.175f;
        com.defaultAccel = 0.008f;
        com.drfaultFricForce = 0.001f;
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
        //Debug.Log (skillSw);
        switch (skillSw)
        {
            case -1:
                ++skillCount[5];
                LeaderSkill();
                break;
            case 1:
                ++skillCount[1];
                ActSkill1();
                break;
            case 2:
                ++skillCount[2];
                ActSkill2();
                break;
            case 3:
                ++skillCount[3];
                ActSkill3();
                break;
            case 4:
                ++skillCount[4];
                ActSkill4();
                break;
            default:
                if (com.skillCool[0] <= 0)
                {
                    DefaultAct(true);
                    com.skillCool[0] = com.skillWaitTime[0];
                }
                else
                    DefaultAct(false);
                break;
        }
    }

	public void DefaultAct(bool pasSw)
	{
        com.direction = com._player.transform.position - Variables.enemy1.transform.position;
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
        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, 3, com.transform.position.z + com.speed_y);
        rader.transform.position = new Vector3(com.transform.position.x, 1, com.transform.position.z);
    }
	public void ActSkill1() // Player Can't Use One Of Skills
	{
        Debug.Log("독수리 스킬 1");
        if(skillCount[1]==0)
        {
            com.direction = com._player.transform.position - Variables.enemy1.transform.position;
            com.angle = Mathf.Atan2(com.direction.z, com.direction.x);
            com.speed_x = 2 * Mathf.Cos(com.angle);
            com.speed_y = 2 * Mathf.Sin(com.angle);
            com.transform.rotation = Quaternion.Euler(0.0f, -1 * Mathf.Atan2(com.speed_y, com.speed_x) * Mathf.Rad2Deg, 0.0f);
            //com.transform.position = new Vector3(com.transform.position.x + com.speed_x, 3, com.transform.position.z + com.speed_y);
        }
        else if(skillCount[1] <120)
        {
            com.transform.position = new Vector3(com.transform.position.x + com.speed_x*2, 3, com.transform.position.z + com.speed_y*2);
            rader.transform.position = new Vector3(com.transform.position.x, 1, com.transform.position.z);
        }
        else if (skillCount[1] == 120)
        {
            com.direction = com._player.transform.position - Variables.enemy1.transform.position;
            com.angle = Mathf.Atan2(com.direction.z, com.direction.x);
            com.speed_x = 2 * Mathf.Cos(com.angle);
            com.speed_y = 2 * Mathf.Sin(com.angle);
            com.transform.rotation = Quaternion.Euler(0.0f, -1 * Mathf.Atan2(com.speed_y, com.speed_x) * Mathf.Rad2Deg, 0.0f);
            //com.transform.position = new Vector3(com.transform.position.x + com.speed_x * 2, 3, com.transform.position.z + com.speed_y * 2);
        }
        else if(skillCount[1] < 240)
        {
            if(com.transform.position.y > 0.5f)
            {
            com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y - 1, com.transform.position.z + com.speed_y);
            rader.transform.position = new Vector3(com.transform.position.x, 1, com.transform.position.z);
            }
            else
            {
                com.speed_x = 0.1f;
                com.speed_x = 0.1f;
                com.skillSw = 0;
                skillCount[1] = -1;
                DefaultAct(false);
                return;
            }
        }
        else if (skillCount[1]==240)
        {
            com.speed_x = 0.1f;
            com.speed_x = 0.1f;
            com.skillSw = 0;
            skillCount[1] = -1;
            DefaultAct(false);
        }
    }
	public void ActSkill2()
	{
        com.skillSw = 0;
        skillCount[2] = -1;
        DefaultAct(false);
    }
	public void ActSkill3()
	{
        com.skillSw = 0;
        skillCount[3] = -1;
        DefaultAct(false);
    }
	public void ActSkill4()
	{
        com.skillSw = 0;
        skillCount[4] = -1;
        DefaultAct(false);
    }
	public void LeaderSkill()
	{
        com.skillSw = 0;
        skillCount[5] = -1;
        DefaultAct(false);

    }
}
