using System;
using System.Collections.Generic;

namespace Misc.UI
{
    public class UIEventPublisher
    {
        private static UIEventPublisher instance;
        protected UIEventPublisher() { }

        public static UIEventPublisher i
        {
            get
            {
                if (instance == null) instance = new UIEventPublisher();
                return instance;
            }
        }



        public delegate void OnUIActivating(object sender, EventArgs e);
        public event OnUIActivating onUIActivating;
        public void DoUIActivating(object sender, EventArgs e) => onUIActivating?.Invoke(sender, e);
        public delegate void OnUIDeactivating(object sender, EventArgs e);
        public event OnUIDeactivating onUIDeactivating;
        public void DoUIDeactivating(object sender, EventArgs e) => onUIDeactivating?.Invoke(sender, e);
        public delegate void OnUIDeactivateToZero(object sender, EventArgs e);
        public event OnUIDeactivateToZero onUIDeactivateToZero;
        public void DeactivateToZero(object sender, EventArgs e) => onUIDeactivateToZero?.Invoke(sender, e);
        public delegate void OnUIFullyDeactivating(object sender, EventArgs e);
        public event OnUIFullyDeactivating onUIFullyDeactivating;
        public void DoUIFullyDeactivating(object sender, EventArgs e) => onUIFullyDeactivating?.Invoke(sender, e);




        public delegate void OnPauseMenuDeActivating(object sender, EventArgs e);
        public event OnPauseMenuDeActivating onPauseMenuDeActivating;
        public void DoOnPauseMenuDeActivating(object sender, EventArgs e) => onPauseMenuDeActivating?.Invoke(sender, e);

        public delegate void OnInventoryMenuDeActivating(object sender, EventArgs e);
        public event OnInventoryMenuDeActivating onInventoryMenuDeActivating;
        public void DoOnInventoryMenuDeActivating(object sender, EventArgs e) => onInventoryMenuDeActivating?.Invoke(sender, e);

        public delegate void OnDiaryMenuDeActivating(object sender, EventArgs e);
        public event OnInventoryMenuDeActivating onDiaryMenuDeActivating;
        public void DoOnDiaryMenuDeActivating(object sender, EventArgs e) => onDiaryMenuDeActivating?.Invoke(sender, e);
        

        public delegate void OnDialogueBoxActivating(object sender, EventArgs e);
        public event OnDialogueBoxActivating onDialogueBoxActivating;
        public void DoDialogueBoxActivating(object sender, EventArgs e) => onDialogueBoxActivating?.Invoke(sender, e);
        public delegate void OnDialogueBoxDeactivating(object sender, EventArgs e);
        public event OnDialogueBoxDeactivating onDialogueBoxDeactivating;
        public void DoDialogueBoxDeactivating(object sender, EventArgs e) => onDialogueBoxDeactivating?.Invoke(sender, e);
        

        public delegate void OnNoteUIActivating(object sender, EventArgs e);
        public event OnNoteUIActivating onNoteUIActivating;
        public void DoNoteUIActivating(object sender, EventArgs e) => onNoteUIActivating?.Invoke(sender, e);
        public delegate void OnNoteUIDeactivating(object sender, EventArgs e);
        public event OnNoteUIDeactivating onNoteUIDeactivating;
        public void DoNoteUIDeactivating(object sender, EventArgs e) => onNoteUIDeactivating?.Invoke(sender, e);



        public delegate void OnMemoReadingDeActivating(object sender, EventArgs e);
        public event OnMemoReadingDeActivating onMemoReadingDeActivating;
        public void DoMemoReadingDeActivating(object sender, EventArgs e) => onMemoReadingDeActivating?.Invoke(sender, e);

        public delegate void OnChoicesMenuActivating(object sender, EventArgs e);
        public event OnChoicesMenuActivating onChoicesMenuActivating;
        public void DoChoicesMenuActivating(object sender, EventArgs e) => onChoicesMenuActivating?.Invoke(sender, e);
        public delegate void OnChoicesMenuDeactivating(object sender, EventArgs e);
        public event OnChoicesMenuDeactivating onChoicesMenuDeactivating;
        public void DoChoicesMenuDeactivating(object sender, EventArgs e) => onChoicesMenuDeactivating?.Invoke(sender, e);

        public delegate void OnCGDeActivating(object sender, EventArgs e);
        public event OnCGDeActivating onCGDeActivating;
        public void DoCGDeActivating(object sender, EventArgs e) => onCGDeActivating?.Invoke(sender, e);


        public delegate void OnDeductionDeActivating(object sender, EventArgs e);
        public event OnDeductionDeActivating onDeductionDeActivating;
        public void DoDeductionDeActivating(object sender, EventArgs e) => onDeductionDeActivating?.Invoke(sender, e);
        public delegate void OnDialogueDeActivating(object sender, EventArgs e);
        public event OnDialogueDeActivating onDialogueDeActivating;
        public void DoDialogueDeActivating(object sender, EventArgs e) => onDialogueDeActivating?.Invoke(sender, e);
        

        public delegate void OnItemShowingDeActivating(object sender, EventArgs e);
        public event OnItemShowingDeActivating onItemShowingDeActivating;
        public void DoItemShowingDeActivating(object sender, EventArgs e) => onItemShowingDeActivating?.Invoke(sender, e);

        

        public delegate void OnClockImageDeActivating(object sender, EventArgs e);
        public event OnClockImageDeActivating onClockImageDeActivating;
        public void DoOnClockImageDeActivating(object sender, EventArgs e) => onClockImageDeActivating?.Invoke(sender, e);
    }
    
    
}
