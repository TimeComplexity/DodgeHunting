using UnityEngine;
using System.Collections;
using UnityEngine.UI; //추가함

public class Enemy_Hp : MonoBehaviour
{

    public GameObject _enemy;
    public GameObject Bar;
    public GameObject Indicator;
	public GameObject pauseManager;
    public Slider hpBar;
	public Text hpText;
    public float maxHp; //플레이 시간을 에디터에서 정할 수 있게
    public float DefaultDamage; //원래 _damage
	public float damage;
    int enemyLevel;

    private Vector3 screenPos;
    //private int enemyHp;

    // Use this for initialization
    void Start()
    {
        hpBar.maxValue = maxHp;
        hpBar.value = maxHp;
        //enemyHp = maxHp;
        DefaultDamage = 1f/60f; //60fps 기준 1초마다 hp 1 감소 1프레임당 0.017
		damage = DefaultDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime == 0)
            return;

        hpBar.value -= damage * Time.deltaTime * 60;

        if (hpBar.value <= 0)
        {
            //클리어!
			_enemy.SetActive(false);
            enemyClear(Variables.enemySwitch, Variables.enemyLevel);
            //PlayerPrefs.SetInt(enemyName, enemyLevel);

            Bar.SetActive(false);
			if(_enemy == Variables.enemy1 && Time.timeScale!=0)
			{
				pauseManager.GetComponent<PauseManager>().ShowResult();
			}
        }

        ShowHp();

    }

    void ShowHp()
    {
        screenPos = Camera.main.WorldToScreenPoint(_enemy.transform.position);

        if (screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
        {
            transform.position = screenPos;
        }
        else
        {
            screenPos = new Vector3(-100, -100);
            transform.position = screenPos;
        }
		hpText.text = "EnemyHp: " + Mathf.Round(hpBar.value * 10) * .1f;
    }

    void enemyClear(int enemy, int level)
    {
        enemyLevel = PlayerPrefs.GetInt(Variables.enemyName[enemy]);
        // 0 안열림 1 이지 2 노말 3 하드 4 익스 5 대장 6 대장클
        if (level == 0 && PlayerPrefs.GetInt(Variables.enemyName[enemy]) < 2)
        {
            enemyLevel = 2;
            PlayerPrefs.SetInt(Variables.enemyName[enemy+1], 1);
        }
        else if (level >= 1 && PlayerPrefs.GetInt(Variables.enemyName[enemy]) < level+2)
            enemyLevel = level+2;
      
        PlayerPrefs.SetInt(Variables.enemyName[enemy], enemyLevel);
    }
}