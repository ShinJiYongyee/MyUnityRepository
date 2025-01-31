using UnityEngine;

public class CircleMove : MonoBehaviour
{
    //Circle을 지정된 위치로 Lerp시키는 스크립트
    public GameObject Circle;
    Vector3 pos = new Vector3 (4, -3, 0);


    // Update is called once per frame
    //지속적인 움직임이 필요하므로 Update에 구현
    void Update()
    {


        Circle.transform.position = Vector3.Lerp(Circle.transform.position, pos, 0.05f);

    }
}
