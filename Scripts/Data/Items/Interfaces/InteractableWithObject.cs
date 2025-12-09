using System.Collections.Generic;
using Overworld.ObjectsAndNPCs;
using Utils.Classes;

public interface InteractableWithObject : Interactable
{
    public List<PhraseToSay> TryToInteractWithAnObject(int itemID);

    public bool IsThisPuzzle();
}