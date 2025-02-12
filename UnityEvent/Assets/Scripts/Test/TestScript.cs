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
        Debug.Log("¿ÞÁÖ¸Ô!");
    }
    void RightPunch()
    {
        Debug.Log("¿À¸¥ÁÖ¸Ô!");
    }
    void LeftKick()
    {
        Debug.Log("¿Þ¹ßÂ÷±â!");
    }
    void RightKick()
    {
        Debug.Log("¿À¸¥¹ßÂ÷±â!");
    }
}
