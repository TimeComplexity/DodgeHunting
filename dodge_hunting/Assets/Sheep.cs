using UnityEngine;
using System.Collections;

public class Sheep : Enemy
{

    public GameObject _enemy;

    public Player_Move user;
    public Enemy_Move com;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void setCool()
    {
        com = _enemy.GetComponent<Enemy_Move>();
        user = com._player.GetComponent<Player_Move>();
        com.maxSpeed = 0.187f;
        com.accel = 0.006f;
        com.fricForce = 0.003f;
    }
    public override void ActSkill(int skillSw)
    {
        DefaultAct(false);
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
        com.transform.position = new Vector3(com.transform.position.x + com.speed_x, com.transform.position.y, com.transform.position.z + com.speed_y);
    }
}
