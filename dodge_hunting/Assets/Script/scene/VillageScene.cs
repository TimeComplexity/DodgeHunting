using UnityEngine;
using System.Collections;

public class VillageScene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.SetInt("ClearLevel", 0);
        //Destroy(GameObject.Find("/child").gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                //Application.LoadLevel(1);
            }
        }
    }

    public void LevelClear()
    {
        PlayerPrefs.SetInt("wildBoarClearLevel", 5);
        PlayerPrefs.SetInt("wolfClearLevel", 5);
        PlayerPrefs.SetInt("kangarooClearLevel", 5);
        PlayerPrefs.SetInt("suriClearLevel", 5);

    }
    public void LevelInit()
    {
        PlayerPrefs.DeleteKey("wildBoarClearLevel");
        PlayerPrefs.DeleteKey("wolfClearLevel");
        PlayerPrefs.DeleteKey("kangarooClearLevel");
        PlayerPrefs.DeleteKey("suriClearLevel");
    }



}
