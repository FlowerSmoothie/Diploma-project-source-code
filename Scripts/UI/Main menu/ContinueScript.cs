using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Misc.UI.Main
{
    public class ContinueScript : MonoBehaviour
    {
        private void Start()
        {
            if(SaveSystem.LoadData() == null)
            {
                GetComponent<Button>().interactable = false;
            }
        }

        public void OnClick()
        {
            FindAnyObjectByType<AudioSourcesScript>().DeleteThis();
            GetComponent<SceneSwitcher>().ChangeToScene(9);
        }
    }
}