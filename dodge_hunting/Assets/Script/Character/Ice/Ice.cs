using UnityEngine;
using System.Collections;

public class Ice : MonoBehaviour {
	public GameObject _player;
	public GameObject _Enemy;
	public GameObject iceWall;
	//public GameObject _iceShadow;
	public GameObject _iceFog;
//	public GameObject _iceField;
	public GameObject _iceBomb;
	public GameObject _iceWave;
	public GameObject _bubble;
	public Player_Move com;
    public int[] cool;
	public bool iceSw=false;

    private int[] cooltime;
	public bool[] beStrongSw = new bool[4];
//	private int shadowCount = -1;
//	private GameObject shadow1;
//	private GameObject shadow2;
	
	// Use this for initialization
	void Start () {
		cool = new int[5];
		for (int i=0; i<5; ++i) 
			cool [i] = 0;
		for(int i=0;i<4;++i)
			beStrongSw[i] = false;
		cooltime = new int[5];
		cooltime [0] = 30;
		cooltime [1] = 20;
		cooltime [2] = 30;
		cooltime [3] = 30;
		cooltime [4] = 30;
		//cooltime [5] = 30;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0; i<5; ++i)
			--cool [i];
		/*if (shadowCount != -1) 
		{
			++shadowCount;
			IceShadow ();
		}*/
	}

	public void SkillCheck(int sw)
	{
		switch(sw)
		{
		case 0:
			Bubble();
			break;
		case 1:
			IceWall();
			break;
		case 2:
			IceFog();
			break;
		case 3:
			IceBomb ();
			break;
/*		case 4:
			IceField();
			break;
		case 5:
			IceBomb();
			break;*/
		}
	}
	void Bubble()
	{
		if (cool [0] <= 0) 
		{
			if(iceSw==true)
			{
				Instantiate (_bubble, _player.transform.position + new Vector3 (-1, 4, -1), Quaternion.identity);
				Instantiate (_bubble, _player.transform.position + new Vector3 (1, 4, -1), Quaternion.identity);
				Instantiate (_bubble, _player.transform.position + new Vector3 (0, 4, 1), Quaternion.identity);
				iceSw=false;
			}
			else
			{
				Instantiate (_bubble, _player.transform.position + new Vector3 (0, 4, 0), Quaternion.identity);
			}
			cool[0]=cooltime[0]*Time.captureFramerate;
		}
	}
    void IceWall()
    {
        if (cool[1] <= 0)
        {
			if(iceSw==true)
			{
				beStrongSw[1]=true;
				iceSw=false;
			}
            Vector3 currentPos = _player.transform.position;
			float angle = com.angle;
			currentPos.x -= 1 * Mathf.Cos(angle);
            currentPos.y = -1;
            currentPos.z -= 1 * Mathf.Sin(angle);
            Instantiate(iceWall, currentPos, com.transform.rotation);

            cool[1] = cooltime[1] * Time.captureFramerate; //쿨타임 초기화
        }
    }

	void IceFog()
	{
		if(cool[2]<0)
		{
			if(iceSw==true)
			{
				beStrongSw[2]=true;
				iceSw=false;
			}
			Instantiate(_iceFog,com.transform.position+new Vector3(0,1,0),Quaternion.identity);
			cool[2]=cooltime[2]*Time.captureFramerate;
		}
	}

	void IceBomb()
	{
		if(cool[3]<0)
		{
			if(iceSw==true)
			{
				beStrongSw[3]=true;
				iceSw=false;
			}
			Instantiate (_iceBomb,new Vector3(0,10,0),Quaternion.identity);
			Instantiate (_iceWave,new Vector3(0,7,0),Quaternion.identity);
			cool[3]=cooltime[3]*Time.captureFramerate;
		}
	}
	
	public void WeatherIsRain()
	{
		if(cool[4]<0)
		{
			iceSw=true;
			cooltime[4]=cool[4]*Time.captureFramerate;
		}
	}

/*	void IceField()
	{
		if(cool[4]<0)
		{
			if(iceSw==1)
			{
				iceSw=5;
			}
			Instantiate(_iceField,new Vector3(0,0,0),Quaternion.identity);
			cool[4]=cooltime[4]*Time.captureFramerate;
		}
	}*/


}
