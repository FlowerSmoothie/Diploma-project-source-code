using System;
using System.Collections.Generic;
using General;
using Misc.Overworld;
using Misc.Saving;
using Misc.UI.Deduction;
using Overworld.Clues;
using Overworld.Items.Containers;
using Overworld.ObjectsAndNPCs;
using UnityEngine;

namespace Misc
{
    public class DataHolderScript : MonoBehaviour
    {

        private bool isSaved = false;

        private static DataHolderScript instance;

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

        private DataHolder holder;

        private List<PlainObjectInInventoryInfoContainer> objectsHolding = new List<PlainObjectInInventoryInfoContainer>();
        private List<ClueInDiaryInfoContainer> cluesKnowing = new List<ClueInDiaryInfoContainer>();
        private List<SetOfCluesInfo> setsKnowing = new List<SetOfCluesInfo>();
        private List<ObjectInInventoryInfoContainer> commodePuzzlePositions;// = new List<ObjectInInventoryInfoContainer>();
        private Dictionary<Character, bool> unSatisfiedEntitiesList = new Dictionary<Character, bool>();
        private Dictionary<Character, bool> unTalkedEntitiesList = new Dictionary<Character, bool>();

        private Dictionary<int, bool> lockedDoors = new Dictionary<int, bool>();
        private Dictionary<PuzzleEnums.PuzzleType, bool> puzzlesBeingCompleted = new Dictionary<PuzzleEnums.PuzzleType, bool>();




        public void AddToCollectedItems(int ID) { holder.IDsOfCollectedItems.Add(new CollectedObject(ID)); UnsavedChanges(); }
        private void AddItemToList(object sender, EventArgs e, int ID) { holder.IDsOfCollectedItems.Add(new CollectedObject(ID)); UnsavedChanges(); }
        public bool ItemWasCollected(int ID)
        {
            foreach (var i in objectsHolding)
            {
                if (i.ID == ID)
                    return true;
            }
            return false;
        }
        public int ItemsCollected() { return holder.IDsOfCollectedItems.Count; }

        public void AddToInteractedItems(int ID) { holder.IDsOfObjectsWeInteractedWith.Add(ID); UnsavedChanges(); }
        public bool DidInteractWithAnItem(int ID) { return holder.IDsOfObjectsWeInteractedWith.Contains(ID); }
        public void SetHealth(int health) { holder.health = health; UnsavedChanges(); }
        public int GetHealth() { return holder.health; }
        private void UpdateHealth(object sender, EventArgs e, int health)
        {
            holder.health += health;
            if (holder.health > 100) holder.health = 100;
            else if (holder.health < 0) holder.health = 0;
            UnsavedChanges();
        }



        public void SaveInventory(List<PlainObjectInInventoryInfoContainer> inventory) { objectsHolding = inventory; }
        public List<PlainObjectInInventoryInfoContainer> GetInventory() { return objectsHolding; }


        public void SaveDiary(List<ClueInDiaryInfoContainer> diary, List<SetOfCluesInfo> sets) { cluesKnowing = diary; setsKnowing = sets; }
        public List<ClueInDiaryInfoContainer> GetClues() { return cluesKnowing; }
        public List<SetOfCluesInfo> GetSets() { return setsKnowing; }


        private void ReactToDeductionCompletion(object sender, EventArgs e, Character character, bool everythingIsOkay)
        {
            if (everythingIsOkay) UpdateEntityStatus(character, true);
        }
        public void UpdateEntityStatus(Character character, bool newStatus)
        {
            if (unSatisfiedEntitiesList.ContainsKey(character)) unSatisfiedEntitiesList[character] = newStatus;
            else unSatisfiedEntitiesList.Add(character, newStatus);

            UnsavedChanges();
        }
        public bool EntityIsSatisfied(Character character)
        {
            if (!unSatisfiedEntitiesList.ContainsKey(character)) return false;
            return unSatisfiedEntitiesList[character];
        }
        public void UpdateEntityDialogueStatus(Character character, bool newStatus)
        {
            if (unTalkedEntitiesList.ContainsKey(character)) unTalkedEntitiesList[character] = newStatus;
            else unTalkedEntitiesList.Add(character, newStatus);

            UnsavedChanges();
        }
        public bool EntityIsTalked(Character character)
        {
            if (!unTalkedEntitiesList.ContainsKey(character)) return false;
            return unTalkedEntitiesList[character];
        }


        private void LockDoor(object sender, EventArgs e, int doorID, bool isLockedNow)
        {
            if (lockedDoors.ContainsKey(doorID)) { lockedDoors[doorID] = isLockedNow; }
            else lockedDoors.Add(doorID, isLockedNow);

            UnsavedChanges();
        }

        public int IsDoorLocked(int doorID)
        {
            if (!lockedDoors.ContainsKey(doorID)) return -1;
            if (lockedDoors[doorID] == true) return 1;
            else return 0;
        }


        public int GetSpawnPoint() { return holder.IdOfSpawnPoint; }
        public void SetSpawnPoint(int ID) { holder.IdOfSpawnPoint = ID; UnsavedChanges(); }



        public bool IsThePuzzleCompleted(PuzzleEnums.PuzzleType type)
        {
            if (puzzlesBeingCompleted.ContainsKey(type)) return puzzlesBeingCompleted[type];

            puzzlesBeingCompleted.Add(type, false);
            return false;
        }
        public void CompletePuzzle(PuzzleEnums.PuzzleType type) { puzzlesBeingCompleted[type] = true; UnsavedChanges(); }


        public List<ObjectInInventoryInfoContainer> GetCommodePuzzleData() { return commodePuzzlePositions; }
        public void UpdateCommodePuzzleData(List<ObjectInInventoryInfoContainer> commodePuzzlePositions) { this.commodePuzzlePositions = commodePuzzlePositions; UnsavedChanges(); }


        public Tuple<int, int> GetClockPuzzleData() { return new Tuple<int, int>(holder.clockPuzzleHours, holder.clockPuzzleMinutes); }
        public void UpdateClockPuzzleData(int hours, int minutes) { holder.clockPuzzleHours = hours; holder.clockPuzzleMinutes = minutes; UnsavedChanges(); }
        public void OpenClockPuzzle() { holder.clockPuzzleIsOpened = true; UnsavedChanges(); }
        public bool ClockPuzzleIsOpened() { return holder.clockPuzzleIsOpened; }

        public bool WasCutsceneSeen() { return holder.mirrorCutsceneIsShown; }
        public void CutsceneIsSeen() { holder.mirrorCutsceneIsShown = true; UnsavedChanges(); }

        public bool IsSaved() { return isSaved; }
        public void UnsavedChanges() { isSaved = false; }

        public void SetScene(int number) { holder.sceneNumber = number; UnsavedChanges(); }
        public int GetScene() { return holder.sceneNumber; }





        private void Start()
        {
            isSaved = false;

            holder = new DataHolder();

            PlayerEventPublisher.i.onCollectingAnItem += AddItemToList;

            DeductionEventPublisher.i.onDeductionCompleted += ReactToDeductionCompletion;
            OverworldEventPublisher.i.onDoorUnLocked += LockDoor;

            PlayerEventPublisher.i.onHealthChanged += UpdateHealth;
        }


        private void OnDestroy()
        {
            PlayerEventPublisher.i.onCollectingAnItem -= AddItemToList;
            DeductionEventPublisher.i.onDeductionCompleted -= ReactToDeductionCompletion;
            OverworldEventPublisher.i.onDoorUnLocked -= LockDoor;

            PlayerEventPublisher.i.onHealthChanged -= UpdateHealth;
        }







        public void ClearData(bool immidiately = false)
        {
            if (immidiately)
            {
                Destroy(gameObject);
            }
            else
                Destroy(gameObject, 8f);
        }

        public void SaveData()
        {
            holder.objectsHolding.Clear();
            foreach (PlainObjectInInventoryInfoContainer obj in objectsHolding)
            {
                holder.objectsHolding.Add(DefaultToSaveableConverter.DefaultPlainObjectToSaveable(obj));
            }

            holder.cluesKnowing.Clear();
            foreach (ClueInDiaryInfoContainer clue in cluesKnowing)
            {
                holder.cluesKnowing.Add(DefaultToSaveableConverter.DefaultClueToSaveable(clue));
            }

            holder.setsKnowing.Clear();
            foreach (SetOfCluesInfo set in setsKnowing)
            {
                holder.setsKnowing.Add(DefaultToSaveableConverter.DefaultSetOfCluesToSaveable(set));
            }

            if (commodePuzzlePositions != null)
            {
                holder.commodePuzzlePositions = new List<SaveableObjectInInventory>();
                foreach (ObjectInInventoryInfoContainer obj in commodePuzzlePositions)
                {
                    if (obj != null)
                        holder.commodePuzzlePositions.Add(DefaultToSaveableConverter.DefaultObjectToSaveable(obj));
                    else
                        holder.commodePuzzlePositions.Add(null);
                }
            }


            holder.unSatisfiedEntitiesList.Clear();
            foreach (KeyValuePair<Character, bool> pair in unSatisfiedEntitiesList)
            {
                if (pair.Value == true) { holder.unSatisfiedEntitiesList.Add(pair.Key); }
            }

            holder.unTalkedEntitiesList.Clear();
            foreach (KeyValuePair<Character, bool> pair in unTalkedEntitiesList)
            {
                if (pair.Value == true) { holder.unTalkedEntitiesList.Add(pair.Key); }
            }

            holder.lockedDoors.Clear();
            foreach (KeyValuePair<int, bool> pair in lockedDoors)
            {
                holder.lockedDoors.Add(new DoorData(pair.Key, pair.Value));
            }

            holder.puzzlesBeingCompleted.Clear();
            foreach (KeyValuePair<PuzzleEnums.PuzzleType, bool> pair in puzzlesBeingCompleted)
            {
                if (pair.Value == true) { holder.puzzlesBeingCompleted.Add(pair.Key); }
            }

            SaveSystem.SaveData(holder);

            Debug.Log("Items collected, saving: " + holder.IDsOfCollectedItems.Count);

            isSaved = true;
        }

        public void LoadData()
        {
            holder = new DataHolder();
            holder = SaveSystem.LoadData();

            Debug.Log("Items collected, loading: " + holder.IDsOfCollectedItems.Count);

            objectsHolding.Clear();
            foreach (SaveablePlainObjectInInventory obj in holder.objectsHolding)
            {
                objectsHolding.Add(SaveableToDefaultConverter.SaveablePlainObjectToDefault(obj));
            }

            cluesKnowing.Clear();
            foreach (SaveableClueInDiaryInfoContainer clue in holder.cluesKnowing)
            {
                cluesKnowing.Add(SaveableToDefaultConverter.SaveableClueToDefault(clue));
            }

            setsKnowing.Clear();
            foreach (SaveableSetOfCluesInfo set in holder.setsKnowing)
            {
                setsKnowing.Add(SaveableToDefaultConverter.SaveableSetOfCluesToDefault(set));
            }

            if (holder.commodePuzzlePositions != null)
            {
                commodePuzzlePositions = new List<ObjectInInventoryInfoContainer>();
                foreach (SaveableObjectInInventory obj in holder.commodePuzzlePositions)
                {
                    if (obj != null)
                        commodePuzzlePositions.Add(SaveableToDefaultConverter.SaveableObjectToDefault(obj));
                    else
                        commodePuzzlePositions.Add(null);
                }
            }


            unSatisfiedEntitiesList.Clear();
            foreach (Character character in holder.unSatisfiedEntitiesList)
            {
                unSatisfiedEntitiesList.Add(character, true);
            }

            unTalkedEntitiesList.Clear();
            foreach (Character character in holder.unTalkedEntitiesList)
            {
                unTalkedEntitiesList.Add(character, true);
            }

            lockedDoors.Clear();
            foreach (DoorData data in holder.lockedDoors)
            {
                lockedDoors.Add(data.ID, data.state);
            }

            puzzlesBeingCompleted.Clear();
            foreach (PuzzleEnums.PuzzleType puzzle in holder.puzzlesBeingCompleted)
            {
                puzzlesBeingCompleted.Add(puzzle, false);
            }
        }

        public void DeleteData()
        {
            SaveSystem.DeleteData();
        }
    }
}