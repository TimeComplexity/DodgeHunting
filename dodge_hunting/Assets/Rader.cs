using UnityEngine;
using System.Collections;

public class Rader : MonoBehaviour {

    //public GameObject enemy;
    //public Suri suri;
    public int chaseCount = 0;
    public int attackReady = 0;
    //Vector3 orig;
    //Vector3 init = new Vector3(5, 0, 5);
    //int circleSw = 0;
    int triggerState = 0;

    

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if(triggerState == 0)
            circleRecovery();
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            triggerState = 1;
            chaseCount = 1;
            if (this.transform.localScale.x < 3)
            {
                Debug.Log("레이더 축소 끝");
                attackReady = 1;
                return;
            }
            else if (this.transform.localScale.x > 2)
            {
                this.transform.localScale += new Vector3(-0.5f, 0, -0.5f);
            }
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            triggerState = 0;
            attackReady = 0;
            chaseCount = 0;
        }
    }

    void circleRecovery()
    {
        if (this.transform.localScale.x > 11)
        {
            Debug.Log("레이더 확장 끝");
            return;
        }
        else if (this.transform.localScale.x < 9)
        {
            this.transform.localScale += new Vector3(1f, 0, 1f);
        }
    }
}