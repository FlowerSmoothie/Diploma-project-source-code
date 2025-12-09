using UnityEngine;

namespace Misc.UI
{
    public class ClosePauseScript : CloseUIScript
    {
        public override void CloseUI()
        {
            UIEventPublisher.i.DoOnPauseMenuDeActivating(this, null);
        }
    }
}