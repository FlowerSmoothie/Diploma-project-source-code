using System.Collections.Generic;
using Utils.Classes;

namespace Overworld.ObjectsAndNPCs
{
    public interface InteractableObject : Interactable
    {

        //public List<PhraseToSay> Interact(); // to get phrases that will be displayed on dialogue box

        public int GetID();
        public bool IsCollectable();
        public bool IsNote();
        public bool CanBeInteractedWithItems();

        public float DelayedInteracting();

        public bool IsZoomable();
        public bool IsItem();

    }
}