using System;
using EntityUtils.PlayerUtils;
using Overworld.Items;
using Overworld.Items.Containers;
using UnityEngine;
using Utils.Craft;



namespace Misc.UI.Inventory
{
    public class DecombineButtonScript : ActionButtonScript
    {
        private InventoryMenuScript inventory;
        private PlayerInventoryScript playerInventory;
        private ItemDeCombiningScript kitchenCombine;

        PlainObjectInInventoryInfoContainer info;

        private void Start()
        {
            kitchenCombine = FindAnyObjectByType<ItemDeCombiningScript>();
            inventory = FindAnyObjectByType<InventoryMenuScript>();
            playerInventory = FindAnyObjectByType<PlayerInventoryScript>();
        }

        protected override void DoOnClick()
        {
            info = inventory.GetCurrentObject();

            Tuple<PlainObjectInInventoryInfoContainer, PlainObjectInInventoryInfoContainer> newObjects = kitchenCombine.UnCraft(info);

            if (newObjects != null)
            {
                playerInventory.DeleteFromInventory(info);
                playerInventory.AddToInventory(newObjects.Item1);
                playerInventory.AddToInventory(newObjects.Item2);

                inventory.RefreshList();
            }
        }
    }
}