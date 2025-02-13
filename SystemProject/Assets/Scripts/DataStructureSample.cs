using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructureSample : MonoBehaviour
{
    //string 형태의 값만 저장할 수 있는 큐
    public Queue<string> stringQueue = new Queue<string>();

    private void Start()
    {
        //1) 데이터 추가
        stringQueue.Enqueue("나좀 도와주세요");
        stringQueue.Enqueue("무슨 일이죠?");
        stringQueue.Enqueue("바나나 껍질 20개가 필요해요");
        stringQueue.Enqueue("도와드리겠습니다");
        stringQueue.Enqueue("감사합니다");

        //2) 첫 번째 데이터 조회
        foreach (string dialog in stringQueue)
        {
            Debug.Log(stringQueue.Peek());     //큐의 첫 번째 값 반환
        }

        //3) 큐의 데이터 삭제
        
        Debug.Log(stringQueue.Dequeue());  //큐의 첫 번째 값 반환과 동시에 제거
        Debug.Log(stringQueue.Dequeue());  //큐의 첫 번째 값 반환과 동시에 제거
        Debug.Log(stringQueue.Dequeue());  //큐의 첫 번째 값 반환과 동시에 제거
        Debug.Log(stringQueue.Dequeue());  //큐의 첫 번째 값 반환과 동시에 제거
        Debug.Log(stringQueue.Dequeue());  //큐의 첫 번째 값 반환과 동시에 제거

    }
}
