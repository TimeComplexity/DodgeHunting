using UnityEngine;
using System.Collections;

public class Rader : MonoBehaviour {

    public int chaseSw = 0;
    public int attackSw = 0;

    public int triggerState = 0;
    //public GameObject enemy;
    //public Suri suri;
    //Vector3 orig;
    //Vector3 init = new Vector3(5, 0, 5);
    //int circleSw = 0;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        //if(triggerState == 0)
            //circleRecovery();
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            triggerState = 1;
            //chaseSw = 1;
            /*
            if (this.transform.localScale.x < 2)
            {
                Debug.Log("레이더 축소 끝");
                attackSw = 1;
                chaseSw = 0;
                return;
            }
            else if (this.transform.localScale.x > 1)
            {
                this.transform.localScale += new Vector3(-0.3f, 0, -0.3f);
            }
            */
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            triggerState = 0;
            //attackSw = 0;
            //chaseSw = 0;
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
            this.transform.localScale += new Vector3(0.7f, 0, 0.7f);
        }
    }
}