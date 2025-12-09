using System.Collections.Generic;
using UnityEngine;

using Overworld.ObjectsAndNPCs;
using Utils.Text;
using Utils.Classes;
using Misc.UI;
using Overworld.Items;
using EntityUtils.NPCUtils;
using Overworld.Clues;
using System;
using System.Collections;
using Misc;

namespace EntityUtils.PlayerUtils
{


    public class PlayerInteractionScript : MonoBehaviour
    {
        [SerializeField] private BoxCollider interactableBox;

        private List<GameObject> objectsWeCanInteractWith;

        private DialogueBoxScript dialogueBox;
        private NoteReadingUI noteReading;
        private ZoomedImageShower zoomedShower;

        private PlayerInventoryScript inventory;
        private PlayerCluesListScript cluesList;
        private PlayerHealthScript health;

        private bool freezed = false;

        private GhostInteractingScript ghostToInteract;


        [SerializeField] private AudioClip pickingUpSound;
        private AudioSourcesScript asc;


        void Start()
        {
            objectsWeCanInteractWith = new List<GameObject>();

            dialogueBox = FindAnyObjectByType<DialogueBoxScript>();
            noteReading = FindAnyObjectByType<NoteReadingUI>();
            zoomedShower = FindAnyObjectByType<ZoomedImageShower>();

            //inventory = GetComponent<PlayerInventoryScript>();
            inventory = FindAnyObjectByType<PlayerInventoryScript>();
            //cluesList = GetComponent<PlayerCluesListScript>();
            cluesList = FindAnyObjectByType<PlayerCluesListScript>();
            health = GetComponent<PlayerHealthScript>();

            asc = FindAnyObjectByType<AudioSourcesScript>();


            UIEventPublisher.i.onUIActivating += FreezeInteractions;
            UIEventPublisher.i.onUIFullyDeactivating += UnfreezeInteractions;
        }

        void OnDestroy()
        {
            UIEventPublisher.i.onUIActivating -= FreezeInteractions;
            UIEventPublisher.i.onUIFullyDeactivating -= UnfreezeInteractions;
        }

        public void DeleteFromInteraction(string name)
        {
            foreach (GameObject obj in objectsWeCanInteractWith)
            {
                if (obj.name == name)
                {
                    objectsWeCanInteractWith.Remove(obj);
                    break;
                }
            }
        }

        void Update()
        {
            //Debug.Log(objectsWeCanInteractWith.Count);
            if (!freezed)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (objectsWeCanInteractWith.Count > 0)
                    {
                        List<Tuple<string, int>> names = new List<Tuple<string, int>>();

                        for (int i = 0; i < objectsWeCanInteractWith.Count; i++)
                        {
                            if (objectsWeCanInteractWith[i].GetComponent<Interactable>().Interact() == null) continue;
                            names.Add(new Tuple<string, int>(TextUnitUtils.GetTextUnitText(objectsWeCanInteractWith[i].GetComponent<Interactable>().GetName()), i));
                        }

                        if (names.Count == 0) { return; }

                        SelectableUIPublisher.i.onOptionChoosing += InteractWithChosenInteractable;

                        PlayerEventPublisher.i.DoOnTryingToInteractWithObects(this, null, names);

                        UIEventPublisher.i.DoUIActivating(this, null);
                        UIEventPublisher.i.DoChoicesMenuActivating(this, null);
                    }
                }
            }
        }


        private void FreezeInteractions(object sender, System.EventArgs e)
        {
            PlayerEventPublisher.i.DoOnItemDefound(this, null);
            freezed = true;
        }

        private void UnfreezeInteractions(object sender, System.EventArgs e)
        {
            if (objectsWeCanInteractWith.Count > 0) PlayerEventPublisher.i.DoOnItemFound(this, null);
            freezed = false;
        }


        private void InteractWithChosenInteractable(object sender, EventArgs e, int ID)
        {
            SelectableUIPublisher.i.onOptionChoosing -= InteractWithChosenInteractable;

            if (ID == -1)
            {
                UIEventPublisher.i.DoUIDeactivating(this, null);
                return;
            }

            /*if (objectsWeCanInteractWith[ID].TryGetComponent<SetOfClues>(out SetOfClues set))
            {
                cluesList.AddToClueList(set.GetInfo(health.GetAmount()));
            }*/

            Interactable interactable = objectsWeCanInteractWith[ID].GetComponent<Interactable>();


            if (interactable.IsObject())
            {
                InteractWithAnObject((InteractableObject)interactable, ID);
            }
            else
            {
                ghostToInteract = (GhostInteractingScript)interactable;
                if (ghostToInteract.CanBeSurveyed())
                {
                    SelectableUIPublisher.i.onOptionChoosing += IneractWithGhost;

                    StartCoroutine(waitThenCall());
                }
                //else if(ghostToInteract.Can)
                else
                {
                    dialogueBox.WriteTextFunc(ghostToInteract.Interact(), 0);
                }
            }
        }

        private IEnumerator waitThenCall()
        {
            UIEventPublisher.i.DoUIActivating(this, null);
            yield return new WaitForSecondsRealtime(0.3f);

            List<Tuple<string, int>> options = new List<Tuple<string, int>>() { new Tuple<string, int>("Поговорить", 0), new Tuple<string, int>("Опросить", 1) };

            PlayerEventPublisher.i.DoOnTryingToInteractWithObects(this, null, options);

            UIEventPublisher.i.DoChoicesMenuActivating(this, null);
        }


        private NoteOverworld note;

        private void InteractWithAnObject(InteractableObject saidObject, int ID)
        {
            DataHolderScript dhs = FindAnyObjectByType<DataHolderScript>();

            dialogueBox.WriteTextFunc(saidObject.Interact(false), saidObject.DelayedInteracting());

            if (saidObject.IsZoomable())
            {
                if (objectsWeCanInteractWith[ID].TryGetComponent<SetOfClues>(out SetOfClues set))
                    cluesList.AddToClueList(set.GetInfo(health.GetAmount()));

                if (saidObject.IsItem()) zoomedShower.Show((ZoomableItemInOverworld)saidObject, health.GetAmount());
                else zoomedShower.Show((ZoomableObjectInOverworldImage)saidObject, health.GetAmount());
            }

            if (saidObject.IsCollectable())
            {
                CollectableItemInOverworld ciio = objectsWeCanInteractWith[ID].GetComponent<CollectableItemInOverworld>();
                PlayerEventPublisher.i.DoOnCollectingAnItem(this, null, ciio.GetID());
                inventory.AddToInventory(ciio.Collect());
                dhs.AddToCollectedItems(ciio.GetID());
                objectsWeCanInteractWith.RemoveAt(ID);

                asc.PlayPlayer(pickingUpSound);

                if (saidObject.IsNote())
                {
                    note = (NoteOverworld)ciio;
                    UIEventPublisher.i.onDialogueBoxDeactivating += ReadANote;
                }
            }
            else
            {
                if (objectsWeCanInteractWith[ID].TryGetComponent<SetOfClues>(out SetOfClues set))
                    cluesList.AddToClueList(set.GetInfo(health.GetAmount()));
            }
        }

        private void ReadANote(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= ReadANote;
            noteReading.WriteTextFunc(note.GetName(), note.Read(health.GetAmount()));
        }

        private void IneractWithGhost(object sender, System.EventArgs e, int ID)
        {
            SelectableUIPublisher.i.onOptionChoosing -= IneractWithGhost;
            if (ID == -1)
            {
                UIEventPublisher.i.DoUIDeactivating(this, null);
                return;
            }
            if (ID == 0)
            {
                dialogueBox.WriteTextFunc(ghostToInteract.Interact(), 0);
            }
            else if (ID == 1)
            {
                ghostToInteract.Survey();
            }
        }


        public List<GameObject> GetListOfInteractableObjects() { return objectsWeCanInteractWith; }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Interactable interactable) && interactable.Interact().Count != 0)
            {
                if (!objectsWeCanInteractWith.Contains(other.gameObject))
                {
                    objectsWeCanInteractWith.Add(other.gameObject);
                    PlayerEventPublisher.i.DoOnItemFound(other.gameObject, null);
                }
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Interactable interactable))
            {
                objectsWeCanInteractWith.Remove(other.gameObject);
                if (objectsWeCanInteractWith.Count == 0)
                {
                    PlayerEventPublisher.i.DoOnItemDefound(this, null);
                }
            }
        }

        public void LockInteraction()
        {
            FreezeInteractions(this, null);
        }
    }
}