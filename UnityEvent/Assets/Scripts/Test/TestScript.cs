using UnityEngine;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour, IPointerClickHandler
{
    delegate void PointerClickHandler();
    PointerClickHandler handler;
    public void OnPointerClick(PointerEventData eventData)
    {
        handler();
    }
    void Start()
    {

        handler = new PointerClickHandler(EmptyAction);

        handler += LeftKick;
        handler += RightKick;
        handler += RightPunch;
    }
    void EmptyAction()
    {

    }
    void LeftPunch()
    {
        Debug.Log("���ָ�!");
    }
    void RightPunch()
    {
        Debug.Log("�����ָ�!");
    }
    void LeftKick()
    {
        Debug.Log("�޹�����!");
    }
    void RightKick()
    {
        Debug.Log("����������!");
    }
}
