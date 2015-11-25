using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{
    //string enemyName;
    string[] enemyName = new string[4] { "wildBoarClearLevel", "wolfClearLevel", "kangarooClearLevel", "suriClearLevel" };
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }

    public void ToMain()    { Application.LoadLevel("MainMenuScene"); } 
    public void ToVillage() { Application.LoadLevel("VillageScene"); }
    public void ToHunting() { Application.LoadLevel("MonsterSelectScene"); }
    public void ToStore()   { Application.LoadLevel("StoreScene"); }

    public void BoarHunt()  {
        Variables.enemySwitch = 0;
        if (PlayerPrefs.GetInt("wildBoarClearLevel") >= 0)
            Application.LoadLevel("LevelSelectScene");
        else
            return;
    }
    public void WolfHunt() {
        Variables.enemySwitch = 1;
        if (PlayerPrefs.GetInt("wolfClearLevel") > 0)
            Application.LoadLevel("LevelSelectScene");
        else
            return;
    }
    public void kanHunt() {
        Variables.enemySwitch = 2;
        if (PlayerPrefs.GetInt("kangarooClearLevel") > 0)
            Application.LoadLevel("LevelSelectScene");
        else
            return;
    }
    public void SuriHunt() {
        Variables.enemySwitch = 3;
        if (PlayerPrefs.GetInt("suriClearLevel") > 0)
            Application.LoadLevel("LevelSelectScene");
        else
            return;
    }

    public void Difficulty(int level)
    {
        if (PlayerPrefs.GetInt(enemyName[Variables.enemySwitch]) > level - 1)
        {
            Variables.enemyLevel = level;
            Application.LoadLevel("Scene");
        }
        else
            return;
    }

    public void TypeSelect() { Application.LoadLevel("TypeSelectScene"); }
    public void TypeFire()
    { Variables.skillType = "Fire";     Application.LoadLevel("SkillSelectScene"); }
    public void TypeWater()
    { Variables.skillType = "Ice";    Application.LoadLevel("SkillSelectScene"); }
    public void TypeWind()
    { Variables.skillType = "Wind";     Application.LoadLevel("SkillSelectScene"); }
    public void TypeEarth()
    { Variables.skillType = "Earth";    Application.LoadLevel("SkillSelectScene"); }   

    public void Exit() { Application.Quit(); }
}