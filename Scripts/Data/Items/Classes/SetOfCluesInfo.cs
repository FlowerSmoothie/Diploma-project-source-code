using System;
using UnityEngine;
using Utils;

namespace Overworld.Clues
{
    [Serializable]
    public class SetOfCluesInfo
    {
        public ClueInDiaryInfoContainer greatMental;
        public int greatDownBorder = Consts.GREAT_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;
        public ClueInDiaryInfoContainer normalMental;
        public int normalDownBorder = Consts.NORMAL_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;
        public ClueInDiaryInfoContainer badMental;

        

        public int GetID() { return greatMental.GetComparisonID(); }

        public ClueInDiaryInfoContainer GetInfo(int mcHealth)
        {
            if (mcHealth <= 100 && mcHealth >= greatDownBorder)
            {
                return greatMental;
            }
            else if (mcHealth < greatDownBorder && mcHealth >= normalDownBorder)
            {
                return normalMental;
            }
            else
            {
                return badMental;
            }
        }
    }
}