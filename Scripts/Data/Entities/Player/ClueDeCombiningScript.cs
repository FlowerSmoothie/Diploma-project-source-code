using System;
using System.Collections.Generic;
using Overworld.Clues;
using Overworld.Items;
using UnityEngine;

namespace Utils.Craft
{
    public class ClueDeCombiningScript : MonoBehaviour
    {
        private static ClueDeCombiningScript instance;

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

        [SerializeField] List<AssumptionRecipe> craftRecipes;

        public SetOfCluesInfo Craft(ClueInDiaryInfoContainer first, ClueInDiaryInfoContainer second)
        {
            SetOfCluesInfo result = null;


            foreach (AssumptionRecipe recipe in craftRecipes)
            {
                result = recipe.Craft(first, second);
                if (result != null) break;
            }

            return result;
        }

        public Tuple<SetOfCluesInfo, SetOfCluesInfo> UnCraft(ClueInDiaryInfo obj)
        {
            Tuple<SetOfCluesInfo, SetOfCluesInfo> result = null;

            foreach (AssumptionRecipe recipe in craftRecipes)
            {
                result = recipe.GetIngredietnsOf(obj);
                if (result != null) break;
            }

            return result;
        }
    }
}