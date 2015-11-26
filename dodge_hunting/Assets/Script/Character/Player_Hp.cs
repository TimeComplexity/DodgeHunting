using UnityEngine;
using System.Collections;

public class Player_Hp : MonoBehaviour {

    public GameObject player;
    public GameObject enemy;
	public GameObject pauseManager;

    private Player_Move playerScript;

    public GameObject[] bar = new GameObject[3];
    int hp, prevHp;

	// Use this for initialization
	void Start (){
        playerScript = player.GetComponent<Player_Move>();
	}
	
	// Update is called once per frame
	void Update () {
        hp = playerScript.playerHp; //Player_Move 스크립트의 _playerHp 전역변수를 가져옴

        if (hp <= 0)
        {
			if(playerScript.pheonixSw)
			{
				prevHp=0;
				playerScript.playerHp=1;
				Destroy(GameObject.FindGameObjectWithTag("Pheonix").gameObject);
				playerScript.pheonixSw=false;
			}
			else
			{
	            enemy.GetComponent<Enemy_Hp>().damage = 0f;
    	        player.SetActive(false);
                if (Time.timeScale != 0)
                    pauseManager.GetComponent<PauseManager>().ShowResult();
			}
        }
        if (hp != prevHp) //전 프레임이랑 hp 값이 다를 때만 확인
        {
            for (int i = 0; i < 3; i++)
                if (hp > i)
                    bar[i].SetActive(true);
                else
                    bar[i].SetActive(false);
        }
        prevHp = hp;
    }
}