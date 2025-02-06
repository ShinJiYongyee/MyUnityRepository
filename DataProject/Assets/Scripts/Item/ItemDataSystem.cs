using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataSystem : MonoBehaviour
{
    //입력 필드
    public TMP_InputField nameInputField;
    public TMP_InputField descriptionInputField;

    //버튼 상호작용
    public Button loadButton;
    public bool interactable;   //true or false

    //아이템 정보
    public TMP_Text itemName;
    public TMP_Text itemDescription;


    void Start()
    {
        //이 방식으로 등록한 기능은 인스펙터에서 보이지 않음
        nameInputField.onEndEdit.AddListener(ValueChanged);

        //버튼의 interactable 값은 사용자와의 상호작용 여부를 제어할 때 사용
        loadButton.interactable = interactable;
    }

    public void Sample()
    {
        Debug.Log("Input Fields on value changed");
    }

    /// <summary>
    /// 작업이 마무리 되었을 때 text의 문구를 알려주는 함수
    /// </summary>
    void ValueChanged(string text)
    {
        Debug.Log($"{text} 입력 했습니다.");
    }

    public void SetItemData(string itemName, string itemDescription)
    {
        PlayerPrefs.SetString("itemname", itemName);
        PlayerPrefs.SetString("itemdescription", itemDescription);

    }

    public string GetItemName(string itemName)
    {
        return PlayerPrefs.GetString(itemName);
    }
    public void ItemDataLoad() 
    {
        itemName.text=PlayerPrefs.GetString("itemName");
        itemDescription.text=PlayerPrefs.GetString("itemDescription");
    }

    public void SetInteractable()
    {
        if (PlayerPrefs.HasKey("itemName") && PlayerPrefs.HasKey("itemDescription"))
        {
            interactable = true;
        }
        else
        {
            interactable = false;
        }
    }
    public void RemoveItemData()
    {
        PlayerPrefs.DeleteAll();
    }

   
}
