using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils.Text;

namespace Misc.UI.Diary
{
    public class SaveButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private bool isSaved;

        private DiaryMenuScript diary;
        private DialogueBoxScript dialogueBox;

        private Animator animator;

        [SerializeField] AudioClip savingSound;
        private AudioSourcesScript asc;

        private bool waitingForPressing;

        private void Start()
        {
            diary = FindAnyObjectByType<DiaryMenuScript>();
            dialogueBox = FindAnyObjectByType<DialogueBoxScript>();

            animator = GetComponent<Animator>();

            asc = FindAnyObjectByType<AudioSourcesScript>();

            waitingForPressing = false;
        }

        private void Update()
        {
            if (waitingForPressing)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SaveData();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    DiscardSavingData();
                }
            }
        }

        public void SetSaved(bool saved)
        {
            isSaved = saved;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isSaved)
            {
                animator.SetBool("active", true);
                diary.SetActive();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isSaved)
            {
                animator.SetBool("active", false);
                diary.SetSleep();
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            //UIEventPublisher.i.DoUIActivating(this, null);
            dialogueBox.SelfClosingDeactivate();
            dialogueBox.WriteTextFunc(PhrasesList.SAVE_GAME_OPTIONS, 0.1f, true);

            StartCoroutine(WaitSomeSecondsThenWaitForInput());
        }

        private IEnumerator WaitSomeSecondsThenWaitForInput()
        {
            yield return new WaitForSecondsRealtime(1f);
            waitingForPressing = true;
        }

        private void SaveData()
        {
            CloseDialogue();

            asc.PlayUI(savingSound);

            FindAnyObjectByType<DataHolderScript>().SaveData();
            //FindAnyObjectByType<DataHolderScript>().IsSaved(true);

            diary.SetSaved();
        }
        private void DiscardSavingData()
        {
            CloseDialogue();

            diary.SetNotSaved();
        }

        private void CloseDialogue()
        {
            waitingForPressing = false;
            animator.SetBool("active", false);
            UIEventPublisher.i.DoDialogueBoxDeactivating(this, null);
            UIEventPublisher.i.DoUIDeactivating(this, null);
            //UIEventPublisher.i.DoUIDeactivating(this, null);
        }
    }
}