using UnityEngine;

public class GenericSample : MonoBehaviour
{
    //배열을 전달받아서 배열의 요소를 순서대로 출력하는 코드
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
        //printGArray<int>(floats);     //실행 불가
        printGArray(floats);

        string[] strings = { "ass", "boob", "cock" };
        printGArray(strings);
    }

}
