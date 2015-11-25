using UnityEngine;
using System.Collections;

public class Earth : MonoBehaviour {

	public GameObject _player;
	public GameObject _enemy;
	public GameObject fake;
	public Player_Move com;
	private int medusaCount =-1;

    public int cool;
    private int cooltime;
	
	// Use this for initialization
	void Start () {
        cool = 0;
        cooltime = 30;
	}

	// Update is called once per frame
	void Update () {
        if (medusaCount!=-1) 
		{
			--cool;
			++medusaCount;
			//Medusa ();
		}
	}

/*    public void Medusa()
    {
        if (medusaCount == -1)
        {
            cool = cooltime * Time.captureFramerate; //쿨타임 초기화
            com.speed_x = 0;
            com.speed_y = 0;
//            com.accel = 0;
//           com.fricForce = 0;
			medusaCount = 0;
        }
        if (medusaCount == 60 || medusaCount == 120)
        {
            Enemy_Move ene = _enemy.GetComponent<Enemy_Move>();
            Instantiate(fake, new Vector3(Random.Range(-15.0f, 15.0f), 0.5f, Random.Range(-8.0f, 8.0f)), Quaternion.identity);
            ene._player = GameObject.Find("Fake(Clone)");
        }
        else if (medusaCount == 150)
        {
//            com.accel = com.defaultAccel;
//            com.fricForce = com.defaultFricForce;
            com.windBarrierSw = 0;
            Enemy_Move ene = _enemy.GetComponent<Enemy_Move>();
            ene._player = _player;
        }
        else if (medusaCount == cooltime * Time.captureFramerate) //잘 안 돼서 count를 cool 대신 사용
        {
            medusaCount = -1;
        }
    }*/
}
