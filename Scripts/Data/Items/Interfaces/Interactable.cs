using System.Collections.Generic;
using Utils.Classes;

namespace Overworld.ObjectsAndNPCs
{
    public interface Interactable
    {
        public bool CanBeSurveyed();
        public abstract bool IsObject();
        public TextUnit GetName();

        public List<PhraseToSay> Interact(bool justCheck = true);

        public bool CanItemsBeUsedOnIt();


    }
}