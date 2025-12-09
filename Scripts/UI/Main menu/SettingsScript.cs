using UnityEngine;

namespace Misc.UI.Main
{
    public class SettingsScript : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] bool toSettings;
        public void OnClick()
        {
            animator.SetBool("settings", toSettings);
        }
    }
}