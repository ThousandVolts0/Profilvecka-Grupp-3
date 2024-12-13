using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class Upgrade3 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("U3 Hover Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("U3 Hover Exit");
    }
}