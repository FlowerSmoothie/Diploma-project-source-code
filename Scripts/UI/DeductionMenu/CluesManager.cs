using System.Collections.Generic;
using Overworld.Clues;
using UnityEngine;
using Utils;

namespace Misc.UI.Deduction
{
    public class CluesManager : MonoBehaviour
    {
        [SerializeField] GameObject cluePrefabToInstantiate;
        [SerializeField] Transform parentForPrefabs;

        [SerializeField] GameObject mergingCell;

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void InstantiateClues(List<ClueInDiaryInfoContainer> clues)
        {
            foreach (ClueInDiaryInfoContainer clue in clues)
            {
                Instantiate(cluePrefabToInstantiate, parentForPrefabs).GetComponent<ClueWithCellScript>().SetData(clue);
            }
            //animator.SetTrigger(Consts.DEFAULT_TRIGGER_CONST);
        }

        public void DeleteInstantiatedClues()
        {
            foreach (Transform child in parentForPrefabs)
            {
                Destroy(child.gameObject);
            }
        }
    }
}