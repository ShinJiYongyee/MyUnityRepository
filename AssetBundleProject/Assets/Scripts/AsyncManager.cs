using System.Threading.Tasks;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class AsyncManager : MonoBehaviour
{

    void Start()
    {
        Debug.Log("�۾��� �����մϴ�.");
        Sample1();
        Debug.Log("Process A");
    }

    private async void Sample1()
    {
        Debug.Log("Process B");
        await Task.Delay(5000); //1000 -> 1��
        Debug.Log("Process C");
    }
}
