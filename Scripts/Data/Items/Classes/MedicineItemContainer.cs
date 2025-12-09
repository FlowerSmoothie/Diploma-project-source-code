using System;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld.Items.Containers
{
    [Serializable]
    public class MedicineItemContainer : PlainObjectInInventoryInfoContainer
    {
        [SerializeField] public List<MedicineItemPiece> medicinePieces;
        [SerializeField] public int currentCount;
        [SerializeField] public int healCount;
        public void TakeMedicine()
        {
            currentCount--;
            PlayerEventPublisher.i.TakeMedicine(this, null, healCount);
            UpdateStatus();
        }

        public void UpdateData()
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            currentSprite = medicinePieces[currentCount].GetIcon();
            currentDescription = medicinePieces[currentCount].GetDescription();
        }

        public override bool IsUsable() { return currentCount == 0 ? false : true; }
        public override bool IsMedicine() { return true; }

    }

}