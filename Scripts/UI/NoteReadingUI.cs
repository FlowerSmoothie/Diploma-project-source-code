using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Classes;

namespace Misc.UI
{
    public class NoteReadingUI : TextWritingUI
    {
        [SerializeField] GameObject closeButton;

        public override void NotifyAboutStartOfWriting()
        {
            MemoReadingEventPublisher.i.DoOnWritingStarting(this, null);
        }
        public override void NotifyAboutEndOfWriting(bool send = true)
        {
            MemoReadingEventPublisher.i.DoOnWritingStopping(this, null);
        }

        protected override void Start()
        {
            base.Start();

            UIEventPublisher.i.onNoteUIActivating += ActivateMenu;
            UIEventPublisher.i.onNoteUIDeactivating += DeactivateMenu;

            MemoReadingEventPublisher.i.onWritingStopping += AllowCloseButton;
        }

        private void AllowCloseButton(object sender, EventArgs e)
        {
            //MemoReadingEventPublisher.i.onWritingStopping -= AllowCloseButton;
            closeButton.SetActive(true);
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onNoteUIActivating -= ActivateMenu;
            UIEventPublisher.i.onNoteUIDeactivating -= DeactivateMenu;

            MemoReadingEventPublisher.i.onWritingStopping -= AllowCloseButton;
        }

        protected override void ActivateMenu(object sender, EventArgs e)
        {
            isActive = !isActive;
            //if(animator == null) { animator = GetComponent<Animator>(); }
            closeButton.SetActive(false);
            animator.SetBool("visible", true);
        }

        protected void DeactivateMenu(object sender, EventArgs e)
        {
            isActive = !isActive;
            closeButton.SetActive(false);
            animator.SetBool("visible", false);
        }



        public void WriteTextFunc(TextUnit name, List<TextUnit> phrases)
        {
            UIEventPublisher.i.DoUIActivating(this, null);
            UIEventPublisher.i.DoNoteUIActivating(this, null);
            writeCoroutine = StartCoroutine(WriteText(name, phrases));
        }

        public void WriteTextFunc(string name, List<string> phrases)
        {
            UIEventPublisher.i.DoUIActivating(this, null);
            UIEventPublisher.i.DoNoteUIActivating(this, null);
            writeCoroutine = StartCoroutine(WriteText(name, phrases));
        }
    }
}