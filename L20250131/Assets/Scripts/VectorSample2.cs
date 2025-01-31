using UnityEngine;

public class VectorSample2 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //normalization
        Vector3 a = new Vector3(1, 2, 0);
        Vector3 normal_a=a.normalized;

        //두 지점 사이의 거리 계산
        Vector3 positionA=new Vector3(1,2,3);
        Vector3 positionB=new Vector3(4,5,6);

        float distance=Vector3.Distance(positionA, positionB);
        //두 벡터 차이의 크기 계산
        //뺄셈을 이용해 거리를 float값으로 반환


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
