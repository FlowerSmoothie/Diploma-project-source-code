using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Misc.UI.Deduction
{
    public class DragDropClueScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [HideInInspector] public Transform parentAfterDrag;

        private Canvas canvas;



        private string clueName;
        private ClueInDeductionScript clue;
        private Image image;

        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            clue = GetComponent<ClueInDeductionScript>();
            //clueName = clue.GetName();
            image = clue.GetImage();

            canvas = transform.root.GetComponent<Canvas>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            image.raycastTarget = false;
        }
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            animator.SetBool("hovered", true);
            DeductionEventPublisher.i.DoOnClueHover(this, null, clue.GetName());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            animator.SetBool("hovered", false);
            DeductionEventPublisher.i.DoOnClueDehover(this, null);
        }
    }
}