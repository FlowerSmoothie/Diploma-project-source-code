using System;
using System.Collections.Generic;
using ItemUtils;
using Utils.Classes;

namespace Misc.Saving
{
    [Serializable]
    public class SaveablePlainObjectInInventory
    {
        public int ID;
        public string nameInInventory;
        public bool isUsable;
        public bool isComposite;
        public bool isCombinable;
        public string currentSprite = null;
        public ItemStates currentState;
        public List<PhraseToSay> currentDescription;
        public bool isUsableCurrently;

        public bool isVisible = true;

        public virtual bool IsMedicine() { return false; }
        public virtual bool IsNote() { return false; }
    }
}