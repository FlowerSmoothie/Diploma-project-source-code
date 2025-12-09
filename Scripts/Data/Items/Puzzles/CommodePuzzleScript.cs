using System;
using System.Collections;
using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Exeptions;
using Misc;
using Misc.Overworld;
using Misc.UI;
using Overworld.Items;
using Overworld.Items.Containers;
using Overworld.ObjectsAndNPCs;
using UnityEngine;
using Utils.Classes;

namespace Overworld.ObjectsAndNPC.Puzzles
{
    public class CommodePuzzleScript : PuzzleScript
    {
        [Space]
        [Header("Commode options:")]
        [SerializeField] private AudioClip settingFigureSound;
        [SerializeField] private AudioClip pickingSound;
        [SerializeField] private AudioClip unlockSound;
        private AudioSourcesScript asc;


        [SerializeField] private List<GameObject> solution; // force griffith none guts hope
        [SerializeField] private List<GameObject> defaultPositions;
        List<ObjectInInventoryInfoContainer> currentPositions;


        private DataHolderScript data;



        public bool IsItEmpty()
        {
            foreach (ObjectInInventoryInfoContainer i in currentPositions)
            {
                if (i != null) return false;
            }
            return true;
        }

        protected override void Start()
        {
            base.Start();

            data = FindAnyObjectByType<DataHolderScript>();

            if (data.IsThePuzzleCompleted(PuzzleEnums.PuzzleType.COMMODE)) CompletePuzzle();

            currentPositions = new List<ObjectInInventoryInfoContainer>();
            currentPositions.Clear();
            foreach (GameObject go in defaultPositions)
            {
                if (go == null) currentPositions.Add(null);
                else
                {
                    ObjectInInventoryInfoContainer obj = (ObjectInInventoryInfoContainer)go.GetComponent<ObjectInInventoryInfo>().GetItemInfo();
                    currentPositions.Add(obj);
                }
            }
            //currentPositions.Add(go == null ? null : go.GetComponent<ObjectInInventoryInfo>().GetItemInfo());

            //currentPositions = defaultPositions;

            List<ObjectInInventoryInfoContainer> temp = data.GetCommodePuzzleData();

            //Debug.Log(temp != null);

            if (temp != null && temp.Count == 5) { currentPositions = temp; }

            asc = FindAnyObjectByType<AudioSourcesScript>();

            OverworldEventPublisher.i.onSavingData += SaveData;
        }
        private void OnDestroy()
        {
            OverworldEventPublisher.i.onSavingData -= SaveData;
        }

        private void SaveData(object sender, EventArgs e)
        {
            FindAnyObjectByType<DataHolderScript>().UpdateCommodePuzzleData(currentPositions);
            if (IsComplete()) FindAnyObjectByType<DataHolderScript>().CompletePuzzle(PuzzleEnums.PuzzleType.COMMODE);
        }

        private bool CheckIfPositionsAreCorrect()
        {
            for (int i = 0; i < solution.Count; i++)
            {
                if (solution[i] == null && currentPositions[i] == null) continue;
                if ((solution[i] == null && currentPositions[i] != null) || (solution[i] != null && currentPositions[i] == null)) return false;
                if (solution[i].GetComponent<ObjectInInventoryInfo>().GetItemInfo().GetID() != currentPositions[i].GetID()) return false;
            }
            return true;
        }

        private string GetPositionsString()
        {
            string s = "...";

            foreach (ObjectInInventoryInfoContainer i in currentPositions)
            {
                if (i == null) s += "empty";
                else
                {
                    switch (i.GetID())
                    {
                        case 12:
                            s += "knight";
                            break;
                        case 11:
                            s += "mercenary";
                            break;
                        case 10:
                            s += "hope";
                            break;
                        case 9:
                            s += "power";
                            break;
                    }
                }
                s += "... ";
            }

            return s;
        }

        private List<bool> GetPositions()
        {
            List<bool> list = new List<bool>();
            foreach (ObjectInInventoryInfoContainer i in currentPositions)
            {
                if (i == null) list.Add(false);
                else list.Add(true);
            }
            return list;
        }

        public ObjectInInventoryInfoContainer GetAnObjectFromHole(int id)
        {
            ObjectInInventoryInfoContainer temp = currentPositions[id];
            currentPositions[id] = null;
            return temp;
        }

        int itemIDtoUse;

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

            itemIDtoUse = itemID;

            List<PhraseToSay> phrases = new List<PhraseToSay>
            {
                new PhraseToSay(GetPositionsString() + " Which cell should I put the figure in?")
            };

            List<Tuple<string, int>> positions = new List<Tuple<string, int>>();

            List<bool> positionsBools = GetPositions();
            for (int i = 0; i < positionsBools.Count; i++)
            {
                if (!positionsBools[i]) positions.Add(new Tuple<string, int>((i + 1).ToString() + "th", i));
            }

            if (positionsBools.Count == 0) return null;

            SelectableUIPublisher.i.onOptionChoosing += PutObjectInChosenHole;

            UIEventPublisher.i.DoUIActivating(this, null);
            FindAnyObjectByType<DialogueBoxScript>().SelfClosingDeactivate();

            PlayerEventPublisher.i.DoOnTryingToInteractWithObects(this, null, positions);
            UIEventPublisher.i.DoChoicesMenuActivating(this, null);

            return phrases;
        }

        protected override void DoThings(int itemID)
        {
            itemIDtoUse = itemID;

            List<PhraseToSay> phrases = new List<PhraseToSay>
            {
                new PhraseToSay(GetPositionsString() + " Which cell should I put the figure in?")
            };

            List<Tuple<string, int>> positions = new List<Tuple<string, int>>();

            List<bool> positionsBools = GetPositions();
            for (int i = 0; i < positionsBools.Count; i++)
            {
                if (!positionsBools[i]) positions.Add(new Tuple<string, int>((i + 1).ToString() + "th", i));
            }

            if (positionsBools.Count == 0) return;

            SelectableUIPublisher.i.onOptionChoosing += PutObjectInChosenHole;

            UIEventPublisher.i.DoUIActivating(this, null);
            FindAnyObjectByType<DialogueBoxScript>().SelfClosingDeactivate();
            FindAnyObjectByType<DialogueBoxScript>().WriteTextFunc(phrases, 0);
            //DialogueBoxEventPublisher.i.DoDiscardSelfClosing(this, null);

            PlayerEventPublisher.i.DoOnTryingToInteractWithObects(this, null, positions);
            UIEventPublisher.i.DoChoicesMenuActivating(this, null);
        }

        private void PutObjectInChosenHole(object sender, EventArgs e, int option)
        {
            UIEventPublisher.i.DoDialogueBoxDeactivating(this, null);
            UIEventPublisher.i.DoUIDeactivating(this, null);
            //UIEventPublisher.i.DoUIDeactivating(this, null);
            SelectableUIPublisher.i.onOptionChoosing -= PutObjectInChosenHole;

            if (option == -1) return;

            InventoryEventPublisher.i.DeleteFromInventory(this, null, itemIDtoUse);

            ObjectInInventoryInfoContainer curItem = null;
            foreach (GameObject go in itemsThatCanBeUsedOnItem)
            {
                curItem = (ObjectInInventoryInfoContainer)go.GetComponent<ObjectInInventoryInfo>().GetItemInfo();
                if (curItem.GetID() == itemIDtoUse)
                    break;
            }

            currentPositions[option] = curItem;
            asc.PlaySound(settingFigureSound);

            if (CheckIfPositionsAreCorrect())
            {
                //controller.clip = unlockSound;
                //controller.Play();

                CompletePuzzle();
                asc.PlaySound(unlockSound);

                SpawnReward();
            }
            FindAnyObjectByType<DialogueBoxScript>().SelfClosingActivate();
            UIEventPublisher.i.DeactivateToZero(this, null);
        }

        private IEnumerator waitThenDo(List<Tuple<string, int>> positions)
        {
            yield return new WaitForSecondsRealtime(0.3f);

            Interaction(positions);
        }

        private void Interaction(List<Tuple<string, int>> positions)
        {
            UIEventPublisher.i.DoUIActivating(this, null);
            PlayerEventPublisher.i.DoOnTryingToInteractWithObects(this, null, positions);
            UIEventPublisher.i.DoChoicesMenuActivating(this, null);
            SelectableUIPublisher.i.onOptionChoosing += GetObjectFromChosenHole;
        }

        public override List<PhraseToSay> Interact(bool justCheck = true)
        {
            if (IsComplete()) return solvedInteractionText;
            List<Tuple<string, int>> positions = new List<Tuple<string, int>>();

            List<bool> positionsBools = GetPositions();
            for (int i = 0; i < positionsBools.Count; i++)
            {
                if (positionsBools[i]) positions.Add(new Tuple<string, int>((i + 1).ToString() + "th", i));
            }

            List<PhraseToSay> phrases = new List<PhraseToSay>();

            if (positions.Count == 0)
            {
                phrases.Add(new PhraseToSay("Some drawer with holes stands against the wall. Maybe something needs to be inserted?"));
                return phrases;
            }

            phrases.Add(new PhraseToSay(GetPositionsString() + " From which cell should I take a figure?"));


            if (!justCheck)
            {
                FindAnyObjectByType<DialogueBoxScript>().SelfClosingDeactivate();
                StartCoroutine(waitThenDo(positions));
            }

            return phrases;
        }

        private void GetObjectFromChosenHole(object sender, EventArgs e, int option)
        {
            SelectableUIPublisher.i.onOptionChoosing -= GetObjectFromChosenHole;
            UIEventPublisher.i.DoUIDeactivating(this, null);
            UIEventPublisher.i.DoDialogueBoxDeactivating(this, null);
            FindAnyObjectByType<DialogueBoxScript>().SelfClosingActivate();

            if (option != -1)
            {
                FindAnyObjectByType<PlayerInventoryScript>().AddToInventory(currentPositions[option]);
                asc.PlaySound(pickingSound);
                currentPositions[option] = null;
            }
        }

        public override bool CanItemsBeUsedOnIt()
        {
            return true;
        }

        /*public ObjectInInventoryInfoContainer Interact(int position, bool tryToPlace, ObjectInInventoryInfoContainer item = null)
        {
            if (IsComplete()) { return null; }

            ObjectInInventoryInfoContainer curItem;
            curItem = currentPositions[position];


            if (CheckIfPositionsAreCorrect()) { return null; }
            if (tryToPlace)
            {
                if (currentPositions[position] == null) throw new HoleIsFullException();
                else { currentPositions[position] = item; controller.clip = settingFigureSound; controller.Play(); }
            }
            else
            {
                if (currentPositions[position] == null) throw new HoleIsEmptyException();
                else
                {
                    currentPositions[position] = null;
                    controller.clip = pickingSound;
                    controller.Play();
                    data.UpdateCommodePuzzleData(currentPositions);

                    return curItem;
                }
            }

            //data.UpdateCommodePuzzleData(currentPositions);

            if (CheckIfPositionsAreCorrect())
            {
                controller.clip = unlockSound;
                controller.Play();

                CompletePuzzle();
                SpawnReward();
            }


            return null;
        }*/
    }
}