using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Suri : Enemy {

    public GameObject player;
    public GameObject rader;
    public GameObject defaultRader;
    public GameObject enemy;

    public Vector2 circleDir;
    public Vector2 circlePos;
    public Vector2 circleSpeed;
    public float circleAngle;
    //public int chaseCount;
    int raderCount = 0;
    int attackCount = 0;
    int returnCount = 0;
    int returnSw = 0;
    public Enemy_Move com;
    public Player_Move user;
    public Rader rad;
    // Use this for initialization
    void Start () {
        
    }
    void update ()
    {
        //raderMove();
    }

	public override void setCool()
	{
        com = enemy.GetComponent<Enemy_Move>();
        user = com._player.GetComponent<Player_Move>();
        rader = (GameObject)Instantiate(defaultRader, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        rad = rader.GetComponent<Rader>();

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

        this.transform.position = new Vector3(0, 5, 0);
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

    public void Radar()
    {
        if (rad.triggerState == 1) // 레이더가 플레이어를 추적할 때 플레이어의 좌표로 따라가면서 점점 크기가 작아짐
        {
            circlePos.x = user.transform.position.x;
            circlePos.y = user.transform.position.z;
            circleDir.x = circlePos.x - rader.transform.position.x;
            circleDir.y = circlePos.y - rader.transform.position.z;
            circleAngle = Mathf.Atan2(circleDir.y, circleDir.x);
            circleSpeed.x = 0.2f * Mathf.Cos(circleAngle);
            circleSpeed.y = 0.2f * Mathf.Sin(circleAngle);

            rader.transform.position = new Vector3(rader.transform.position.x + circleSpeed.x, 0, rader.transform.position.z + circleSpeed.y);

            // 레이더 크기 축소
            if (rader.transform.localScale.x < 2)
            {
                Debug.Log("레이더 축소 끝");
                return;
            }
            else if (rader.transform.localScale.x > 1)
            {
                rader.transform.localScale += new Vector3(-0.3f, 0, -0.3f);
            }

            /*
            if (rad.attackSw == 1)
            {
                rader.transform.position = new Vector3(-5, -5, -5);
                return;
            }
            if (this.transform.localScale.x > 11)
            {
                Debug.Log("레이더 확장 끝");
                return;
            }
            else if (this.transform.localScale.x < 9)
            {
                this.transform.localScale += new Vector3(0.7f, 0, 0.7f);
            }
            */
        }
        else if (rad.triggerState == 0) // 레이더가 플레이어를 추적하지 않을때 x는 -15부터 15까지 z는 -8부터 8까지 랜덤한 위치로 60프레임동안 이동
        {
            if (rad.transform.localScale.x < 10)
            {
                rader.transform.localScale += new Vector3(0.7f, 0, 0.7f);
                if (rader.transform.localScale.x > 10)
                {
                    return;
                }
            }

            raderCount++;
            if (raderCount == 1)
            {
                circlePos.x = Random.Range(-15f, 15f);
                circlePos.y = Random.Range(-8f, 8f);
                circleDir.x = circlePos.x - rader.transform.position.x;
                circleDir.y = circlePos.y - rader.transform.position.z;
                circleAngle = Mathf.Atan2(circleDir.y, circleDir.x);
                circleSpeed.x = 0.2f * Mathf.Cos(circleAngle);
                circleSpeed.y = 0.2f * Mathf.Sin(circleAngle);
            }
            else if (raderCount < 60)
            {
                if (rader.transform.position.x > circlePos.x - 0.3f && rader.transform.position.x < circlePos.x + 0.3f)
                {
                    if (rader.transform.position.z > circlePos.y - 0.3f && rader.transform.position.z < circlePos.y + 0.3f)
                    {
                        circlePos.x = Random.Range(-15f, 15f);
                        circlePos.y = Random.Range(-8f, 8f);
                    }
                }

                if (rader.transform.position.x < -14 || rader.transform.position.x > 14)
                {
                    if (rader.transform.position.x < -14)
                    {
                        circlePos.x = Random.Range(1f, 15f);
                        circlePos.y = Random.Range(-8f, 8f);
                    }
                    else if (rader.transform.position.x > 14)
                    {
                        circlePos.x = Random.Range(-15f, -1f);
                        circlePos.y = Random.Range(-8f, 8f);
                    }
                }
                else if (rader.transform.position.z < 7 || rader.transform.position.z > 7)
                {
                    if (rader.transform.position.z < -7)
                    {
                        circlePos.x = Random.Range(-15f, 15f);
                        circlePos.y = Random.Range(1f, 8f);
                    }
                    else if (rader.transform.position.z > 7)
                    {
                        circlePos.x = Random.Range(-15f, 15f);
                        circlePos.y = Random.Range(-8f, -1f);
                    }
                }
                circleDir.x = circlePos.x - rader.transform.position.x;
                circleDir.y = circlePos.y - rader.transform.position.z;
                circleAngle = Mathf.Atan2(circleDir.y, circleDir.x);
                circleSpeed.x = 0.2f * Mathf.Cos(circleAngle);
                circleSpeed.y = 0.2f * Mathf.Sin(circleAngle);
                rader.transform.position = new Vector3(rader.transform.position.x + circleSpeed.x, 0, rader.transform.position.z + circleSpeed.y);
            }
            else
            {
                raderCount = 0;
            }
        }
    }
	public void DefaultAct(bool pasSw)
	{
        Radar();
        /*
        if (rad.chaseSw == 1) // 레이더가 플레이어를 추적할 때 플레이어의 좌표로 따라가면서 점점 크기가 작아짐
        {
            circlePos.x = user.transform.position.x;
            circlePos.y = user.transform.position.z;
            circleDir.x = circlePos.x - rader.transform.position.x;
            circleDir.y = circlePos.y - rader.transform.position.z;
            circleAngle = Mathf.Atan2(circleDir.y, circleDir.x);
            circleSpeed.x = 0.2f * Mathf.Cos(circleAngle);
            circleSpeed.y = 0.2f * Mathf.Sin(circleAngle);
            rader.transform.position = new Vector3(rader.transform.position.x + circleSpeed.x, 0, rader.transform.position.z + circleSpeed.y);

            if (rad.attackSw == 1) // 크기가 완전히 작아져서 공격 준비 끝
            {
                rader.transform.position = new Vector3(-1, -10, -1);
                while(attackCount<60)
                {
                    attackCount++;
                    if (attackCount == 1)
                    {
                        com.direction = Vector3.Normalize(user.transform.position - com.transform.position);
                        com.angle = Mathf.Atan2(com.direction.z, com.direction.x);
                        com.speed_x = 0.4f * com.direction.x;
                        com.speed_z = 0.4f * com.direction.z;
                        com.speed_y = 0.4f * com.direction.y; // 각이 나오는 위치 주의
                    }
                    else if (attackCount < 60)
                    {
                        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y + com.speed_y, com.transform.position.z + com.speed_z);
                        if (com.transform.position.y < 0.5)
                        {
                            Debug.Log("공격 끝");
                            returnSw = 1;
                            attackCount = 0;
                            rad.attackSw = 0;
                        }
                    }
                    else if (attackCount == 60)
                    {
                        Debug.Log("공격 끝");
                        returnSw = 1;
                        rad.attackSw = 0;
                        attackCount = 0;
                    }
                }
                if (returnSw == 1)
                {
                    while (returnCount < 60)
                    {
                        returnCount++;
                        if (returnCount == 1)
                        {
                            com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y - 1.5f * com.speed_y, com.transform.position.z + com.speed_z);
                        }
                        else if (returnCount == 60)
                        {
                            Debug.Log("귀환끝");
                            com.transform.position = new Vector3(0, 5, 0);
                            //rad.attackSw = 0;
                            //attackCount = 0;
                            returnSw = 0;
                        }
                    }
                }
            }
            
            else if (rad.attackSw == 0)
            {
                rader.transform.position = new Vector3(Random.Range(-14, 14), 0.1f, Random.Range(-7, 7));
            }

        }
        else if (rad.chaseSw == 0) // 레이더가 플레이어를 추적하지 않을때 x는 -15부터 15까지 z는 -8부터 8까지 랜덤한 위치로 60프레임동안 이동
        {
            raderCount++;
            if (raderCount == 1)
            {
                circlePos.x = Random.Range(-15f, 15f);
                circlePos.y = Random.Range(-8f, 8f);
                circleDir.x = circlePos.x - rader.transform.position.x;
                circleDir.y = circlePos.y - rader.transform.position.z;
                circleAngle = Mathf.Atan2(circleDir.y, circleDir.x);
                circleSpeed.x = 0.2f * Mathf.Cos(circleAngle);
                circleSpeed.y = 0.2f * Mathf.Sin(circleAngle);
            }
            else if (raderCount < 60)
            {
                if (rader.transform.position.x > circlePos.x - 0.3f && rader.transform.position.x < circlePos.x + 0.3f)
                {
                    if (rader.transform.position.z > circlePos.y - 0.3f && rader.transform.position.z < circlePos.y + 0.3f)
                    {
                        circlePos.x = Random.Range(-15f, 15f);
                        circlePos.y = Random.Range(-8f, 8f);
                    }
                }

                if (rader.transform.position.x < -14 || rader.transform.position.x > 14)
                {
                    if (rader.transform.position.x < -14)
                    {
                        circlePos.x = Random.Range(1f, 15f);
                        circlePos.y = Random.Range(-8f, 8f);
                    }
                    else if (rader.transform.position.x > 14)
                    {
                        circlePos.x = Random.Range(-15f, -1f);
                        circlePos.y = Random.Range(-8f, 8f);
                    }
                }
                else if (rader.transform.position.z < 7 || rader.transform.position.z > 7)
                {
                    if (rader.transform.position.z < -7)
                    {
                        circlePos.x = Random.Range(-15f, 15f);
                        circlePos.y = Random.Range(1f, 8f);
                    }
                    else if (rader.transform.position.z > 7)
                    {
                        circlePos.x = Random.Range(-15f, 15f);
                        circlePos.y = Random.Range(-8f, -1f);
                    }
                }
                circleDir.x = circlePos.x - rader.transform.position.x;
                circleDir.y = circlePos.y - rader.transform.position.z;
                circleAngle = Mathf.Atan2(circleDir.y, circleDir.x);
                circleSpeed.x = 0.2f * Mathf.Cos(circleAngle);
                circleSpeed.y = 0.2f * Mathf.Sin(circleAngle);
                rader.transform.position = new Vector3(rader.transform.position.x + circleSpeed.x, 0, rader.transform.position.z + circleSpeed.y);
            }
            else
            {
                raderCount = 0;
            }
        }
        */
    }
    
	public void ActSkill1() // Player Can't Use One Of Skills
	{
        Debug.Log("독수리 스킬 1");
        com.skillSw = 0;
        skillCount[1] = -1;
        DefaultAct(false);
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
