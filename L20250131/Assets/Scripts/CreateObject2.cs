using UnityEngine;

public class CreateObject2 : MonoBehaviour
{
    public GameObject prefab;

    private GameObject square;
    void Start()
    {
        square=Instantiate(prefab);

        Destroy(square, 5.0f);  //square�� ���� �� 5�� �� �ı�
        Debug.Log("�ı��Ǿ����ϴ�.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
