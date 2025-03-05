using UnityEngine;


//�Է��̳� �ٸ� �̺�Ʈ�� ó���Ѵ�.
//���� ���� ������Ʈ�� �ٸ� ������Ʈ�� ����� ������.
//����� ������Ʈ�� �Ѱ��� �ϸ� �ؾ� �ȴ�.
public class PlayerController : MonoBehaviour
{
    MeshRenderer meshRenderer;

    float moveSpeed = 3.0f;
    float rotationSpeed = 60.0f;
    float h;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        //velocity => vector ũ��� ����
        //s = v * t => speed * direction * time 

        float v = Input.GetAxisRaw("Vertical");
        h += Input.GetAxisRaw("Horizontal") * rotationSpeed * Time.deltaTime;


        //������ǥ��, ���� ��ǥ��
        //������ ������
        //transform.position += transform.up * v * Time.deltaTime * moveSpeed;
        transform.rotation = Quaternion.Euler(0, 0, -h);
        transform.Translate(Vector3.up * v * Time.deltaTime * moveSpeed);
        //transform.eulerAngles += transform.forward * -h * Time.deltaTime * rotationSpeed;
    }
}
