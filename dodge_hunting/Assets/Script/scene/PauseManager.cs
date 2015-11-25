using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{

    public GameObject PausePanelCanvas;
    public GameObject PauseButton;
    public GameObject SkillButton;
    public GameObject TouchPad;
    public GameObject Countdown;
    public Toggle pauseToggle;

    public Text title;

    public Sprite[] CountdownSprite = new Sprite[3];

    //public static bool isVibrationOn = true;  Gamemanaver.cs로 옮김

    private int startTime;

    // Use this for initialization
    void Awake()
    {
        PausePanelCanvas.GetComponent<Canvas>().enabled = false;
        PauseButton.SetActive(false);
        SkillButton.SetActive(false);

        StartCoroutine(pauseFor(3));
        if (PlayerPrefs.GetInt("isVibrate", 1) == 1)
            pauseToggle.isOn = true;
        else
            pauseToggle.isOn = false;
    }

    IEnumerator pauseFor(float sec)
    {
        Time.timeScale = 0;
        PauseButton.SetActive(false);
        SkillButton.SetActive(false);
        TouchPad.SetActive(true);
        Countdown.SetActive(true);

        Countdown.GetComponent<Image>().sprite = CountdownSprite[2];

        for (int i = 0; i < Time.captureFramerate * sec; i++)
        {
            if (i == Time.captureFramerate * 1)
                Countdown.GetComponent<Image>().sprite = CountdownSprite[1];

            if (i == Time.captureFramerate * 2)
                Countdown.GetComponent<Image>().sprite = CountdownSprite[0];

            yield return null;
        }

        Countdown.SetActive(false);
        PauseButton.SetActive(true);
        SkillButton.SetActive(true);
        Time.timeScale = 1;
    }


    public void ToggleVibration()
    {
        PlayerPrefs.SetInt("isVibrate", PlayerPrefs.GetInt("isVibrate",1) * -1);
        //Variables.isVibrationOn = !Variables.isVibrationOn;
    }
    public void TogglePauseMenu(int sw)
    {
        if (PausePanelCanvas.GetComponent<Canvas>().enabled)
        {
            PausePanelCanvas.GetComponentInChildren<Canvas>().enabled = false;
            PauseButton.SetActive(true);
            SkillButton.SetActive(true);
            TouchPad.SetActive(true);

            StartCoroutine(pauseFor(3));
        }
        else
        {
            if (sw == -1)
            {
                title.text = "패배";
            }
            else if (sw == 1)
            {
                title.text = "승리";
            }
            PausePanelCanvas.GetComponentInChildren<Canvas>().enabled = true;
            PauseButton.SetActive(false);
            SkillButton.SetActive(false);
            TouchPad.SetActive(false);

            Time.timeScale = 0;
        }
    }

    public void Quit()
    {
        Application.LoadLevel(2);
        //TogglePauseMenu(0);
    }

    public void Retry()
    {
        Application.LoadLevel(3);
        //TogglePauseMenu(0);
    }
}
