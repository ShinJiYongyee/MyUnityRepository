using UnityEngine;

public class GenericSample : MonoBehaviour
{
    //�迭�� ���޹޾Ƽ� �迭�� ��Ҹ� ������� ����ϴ� �ڵ�
    public static void printGArray<T>(T[] value)
    {
        for (int i = 0; i < value.Length; i++)
        {
            Debug.Log(value[i]);
        }
    }

    void Start()
    {
        int[] numbers = { 1, 2, 3 };
        printGArray(numbers);

        float[] floats = {1.1f, 2f, 3f };
        //printGArray<int>(floats);     //���� �Ұ�
        printGArray(floats);

        string[] strings = { "ass", "boob", "cock" };
        printGArray(strings);
    }

}
