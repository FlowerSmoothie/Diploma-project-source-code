using System.Collections;
using UnityEngine;

namespace Misc.UI.Inventory
{
    public class ActionButtonScript : MonoBehaviour
    {
        private Animator animator;

        private bool isClicked;

        public void UnShow()
        {
            //animator.SetTrigger(Utils.Consts.DEFAULT_TRIGGER_CONST);
        }

        protected virtual void DoOnClick()
        {
            return;
        }

        public void OnClick()
        {
            if (!isClicked)
            {
                isClicked = true;
                StartCoroutine(WaitCooldown());
                DoOnClick();
            }
        }

        private void OnDisable()
        {
            isClicked = false;
        }

        private IEnumerator WaitCooldown()
        {
            yield return new WaitForSecondsRealtime(1f);
            isClicked = false;
        }
    }
}