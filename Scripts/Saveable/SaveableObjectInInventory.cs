using System;
using System.Collections.Generic;
using ItemUtils;
using Misc.Saving;
using Utils;
using Utils.Classes;

namespace Misc.Saving
{
    [Serializable]
    public class SaveableObjectInInventory : SaveablePlainObjectInInventory
    {
        public bool hasClues = true;
        public SaveableSetOfCluesInfo clues;


        public string iconGreat;
        public bool isUsableOnGreat = true;
        public List<PhraseToSay> descriptionGreat;
        public int greatDownBorder = Consts.GREAT_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;

        public string iconNormal;
        public bool isUsableOnNormal;
        public List<PhraseToSay> descriptionNormal;
        public int normalDownBorder = Consts.NORMAL_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;

        public string iconBad;
        public bool isUsableOnBad;
        public List<PhraseToSay> descriptionBad;

    }
}