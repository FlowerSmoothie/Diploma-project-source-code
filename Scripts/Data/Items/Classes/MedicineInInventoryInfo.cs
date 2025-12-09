using System;
using Overworld.Items.Containers;
using UnityEngine;

namespace Overworld.Items
{
    [Serializable]
    public class MedicineInInventoryInfo : MonoBehaviour, PuttableInInventory
    {
        [SerializeField] private MedicineItemContainer medicineInfo;

        public PlainObjectInInventoryInfoContainer GetItemInfo()
        {
            return medicineInfo;
        }

        public bool IsComposite()
        {
            return false;
        }

        public bool IsNote() { return false; }
        public bool IsMedicine() { return true; }

        public bool IsUsable()
        {
            return false;
        }

        public void SetBorders(int great, int normal)
        {

        }

        public void SetID(int ID)
        {
            medicineInfo.ID = ID;
        }
    }
}