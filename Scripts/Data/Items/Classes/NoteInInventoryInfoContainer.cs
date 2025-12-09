using System;
using System.Collections.Generic;

namespace Overworld.Items.Containers
{
    [Serializable]
    public class NoteInInventoryInfoContainer : ObjectInInventoryInfoContainer
    {
        public List<string> notesGreat;
        public List<string> notesNormal;
        public List<string> notesBad;
        public List<string> Read(int mcHealth)
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