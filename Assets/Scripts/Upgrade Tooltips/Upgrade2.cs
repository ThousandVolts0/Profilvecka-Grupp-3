using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrade2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("U2 Hover Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("U2 Hover Exit");
    }
}