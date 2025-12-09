using Overworld.Clues;
using UnityEngine;
using UnityEngine.UI;

namespace Misc.UI.Deduction
{
    public class ClueInDeductionScript : MonoBehaviour
    {
        [SerializeField] private Image image;
        private string clueName;

        //private bool isReadyToMerge;

        private ClueInDiaryInfoContainer info;


        public string GetName() { return clueName; }
        public Image GetImage() { return image; }

        private void Start()
        {
            //isReadyToMerge = false;
        }

        //public void LockMerging() { isReadyToMerge = false; }
        //public void UnlockMerging() { isReadyToMerge = true; }

        public void SetEverything(ClueInDiaryInfoContainer info)
        {
            this.info = info;
            image.sprite = info.GetIcon();
            clueName = info.GetName();
        }

        public ClueInDiaryInfoContainer GetData() { return info; }
    }
}