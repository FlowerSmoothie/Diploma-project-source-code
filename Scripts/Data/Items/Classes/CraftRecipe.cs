using System;
using Overworld.Items;
using Overworld.Items.Containers;
using UnityEngine;

namespace Utils.Craft
{
    [Serializable]
    public class CraftRecipe
    {
        [SerializeField] private ObjectInInventoryInfo firstInfo;
        [SerializeField] private ObjectInInventoryInfo secondInfo;
        [SerializeField] private ObjectInInventoryInfo resultInfo;


        public Tuple<PlainObjectInInventoryInfoContainer, PlainObjectInInventoryInfoContainer> GetIngredietnsOf(int that)
        {
            if (that.Equals(resultInfo.GetItemInfo().GetID()))
            {
                return new Tuple<PlainObjectInInventoryInfoContainer, PlainObjectInInventoryInfoContainer>(firstInfo.GetItemInfo(), secondInfo.GetItemInfo());
            }
            return null;
        }

        public PlainObjectInInventoryInfoContainer Craft(PlainObjectInInventoryInfoContainer first, PlainObjectInInventoryInfoContainer second)
        {
            Debug.Log(firstInfo.GetItemInfo().GetID() + " " + first.GetID());
            Debug.Log(secondInfo.GetItemInfo().GetID() + " " + second.GetID());
            if ((firstInfo.GetItemInfo().GetID() == first.GetID() && secondInfo.GetItemInfo().GetID() == second.GetID()) || (secondInfo.GetItemInfo().GetID() == first.GetID() && firstInfo.GetItemInfo().GetID() == second.GetID()))
                return resultInfo.GetItemInfo();
            else return null;
        }
    }
}