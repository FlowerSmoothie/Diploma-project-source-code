using UnityEngine;

using Overworld.Interfaces;
using System.Collections.Generic;
using Utils.Classes;
using Overworld.ObjectsAndNPCs;


namespace Overworld.Items
{
    public class NoteOverworld : CollectableItemInOverworld
    {
        [SerializeField] List<TextUnit> notesGreat;
        [SerializeField] List<TextUnit> notesNormal;
        [SerializeField] List<TextUnit> notesBad;

        public List<TextUnit> Read(int mcHealth)
        {
            if (mcHealth <= 100 && mcHealth >= greatDownBorder)
            {
                return notesGreat;
            }
            else if (mcHealth < greatDownBorder && mcHealth >= normalDownBorder)
            {
                return notesNormal;
            }
            else
            {
                return notesBad;
            }
        }

        public override bool IsNote() { return true; }
    }
}