using Misc.UI;
using UnityEngine;

public class HideUIScript : MonoBehaviour
{
    public void HideUI()
    {        
        UIEventPublisher.i.DoUIDeactivating(this, null);
    }
}