using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scene : MonoBehaviour {

	// Use this for initialization
	public GameObject child;
	public GameObject player;
	public GameObject adult;
	public GameObject leader;
	public GameObject enemyHp1;
	public GameObject enemyHp2;
	public GameObject arrow1;
	public GameObject arrow2;
	public GameObject[] slot = new GameObject[4];
	public Sprite[] spr = new Sprite[24];
	public GameObject type;
	public Sprite[] typeSpr = new Sprite[4];

	// Use this for initialization
	void Start () {
        if (Variables.enemyLevel == -1)
        {
            Variables.enemy1 = Instantiate(child, new Vector3(8.0f, 0.5f, 0.0f), Quaternion.identity) as GameObject;
            Enemy_Hp com = enemyHp1.GetComponent<Enemy_Hp>();
            com._enemy = Variables.enemy1;
            com.maxHp *= 10f;
            Variables.enemy1.GetComponent<Enemy_Move>()._EnemyHp = enemyHp1;
            enemyHp2.SetActive(false);

            Indicator ind = arrow1.GetComponent<Indicator>();
            ind._enemy = Variables.enemy1;
            arrow2.SetActive(false);
        }
        if (Variables.enemyLevel == 0)
		{
			Variables.enemy1 = Instantiate(child, new Vector3(8.0f, 0.5f, 0.0f), Quaternion.identity) as GameObject;
            //Variables.enemy1.name = "child";
			Enemy_Hp com = enemyHp1.GetComponent<Enemy_Hp>();
			com._enemy = Variables.enemy1;
			com.maxHp*=0.8f;
			Variables.enemy1.GetComponent<Enemy_Move>()._EnemyHp=enemyHp1;
			enemyHp2.SetActive(false);

			Indicator ind = arrow1.GetComponent<Indicator>();
			ind._enemy = Variables.enemy1;
			arrow2.SetActive (false);
		}
		if (Variables.enemyLevel == 1)
		{
			Variables.enemy1 = Instantiate(adult, new Vector3(8.0f, 0.5f, 0.0f), Quaternion.identity) as GameObject;
			Enemy_Hp com = enemyHp1.GetComponent<Enemy_Hp>();
			com._enemy = Variables.enemy1;
			Variables.enemy1.GetComponent<Enemy_Move>()._EnemyHp=enemyHp1;
			enemyHp2.SetActive(false);

			Indicator ind = arrow1.GetComponent<Indicator>();
			ind._enemy = Variables.enemy1;
			arrow2.SetActive (false);
		}
		if (Variables.enemyLevel == 2)
		{
			Variables.enemy1 = Instantiate(adult, new Vector3(8.0f, 0.5f, 2.0f), Quaternion.identity) as GameObject;
			Variables.enemy2 = Instantiate(child, new Vector3(8.0f, 0.5f, -2.0f), Quaternion.identity) as GameObject;
			Enemy_Hp com = enemyHp1.GetComponent<Enemy_Hp>();
			com._enemy = Variables.enemy1;
			Variables.enemy1.GetComponent<Enemy_Move>()._EnemyHp=enemyHp1;
			com = enemyHp2.GetComponent<Enemy_Hp>();
			com._enemy = Variables.enemy2;
			Variables.enemy2.GetComponent<Enemy_Move>()._EnemyHp=enemyHp2;
			com.maxHp*=0.8f;

			Indicator ind = arrow1.GetComponent<Indicator>();
			ind._enemy = Variables.enemy1;
			ind = arrow2.GetComponent<Indicator>();
			ind._enemy = Variables.enemy2;

		}
		if (Variables.enemyLevel == 3)
		{
			Variables.enemy1 = Instantiate(adult, new Vector3(8.0f, 0.5f, 2.0f), Quaternion.identity) as GameObject;
			Variables.enemy2 = Instantiate(adult, new Vector3(8.0f, 0.5f, -2.0f), Quaternion.identity) as GameObject;
			Enemy_Hp com = enemyHp1.GetComponent<Enemy_Hp>();
			com._enemy = Variables.enemy1;
			Variables.enemy1.GetComponent<Enemy_Move>()._EnemyHp=enemyHp1;
			com = enemyHp2.GetComponent<Enemy_Hp>();
			com._enemy = Variables.enemy2;
			Variables.enemy2.GetComponent<Enemy_Move>()._EnemyHp=enemyHp2;

			Indicator ind = arrow1.GetComponent<Indicator>();
			ind._enemy = Variables.enemy1;
			ind = arrow2.GetComponent<Indicator>();
			ind._enemy = Variables.enemy2;
		}
		if (Variables.enemyLevel == 4)
		{
			Variables.enemy1 = Instantiate(leader, new Vector3(8.0f, 0.5f, 0.0f), Quaternion.identity) as GameObject;
			Enemy_Hp com = enemyHp1.GetComponent<Enemy_Hp>();
			com._enemy = Variables.enemy1;
			com.maxHp*=1.3f;
			Variables.enemy1.GetComponent<Enemy_Move>()._EnemyHp=enemyHp1;
			enemyHp2.SetActive(false);

			Indicator ind = arrow1.GetComponent<Indicator>();
			ind._enemy = Variables.enemy1;
			arrow2.SetActive (false);
		}

		if(Variables.skillType=="Fire")
		{
			slot[0].gameObject.GetComponent<Image>().sprite=spr[Variables.skillEquipped[0,0]];
			slot[1].gameObject.GetComponent<Image>().sprite=spr[Variables.skillEquipped[0,1]];
			slot[2].gameObject.GetComponent<Image>().sprite=spr[Variables.skillEquipped[0,2]];
			slot[3].gameObject.GetComponent<Image>().sprite=spr[Variables.skillEquipped[0,3]];
			type.gameObject.GetComponent<Image>().sprite=typeSpr[0];
		}
		else if(Variables.skillType=="Ice")
		{
			slot[0].gameObject.GetComponent<Image>().sprite=spr[6+Variables.skillEquipped[1,0]];
			slot[1].gameObject.GetComponent<Image>().sprite=spr[6+Variables.skillEquipped[1,1]];
			slot[2].gameObject.GetComponent<Image>().sprite=spr[6+Variables.skillEquipped[1,2]];
			slot[3].gameObject.GetComponent<Image>().sprite=spr[6+Variables.skillEquipped[1,3]];
			type.gameObject.GetComponent<Image>().sprite=typeSpr[1];
		}
		else if(Variables.skillType=="Wind")
		{
			slot[0].gameObject.GetComponent<Image>().sprite=spr[12+Variables.skillEquipped[2,0]];
			slot[1].gameObject.GetComponent<Image>().sprite=spr[12+Variables.skillEquipped[2,1]];
			slot[2].gameObject.GetComponent<Image>().sprite=spr[12+Variables.skillEquipped[2,2]];
			slot[3].gameObject.GetComponent<Image>().sprite=spr[12+Variables.skillEquipped[2,3]];
			type.gameObject.GetComponent<Image>().sprite=typeSpr[2];
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Application.platform == RuntimePlatform.Android)
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.LoadLevel(1);
			}
		}
		
	}
}
