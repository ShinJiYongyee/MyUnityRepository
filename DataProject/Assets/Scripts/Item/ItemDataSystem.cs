using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDataSystem : MonoBehaviour
{
    //�Է� �ʵ�
    public TMP_InputField nameInputField;
    public TMP_InputField descriptionInputField;

    //��ư ��ȣ�ۿ�
    public Button loadButton;
    public bool interactable;   //true or false

    //������ ����
    public TMP_Text itemName;
    public TMP_Text itemDescription;


    void Start()
    {
        //�� ������� ����� ����� �ν����Ϳ��� ������ ����
        nameInputField.onEndEdit.AddListener(ValueChanged);

        //��ư�� interactable ���� ����ڿ��� ��ȣ�ۿ� ���θ� ������ �� ���
        loadButton.interactable = interactable;
    }

    public void Sample()
    {
        Debug.Log("Input Fields on value changed");
    }

    /// <summary>
    /// �۾��� ������ �Ǿ��� �� text�� ������ �˷��ִ� �Լ�
    /// </summary>
    void ValueChanged(string text)
    {
        Debug.Log($"{text} �Է� �߽��ϴ�.");
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
