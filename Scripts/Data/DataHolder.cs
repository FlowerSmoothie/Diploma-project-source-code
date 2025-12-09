using System;
using System.Collections.Generic;
using General;
using Overworld.ObjectsAndNPCs;
using UnityEngine;

namespace Misc.Saving
{
    [Serializable]
    public class DataHolder
    {
        public DataHolder()
        {
            sceneNumber = 0;

            IDsOfCollectedItems = new List<CollectedObject>();
            IDsOfObjectsWeInteractedWith = new List<int>();

            health = 100;

            objectsHolding = new List<SaveablePlainObjectInInventory>();

            cluesKnowing = new List<SaveableClueInDiaryInfoContainer>();

            setsKnowing = new List<SaveableSetOfCluesInfo>();

            commodePuzzlePositions = new List<SaveableObjectInInventory>();

            unSatisfiedEntitiesList = new List<Character>();
            unTalkedEntitiesList = new List<Character>();

            lockedDoors = new List<DoorData>();

            puzzlesBeingCompleted = new List<PuzzleEnums.PuzzleType>();


            clockPuzzleMinutes = -1;
            clockPuzzleHours = -1;
            clockPuzzleIsOpened = false;


            mirrorCutsceneIsShown = false;
        }

        public int sceneNumber;

        [SerializeReference]
        public List<CollectedObject> IDsOfCollectedItems;

        [SerializeReference]
        public List<int> IDsOfObjectsWeInteractedWith;


        public int health;

        [SerializeReference]
        public List<SaveablePlainObjectInInventory> objectsHolding;

        [SerializeReference]
        public List<SaveableClueInDiaryInfoContainer> cluesKnowing;
        [SerializeReference]
        public List<SaveableSetOfCluesInfo> setsKnowing;

        [SerializeReference]
        public List<Character> unSatisfiedEntitiesList;
        [SerializeReference]
        public List<Character> unTalkedEntitiesList;

        [SerializeReference]
        public List<DoorData> lockedDoors;

        /*public Dictionary<Character, bool> unSatisfiedEntitiesList;
        public Dictionary<Character, bool> unTalkedEntitiesList;

        public Dictionary<int, bool> lockedDoors;*/


        public int IdOfSpawnPoint;

        //public Dictionary<PuzzleEnums.PuzzleType, bool> puzzlesBeingCompleted;
        [SerializeReference]
        public List<PuzzleEnums.PuzzleType> puzzlesBeingCompleted;


        [SerializeReference]
        public List<SaveableObjectInInventory> commodePuzzlePositions;

        public int clockPuzzleMinutes;
        public int clockPuzzleHours;
        public bool clockPuzzleIsOpened;


        public bool mirrorCutsceneIsShown;
    }
}