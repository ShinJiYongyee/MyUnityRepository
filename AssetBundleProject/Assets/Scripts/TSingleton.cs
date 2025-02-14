using System;
using UnityEngine;

//T Singleton
//<T>�κп� Ÿ���� �־ �ش� ���·� ������ִ� �̱���
public class TSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            //�ν��Ͻ��� ��� �ִٸ� 
            if (instance == null)
            {
                //���� �� ������ �ش� Ÿ���� ������ �ִ� ������Ʈ Ž��
                //(T)�� ����ȯ
                instance = (T)FindAnyObjectByType(typeof(T));    

                //������ ������������ �������(=ã�� ������) ���
                if (instance == null)
                {
                    //�ش� Ÿ���� �̸����� ���� ������Ʈ�� ����
                    //������� �����Ͱ� GameObject��� GameObject�� ���
                    var manager = new GameObject(typeof(T).Name);
                    //�Ŵ����� �ش� Ÿ���� ������Ʈ�ν� ����
                    instance = manager.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    //protected : ��� �������� ����
    protected void Awake()
    {
        if(instance == null)
        {
            //Ŭ���� �ڽ�(this)�� T�� ���
            instance = this as T;
            //�ε��ص� �ı����� �ʵ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(Instance);
        }
    }
}
