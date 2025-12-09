using Overworld.Clues;
using UnityEngine;

namespace Misc.UI.Deduction
{
    public class AssumptionSolvingChecker : MonoBehaviour
    {

        int currentAssumption;

        public void SetInfo(int infoList)
        {
            currentAssumption = infoList;
        }

        public bool CheckIfOkay(SetOfCluesInfo whatToCheck)
        {
            if (whatToCheck == null) return false;
            if (whatToCheck.GetID() == currentAssumption) return true;
            return false;
        }
    }
}