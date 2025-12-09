using UnityEngine;

namespace Misc.UI
{
    public class CloseNoteReadingUIScript : CloseUIScript
    {
        public override void CloseUI()
        {
            UIEventPublisher.i.DoNoteUIDeactivating(this, null);
            UIEventPublisher.i.DoUIDeactivating(this, null);
        }
    }
}