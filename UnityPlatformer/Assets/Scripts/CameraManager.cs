using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //ī�޶��� ��ũ�� ���� ��
    public float left_limit = 0.0f;
    public float right_limit = 0.0f;
    public float top_limit = 0.0f;
    public float bottom_limit = 0.0f;

    //���� ��ũ��
    public GameObject sub_screen;

    //���� ��ũ�� ��� ó��
    public bool isForceScrollX = false;     //X�� ����
    public bool isForceScrollY = false;     //Y�� ����
    public float forceScrollSpeedX = 0.5f;  //�ʴ� ������ X�� �Ÿ�
    public float forceScrollSpeedY = 0.5f;  //�ʴ� ������ Y�� �Ÿ�

    // Update is called once per frame
    void Update()
    {
        //�÷��̾� Ž��
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = transform.position.z;

        //���� ���� ��ũ��
        if (isForceScrollX)
        {
            x=transform.position.x + (forceScrollSpeedX*Time.deltaTime);
        }
        //���� ���� ��ũ��
        if (isForceScrollY)
        {
            x = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
        }

        //���� ���� ����ȭ
        if (x < left_limit) x = left_limit;
        else if (x > right_limit) x = right_limit;
        //���� ���� ����ȭ
        if (y < bottom_limit) y = bottom_limit;
        else if (y > top_limit) y = top_limit;

        //������ ī�޶� ��ġ�� Vector3�� ǥ��
        Vector3 vector3 = new Vector3(x, y, z);
        //ī�޶� ��ġ�� ���������� ǥ��
        transform.position = vector3;

        //���� ��ũ���� x�� �̵��� �÷��̾��� 0.5�� �ӷ����� �̷������
        if(sub_screen != null)
        {
            y=sub_screen.transform.position.y;
            z=sub_screen.transform.position.z;
            Vector3 v = new Vector3(x * 0.5f, y, z);
            sub_screen.transform.position = v;
        }
    }

}
