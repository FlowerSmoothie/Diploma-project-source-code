using System;
using System.Collections.Generic;
using Overworld.Items.Containers;
using UnityEngine;
using Utils.Classes;

namespace Overworld.Items
{
    [Serializable]
    public class ObjectInInventoryInfo : MonoBehaviour, PuttableInInventory
    {
        [SerializeField] private ObjectInInventoryInfoContainer itemInfo;

        public PlainObjectInInventoryInfoContainer GetItemInfo() { return itemInfo; }

        public void UpdateInformation(int mcHealth)
        {
            if (mcHealth <= 100 && mcHealth >= itemInfo.greatDownBorder)
            {
                itemInfo.currentSprite = itemInfo.iconGreat;
                itemInfo.currentSpritePath = itemInfo.pathToiconGreat;
                itemInfo.currentDescription = itemInfo.descriptionGreat;
                itemInfo.isUsable = itemInfo.isUsableOnGreat;
            }
            else if (mcHealth < itemInfo.greatDownBorder && mcHealth >= itemInfo.normalDownBorder)
            {
                itemInfo.currentSprite = itemInfo.iconNormal;
                itemInfo.currentSpritePath = itemInfo.pathToiconNormal;
                itemInfo.currentDescription = itemInfo.descriptionNormal;
                itemInfo.isUsable = itemInfo.isUsableOnNormal;
            }
            else
            {                
                itemInfo.currentSprite = itemInfo.iconBad;
                itemInfo.currentSpritePath = itemInfo.pathToiconBad;
                itemInfo.currentDescription = itemInfo.descriptionBad;
                itemInfo.isUsable = itemInfo.isUsableOnBad;
            }
        }
        

        public bool IsUsable() { return itemInfo.isUsable; }
        public bool IsComposite() { return itemInfo.isComposite; }
        public virtual bool IsNote() { return false; } 

        public List<PhraseToSay> GetDescription() { return itemInfo.currentDescription; }


        
        public void SetID(int ID) { itemInfo.ID = ID; }
        

        public void SetBorders(int great, int normal)
        {
            itemInfo.greatDownBorder = great;
            itemInfo.normalDownBorder = normal;
        }
    }
}
