using System.Collections;
using UnityEngine;
using Utils;
using Utils.Text;

namespace Misc.UI.Main
{
    public class NewGameScript : MonoBehaviour
    {
        bool hasASave;
        bool waiting = false;

        DialogueBoxScript dialogueBox;
        private void Start()
        {
            hasASave = SaveSystem.LoadData() == null ? false : true;

            waiting = false;
            dialogueBox = FindAnyObjectByType<DialogueBoxScript>();
        }
        private void Update()
        {
            if (waiting)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    StartGame();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Cancel();
                }
            }
        }

        public void OnClick()
        {
            if (hasASave)
            {
                dialogueBox.SelfClosingDeactivate();
                dialogueBox.WriteTextFunc(PhrasesList.NEW_GAME_OPTIONS, 0.1f, true);
                StartCoroutine(WaitThenWait());
            }
            else
            {
                GetComponent<SceneSwitcher>().ChangeToScene(1);
                FindAnyObjectByType<AudioSourcesScript>().DeleteThis();
            }
        }

        private void CloseDialogue()
        {
            waiting = false;
            UIEventPublisher.i.DoDialogueBoxDeactivating(this, null);
        }

        private IEnumerator WaitThenWait()
        {
            yield return new WaitForSecondsRealtime(1f);
            waiting = true;
        }

        private void StartGame()
        {
            CloseDialogue();

            SaveSystem.DeleteData();
            FindAnyObjectByType<AudioSourcesScript>().DeleteThis();
            GetComponent<SceneSwitcher>().ChangeToScene(1);
        }

        private void Cancel()
        {
            CloseDialogue();
        }
    }
}