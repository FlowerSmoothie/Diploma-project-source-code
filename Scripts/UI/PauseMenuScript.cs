using Misc.UI;

namespace Misc.UI
{
    public class PauseMenuScript : UIMenuScript
    {
        private void Start()
        {
            UIEventPublisher.i.onPauseMenuDeActivating += ActivateMenu;
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onPauseMenuDeActivating -= ActivateMenu;
        }
    }
}
