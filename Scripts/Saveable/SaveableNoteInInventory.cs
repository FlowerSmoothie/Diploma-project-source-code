using System;
using System.Collections.Generic;

namespace Misc.Saving
{
    [Serializable]
    public class SaveableNoteInInventory : SaveableObjectInInventory
    {
        public List<string> notesGreat;
        public List<string> notesNormal;
        public List<string> notesBad;

        public override bool IsNote() { return true; }
    }
}