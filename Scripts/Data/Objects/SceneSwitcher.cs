using System.Collections;
using Misc;
using Misc.Overworld;
using Misc.UI;
using UnityEngine;

namespace Utils
{
    public class SceneSwitcher : MonoBehaviour
    {
        private Animator animator;

        [SerializeField] bool isExitingToMainMenu;

        private void Start()
        {
            animator = FindAnyObjectByType<CurtainScript>().GetComponent<Animator>();
        }

        public void ChangeToMainMenu()
        {
            animator.SetTrigger("go");
            FindFirstObjectByType<DataHolderScript>().ClearData(true);
            StartCoroutine(wait(0, 2));
        }

        public void ChangeToScene(int sceneNumber)
        {
            animator.SetTrigger("go");
            StartCoroutine(wait(sceneNumber, 1));
        }

        public void LoadScene(int sceneNumber)
        {
            StartCoroutine(wait(sceneNumber, 5));
        }

        public void ChangeScene(int sceneNumber, int secondsToWait = 3)
        {
            if (isExitingToMainMenu) { FindFirstObjectByType<DataHolderScript>().ClearData(); }
            else
            {
                FindFirstObjectByType<DataHolderScript>().SetScene(sceneNumber);
                OverworldEventPublisher.i.SaveData(this, null);
            }

            animator.SetTrigger("go");
            StartCoroutine(wait(sceneNumber, secondsToWait));
        }

        private IEnumerator wait(int scene, int secondsToWait)
        {
            yield return new WaitForSecondsRealtime(secondsToWait);
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }
    }
}