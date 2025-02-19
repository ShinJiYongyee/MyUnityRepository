using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //���͸� �ʿ� ���� �� ��ŭ �� �� �������� ��ȯ
    public GameObject monster_prefab;
    public int monster_count;
    public float monster_spawn_time;
    public float summon_radius = 5.0f;    //��ȯ �ݰ�
    public float min_spawn_distance = 3f;     //�ּ� ��ȯ �Ÿ�

    //������ ����/�÷��̾�
    public static List<Monster> monster_list = new List<Monster>();
    public static List<Player> player_list = new List<Player>();

    private void Start()
    {
        StartCoroutine("SpawnMonsterPooling");
    }

    /// <summary>
    /// �Ϲ����� ���� ���
    /// </summary>
    IEnumerator SpawnMonster()
    {
        Vector3 pos;        //���� ��ǥ

        for (int i = 0; i < monster_count; i++)
        {
            //�������� �������� ���� �Ÿ� ������ ��ġ�� ���� ���� ��� ����
            pos = Vector3.zero + Random.insideUnitSphere * summon_radius;

            pos.y = 0.0f;   //����/���� ���� ����

            //��ȯ �Ÿ��� �ּ�ġ ������ ��� �����
            while (Vector3.Distance(pos, Vector3.zero)<=min_spawn_distance)
            {
                //�������� �������� ���� �Ÿ� ������ ��ġ�� ���� ���� ��� ����
                pos = Vector3.zero + Random.insideUnitSphere * summon_radius;

                pos.y = 0.0f;   //����/���� ���� ����

            }
            //���� �������� ������ ��ġ��(pos) ȸ�� ���� ����
            GameObject go = Instantiate(monster_prefab,pos,Quaternion.identity);
        }
        //���� Ÿ�Ӹ�ŭ ��ٸ� ���� �ٽ� �ڷ�ƾ ����
        yield return new WaitForSeconds(monster_spawn_time);
        StartCoroutine("SpawnMonster");
    }  

    /// <summary>
    /// ������Ʈ Ǯ�� ������� ����� ���
    /// </summary>
    IEnumerator SpawnMonsterPooling()
    {
        Vector3 pos;        //���� ��ǥ

        for (int i = 0; i < monster_count; i++)
        {
            //�������� �������� ���� �Ÿ� ������ ��ġ�� ���� ���� ��� ����
            pos = Vector3.zero + Random.insideUnitSphere * summon_radius;

            pos.y = 0.0f;   //����/���� ���� ����

            //��ȯ �Ÿ��� �ּ�ġ ������ ��� �����
            while (Vector3.Distance(pos, Vector3.zero) <= min_spawn_distance)
            {
                //�������� �������� ���� �Ÿ� ������ ��ġ�� ���� ���� ��� ����
                pos = Vector3.zero + Random.insideUnitSphere * summon_radius;

                pos.y = 0.0f;   //����/���� ���� ����

            }

            //������ �Լ��� ���� ���(�Ϲ� ����)
            //var go = BaseManager.POOL.PoolObject("Monster").GetGameObject();

            //������ �Լ��� �ִ� ���(Action<GameObject>)
            //�׼��� ���� ��� ó��
            var go = BaseManager.POOL.PoolObject("Monster").GetGameObject((value) =>
            {
                //value.GetComponent<Monster>().MonsterSample();
                value.transform.position = pos;
                value.transform.LookAt(Vector3.zero);

                monster_list.Add(value.GetComponent<Monster>());

            });

            //StartCoroutine(ReturnMonsterPooling(go));

        }


        //���� Ÿ�Ӹ�ŭ ��ٸ� ���� �ٽ� �ڷ�ƾ ����
        yield return new WaitForSeconds(monster_spawn_time);
        StartCoroutine("SpawnMonsterPooling");
    }

    //Ǯ���� �� �ݳ�, ������ ���� �ı�
    IEnumerator ReturnMonsterPooling(GameObject go)
    {
        yield return new WaitForSeconds(monster_spawn_time);
        BaseManager.POOL.pool_dict["Monster"].ObjectReturn(go);
    }

}
