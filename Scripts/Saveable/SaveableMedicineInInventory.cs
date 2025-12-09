using System;
using System.Collections.Generic;

namespace Misc.Saving
{
    [Serializable]
    public class SaveableMedicine : SaveablePlainObjectInInventory
    {
        public List<SaveableMedicinePiece> medicinePieces;
        public int currentCount;
        public int healCount;

        public override bool IsMedicine() { return true; }
    }

}