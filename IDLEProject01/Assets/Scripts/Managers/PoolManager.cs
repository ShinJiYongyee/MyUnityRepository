using System;
using System.Collections.Generic;
using UnityEngine;

//Ǯ�� ���� �۾� �� �ʿ��� �������� �����ϴ� �������̽�
public interface IPool
{
    //property
    Transform parent { get; set; } //�������̽��� �ν��Ͻ�ȭ �Ұ�, �ʵ带 ������Ƽ ���·� ����
    Queue<GameObject> pool { get; set; }

    //Function
    //default parameter : ���� ���� ���� �ʾ��� ��� ������ ������ �ڵ� ó��
    //ex) var go = GetGameObject(action);

    //���͸� �������� ���
    GameObject GetGameObject(Action<GameObject> action = null);

    //���͸� �ݳ��ϴ� ���
    void ObjectReturn(GameObject obj, Action<GameObject> action = null);  //ù ��° �Ű������� null �Ұ�
}

public class ObjectPool : IPool
{
    public Transform parent
    {
        get;
        set;
    }
    public Queue<GameObject> pool
    {
        get;
        set;
    } = new Queue<GameObject>();    //�� �ڷᱸ�� ����

    public GameObject GetGameObject(Action<GameObject> action = null)
    {
        var obj = pool.Dequeue();   //ť���� �� ������
        obj.SetActive(true);        //SetActive: ����Ƽ �󿡼� ������Ʈ Ȱ��ȭ

        //�׼����� ���޹��� ���� �ִٸ�
        if(action != null)
        {
            action.Invoke(obj);    //���޹��� �׼� ����
        }

        return obj;
    }

    public void ObjectReturn(GameObject obj, Action<GameObject> action = null)
    {
        pool.Enqueue(obj);
        obj.transform.parent = parent;
        obj.SetActive(false);

        //�׼����� ���޹��� ���� �ִٸ�
        if (action != null)
        {
            action.Invoke(obj);    //���޹��� �׼� ����
        }
    }
}

public class PoolManager //BaseManager�� ���� �����ϴ� ������ ����
{
    //(key, value) = (string, IPool�� ���� ������Ʈ)
    public Dictionary<string, IPool> pool_dict = new Dictionary<string, IPool>();

    public IPool PoolObject(string path)
    {
        //�ش� Ű�� ���ٸ� �߰�
        if (!pool_dict.ContainsKey(path))
        {
            Add(path);
        }

        //ť�� ���� ��� enqueue
        if(pool_dict[path].pool.Count <= 0)
        {
            AddQ(path);
        }

        return pool_dict[path];
    }

    public GameObject Add(string path)
    {
        //���޹��� �̸����� Pool Object ����
        var obj = new GameObject(path + "Pool");

        //������Ʈ Ǯ ����
        ObjectPool object_pool = new ObjectPool();

        //��ο� ������Ʈ Ǯ�� ��ųʸ��� ����
        pool_dict.Add(path, object_pool);

        //Ʈ������ ����
        object_pool.parent = obj.transform;

        return obj;
    }

    public void AddQ(string path)
    {
        var go = BaseManager.instance.CreateFromPath(path);
        go.transform.parent = pool_dict[path].parent;
        pool_dict[path].ObjectReturn(go);
    }
}
