using System;

namespace Misc.UI
{
    public class DialogueBoxEventPublisher
    {
        private static DialogueBoxEventPublisher instance;
        protected DialogueBoxEventPublisher() { }

        public static DialogueBoxEventPublisher i
        {
            get
            {
                if (instance == null) instance = new DialogueBoxEventPublisher();
                return instance;
            }
        }

        public delegate void OnWritingStarting(object sender, EventArgs e);
        public event OnWritingStarting onWritingStarting;
        public void DoOnWritingStarting(object sender, EventArgs e) => onWritingStarting?.Invoke(sender, e);


        public delegate void OnPhraseWritingFinishing(object sender, EventArgs e);
        public event OnPhraseWritingFinishing onPhraseWritingFinishing;
        public void PhraseWritingFinishing(object sender, EventArgs e) => onPhraseWritingFinishing?.Invoke(sender, e);
        public delegate void OnPhraseWritingStarting(object sender, EventArgs e);
        public event OnPhraseWritingStarting onPhraseWritingStarting;
        public void PhraseWritingStarting(object sender, EventArgs e) => onPhraseWritingStarting?.Invoke(sender, e);

        public delegate void OnWritingStopping(object sender, EventArgs e);
        public event OnWritingStopping onWritingStopping;
        public void DoOnWritingStopping(object sender, EventArgs e) => onWritingStopping?.Invoke(sender, e);

        public delegate void DiscardSelfClosing(object sender, EventArgs e);
        public event DiscardSelfClosing onDiscardSelfClosing;
        public void DoDiscardSelfClosing(object sender, EventArgs e) => onDiscardSelfClosing?.Invoke(sender, e);

    }
    
    public class MemoReadingEventPublisher
    {
        private static MemoReadingEventPublisher instance;
        protected MemoReadingEventPublisher() { }

        public static MemoReadingEventPublisher i
        {
            get
            {
                if (instance == null) instance = new MemoReadingEventPublisher();
                return instance;
            }
        }

        public delegate void OnWritingStarting(object sender, EventArgs e);
        public event OnWritingStarting onWritingStarting;
        public void DoOnWritingStarting(object sender, EventArgs e) => onWritingStarting?.Invoke(sender, e);

        public delegate void OnWritingStopping(object sender, EventArgs e);
        public event OnWritingStopping onWritingStopping;
        public void DoOnWritingStopping(object sender, EventArgs e) => onWritingStopping?.Invoke(sender, e);

        public delegate void OnPhraseWritingFinishing(object sender, EventArgs e);
        public event OnPhraseWritingFinishing onPhraseWritingFinishing;
        public void PhraseWritingFinishing(object sender, EventArgs e) => onPhraseWritingFinishing?.Invoke(sender, e);
        public delegate void OnPhraseWritingStarting(object sender, EventArgs e);
        public event OnPhraseWritingStarting onPhraseWritingStarting;
        public void PhraseWritingStarting(object sender, EventArgs e) => onPhraseWritingStarting?.Invoke(sender, e);
    }
}