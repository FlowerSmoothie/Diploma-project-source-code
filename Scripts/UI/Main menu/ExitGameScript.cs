using System.Collections;
using UnityEngine;

namespace Misc.UI.Main
{
    public class ExitGameScript : MonoBehaviour
    {
        public void OnClick()
        {
            FindAnyObjectByType<CurtainScript>().GetComponent<Animator>().SetTrigger("go");
            StartCoroutine(WaitThenExit());
        }

        private IEnumerator WaitThenExit()
        {
            yield return new WaitForSecondsRealtime(2);
            Application.Quit();
        }
    }
}