using System;
using System.Collections.Generic;
using ItemUtils;
using Overworld.Clues;
using UnityEngine;
using Utils;
using Utils.Classes;

namespace Overworld.Items.Containers
{
    [Serializable]
    public class PlainObjectInInventoryInfoContainer
    {
        [Header("Base options:")]
        public int ID;
        public string nameInInventory;
        public bool isUsable;
        public bool isComposite;
        public bool isCombinable;
        [HideInInspector] public string currentSpritePath = null;
        [HideInInspector] public Sprite currentSprite = null;
        [HideInInspector] public ItemStates currentState;
        [HideInInspector] public List<PhraseToSay> currentDescription;
        [HideInInspector] public bool isUsableCurrently;
        
        public bool isVisible = true;


        public int GetID() { return ID; }

        public string GetName()
        {
            return nameInInventory;
        }
        public Sprite GetIcon()
        {
            return currentSprite;
        }
        public List<PhraseToSay> GetDescription() { return currentDescription; }

        public virtual bool IsUsable() { return isUsableCurrently; }
        public bool IsComposite() { return isComposite; }
        public virtual bool IsNote() { return false; }
        public virtual bool IsMedicine() { return false; }

    }
}