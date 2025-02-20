using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //카메라의 스크롤 제한 값
    public float left_limit = 0.0f;
    public float right_limit = 0.0f;
    public float top_limit = 0.0f;
    public float bottom_limit = 0.0f;

    //서브 스크린
    public GameObject sub_screen;

    //강제 스크롤 기능 처리
    public bool isForceScrollX = false;     //X축 조건
    public bool isForceScrollY = false;     //Y축 조건
    public float forceScrollSpeedX = 0.5f;  //초당 움직일 X축 거리
    public float forceScrollSpeedY = 0.5f;  //초당 움직일 Y축 거리

    // Update is called once per frame
    void Update()
    {
        //플레이어 탐색
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = transform.position.z;

        //가로 강제 스크롤
        if (isForceScrollX)
        {
            x=transform.position.x + (forceScrollSpeedX*Time.deltaTime);
        }
        //세로 강제 스크롤
        if (isForceScrollY)
        {
            x = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
        }

        //가로 방향 동기화
        if (x < left_limit) x = left_limit;
        else if (x > right_limit) x = right_limit;
        //세로 방향 동기화
        if (y < bottom_limit) y = bottom_limit;
        else if (y > top_limit) y = top_limit;

        //현재의 카메라 위치를 Vector3로 표현
        Vector3 vector3 = new Vector3(x, y, z);
        //카메라 위치를 설정값으로 표현
        transform.position = vector3;

        //서브 스크린의 x축 이동이 플레이어의 0.5배 속력으로 이루어진다
        if(sub_screen != null)
        {
            y=sub_screen.transform.position.y;
            z=sub_screen.transform.position.z;
            Vector3 v = new Vector3(x * 0.5f, y, z);
            sub_screen.transform.position = v;
        }
    }

}
