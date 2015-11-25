using UnityEngine;
using System.Collections;

public class OptionScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.LoadLevel(0);
            }
        }
	}

    void OnGUI()
    {


		if (GUI.Button(new Rect(380*Screen.width/960, 100*Screen.height/540, 200*Screen.width/960, 75*Screen.height/540), "Vibration"))
        {
            Debug.Log("Vibration Button");
        }
		if (GUI.Button(new Rect(380*Screen.width/960, 200*Screen.height/540, 200*Screen.width/960, 75*Screen.height/540), "Sound"))
        {
            Debug.Log("Sound Button");
        }
		if (GUI.Button(new Rect(380*Screen.width/960, 300*Screen.height/540, 200*Screen.width/960, 75*Screen.height/540), "Credit"))
        {
            Debug.Log("Credit Button");
            // Application.LoadLevel(6);
        }
        if (GUI.Button(new Rect(760*Screen.width/960, 50*Screen.height/540, 200*Screen.width/960, 75*Screen.height/540), "Back"))
        {
            Application.LoadLevel(0);
        }
    }
}
