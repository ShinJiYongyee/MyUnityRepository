using System;
using UnityEngine;

//T Singleton
//<T>부분에 타입을 넣어서 해당 형태로 만들어주는 싱글톤
public class TSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            //인스턴스가 비어 있다면 
            if (instance == null)
            {
                //게임 씬 내에서 해당 타입을 가지고 있는 오브젝트 탐색
                //(T)로 형변환
                instance = (T)FindAnyObjectByType(typeof(T));    

                //씬에서 조사했음에도 비어있을(=찾지 못했을) 경우
                if (instance == null)
                {
                    //해당 타입의 이름으로 게임 오브젝트를 생성
                    //만드려는 데이터가 GameObject라면 GameObject로 명명
                    var manager = new GameObject(typeof(T).Name);
                    //매니저에 해당 타입을 컴포넌트로써 연결
                    instance = manager.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    //protected : 상속 범위까지 적용
    protected void Awake()
    {
        if(instance == null)
        {
            //클래스 자신(this)을 T로 취급
            instance = this as T;
            //로드해도 파괴되지 않도록 설정
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(Instance);
        }
    }
}
