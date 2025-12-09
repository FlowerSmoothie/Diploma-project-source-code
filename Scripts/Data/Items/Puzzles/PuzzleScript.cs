using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Misc;
using Overworld.Items;
using Overworld.Items.Containers;
using Overworld.ObjectsAndNPCs;
using UnityEngine;
using Utils.Classes;

namespace Overworld.ObjectsAndNPC.Puzzles
{
    public class PuzzleScript : InteractableObjectInOverworld
    {
        [Space]
        [Header("Puzzle options:")]
        [SerializeField] private GameObject reward;
        [SerializeField] private Transform whereToSpawnReward;

        [SerializeField] protected List<PhraseToSay> solvingPuzzleText;
        [SerializeField] protected List<PhraseToSay> solvedInteractionText;


        [SerializeField] private PuzzleEnums.PuzzleType type;
        private bool isCompleted;


        protected override void Start()
        {
            base.Start();

            isCompleted = FindAnyObjectByType<DataHolderScript>().IsThePuzzleCompleted(type);

            if(isCompleted) SpawnReward();
        }

        protected bool IsComplete() { return isCompleted; }
        protected virtual void CompletePuzzle()
        {
            isCompleted = true;
            FindAnyObjectByType<DataHolderScript>().CompletePuzzle(type);
        }

        public override List<PhraseToSay> TryToInteractWithAnObject(int itemID)
        {
            if (IsComplete()) { return null; }

            return base.TryToInteractWithAnObject(itemID);
        }


        public override bool IsThisPuzzle() { return true; }

        public virtual PuzzleEnums.PuzzleType GetPuzzleType() { return PuzzleEnums.PuzzleType.NULL; }

        protected void SpawnReward()
        {
            //FindAnyObjectByType<PlayerInventoryScript>().AddToInventory(reward.GetComponent<ObjectInInventoryInfo>().GetItemInfo());
            Debug.Log(FindAnyObjectByType<PlayerInventoryScript>().Contains(reward.GetComponent<ObjectInInventoryInfo>().GetItemInfo().GetID()));
            if (!FindAnyObjectByType<PlayerInventoryScript>().Contains(reward.GetComponent<ObjectInInventoryInfo>().GetItemInfo().GetID())) Instantiate(reward, whereToSpawnReward);
        }

    }

}