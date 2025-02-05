using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    // ������Ʈ�� �����ϴ� ��ũ��Ʈ
    // ���콺 ��ư�� ���� �� ī�޶��� ��ũ�� ������ ���� ��ü�� ������ ����
    // ��ü�� ���⿡ ���� �߻��ϴ� ����� ȣ��

    public GameObject prefab;   //������Ʈ ������ ���
    public float power = 1000f; //������ ��
    GameObject scoreText;       //���� ǥ�� �ؽ�Ʈ
    public int score = 0;       //�Ŵ����� ���� ���¿��� ����Ƿ� ������ ������ ����

    void Update()
    {
        ///���콺 0�� ��ư�� ������ ��
        ///0: LMB
        ///1: RMB
        ///2: MMB
        ///~~Down: Ŭ���� 1ȸ
        ///~~Up: ��ư�� ������ �� 1ȸ
        ///~~: ������ ���� ���

        //��Ŭ�� 1ȸ�� �����Ǵ� �Լ�
        //1�� �߻�
        //bool�� ��ȯ
        if (Input.GetMouseButtonDown(0))
        {
            var thrown = Instantiate(prefab);
            //as GameObject�� Instantiate�� ���� ����ϸ�
            //���ӿ�����Ʈ�μ� �����Ѵٴ� �ǹ�
            //�׷��� ��Ȱ��ȭ��->���ó����� ������ ������� ����

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //ī�޶� ���� �������� ��� ������ ���� ���ͷ� �޾ƿ��� �ڵ�
            //Ray: �߻�Ǵ� ������ ������ ���� ������ ������(Vector3)
            //Ray2D: Vector2 ������ Ray

            Vector3 direction = ray.direction;
            //ray�� ������ ���ͷμ� �޾ƿ�

            thrown.GetComponent<ObjectShooter>().Shoot(direction.normalized*power);
            //ObjectShooter�� Shoot() �Լ��� public���� �����ؾ� ����
            //���⿡ ������� ���� ���� ���ϹǷ� normalized
        }
    }
    /// <summary>
    /// ���� ȹ��
    /// </summary>
    /// <param name="value"></param>
    public void Start()
    {
        scoreText = GameObject.Find("score");   //������Ʈ score�� ã�� ��������
        SetScoreText();                         //���� 1ȸ
    }
    public void ScorePlus(int value)
    {
        score += value;
        SetScoreText();     //���� �ؽ�Ʈ ����
    }
    /// <summary>
    /// ���� �ؽ�Ʈ ����
    /// ���� �� ����� ����̹Ƿ� �Լ�ȭ
    /// </summary>
    void SetScoreText()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text=$"����: {score}";
    }
}
