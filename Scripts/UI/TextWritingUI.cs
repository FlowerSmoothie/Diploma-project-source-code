using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils;
using Utils.Classes;
namespace Misc.UI
{
    public abstract class TextWritingUI : UIMenuScript
    {
        //private InteractableKeyScriptUI Ekey;

        protected bool isActive;

        private GameObject container;

        [SerializeField] private AudioClip typingSound;
        [SerializeField] private AudioClip openingSound;
        [SerializeField] private AudioClip closingSound;

        [SerializeField] private TextMeshProUGUI topTextField;
        [SerializeField] private TextMeshProUGUI mainTextField;

        private bool skipping = false;

        private bool coroutineIsRunning = false;
        protected Coroutine writeCoroutine = null;

        private float textSpeed = 0.035f;

        private float speed;


        private bool waitForInput = false;

        private bool doesCloseItself = true;

        private AudioSourcesScript asc;


        protected virtual void Start()
        {
            speed = textSpeed;

            container = transform.GetChild(0).gameObject;

            isActive = false;

            asc = FindAnyObjectByType<AudioSourcesScript>();

            //Ekey = FindAnyObjectByType<InteractableKeyScriptUI>();
        }

        public void SelfClosingDeactivate()
        {
            doesCloseItself = false;
        }
        public void SelfClosingActivate()
        {
            doesCloseItself = true;
        }

        private void Update()
        {
            if (isActive)
            {
                if (Input.GetButtonDown("ForwardToNextDialoguePhrase"))
                {
                    if (waitForInput) { waitForInput = false; }
                    else if (coroutineIsRunning) { skipping = true; }
                }
            }
        }

        protected override void ActivateMenu(object sender, EventArgs e)
        {
            isActive = !isActive;
            animator.SetTrigger(Consts.DEFAULT_TRIGGER_CONST);
        }

        public void DisAbleBoxContainer() { container.SetActive(!container.activeSelf); }

        public void stopWritingCoroutine() { StopCoroutine(writeCoroutine); }

        public abstract void NotifyAboutStartOfWriting();
        public abstract void NotifyAboutEndOfWriting(bool send = true);

        protected IEnumerator WriteText(List<PhraseToSay> phrases, bool immideately, float delay) //dialogue box
        {
            asc.PlayUI(openingSound);
            if (delay != 0)
            {
                yield return new WaitForSecondsRealtime(delay);
            }
            NotifyAboutStartOfWriting();
            foreach (PhraseToSay phrase in phrases)
            {
                DialogueBoxEventPublisher.i.PhraseWritingStarting(this, null);
                waitForInput = false;
                //Ekey.SetVisible(false);
                skipping = false;
                //auS.Play();
                topTextField.text = CharacterUtils.GetCharacterNickName(phrase.GetCharacter());
                mainTextField.text = "";

                string piece = TextUnitUtils.GetTextUnitText(phrase.GetText());
                mainTextField.text += " ";

                if (!immideately) yield return StartCoroutine(TypeWriterTMP(piece, speed));
                else mainTextField.text += piece;
                if (skipping)
                {
                    mainTextField.text = piece;
                }
                skipping = false;
                //auS.Stop();
                //tcps.SetWaitForInput(true);
                waitForInput = true;
                //Ekey.SetVisible(true);
                asc.StopUI(true);
                while (waitForInput == true && doesCloseItself)
                {
                    DialogueBoxEventPublisher.i.PhraseWritingFinishing(this, null);yield return new WaitForEndOfFrame();
                }
            }
            if(doesCloseItself) NotifyAboutEndOfWriting(doesCloseItself);

            if (doesCloseItself)
            {
                UIEventPublisher.i.DoDialogueBoxDeactivating(this, null);
                UIEventPublisher.i.DoUIDeactivating(this, null);
                asc.PlayUI(closingSound);
            }
            else
            {
                doesCloseItself = true;
            }


            //UICanvasEventPublisher.i.DoOnDialogueBoxClosed(this, null);
            //uics.DisableUI(UIControllerScript.UIInstance.Dialogue_box);
        }

        protected IEnumerator WriteText(TextUnit name, List<TextUnit> phrases) //note reading
        {
            string temp;
            NotifyAboutStartOfWriting();
            asc.PlayUI(openingSound);

            mainTextField.text = " ";

            foreach (TextUnit phrase in phrases)
            {
                MemoReadingEventPublisher.i.PhraseWritingStarting(this, null);
                //Ekey.SetVisible(false);
                waitForInput = false;
                skipping = false;
                //auS.Play();
                topTextField.text = TextUnitUtils.GetTextUnitText(name);

                string piece = TextUnitUtils.GetTextUnitText(phrase);
                mainTextField.text += " ";

                temp = mainTextField.text;

                yield return StartCoroutine(TypeWriterTMP(piece, speed));

                if (skipping)
                {
                    mainTextField.text = temp + piece;
                }

                skipping = false;
                //auS.Stop();
                //tcps.SetWaitForInput(true);
                waitForInput = true;
                //Ekey.SetVisible(true);
                while (waitForInput == true)
                {
                    MemoReadingEventPublisher.i.PhraseWritingFinishing(this, null);
                    yield return new WaitForEndOfFrame();
                }

                mainTextField.text += "\n\n ";
            }
            NotifyAboutEndOfWriting();

            asc.PlayUI(closingSound);

            /*UIEventPublisher.i.DoNoteUIDeactivating(this, null);
            UIEventPublisher.i.DoUIDeactivating(this, null);*/

            //UICanvasEventPublisher.i.DoOnDialogueBoxClosed(this, null);
            //uics.DisableUI(UIControllerScript.UIInstance.Dialogue_box);
        }

        protected IEnumerator WriteText(string name, List<string> phrases)
        {
            string temp;
            NotifyAboutStartOfWriting();
            asc.PlayUI(openingSound);

            mainTextField.text = " ";

            foreach (string phrase in phrases)
            {
                MemoReadingEventPublisher.i.PhraseWritingStarting(this, null);
                //Ekey.SetVisible(false);
                skipping = false;
                //auS.Play();
                topTextField.text = name;

                string piece = phrase;
                mainTextField.text += " ";

                temp = mainTextField.text;

                yield return StartCoroutine(TypeWriterTMP(piece, speed));

                if (skipping)
                {
                    asc.StopUI(true);
                    mainTextField.text = temp + piece;
                }

                skipping = false;
                //auS.Stop();
                //tcps.SetWaitForInput(true);
                waitForInput = true;
                //Ekey.SetVisible(true);
                while (waitForInput == true)
                {
                    asc.StopUI(true);
                    MemoReadingEventPublisher.i.PhraseWritingFinishing(this, null);
                    MemoReadingEventPublisher.i.PhraseWritingFinishing(this, null); yield return new WaitForEndOfFrame();
                }

                mainTextField.text += "\n\n ";
            }
            NotifyAboutEndOfWriting();

            asc.PlayUI(closingSound);

            /*UIEventPublisher.i.DoNoteUIDeactivating(this, null);
            UIEventPublisher.i.DoUIDeactivating(this, null);*/

            //UICanvasEventPublisher.i.DoOnDialogueBoxClosed(this, null);
            //uics.DisableUI(UIControllerScript.UIInstance.Dialogue_box);
        }

        private IEnumerator TypeWriterTMP(string story, float typingSpeed = -1)
        {
            coroutineIsRunning = true;
            asc.PlayUI(typingSound, true);
            foreach (char c in story)
            {
                mainTextField.text += c;
                if (skipping)
                    break;
                if (typingSpeed == -1)
                    yield return new WaitForSeconds(speed);
                else
                    yield return new WaitForSeconds(typingSpeed);
            }
            coroutineIsRunning = false;
            asc.StopUI(true);
            speed = typingSpeed;
            //yield return new WaitForSeconds(0.5f);
        }

        public void skip() { skipping = true; }


        public void clearFields()
        {
            topTextField.text = "";
            mainTextField.text = "";
        }

        //public abstract void writeTextFunc(List<PhraseToSay> phrases);
    }
}