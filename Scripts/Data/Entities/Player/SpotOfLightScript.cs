using UnityEngine;

namespace EntityUtils.PlayerUtils
{
    public class SpotOfLightScript : MonoBehaviour
    {
        void Start()
        {
            Physics.IgnoreLayerCollision(0, 7);
        }
    }
}