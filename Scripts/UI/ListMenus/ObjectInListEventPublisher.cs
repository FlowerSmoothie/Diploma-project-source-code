using System;
using UnityEngine;

public class ObjectInListEventPublisher
{
    private static ObjectInListEventPublisher instance;
        protected ObjectInListEventPublisher() { }

        public static ObjectInListEventPublisher i
        {
            get
            {
                if (instance == null) instance = new ObjectInListEventPublisher();
                return instance;
            }
        }

        public delegate void OnButonClicking(object sender, EventArgs e, int ID);
        public event OnButonClicking onButonClicking;
        public void DoOnButonClicking(object sender, EventArgs e, int ID) => onButonClicking?.Invoke(sender, e, ID);

        public delegate void OnMergeClicking(object sender, EventArgs e);
        public event OnMergeClicking onMergeClicking;
        public void DoOnMergeClicking(object sender, EventArgs e) => onMergeClicking?.Invoke(sender, e);
}