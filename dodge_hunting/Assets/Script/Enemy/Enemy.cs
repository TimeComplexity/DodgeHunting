using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int[] skillWaitTime = new int[6]; // 0 패시브, 1~4 액티브, 5 리더
	public int[] skillCount = new int[6]; // 0 패시브, 1~4 액티브, 5 리더

	// Use this for initialization
	void Start () {
		for(int i=0;i<6;++i)
			skillCount[i]=0;
	}
	
	public virtual void ActSkill(int skillSw)
	{
	}

	public virtual void setCool()
	{

	}

	/*public virtual void DefaultAct( bool pasSw)
	{
	}

	public virtual void ActSkill1(int count)
	{
	}

	public virtual void ActSkill2(int count)
	{
	}

	public virtual void ActSkill3(int count)
	{
	}

	public virtual void ActSkill4(int count)
	{
	}

	public virtual void LeaderSkill(int count)
	{
	}*/
}
