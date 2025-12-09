using Misc.UI;
using Overworld.Clues;
using UnityEngine;

namespace Misc.UI.Deduction
{
    public class ClueWithCellScript : MonoBehaviour
    {
        [SerializeField] GameObject cell;
        [SerializeField] GameObject clue;
        [SerializeField] private ClueInDeductionScript clueData;

        public void SetData(ClueInDiaryInfoContainer clueData)
        {
            this.clueData.SetEverything(clueData);
        }
    }
}