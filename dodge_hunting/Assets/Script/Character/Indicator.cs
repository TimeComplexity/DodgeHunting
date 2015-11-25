using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Indicator : MonoBehaviour {
	
	public GameObject _enemy; 
	public GameObject _player;
	
	private Vector3 direction;
	private Vector3 arrowPos;
	
	// Update is called once per frame
	void Update()
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(_enemy.transform.position);
		
		if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
		{
			ShowIndicator(screenPos); //적이 화면 밖에 있으면 화살표를 놓는다
			ReSizeIndicator();
		}
		else
			HideIndicator(); 
			//화면 안에 있으면 화살표를 화면 밖으로 치운다, 원래 안 보이게만 하려고 했었는데 잘 안 돼서...
	}
	
	void ShowIndicator(Vector3 pos)
	{
		float offset = 30f; //화살표와 화면 모서리 사이의 여백
		
		//플레이어에서 적을 가리키는 방향 구하기
		direction = _player.transform.position - _enemy.transform.position;
		
		//UI 캔버스의 상하는 y축이지만, 게임 필드의 상하는 z축이므로
		float angle = Mathf.Atan2(direction.z, direction.x); //direction 벡터의 arctan(z/x) 구하기
		angle -= 90 * Mathf.Rad2Deg; //원본 이미지가 90도 돌아가 있으므로
		
		transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg); //z축을 회전축으로 돌린다
		
		arrowPos = pos;
		
		if (pos.x < 0)
			arrowPos.x = offset;
		if (pos.x > Screen.width)
			arrowPos.x = Screen.width - offset;
		if (pos.y < 0)
			arrowPos.y = offset;
		if (pos.y > Screen.height)
			arrowPos.y = Screen.height - offset;
		
		transform.position = arrowPos; //화살표를 arrowPos에 놓는다
		
	}
	
	void ReSizeIndicator()
	{
		float distance = Vector3.Distance(_player.transform.position, _enemy.transform.position);
		float size = (5/distance) + .2f;
		transform.localScale = new Vector3(size, size);
	}
	
	void HideIndicator()
	{
		arrowPos = new Vector3(-100, -100);
		transform.position = arrowPos;
	}
}
