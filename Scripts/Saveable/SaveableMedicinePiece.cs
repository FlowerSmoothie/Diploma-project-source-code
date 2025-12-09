using System;
using System.Collections.Generic;
using Utils.Classes;

namespace Misc.Saving
{
    [Serializable]
    public class SaveableMedicinePiece
    {
        public string sprite;
        public List<PhraseToSay> description;
    }

}