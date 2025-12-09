using System;
using UnityEngine;

namespace Misc.UI
{
    [RequireComponent(typeof(Animator))]
    public class MenusButtonScript : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();  
            animator.SetBool("visible", true);
            UIEventPublisher.i.onUIActivating += HideHelpers;
            UIEventPublisher.i.onUIFullyDeactivating += UnhideHelpers; 
        }

        

        private void HideHelpers(object sender, EventArgs e)
        {
            animator.SetBool("visible", false);
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onUIActivating -= HideHelpers;
            UIEventPublisher.i.onUIFullyDeactivating -= UnhideHelpers; 
        }

        private void UnhideHelpers(object sender, EventArgs e)
        {
            animator.SetBool("visible", true);
        }
    }
}