using System;
using System.Collections.Generic;
using Misc;
using Misc.Overworld;
using Overworld.Items;
using Overworld.Items.Containers;
using UnityEngine;

namespace EntityUtils.PlayerUtils
{
    public class PlayerInventoryScript : MonoBehaviour
    {

        List<PlainObjectInInventoryInfoContainer> objectsHolding;

        private void Start()
        {
            objectsHolding = new List<PlainObjectInInventoryInfoContainer>();

            objectsHolding = FindAnyObjectByType<DataHolderScript>().GetInventory();

            OverworldEventPublisher.i.onSavingData += SaveData;

            InventoryEventPublisher.i.onDeletingFromInventory += DeleteItemFromInventory;
        }

        private void OnDestroy()
        {
            OverworldEventPublisher.i.onSavingData -= SaveData;

            InventoryEventPublisher.i.onDeletingFromInventory -= DeleteItemFromInventory;
        }

        private void DeleteItemFromInventory(object sender, EventArgs e, int ID)
        {
            foreach (PlainObjectInInventoryInfoContainer obj in objectsHolding)
            {
                if (obj.GetID() == ID)
                {
                    obj.isVisible = false;
                    //objectsHolding.Remove(obj);
                    break;
                }
            }
        }

        public void SaveData(object sender, EventArgs e)
        {
            FindAnyObjectByType<DataHolderScript>().SaveInventory(objectsHolding);
        }

        public void AddToInventory(PlainObjectInInventoryInfoContainer newObject)
        {
            if (newObject != null) objectsHolding.Add(newObject);
        }

        public void DeleteFromInventory(PlainObjectInInventoryInfoContainer objectToDelete)
        {
            int id = objectToDelete.GetID();
            foreach (PlainObjectInInventoryInfoContainer p in objectsHolding)
            {
                if (p.GetID() == id)
                {
                    p.isVisible = false;
                    return;
                }
            }
        }

        public List<PlainObjectInInventoryInfoContainer> GetItemsData() { return objectsHolding; }

        public bool Contains(int ID)
        {
            foreach (PlainObjectInInventoryInfoContainer obj in objectsHolding)
            {
                if (obj.GetID() == ID) return true;
            }
            return false;
        }
    }
}