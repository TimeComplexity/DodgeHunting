using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillManager : MonoBehaviour {

    public enum SkillType { FIRE = 0, WATER, WIND, EARTH };

    [System.Serializable] //skillData 구조체를 인스펙터에서 수정할 수 있게 만든다
    public struct skillData
    {
        public Sprite sprite;
        public string name;
        public string info;
    }
    
    public skillData[] skilldata = new skillData[24];
    //스킬에 관한 데이터들을 저장하는 구조체. 순서에 주의.

	public GameObject[] infoButton = new GameObject[6];
    public GameObject[] equipSlotButton = new GameObject[4];
    //스킬 정보 확인 버튼, 장착 버튼을 저장

    public GameObject selectedSkillIcon;
    public Text selectedSkillName;
    public Text selectecSkillInfo;
    //스킬정보 팝업 내 정보들

    public SkillType typeIndex;
    //스킬 타입을 enum SkillType으로 나타냄

    public int selectedSlotIndex = -1;
    //선택된 슬롯의 번호. -1은 선택 안 된 경우

    public bool isPopupOpened = false;

    public Sprite defaultImage;

    public Canvas PopupCanvas;

	void Start () {
        PopupCanvas.GetComponent<Canvas>().enabled = false; //팝업은 닫혀 있다
        LoadSkillInfo();
	}

    public SkillType returnTypeNum(string type)
    {
        if (type == "Fire") return SkillType.FIRE;
        else if (type == "Ice") return SkillType.WATER;
        else if (type == "Wind") return SkillType.WIND;
        else if (type == "Earth") return SkillType.EARTH;
        else
            Debug.Log("returnTypeNum error. check skillmanager.cs");

        return SkillType.FIRE;

    } //문자열로 저장된 Variable.cs의 type을 enum SkillType형으로 바꾸는 함수

    public void LoadSkillInfo()
    {
        typeIndex = returnTypeNum(Variables.skillType);
        int skillIndex;

        for (skillIndex = 0; skillIndex < 6; skillIndex++)
        {
            infoButton[skillIndex].GetComponent<Image>().sprite = skilldata[(int)typeIndex * 6 + skillIndex].sprite;
        }

        for (skillIndex = 0; skillIndex < 4; skillIndex++)
        {
            equipSlotButton[skillIndex].GetComponent<Image>().sprite = skilldata[(int)typeIndex * 6 + skillIndex].sprite;
        }


    } //스킬의 정보를 불러오는 함수

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
            selectedSkillIcon.GetComponent<Image>().sprite = skilldata[(int)typeIndex * 6 + index].sprite;
            selectedSkillName.text = skilldata[(int)typeIndex * 6 + index].name;
            selectecSkillInfo.text = skilldata[(int)typeIndex * 6 + index].info;
            
        }
        else //슬롯을 선택한 경우
        {
            if(IsThisSkillOverlapped(index) == false)
                Variables.skillEquipped[(int)typeIndex, selectedSlotIndex] = index; //실제로 장착한스킬이 변경되는 부분
			ResizeObject(equipSlotButton[selectedSlotIndex], 1.0f);

            LoadSkillInfo();
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
            if(Variables.skillEquipped[(int)typeIndex, i] == skillIndex)
                return true;
        return false;
    }
}