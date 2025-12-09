using System;

namespace Misc.UI
{
    public class SelectableUIPublisher
    {
        private static SelectableUIPublisher instance;
        protected SelectableUIPublisher() { }

        public static SelectableUIPublisher i
        {
            get
            {
                if (instance == null) instance = new SelectableUIPublisher();
                return instance;
            }
        }



        public delegate void OnOptionChoosing(object sender, EventArgs e, int option);
        public event OnOptionChoosing onOptionChoosing;
        public void DoOptionChoosing(object sender, EventArgs e, int option) => onOptionChoosing?.Invoke(sender, e, option);
    }
    
    
}
