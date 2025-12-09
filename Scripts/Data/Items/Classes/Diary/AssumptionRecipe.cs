using System;
using ItemUtils;
using Overworld.Clues;
using Overworld.Items;
using UnityEngine;

namespace Utils.Craft
{
    [Serializable]
    public class AssumptionRecipe
    {
        [SerializeField] private SetOfClues frst;
        [SerializeField] private ItemStates frstState;
        [SerializeField] private SetOfClues scnd;
        [SerializeField] private ItemStates scndState;
        //[SerializeField] private ImportanceOfState importance;

        [SerializeField] private SetOfClues result;

        public Tuple<SetOfCluesInfo, SetOfCluesInfo> GetIngredietnsOf(ClueInDiaryInfo that)
        {
            ClueInDiaryInfo result = null;
            if (that.Equals(result))
            {
                return new Tuple<SetOfCluesInfo, SetOfCluesInfo>(frst.GetInfo(), scnd.GetInfo());
            }
            return null;
        }

        public SetOfCluesInfo Craft(ClueInDiaryInfoContainer f, ClueInDiaryInfoContainer sec)
        {
            ClueInDiaryInfoContainer first = frst.GetInfo(frstState);
            ClueInDiaryInfoContainer second = scnd.GetInfo(scndState);

            /*switch (importance)
            {
                case ImportanceOfState.FIRST_IMPORTANT:
                    if (first.GetState() != f.GetState()) return null;
                    break;
                case ImportanceOfState.SECOND_IMPORTANT:
                    if (second.GetState() != sec.GetState()) return null;
                    break;
                case ImportanceOfState.ALL_IMPORTANT:
                    if (second.GetState() != sec.GetState() || first.GetState() != f.GetState()) return null;
                    break;
            }*/

            if (f.GetComparisonID() == first.GetComparisonID() && sec.GetComparisonID() == second.GetComparisonID())
            {
                if ((f.GetState() == first.GetState() && sec.GetState() == second.GetState()) || (second.GetState() == sec.GetState() && f.GetState() == second.GetState()))
                    return result.GetInfo();
                if ((f.GetState() != first.GetState() && f.CanBeUsedAsDefault() && sec.GetState() == second.GetState()) || (sec.GetState() != second.GetState() && sec.CanBeUsedAsDefault() && f.GetState() == first.GetState()))
                    return result.GetInfo();
                if (f.GetState() != first.GetState() && f.CanBeUsedAsDefault() && second.GetState() != sec.GetState() && sec.CanBeUsedAsDefault())
                    return result.GetInfo();
                return null;
            }
            else if(sec.GetComparisonID() == first.GetComparisonID() && f.GetComparisonID() == second.GetComparisonID())
            {
                if ((sec.GetState() == first.GetState() && f.GetState() == second.GetState()) || (first.GetState() == sec.GetState() && f.GetState() == second.GetState()))
                    return result.GetInfo();
                if ((sec.GetState() != first.GetState() && sec.CanBeUsedAsDefault() && f.GetState() == second.GetState()) || (f.GetState() != second.GetState() && f.CanBeUsedAsDefault() && sec.GetState() == first.GetState()))
                    return result.GetInfo();
                if (sec.GetState() != first.GetState() && sec.CanBeUsedAsDefault() && second.GetState() != f.GetState() && f.CanBeUsedAsDefault())
                    return result.GetInfo();
                return null;
            }
            else return null;
        }

        public enum ImportanceOfState { FIRST_IMPORTANT, SECOND_IMPORTANT, ALL_IMPORTANT, NONE_IMPORTANT }
    }
}