using UnityEngine;

public class CircleMove : MonoBehaviour
{
    //Circle�� ������ ��ġ�� Lerp��Ű�� ��ũ��Ʈ
    public GameObject Circle;
    Vector3 pos = new Vector3 (4, -3, 0);


    // Update is called once per frame
    //�������� �������� �ʿ��ϹǷ� Update�� ����
    void Update()
    {


        Circle.transform.position = Vector3.Lerp(Circle.transform.position, pos, 0.05f);

    }
}
