using System;
using System.Collections.Generic;
using ItemUtils;
using Utils.Classes;
namespace Misc.Saving
{
    [Serializable]
    public class SaveableClueInDiaryInfoContainer 
    {
        public int comparisonID;
        public bool changesByItself;
        public bool canBeUsedAsDefault;
        public ItemStates state;
        public string nameInDiary;
        public string icon;
        public List<TextUnit> description;
        public bool isComposite;
    }
}