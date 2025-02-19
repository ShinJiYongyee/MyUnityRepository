using System;
using System.Collections.Generic;
using UnityEngine;

//풀에 대한 작업 시 필요한 정보들을 보관하는 인터페이스
public interface IPool
{
    //property
    Transform parent { get; set; } //인터페이스는 인스턴스화 불가, 필드를 프로퍼티 형태로 선언
    Queue<GameObject> pool { get; set; }

    //Function
    //default parameter : 값을 따로 넣지 않았을 경우 지정한 값으로 자동 처리
    //ex) var go = GetGameObject(action);

    //몬스터를 가져오는 기능
    GameObject GetGameObject(Action<GameObject> action = null);

    //몬스터를 반납하는 기능
    void ObjectReturn(GameObject obj, Action<GameObject> action = null);  //첫 번째 매개변수는 null 불가
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
    } = new Queue<GameObject>();    //새 자료구조 선언

    public GameObject GetGameObject(Action<GameObject> action = null)
    {
        var obj = pool.Dequeue();   //큐에서 값 빼오기
        obj.SetActive(true);        //SetActive: 유니티 상에서 오브젝트 활성화

        //액션으로 전달받은 값이 있다면
        if(action != null)
        {
            action.Invoke(obj);    //전달받은 액션 실행
        }

        return obj;
    }

    public void ObjectReturn(GameObject obj, Action<GameObject> action = null)
    {
        pool.Enqueue(obj);
        obj.transform.parent = parent;
        obj.SetActive(false);

        //액션으로 전달받은 값이 있다면
        if (action != null)
        {
            action.Invoke(obj);    //전달받은 액션 실행
        }
    }
}

public class PoolManager //BaseManager를 통해 접근하는 데이터 형식
{
    //(key, value) = (string, IPool로 만든 오브젝트)
    public Dictionary<string, IPool> pool_dict = new Dictionary<string, IPool>();

    public IPool PoolObject(string path)
    {
        //해당 키가 없다면 추가
        if (!pool_dict.ContainsKey(path))
        {
            Add(path);
        }

        //큐에 없는 경우 enqueue
        if(pool_dict[path].pool.Count <= 0)
        {
            AddQ(path);
        }

        return pool_dict[path];
    }

    public GameObject Add(string path)
    {
        //전달받은 이름으로 Pool Object 생성
        var obj = new GameObject(path + "Pool");

        //오브젝트 풀 생성
        ObjectPool object_pool = new ObjectPool();

        //경로와 오브젝트 풀을 딕셔너리에 저장
        pool_dict.Add(path, object_pool);

        //트랜스폼 설정
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
