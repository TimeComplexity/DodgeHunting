using UnityEngine;
using System.Collections;

public class LevelSelectScene : MonoBehaviour {

    public GameObject[] levelButton = new GameObject[5];
    string enemyName;
    void LevelSelect()
    {
            levelButton[0].gameObject.SetActive(false);
		for(int i=1;i<5;++i)
			if (PlayerPrefs.GetInt(Variables.enemyName[Variables.enemySwitch]) > i)
            	levelButton[i].gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start () {
		LevelSelect();
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