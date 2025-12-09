using System.Collections.Generic;
using Overworld.Items;
using Overworld.Items.Containers;
using Overworld.ObjectsAndNPCs;
using UnityEngine;
using Utils.Classes;

namespace Overworld.ObjectsAndNPCs
{
    public class InteractableItemInOverworld : MonoBehaviour, InteractableWithObject, InteractableObject
    {

        [SerializeField] protected List<GameObject> itemsThatCanBeUsedOnItem;
        
        [SerializeField] private List<PhraseToSay> phrasesToSayWhenInteracting;


        /*public override bool CanItemsBeUsedOnIt()
        {
            return true;
        }*/

        protected virtual void DoThings(int itemID)
        {

        }

        public virtual List<PhraseToSay> TryToInteractWithAnObject(int itemID)
        {
            ObjectInInventoryInfoContainer objectInInventoryInfoContainer = null;
            ObjectInInventoryInfo temp;
            foreach (GameObject go in itemsThatCanBeUsedOnItem)
            {
                temp = go.GetComponent<ObjectInInventoryInfo>();
                if (temp.GetItemInfo().GetID() == itemID)
                {
                    objectInInventoryInfoContainer = (ObjectInInventoryInfoContainer)temp.GetItemInfo();
                    break;
                }
            }

            if (objectInInventoryInfoContainer == null) return null;

            DoThings(itemID);

            return phrasesToSayWhenInteracting;
        }

        public virtual bool IsThisPuzzle() { return false; }

        public bool CanBeSurveyed() { return false; }

        public bool IsObject() { return true; }

        public virtual TextUnit GetName() { throw new System.NotImplementedException(); }

        public virtual List<PhraseToSay> Interact(bool justCheck = false) { throw new System.NotImplementedException(); }

        public virtual bool CanItemsBeUsedOnIt() { throw new System.NotImplementedException(); }

        public virtual int GetID()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool IsCollectable()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool IsNote()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool CanBeInteractedWithItems()
        {
            throw new System.NotImplementedException();
        }

        public virtual float DelayedInteracting()
        {
            throw new System.NotImplementedException();
        }

        public virtual bool IsZoomable()
        {
            throw new System.NotImplementedException();
        }

        public bool IsItem()
        {
            throw new System.NotImplementedException();
        }
    }
}