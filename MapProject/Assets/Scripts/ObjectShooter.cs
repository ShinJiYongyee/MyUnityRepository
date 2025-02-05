using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
//Shoot()���� Rigidbody�� �ٷ� �䱸�ϹǷ� �ش� �Ӽ��� ������ �ʵ��� ����
//�ٸ� ����� ������ ���� �ڵ�
public class ObjectShooter : MonoBehaviour
{
    // �߻� ����� �������ִ� Ŭ����(�߻�)
    // �浹 �� ������Ʈ�� �������ִ� ���ҵ� ����(�߻� �� ����)
    GameObject objectGenerator;

    void Start()
    {
        objectGenerator = GameObject.Find("ObjectGenerator");
        //������ �ش� �̸��� ���� ���ӿ�����Ʈ�� ã�� �� ���� ������
        //GameObject.Find()���

        ////==������Ʈ Ž�� ���==
        //objectGenerator = GameObject.FindWithTag("Generator");
        ////Generator �±׸� ���� ������Ʈ Ž��
        ///
        //obj = FindObjectOfType<ObjectGenerator>();
        ////<>�� Ÿ�Կ� �´� ������Ʈ Ž��

        //Find�� ���� ����
        //�˻� ������ ���� ��� ���ʿ��� ���� ���� �߻�
        //�� �� Tag�� Type������ �˻� ���� �����Ͽ� Ž��
        //scene�� �ش� ���� ������ null

    }

    /// <summary>
    /// ��ü�� �߻��ϴ� �Լ�
    /// </summary>
    /// <param name="direction">��ü�� �߻� ����</param>
    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction);
        //objectGenerator ���� GetComponent�� ���� 
        //���� ������Ʈ�� ���� GetComponent�� ���

    }
    /// <summary>
    /// �浹 �� ������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().isKinematic = true;       //�浹�� ���ÿ� ��ǥ ����
        GetComponentInChildren<ParticleSystem>().Play();    
        //�ڽ����� ��ϵ� ��ƼŬ�ý��� ����

        //������ �浹 �� 1���� �ı�
        if (collision.gameObject.tag == "terrain")
        {
            Destroy(gameObject, 1.0f);
        }
        if(collision.gameObject.tag == "target")
        {
            objectGenerator.GetComponent<ObjectGenerator>().ScorePlus(10);
            Debug.Log("�¾ҽ��ϴ�!");
        }
        if(collision.gameObject.tag == "bamsongi")
        {
            Destroy(gameObject,1f);
            Destroy(collision.gameObject,1f);

        }

    }


}
