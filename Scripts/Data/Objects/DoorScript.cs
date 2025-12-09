using System;
using System.Collections.Generic;
using Misc;
using Misc.Overworld;
using Misc.UI;
using Overworld.Items;
using Overworld.Items.Containers;
using UnityEngine;
using Utils;
using Utils.Classes;
using Utils.Text;

namespace Overworld.ObjectsAndNPCs
{
    [RequireComponent(typeof(SceneSwitcher))]
    public class DoorScript : MonoBehaviour, InteractableObject, InteractableWithObject
    {
        [SerializeField] bool isLockedOriginally;
        [SerializeField] GameObject keyToUnlock;
        [SerializeField] List<PhraseToSay> whenDoorIsUnlocked;
        [SerializeField] bool isStairs = false;
        private bool defactoLocked;
        [SerializeField] int objectID;
        [SerializeField] int doorID;
        [SerializeField] int sceneToWhichWillLead;
        [SerializeField] int spawnIdOnNextScene;


        [SerializeField] AudioClip unlocking;
        [SerializeField] AudioClip okay;

        private SceneSwitcher switcher;

        private void Start()
        {
            int res = FindAnyObjectByType<DataHolderScript>().IsDoorLocked(doorID);
            switch (res)
            {
                default:
                    defactoLocked = isLockedOriginally;
                    break;
                case 0:
                    defactoLocked = false;
                    break;
                case 1:
                    defactoLocked = true;
                    break;
            }

            switcher = GetComponent<SceneSwitcher>();

            OverworldEventPublisher.i.onDoorUnLocked += UnLockDoor;
        }
        private void OnDestroy()
        {
            OverworldEventPublisher.i.onDoorUnLocked -= UnLockDoor;
            UIEventPublisher.i.onDialogueBoxDeactivating -= StartTransitioning;
        }

        private void UnLockDoor(object sender, EventArgs e, int doorID, bool isLockedNow)
        {
            if (doorID == this.doorID)
            {
                FindAnyObjectByType<AudioSourcesScript>().PlaySound(unlocking);
                defactoLocked = isLockedNow;
            }
        }

        public bool CanBeSurveyed()
        {
            return false;
        }

        public bool IsObject()
        {
            return true;
        }

        public TextUnit GetName()
        {
            if (isStairs) return DefaultObjectsList.STAIRS;
            return DefaultObjectsList.DOOR;
        }

        public List<PhraseToSay> Interact(bool justCheck = true)
        {

            if (defactoLocked)
            {
                return new List<PhraseToSay> { PhrasesList.CANNOT_USE_THE_DOOR };
            }
            else
            {
                if (!justCheck)
                {
                    SubscribeToDialogueBoxClosing();
                }
                if (isStairs) return new List<PhraseToSay> { PhrasesList.DOWN_THE_STAIRS };
                return new List<PhraseToSay> { PhrasesList.OPENING_THE_DOOR };
            }
        }

        private void SubscribeToDialogueBoxClosing()
        {
            UIEventPublisher.i.onDialogueBoxDeactivating += StartTransitioning;
        }

        private void StartTransitioning(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= StartTransitioning;

            UIEventPublisher.i.DoUIActivating(this, null);
            FindAnyObjectByType<AudioSourcesScript>().PlaySound(okay);

            FindAnyObjectByType<DataHolderScript>().SetSpawnPoint(spawnIdOnNextScene);
            switcher.ChangeScene(sceneToWhichWillLead);
        }

        public bool CanItemsBeUsedOnIt() { return true; }

        public int GetID() { return objectID; }

        public bool IsCollectable() { return false; }

        public bool IsNote() { return false; }

        public bool CanBeInteractedWithItems() { return true; }

        public float DelayedInteracting() { return 0; }

        public bool IsZoomable() { return false; }

        public List<PhraseToSay> TryToInteractWithAnObject(int itemID)
        {
            if (!defactoLocked) return null;
            if (itemID != keyToUnlock.GetComponent<ObjectInInventoryInfo>().GetItemInfo().GetID()) return null;

            OverworldEventPublisher.i.DoorUnLocked(this, null, doorID, false);
            return whenDoorIsUnlocked;
        }

        public bool IsThisPuzzle() { return false; }

        public bool IsItem()
        {
            throw new NotImplementedException();
        }
    }
}