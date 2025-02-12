using UnityEngine;
using UnityEngine.EventSystems;

//유니티에서 제공해주는 Event, IPointer


public class UInterSample : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("클릭했따");
    }

}
