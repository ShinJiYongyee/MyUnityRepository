using System.Threading.Tasks;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class AsyncManager : MonoBehaviour
{

    void Start()
    {
        Debug.Log("작업을 시작합니다.");
        Sample1();
        Debug.Log("Process A");
    }

    private async void Sample1()
    {
        Debug.Log("Process B");
        await Task.Delay(5000); //1000 -> 1초
        Debug.Log("Process C");
    }
}
