using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bullet;    //�Ѿ� ������
    public GameObject firePosition;     //�� �߻� ��ġ

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))  
        {
            //�Ѿ� ����
            GameObject bulletObject = Instantiate(bullet);
            //�Ѿ� ��ġ ����
            bulletObject.transform.position = firePosition.transform.position;
        }
    }
}
