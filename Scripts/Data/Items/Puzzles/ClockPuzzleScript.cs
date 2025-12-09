using System;
using System.Collections.Generic;
using Misc;
using Misc.Overworld;
using Misc.UI;
using Misc.UI.Puzzles;
using Overworld.Items;
using Overworld.Items.Containers;
using UnityEngine;
using Utils.Classes;

namespace Overworld.ObjectsAndNPC.Puzzles
{
    public class ClockPuzzleScript : PuzzleScript
    {
        [SerializeField] private List<PhraseToSay> openingWithKey;
        [SerializeField] private List<PhraseToSay> openingToFix;
        [SerializeField] private List<PhraseToSay> cannotOpen;
        [SerializeField] private AudioClip usingKeySound;
        [SerializeField] private AudioClip failSound;
        private AudioSourcesScript asc;


        [SerializeField][Range(0, 11)] private int hoursSolution;
        [SerializeField][Range(0, 11)] private int minutesSolution;

        [SerializeField][Range(0, 11)] private int hoursDefault;
        [SerializeField][Range(0, 11)] private int minutesDefault;

        private ClockPuzzleMenu clockMenu;

        int currentHours;
        int currentMinutes;

        private bool opened;


        private DataHolderScript data;

        protected override void Start()
        {
            base.Start();

            data = FindAnyObjectByType<DataHolderScript>();

            opened = data.ClockPuzzleIsOpened();

            if (data.IsThePuzzleCompleted(PuzzleEnums.PuzzleType.CLOCK)) CompletePuzzle();

            clockMenu = FindAnyObjectByType<ClockPuzzleMenu>();

            Tuple<int, int> currents = data.GetClockPuzzleData();
            if (currents.Item1 == -1 || currents.Item2 == -1)
            {
                currentHours = hoursDefault;
                currentMinutes = minutesDefault;
            }
            else
            {
                currentHours = currents.Item1;
                currentMinutes = currents.Item2;
            }

            asc = FindAnyObjectByType<AudioSourcesScript>();

            OverworldEventPublisher.i.onSavingData += SaveData;
        }

        public void UpdateData(int hours, int minutes)
        {
            currentHours = hours;
            currentMinutes = minutes;
        }

        private void OnDestroy()
        {
            OverworldEventPublisher.i.onSavingData -= SaveData;
        }

        private void SaveData(object sender, EventArgs e)
        {
            FindAnyObjectByType<DataHolderScript>().UpdateClockPuzzleData(currentHours, currentMinutes);
            if (opened) FindAnyObjectByType<DataHolderScript>().OpenClockPuzzle();
            if (IsComplete()) FindAnyObjectByType<DataHolderScript>().CompletePuzzle(PuzzleEnums.PuzzleType.CLOCK);
        }

        public bool CheckIfTimeIsCorrect()
        {
            if (!IsComplete())
            {
                if (currentHours == hoursSolution && currentMinutes == minutesSolution)
                {
                    clockMenu.RunABird();
                    CompletePuzzle();

                    asc.PlaySound(usingKeySound);

                    SpawnReward();
                    clockMenu.DisableMoving();
                    return true;
                }
            }
            return false;
        }

        public override List<PhraseToSay> TryToInteractWithAnObject(int itemID)
        {
            if (IsComplete()) return null;

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

            UIEventPublisher.i.onDialogueBoxDeactivating += StartFixing;

            return openingToFix;
        }

        protected override void DoThings(int itemID)
        {

        }


        public override List<PhraseToSay> Interact(bool justCheck = true)
        {
            if (IsComplete()) return solvedInteractionText;

            //if (!justCheck) UIEventPublisher.i.onDialogueBoxDeactivating += StartFixing;

            return cannotOpen;
        }

        private void StartFixing(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= StartFixing;

            clockMenu.LoadData(currentHours, currentMinutes);

            UIEventPublisher.i.DoUIActivating(this, null);
            UIEventPublisher.i.DoOnClockImageDeActivating(this, null);
        }

        public override bool CanItemsBeUsedOnIt()
        {
            return true;
        }


    }

}