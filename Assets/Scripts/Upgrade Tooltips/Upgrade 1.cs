using UnityEngine;
using UnityEngine.EventSystems;

public class MyClass : MonoBehaviour, IPointerEnterHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Entered U1");
    }
}