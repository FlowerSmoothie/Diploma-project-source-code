using UnityEngine;

namespace Misc.UI
{
    public class CloseDiaryScript : CloseUIScript
    {
        public override void CloseUI()
        {
            UIEventPublisher.i.DoOnDiaryMenuDeActivating(this, null);
        }
    }
}