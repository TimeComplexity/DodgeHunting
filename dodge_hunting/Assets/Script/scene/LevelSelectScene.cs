using UnityEngine;
using System.Collections;

public class LevelSelectScene : MonoBehaviour {

    public GameObject[] levelButton = new GameObject[5];
    string enemyName;
    void monseterSelect(int enemy)
    {
        if(enemy == 0)
            enemyName = "wildBoarClearLevel";
        if (enemy == 1)
            enemyName = "wolfClearLevel";
        if (enemy == 2)
            enemyName = "kangarooClearLevel";
    }
    void LevelSelect()
    {
            levelButton[0].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt(enemyName) > 1)
            levelButton[1].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt(enemyName) > 2)
            levelButton[2].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt(enemyName) > 3)
            levelButton[3].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt(enemyName) > 4)
            levelButton[4].gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        if (Variables.enemySwitch == 0)
        {
            monseterSelect(Variables.enemySwitch);
            LevelSelect();
        }
        if (Variables.enemySwitch == 1)
        {
            monseterSelect(Variables.enemySwitch);
            LevelSelect();
        }
        if (Variables.enemySwitch == 2)
        {
            monseterSelect(Variables.enemySwitch);
            LevelSelect();
        }
        if (Variables.enemySwitch == 3)
        {
            monseterSelect(Variables.enemySwitch);
            LevelSelect();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.LoadLevel(5);
            }
        }
    }
}