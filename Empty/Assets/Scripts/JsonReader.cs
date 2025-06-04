using UnityEngine;

public class JsonReader : MonoBehaviour
{
    void Start()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("items");
        if(jsonFile != null)
        {
            // ItemList·Î ÆÄ½Ì
            ItemList itemList = JsonUtility.FromJson<ItemList>(jsonFile.text);

            foreach (var item in itemList.data)
            {
                Debug.Log($"id:{item.item_id} name:{item.item_name} attack power:{item.attack_power} defense:{item.defense}");
            }
        }
        else
        {
            Debug.LogError("no file");
        }
    }
}
