using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Suri_2D : MonoBehaviour {

	public int pos_x;
	public int pos_y;
	public int sk_num;
	public int btn_pos_x;
	public int btn_pos_y;
	public GameObject btn;
	public GameObject img_x;
	public GameObject img_x_prefab;

	private GameObject _canvas;


	// Use this for initialization
	void Start () {
		sk_num = Random.Range (1, 4);
		_canvas = GameObject.Find("Canvas");

		switch(sk_num)
		{
			case 1:
			btn_pos_x = 190;
			btn_pos_y = -210;
			btn = GameObject.Find ("/Canvas/SkillButton/Skill1");
			break;

			case 2:
			btn_pos_x = 235;
			btn_pos_y = -120;
			btn = GameObject.Find ("/Canvas/SkillButton/Skill2");
			break;
		
			case 3:
			btn_pos_x = 310;
			btn_pos_y = -45;
			btn = GameObject.Find ("/Canvas/SkillButton/Skill3");
			break;

			case 4:
			btn_pos_x = 400;
			btn_pos_y = 0;
			btn = GameObject.Find ("/Canvas/SkillButton/Skill4");
			break;
		}

		img_x = Instantiate (img_x_prefab, new Vector3(-100, -100, 0), Quaternion.identity) as GameObject;
		img_x.transform.SetParent(_canvas.transform);

		/*
		 * 
			210, -210
			255, -120
			330, -45
			420, 0
		  */

		pos_x = -600;
		pos_y = btn_pos_y;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = new Vector3 (pos_x, pos_y, 0);

		pos_x += 6;

		if (pos_x > btn_pos_x)
		{
			img_x.transform.localPosition = new Vector3(btn_pos_x + 20, btn_pos_y, 0);
			btn.GetComponent<Button>().enabled = false;
			Destroy (gameObject);
		}
	}
}
