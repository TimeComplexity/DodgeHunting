using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillManager : MonoBehaviour {

	public Sprite[] spr1 = new Sprite[4];
	public Sprite[] spr2 = new Sprite[4];
	public Sprite[] spr3 = new Sprite[4];
	public Sprite[] spr4 = new Sprite[4];
	public Sprite[] spr5 = new Sprite[4];
	public Sprite[] spr6 = new Sprite[4];
    public Sprite skillDefaultSprite;
    //스킬 아이콘 스프라이트를 저장

	public GameObject[] infoButton = new GameObject[6];
    public GameObject[] equipSlotButton = new GameObject[4];
    //스킬 정보 확인 버튼, 장착 버튼을 저장

    public GameObject selectedSkillIcon;
    public Text selectedSkillTitle;
    public Text selectecSkillInfo;
    //스킬정보 팝업 내 정보들

    public int typeIndex;
    //스킬 타입을 0, 1, 2, 3으로 나타냄

    public int selectedSlotIndex = -1;
    //선택된 슬롯의 번호. -1은 선택 안 된 경우

    public bool isPopupOpened = false;

    public Sprite defaultImage;

    public Canvas PopupCanvas;

	void Start () {
        PopupCanvas.GetComponent<Canvas>().enabled = false; //팝업은 닫혀 있다
        LoadSkillImages();
	}

    public int returnTypeNum(string type)
    {
        if      (type == "Fire")    return 0;
        else if (type == "Ice")   return 1;
        else if (type == "Wind")    return 2;
        else if (type == "Earth")   return 3;
        else
            Debug.Log ("Shit");

        return -1;
    } //Variable.cs의 skilltype에 문자열로 저장되어 있어서 배열 인덱스로 쓰기 위해 바꾸는 함수

    public void LoadSkillImages()
    {
        typeIndex = returnTypeNum(Variables.skillType);

        infoButton[0].GetComponent<Image>().sprite = spr1[typeIndex];
        infoButton[1].GetComponent<Image>().sprite = spr2[typeIndex];
        infoButton[2].GetComponent<Image>().sprite = spr3[typeIndex];
        infoButton[3].GetComponent<Image>().sprite = spr4[typeIndex];
        infoButton[4].GetComponent<Image>().sprite = spr5[typeIndex];
        infoButton[5].GetComponent<Image>().sprite = spr6[typeIndex];

        equipSlotButton[0].GetComponent<Image>().sprite = GetEquippedSkillSprite(0);
        equipSlotButton[1].GetComponent<Image>().sprite = GetEquippedSkillSprite(1);
        equipSlotButton[2].GetComponent<Image>().sprite = GetEquippedSkillSprite(2);
        equipSlotButton[3].GetComponent<Image>().sprite = GetEquippedSkillSprite(3);


    } //스킬 버튼들의 이미지를 바꾸는 함수

    public Sprite GetEquippedSkillSprite(int skillIndex)
    {
        switch(Variables.skillEquipped[typeIndex, skillIndex])
        {
            case 0: return spr1[typeIndex];
            case 1: return spr2[typeIndex];
            case 2: return spr3[typeIndex];
            case 3: return spr4[typeIndex];
            case 4: return spr5[typeIndex];
            case 5: return spr6[typeIndex];
            case 6: return skillDefaultSprite;
            default: Debug.Log("skillEquipped is broken"); return skillDefaultSprite;
        }
    }

    public void OpenPopup()
    {
        if (selectedSlotIndex == -1)
            PopupCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void ClosePopup()
    {
            PopupCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void clickedButton(Button button)
    {
        int index = int.Parse(button.name.Substring(6));

        if (selectedSlotIndex == -1) //슬롯을 선택하지 않은 경우. 
        {
            switch (index)
            {
            case 0: 
				selectedSkillIcon.GetComponent<Image>().sprite = spr1[typeIndex];
				switch(typeIndex)
				{
				case 0:
					selectedSkillTitle.GetComponent<Text>().text="폭발 지뢰";
					selectecSkillInfo.GetComponent<Text>().text="내 뒤쪽에 밟으면 폭발하는 지뢰 설치";
					break;
				case 1:
					selectedSkillTitle.GetComponent<Text>().text="비눗 방울";
					selectecSkillInfo.GetComponent<Text>().text="내 머리위쪽에 비눗방울을 소환, 플레이어와 닿으면 튕기고 적이 닿으면 몸이 뜨게되고 속박된다." +
																"\n강화 : 비눗방울이 3개 소환된다.";
					break;
				case 2:
					selectedSkillTitle.GetComponent<Text>().text="진공볼";
					selectecSkillInfo.GetComponent<Text>().text="불안정한 진공볼 생성.\n충돌이 일어날 경우 잠시 후에 주위의 모든 것을 흡수한다.";
					break;
				}
                break;
            case 1: 
				selectedSkillIcon.GetComponent<Image>().sprite = spr2[typeIndex];
				switch(typeIndex)
				{
				case 0:
					selectedSkillTitle.GetComponent<Text>().text="각성";
					selectecSkillInfo.GetComponent<Text>().text="일정시간 동안 자신의 최대속도와 가속도를 증가시킨다." +
																"\n지속시간이 끝나면 반동으로 일정시간동안 최대속도와 가속도가 감소한다.";
					break;
				case 1:
					selectedSkillTitle.GetComponent<Text>().text="얼음벽";
					selectecSkillInfo.GetComponent<Text>().text="내 뒤쪽에 얼음벽을 생성. 적이 부딪치면 기절시킨다." +
																"\n강화 : 지속시간 증가";
					break;
				case 2:
					selectedSkillTitle.GetComponent<Text>().text="점멸";
					selectecSkillInfo.GetComponent<Text>().text="진행방향 앞부분으로 순간이동한다.";
					break;
				}
                break;
            case 2: 
				selectedSkillIcon.GetComponent<Image>().sprite = spr3[typeIndex];
				switch(typeIndex)
				{
				case 0:
					selectedSkillTitle.GetComponent<Text>().text="불기둥";
					selectecSkillInfo.GetComponent<Text>().text="맵에 랜덤하게 10초간 불기둥을 3개씩 생성한다.";
					break;
				case 1:
					selectedSkillTitle.GetComponent<Text>().text="안개생성";
					selectecSkillInfo.GetComponent<Text>().text="내 중심으로 원 반경에 안개를 생성한다. 안개속에서 적은 플레이어의 방향을 시각으로 알지 못한다." +
																"\n강화 : 크기 및 지속시간 증가";
					break;
				case 2:
					selectedSkillTitle.GetComponent<Text>().text="바람막";
					selectecSkillInfo.GetComponent<Text>().text="적과 충돌할 경우 적을 튕겨내는 바람막을 몸에 씌운다.";
					break;
				}
                break;
            case 3: 
				selectedSkillIcon.GetComponent<Image>().sprite = spr4[typeIndex];
				switch(typeIndex)
				{
				case 0:
					selectedSkillTitle.GetComponent<Text>().text="도깨비불";
					selectecSkillInfo.GetComponent<Text>().text="도깨비불을 맵에 소환. 도깨비불은 랜덤하게 움직인다." +
																"\n도깨비불에 닿으면 공포에 빠져 멋대로 움직인다.";
					break;
				case 1:
					selectedSkillTitle.GetComponent<Text>().text="물폭탄";
					selectecSkillInfo.GetComponent<Text>().text="맵 중앙에 거대한 물폭탄을 떨어뜨려 모든 캐릭터를 벽쪽으로 밀어낸다." +
																"\n강화 : 물폭탄이 터진후 바닥에 얼음 생성";
					break;
				case 2:
					selectedSkillTitle.GetComponent<Text>().text="신기루";
					selectecSkillInfo.GetComponent<Text>().text="나와 똑같이 생긴 신기루를 2개 소환한다.";
					break;
				}
                break;
            case 4: 
				selectedSkillIcon.GetComponent<Image>().sprite = spr5[typeIndex];
                break;
            case 5: 
				selectedSkillIcon.GetComponent<Image>().sprite = spr6[typeIndex];
                break;
            }
        }
        else //슬롯을 선택한 경우
        {
            if(IsThisSkillOverlapped(index) == false)
                Variables.skillEquipped[typeIndex, selectedSlotIndex] = index; //실제로 장착한스킬이 변경되는 부분
			ResizeObject(equipSlotButton[selectedSlotIndex], 1.0f);
            LoadSkillImages();
            selectedSlotIndex = -1;
		}
    }

    public void selectedSlot(Button slot)
    {
        int slotIndex = int.Parse(slot.name.Substring(5));

        if(selectedSlotIndex != -1)
            for(int i = 0; i < 4; i++)
                ResizeObject(equipSlotButton[i], 1.0f);

        switch (slotIndex)
        {
            case 0: selectedSlotIndex = slotIndex; ResizeObject(equipSlotButton[0], 1.2f); break;
            case 1: selectedSlotIndex = slotIndex; ResizeObject(equipSlotButton[1], 1.2f); break;
            case 2: selectedSlotIndex = slotIndex; ResizeObject(equipSlotButton[2], 1.2f); break;
            case 3: selectedSlotIndex = slotIndex; ResizeObject(equipSlotButton[3], 1.2f); break;
            default: selectedSlotIndex = -1; ResizeObject(equipSlotButton[selectedSlotIndex], 1.0f); break;
        }
    }

    public void ResizeObject(GameObject target, float size)
    {
            target.transform.localScale = new Vector3(size, size);
    }

    public bool IsThisSkillOverlapped(int skillIndex)
    {
        for(int i = 0; i < 4; i++)
            if(Variables.skillEquipped[typeIndex, i] == skillIndex)
                return true;
        return false;
    }
}

// 이하 잉여코드
/*
       for(int i=0; i<4; i++)
       {
           if(GameManager.skillEquipped[type, i] != null)
           {
               for (int j = -1; j <6; j++)
               {
                   if( GameManager.skillEquipped[type,i] == j )
                   {
                       if (GameManager.skillEquipped[type, i] == -1)
                       {
                           equipSlot[i].GetComponentInChildren<Image>().sprite = defaultImage;
                       }
                       else
                       {
                           equipSlot[i].GetComponentInChildren<Image>().sprite = slot[j].gameObject.GetComponent<Image>().sprite;
                           break;
                       }
                   }
               }
           }
           else
           {
               equipSlot[i].GetComponentInChildren<Image>().sprite = defaultImage;
           }
       }
       if(type==0)
       {
           Variables.skillType="Fire";
       }
       else if(type==2)
       {
           Variables.skillType="Wind";
       }
   }
   */