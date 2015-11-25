using UnityEngine;
using System.Collections;

public class SplashScene : MonoBehaviour {

    public float delayTime = 1;

	// Use this for initialization
	IEnumerator Start () {
        //Screen.SetResolution(960, 540, true);

        yield return new WaitForSeconds(delayTime);

        Application.LoadLevel(1);
	}
}
