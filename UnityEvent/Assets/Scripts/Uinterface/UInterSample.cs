using UnityEngine;
using UnityEngine.EventSystems;

//����Ƽ���� �������ִ� Event, IPointer


public class UInterSample : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Ŭ���ߵ�");
    }

}
