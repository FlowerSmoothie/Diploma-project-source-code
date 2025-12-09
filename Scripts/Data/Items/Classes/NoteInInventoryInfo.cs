using System;
using Overworld.Items.Containers;
using UnityEngine;

namespace Overworld.Items
{
    [Serializable]
    public class NoteInInventoryInfo : MonoBehaviour, PuttableInInventory
    {
        [SerializeField] private NoteInInventoryInfoContainer noteInfo;

        public PlainObjectInInventoryInfoContainer GetItemInfo()
        {
            return noteInfo;
        }

        public NoteInInventoryInfoContainer GetNoteInfo() { return noteInfo; }

        public bool IsComposite()
        {
            return false;
        }

        public bool IsNote() { return true; }

        public bool IsUsable()
        {
            return false;
        }

        public void SetBorders(int great, int normal)
        {
            noteInfo.greatDownBorder = great;
            noteInfo.normalDownBorder = normal;
        }

        public void SetID(int ID)
        {
            noteInfo.ID = ID;
        }
    }
}