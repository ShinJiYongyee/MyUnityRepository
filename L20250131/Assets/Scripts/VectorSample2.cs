using UnityEngine;

public class VectorSample2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //normalization
        Vector3 a = new Vector3(1, 2, 0);
        Vector3 normal_a=a.normalized;

        //�� ���� ������ �Ÿ� ���
        Vector3 positionA=new Vector3(1,2,3);
        Vector3 positionB=new Vector3(4,5,6);

        float distance=Vector3.Distance(positionA, positionB);
        //�� ���� ������ ũ�� ���
        //������ �̿��� �Ÿ��� float������ ��ȯ


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
