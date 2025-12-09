using Misc.UI;
using UnityEngine;

namespace Misc.UI
{
    public class CloseInventoryScript : CloseUIScript
    {
        public override void CloseUI()
        {
            UIEventPublisher.i.DoOnInventoryMenuDeActivating(this, null);
        }
    }
}