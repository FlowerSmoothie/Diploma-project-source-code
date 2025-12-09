using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Classes;

namespace Overworld.Items.Containers
{
    [Serializable]
    public class MedicineItemPiece
    {
        [SerializeField] public string spritePath;
        [SerializeField] public Sprite sprite;
        [SerializeField] public List<PhraseToSay> description;

        public Sprite GetIcon() { return sprite; }
        public List<PhraseToSay> GetDescription() { return description; }
    }

}