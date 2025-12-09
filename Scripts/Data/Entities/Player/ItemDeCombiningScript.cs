using System;
using System.Collections.Generic;
using System.ComponentModel;
using Overworld.Items.Containers;
using UnityEngine;

namespace Utils.Craft
{
    public class ItemDeCombiningScript : MonoBehaviour
    {
        private static ItemDeCombiningScript instance;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] List<CraftRecipe> craftRecipes;

        public PlainObjectInInventoryInfoContainer Craft(PlainObjectInInventoryInfoContainer first, PlainObjectInInventoryInfoContainer second)
        {
            PlainObjectInInventoryInfoContainer result = null;


            foreach (CraftRecipe recipe in craftRecipes)
            {
                result = recipe.Craft(first, second);
                if (result != null) break;
            }

            return result;
        }

        public Tuple<PlainObjectInInventoryInfoContainer, PlainObjectInInventoryInfoContainer> UnCraft(PlainObjectInInventoryInfoContainer obj)
        {
            Tuple<PlainObjectInInventoryInfoContainer, PlainObjectInInventoryInfoContainer> result = null;

            foreach (CraftRecipe recipe in craftRecipes)
            {
                result = recipe.GetIngredietnsOf(obj.GetID());
                if (result != null) break;
            }

            return result;
        }
    }
}