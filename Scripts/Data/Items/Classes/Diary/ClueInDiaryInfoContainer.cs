using System;
using System.Collections.Generic;
using ItemUtils;
using UnityEngine;
using Utils.Classes;
namespace Overworld.Clues
{
    [Serializable]
    public class ClueInDiaryInfoContainer 
    {
        public int comparisonID;
        public bool changesByItself;
        public bool canBeUsedAsDefault;
        public ItemStates state;
        public string nameInDiary;
        public string iconPath;
        public Sprite icon;
        public List<TextUnit> description;

        //[SerializeField] bool isUsable;
        public bool isComposite;


        public string GetName()
        {
            return nameInDiary;
        }
        public Sprite GetIcon()
        {
            return icon;
        }
        public bool CanBeUsedAsDefault() { return canBeUsedAsDefault; }
        public ItemStates GetState() { return state; }

        public int GetComparisonID() { return comparisonID; }

        //public bool IsUsable() { return isUsable; }
        public bool IsComposite() { return isComposite; }
        public List<TextUnit> GetDescription() { return description; }

        public bool ChangesByItself() { return changesByItself; }
    }
}