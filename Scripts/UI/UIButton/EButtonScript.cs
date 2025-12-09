using System;
using UnityEngine;

namespace Misc.UI
{
    public class EButtonScript : UIButtonScript
    {
        //private Animator thisAnimator;

        private void Start()
        {
            //thisAnimator = GetComponent<Animator>();

            PlayerEventPublisher.i.onItemFound += LightUp;
            PlayerEventPublisher.i.onItemDefound += LightDown;

            DialogueBoxEventPublisher.i.onPhraseWritingStarting += LightDown;
            DialogueBoxEventPublisher.i.onPhraseWritingFinishing += LightUp;

            MemoReadingEventPublisher.i.onPhraseWritingStarting += LightDown;
            MemoReadingEventPublisher.i.onPhraseWritingFinishing += LightUp;

            DialogueBoxEventPublisher.i.onWritingStopping += LightDown;
            MemoReadingEventPublisher.i.onWritingStopping += LightDown;

            UIEventPublisher.i.onChoicesMenuActivating += LightDown;


            PlayerEventPublisher.i.onRetrieveUI += LightDown;
            //PlayerEventPublisher.i.onHideUI += LightUp;
        }

        private void OnDestroy()
        {
            PlayerEventPublisher.i.onItemFound -= LightUp;
            PlayerEventPublisher.i.onItemDefound -= LightDown;

            DialogueBoxEventPublisher.i.onPhraseWritingStarting -= LightDown;
            DialogueBoxEventPublisher.i.onPhraseWritingFinishing -= LightUp;

            MemoReadingEventPublisher.i.onPhraseWritingStarting -= LightDown;
            MemoReadingEventPublisher.i.onPhraseWritingFinishing -= LightUp;

            DialogueBoxEventPublisher.i.onWritingStopping -= LightDown;
            MemoReadingEventPublisher.i.onWritingStopping -= LightDown;

            UIEventPublisher.i.onChoicesMenuActivating -= LightDown;

            PlayerEventPublisher.i.onRetrieveUI -= LightDown;
            //PlayerEventPublisher.i.onHideUI -= LightUp;
        }

        private void LightDown(object sender, EventArgs e)
        {
            if (animator == null) { LoadThis(); }
            animator.SetBool("visible", false);
        }

        private void LightUp(object sender, EventArgs e)
        {
            if (e == null)
            {
                if (animator == null) { LoadThis(); }
                animator.SetBool("visible", true);
               // Debug.Log(sender);
            }
        }
    }
}