
using System;
using System.Collections;
using System.Collections.Generic;
using Misc.UI.Deduction;
using UnityEngine;
using Utils.Classes;

namespace Misc.UI
{
    public class DialogueBoxScript : TextWritingUI
    {
        protected override void Start()
        {
            base.Start();
            UIEventPublisher.i.onDialogueBoxActivating += ActivateMenu;
            UIEventPublisher.i.onDialogueBoxDeactivating += DeactivateMenu;
        }



        public override void NotifyAboutStartOfWriting()
        {
            DialogueBoxEventPublisher.i.DoOnWritingStarting(this, null);
        }
        public override void NotifyAboutEndOfWriting(bool send = true)
        {
            if (send)
            {
                DialogueBoxEventPublisher.i.DoOnWritingStopping(this, null);
            }
            else
            {
                DialogueBoxEventPublisher.i.DoOnWritingStopping(this, new EventArgs());
            }
        }



        private void OnDestroy()
        {
            UIEventPublisher.i.onDialogueBoxActivating -= ActivateMenu;
            UIEventPublisher.i.onDialogueBoxDeactivating -= DeactivateMenu;
        }

        protected override void ActivateMenu(object sender, EventArgs e)
        {
            isActive = true;
            animator.SetBool("visible", true);
        }

        protected void DeactivateMenu(object sender, EventArgs e)
        {
            isActive = false;
            animator.SetBool("visible", false);
            animator.SetBool("blackVisible", false);
        }

        public void WriteTextFunc(PhraseToSay phrase, float waitingTime, bool needBlack = false)
        {
            List<PhraseToSay> phrases = new List<PhraseToSay> { phrase };

            if(needBlack) { animator.SetBool("blackVisible", true); }

            UIEventPublisher.i.DoUIActivating(this, null);

            StartCoroutine(CallWithDelay(waitingTime));

            if (writeCoroutine != null) StopCoroutine(writeCoroutine);

            writeCoroutine = StartCoroutine(WriteText(phrases, false, waitingTime));
        }

        public void WriteTextFunc(ContinuationUnit unit, float waitingTime)
        {
            List<PhraseToSay> phrases = new List<PhraseToSay>();
            phrases.AddRange(unit.GetTexts());

            UIEventPublisher.i.DoUIActivating(this, null);

            StartCoroutine(CallWithDelay(waitingTime));

            if (writeCoroutine != null) StopCoroutine(writeCoroutine);

            writeCoroutine = StartCoroutine(WriteText(phrases, false, waitingTime));
        }

        private IEnumerator CallWithDelay(float waitingTime)
        {
            yield return new WaitForSecondsRealtime(waitingTime);
            UIEventPublisher.i.DoDialogueBoxActivating(this, null);
        }

        public void WriteTextFunc(List<PhraseToSay> phrases, float waitingTime)
        {
            UIEventPublisher.i.DoUIActivating(this, null);

            StartCoroutine(CallWithDelay(waitingTime));

            if (writeCoroutine != null) StopCoroutine(writeCoroutine);
            writeCoroutine = StartCoroutine(WriteText(phrases, false, waitingTime));
        }
    }
}