using System;
using System.Collections;
using UnityEngine;

namespace Misc.UI
{
    public class UIControllerScript : MonoBehaviour
    {

        private bool UIOpened;
        private int UICount;

        private void Start()
        {
            UIOpened = false;

            UIEventPublisher.i.onUIActivating += UIOpening;
            UIEventPublisher.i.onUIDeactivating += UIClosing;

            UIEventPublisher.i.onUIDeactivateToZero += DeactivateFully;
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onUIActivating -= UIOpening;
            UIEventPublisher.i.onUIDeactivating -= UIClosing;

            UIEventPublisher.i.onUIDeactivateToZero -= DeactivateFully;
        }

        private void Update()
        {
            if (!UIOpened)
            {
                if (Input.GetButton("Pause"))
                {
                    PrepareToUIActivation();
                    CallForPauseMenuToAppear();
                }
                else if (Input.GetButton("Inventory"))
                {
                    PrepareToUIActivation();
                    CallForInevntoryMenuToAppear();
                }
                else if (Input.GetButton("Diary"))
                {
                    PrepareToUIActivation();
                    CallForDiaryMenuToAppear();
                }
            }
        }
        private void UIOpening(object sender, EventArgs e)
        {
            UICount++;

            //Debug.Log("UI opened. Opened now: " + UICount);
            UIOpened = true;
        }
        private void UIClosing(object sender, EventArgs e)
        {
            UICount--;

            //Debug.Log("UI closed. Opened now: " + UICount);

            if (UICount < 0) UICount = 0;
            if (UICount == 0)
            {
                UIOpened = false;
                StartCoroutine(DeactivatingUI());
            }
        }

        private void DeactivateFully(object sender, EventArgs e)
        {
            UICount = 0;
            UIOpened = false;
            UIEventPublisher.i.DoUIFullyDeactivating(this, null);
        }

        private IEnumerator DeactivatingUI()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            UIEventPublisher.i.DoUIFullyDeactivating(this, null);
        }

        public int GetUICount()
        {
            return UICount;
        }

        private void PrepareToUIActivation()
        {
            UIEventPublisher.i.DoUIActivating(this, null);
        }
        private void CallForPauseMenuToAppear()
        {
            UIEventPublisher.i.DoOnPauseMenuDeActivating(this, null);
        }
        private void CallForInevntoryMenuToAppear()
        {
            UIEventPublisher.i.DoOnInventoryMenuDeActivating(this, null);
        }
        private void CallForDiaryMenuToAppear()
        {
            UIEventPublisher.i.DoOnDiaryMenuDeActivating(this, null);
        }
    }
}