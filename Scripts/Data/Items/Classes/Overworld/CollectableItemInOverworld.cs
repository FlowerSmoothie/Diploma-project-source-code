using System;
using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Misc;
using Overworld.Items;
using Overworld.Items.Containers;
using UnityEngine;
using Utils;

namespace Overworld.ObjectsAndNPCs
{
    [Serializable]
    [RequireComponent(typeof(Animator))]
    public class CollectableItemInOverworld : OverworldItemInfo
    {
        PuttableInInventory inInventoryInfo;

        Animator animator;

        protected override void Start()
        {
            Debug.Log("Items collected: " + FindAnyObjectByType<DataHolderScript>().ItemsCollected());
            if (FindAnyObjectByType<DataHolderScript>().ItemWasCollected(itemID))
            {
                Destroy(gameObject, 0.05f);
                return;
            }

            base.Start();

            inInventoryInfo = GetComponent<PuttableInInventory>();
            inInventoryInfo.SetBorders(greatDownBorder, normalDownBorder);
            inInventoryInfo.SetID(itemID);

            animator = GetComponent<Animator>();
        }

        public PuttableInInventory GetInventoryInfo() { return inInventoryInfo; }
        public PlainObjectInInventoryInfoContainer Collect()
        {
            animator.SetTrigger(Consts.DEFAULT_TRIGGER_CONST);
            SelfDesctuct();
            if (inInventoryInfo is NoteInInventoryInfo)
            {
                NoteInInventoryInfo noteInf = (NoteInInventoryInfo)inInventoryInfo;
                return noteInf.GetItemInfo();
            }

            if (inInventoryInfo is ObjectInInventoryInfo)
            {
                ObjectInInventoryInfo objInf = (ObjectInInventoryInfo)inInventoryInfo;
                return objInf.GetItemInfo();
            }

            if (inInventoryInfo is MedicineInInventoryInfo)
            {
                MedicineInInventoryInfo objInf = (MedicineInInventoryInfo)inInventoryInfo;
                return objInf.GetItemInfo();
            }
            return null;
        }

        public void SelfDesctuct()
        {
            Destroy(gameObject, 0.4f);
        }
        public override bool IsCollectable() { return true; }
    }
}