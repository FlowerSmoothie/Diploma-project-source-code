using UnityEngine;
using UnityEngine.EventSystems;

namespace Misc.UI
{
    [RequireComponent(typeof(Animator))]
    public class UIButtonScript : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        protected Animator animator;

        [SerializeField] protected bool isAnimatedByHovering = true;

        private void Start()
        {
            LoadThis();
        }

        protected void LoadThis()
        {
            animator = GetComponent<Animator>();   
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(isAnimatedByHovering) animator.SetBool("visible", true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if(isAnimatedByHovering) animator.SetBool("visible", false);
        }
    }
}