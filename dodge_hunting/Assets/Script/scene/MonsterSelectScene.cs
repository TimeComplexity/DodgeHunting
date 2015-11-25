using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonsterSelectScene : MonoBehaviour {

    public GameObject[] monsterButton = new GameObject[4];

	// Use this for initialization
    void Start(){
        if (PlayerPrefs.GetInt("wildBoarClearLevel") > -1)
            monsterButton[0].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("wolfClearLevel") > 0)
            monsterButton[1].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("kangarooClearLevel") > 0)
            monsterButton[2].gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("suriClearLevel") > 0)
            monsterButton[3].gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.LoadLevel(2);
            }
        }
	}
}