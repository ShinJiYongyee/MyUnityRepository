using UnityEngine;

public class VectorSample : MonoBehaviour
{
    //기본 벡터 (x,y,z)순으로 값이 작성
    //(0,0,0)
    //Rigidbody와 달리 2d 상에서 vector3를 사용해도 상관x
    Vector3 vec = new Vector3();

    float x, y, z;

    //Vector3 custom_vec= new Vector3(x,y,z);

    //유니티 기본 벡터(제공 값)
    //ex)Vector3 a=Vector3.up(0,1,0);
    //이름(좌표)
    //up(0,1,0) down(0,-1,0) left(-1,0,0) right(1,0,0)
    //forward(0,0,1) back(0,0,-1)
    //절대 좌표
    //one(1,1,1) zero(0,0,0)

    //벡터 기본 연산(덧셈, 뺄셈, 나눗셈, 곱셈)
    Vector3 a= new Vector3(1,2,0);
    Vector3 b= new Vector3(3,4,0);
    Vector3 some=Vector3.zero;//고정된 값이므로 new 불필요
    float point = 5.0f;

    Vector3 Asite=new Vector3(10,0,0);
    Vector3 Bsite=new Vector3(5,0,0);
    float attack_position = 5.0f;
    void Start()
    {
        //덧셈: 단계적으로 하나씩 차례대로 처리한다
        //순서가 변경되어도 동일한 결과
        //특정 위치에서 이동
        Vector3 c = a + b;
        var trap_air=some+new Vector3(0, point);    //원점에서 위치를 올림(점프)
        //var는 자료형을 기준으로 설정되는 자료형
        //현재 vector자료형

        //뺄셈
        //오브젝트 간 거리와 방향 계산
        Vector3 d = a - b;
        Vector3 distance=Asite-Bsite;
        //이 거리를 측정 후 지정한 거리와 같거나 가까울 때의 행위를 정의할 때 사용 가능

        //곱셈
        //벡터의 각 성분에 스칼라 값을 곱연산
        //원본과 동일한 방향으로 n배율(방향 자체에 영향x, 크기만 변환)
        //탈것의 속력 등
        Vector3 e = a * 2;  //벡터와 상수의 곱만 가능

        //나눗셈
        Vector3 position = Vector3.one; //(1,1,1)
        position = position * 4;        //(4,4,4)
        position = position / 4;        //(1,1,1)


        //내적과 외적
        //상대적인 각도에 따른 플레이어와 npc의 상호작용 정의

        //내적: 두 벡터의 성분을 곱하고 결과를 모두 더하기
        Vector3 k=new Vector3(1,2,3);
        Vector3 l=new Vector3(4,5,6);

        float dot=Vector3.Dot(k, l);
        //k*l=(kx*lx)+(ky*ly)+(kz*lz);
        //각 좌표가 얼마나 평행한지를 판단
        //두 벡터 사이의 각도 계산 
        //플레이어 움직임 요소를 정의 시 자주 사용

        //외적
        //법선 벡터 계산 시 사용
        //3D 그래픽스에 응용
        Vector3 cross=Vector3.Cross(k, l);
        //k*l=(ky*lz-kz*ly,kz*lx-kx*lz,kx*ly-ky*lx)
        //k(kx,ky,kz)
        //l(lx,ly,lz)

        //벡터의 크기(길이)
        Vector3 m = new Vector3(1, 2, 3);
        float mag=m.magnitude;
        //벡터의 각 성분의 제곱 합의 제곱근
    }

}
