using UnityEngine;
using System.Collections;

public class Scart : MonoBehaviour
{
    public GameObject _player;
    public Player_Move com;
    public int count;
    private float speed = 0.387f;

    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Wall" || col.gameObject.tag =="Player")
        {
			Debug.Log ("!!");
            Destroy(gameObject);
        }
    }
    

    void Start()
    {
        _player = GameObject.Find("Player");
        com = _player.GetComponent<Player_Move>();
        transform.LookAt(new Vector3(com.transform.position.x, com.transform.position.y, com.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        count++;
        transform.Translate(Vector3.forward.x * speed, 0, Vector3.forward.z * speed);
        if(count >90)
        {
            Destroy(this);
            count = 0;
        }
    }
}
