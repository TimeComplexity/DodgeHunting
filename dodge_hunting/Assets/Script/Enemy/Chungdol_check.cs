using UnityEngine;
using System.Collections;

public class Chungdol_check : MonoBehaviour {

	public GameObject _player;
	public GameObject _enemy;
	public Collider _collobject;
	
	public float btwnangle;

	private float vec1_x;
	private float vec1_z;

	private float vec2_x;
	private float vec2_z;

	public Vector3 vec1;
	public Vector3 vec2;

	public int cnt = 0;
	public int cnt2 = 0;
	public int cnt3 = 0;

	public bool caution = false;
	
	void OnTriggerStay(Collider coll)
	{

		vec1_x = _player.transform.position.x - _enemy.transform.position.x;
		vec1_z = _player.transform.position.z - _enemy.transform.position.z;

		//vec2_x = 360.0f - _enemy.transform.rotation.x;
	//	vec2_z = 360.0f - _enemy.transform.rotation.z;

		vec2_x = Mathf.Cos ((_enemy.transform.rotation.y) * Mathf.Deg2Rad);
		vec2_z = Mathf.Sin (( _enemy.transform.rotation.y) * Mathf.Deg2Rad);

		vec2_x = _enemy.transform.forward.x;
		vec2_z = _enemy.transform.forward.z;

		vec1 = new Vector3 (vec1_x, 0, vec1_z);
		vec2 = new Vector3 (vec2_x, 0, vec2_z);


		// anglerad2deg = _enemy.transform.rotation.y * Mathf.Rad2Deg; 아 시발 진짜 이거 왜 거지 발싸개처럼 나오는지 누가 설명좀 

		btwnangle = (Vector3.Angle (vec2, vec1) - 90.0f) ;

		if((coll.gameObject.tag == "Player"))
		{
			_collobject = coll;
			cnt2++;

			if((btwnangle >= -35.0f) && (btwnangle <= 35.0f))
				caution = true;
		}

		/*else if(coll.gameObject.tag == "Mine")
		{
			_collobject = coll;
			cnt2++;
			caution = true;
		}*/

		if(!((btwnangle >= -35.0f) && (btwnangle <= 35.0f)))
		{
			cnt++;
			caution = false;
		}
	}

	void OnTriggerExit(Collider coll)
	{
		caution = false;
	}
}
