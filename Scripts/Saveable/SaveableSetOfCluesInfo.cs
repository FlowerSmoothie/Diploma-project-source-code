using System;
using UnityEngine;
using Utils;

namespace Misc.Saving
{
    [Serializable]
    public class SaveableSetOfCluesInfo
    {
        public SaveableClueInDiaryInfoContainer greatMental;
        public int greatDownBorder = Consts.GREAT_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;
        public SaveableClueInDiaryInfoContainer normalMental;
        public int normalDownBorder = Consts.NORMAL_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;
        public SaveableClueInDiaryInfoContainer badMental;
    }
}