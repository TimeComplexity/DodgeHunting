using UnityEngine;
using System.Collections;

public class TempleScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
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

    void OnGUI()
    {
		if (GUI.Button(new Rect(760*Screen.width/960, 50*Screen.height/540, 200*Screen.width/960, 75*Screen.height/540), "Back"))
        {
            Application.LoadLevel(1);
        }
    }
}
