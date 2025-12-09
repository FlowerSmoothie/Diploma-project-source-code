using UnityEngine;
using UnityEngine.EventSystems;

namespace Misc.UI.Deduction
{
    public class ClueCellScript : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                eventData.pointerDrag.GetComponent<DragDropClueScript>().parentAfterDrag = transform;
            }
        }
    }
}