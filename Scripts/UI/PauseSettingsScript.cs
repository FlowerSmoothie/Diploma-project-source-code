using UnityEngine;

namespace UI.Misc
{
    public class PauseSettingsScript : MonoBehaviour
    {
        [SerializeField] Animator animator;
        public void OnClick()
        {
            animator.SetTrigger("settings");
        }
    }
}