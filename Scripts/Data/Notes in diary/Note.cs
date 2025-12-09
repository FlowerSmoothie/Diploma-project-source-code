using System.Collections.Generic;
using UnityEngine;
using Utils.Classes;

namespace Overworld.Diary
{
    public class Note : MonoBehaviour
    {
        [SerializeField] int noteID;
        [SerializeField] TextUnit noteName;
        [SerializeField] Sprite icon;
        [SerializeField] List<TextUnit> description;

        public int GetID() { return noteID; }
        public TextUnit GetName() { return noteName; }
        public Sprite GetIcon() { return icon; }

        public List<TextUnit> GetDescription() { return description; }
    }
}