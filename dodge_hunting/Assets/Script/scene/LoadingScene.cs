using UnityEngine;
using System.Collections;

public class LoadingScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AsyncOperation async = Application.LoadLevelAsync(0); 

        while(!async.isDone)
        {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
