using System;
using Misc.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Misc.UI
{
    public class ItemShowingUIScript : UIMenuScript
    {
        [SerializeField] Image imageContainer;
        private void Start()
        {
            UIEventPublisher.i.onItemShowingDeActivating += ActivateMenu;
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onItemShowingDeActivating -= ActivateMenu;
        }

        public void SetImage(Sprite sprite)
        {
            imageContainer.sprite = sprite;
            UIEventPublisher.i.DoItemShowingDeActivating(this, null);
            UIEventPublisher.i.onItemShowingDeActivating -= ActivateMenu;
            UIEventPublisher.i.onDialogueBoxDeactivating += DeactivateMenu;
        }

        private void DeactivateMenu(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= DeactivateMenu;
            UIEventPublisher.i.onItemShowingDeActivating += ActivateMenu;
            UIEventPublisher.i.DoItemShowingDeActivating(this, null);
        }
    }
}