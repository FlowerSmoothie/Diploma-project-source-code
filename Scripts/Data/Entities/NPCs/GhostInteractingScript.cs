using System;
using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using General;
using Misc;
using Misc.UI;
using Misc.UI.Deduction;
using Misc.UI.Dialogue;
using Overworld.Clues;
using Overworld.Items;
using Overworld.Items.Containers;
using Overworld.ObjectsAndNPCs;
using UnityEngine;
using Utils.Classes;

namespace EntityUtils.NPCUtils
{
    public class GhostInteractingScript : MonoBehaviour, Interactable, InteractableWithObject
    {
        [SerializeField] List<GhostObjectReaction> ghostReactionsToObjects;

        [SerializeField] List<PhraseToSay> defaultReactionToObject;

        [Space]

        [Header("Speaking")]
        [SerializeField] List<PhraseToSay> reactionTryingToSpeakDefault;
        [SerializeField] List<PhraseToSay> reactionBeforeDialogue;
        [SerializeField] GameObject clueWeNeedToHaveToMakeDialogue;
        private ClueInDiaryInfoContainer clueWeNeed;
        [SerializeField] List<GameObject> clueWellGet;
        [SerializeField] GameObject whatToGive = null;
        [SerializeField] DialogueUnit dialogueData;
        private DialogueMenu dialogueUI;

        //[SerializeField] List<PhraseToSay> reactionTryingToSpeakAfterSuccessfullDeduction;

        private bool isInteractable;
        private bool inGoodRelationsWithPlayer;

        [Header("Deduction")]
        [SerializeField] DeductionUnit deductionEvent;
        [SerializeField] List<GameObject> cluesWellGet;
        [SerializeField] GameObject itemWellGet = null;
        [SerializeField] GameObject itemThatWillSpawn = null;
        [SerializeField] GameObject whatToTakeFromPlayer;
        private DeductionUIScript deductionUI;

        private GhostInfoScript info;
        private GhostBehaviourScript behaviour;

        private PlayerCluesListScript clues;

        private DataHolderScript dhs;

        private void Start()
        {
            isInteractable = false;
            inGoodRelationsWithPlayer = false;

            deductionUI = FindAnyObjectByType<DeductionUIScript>();
            behaviour = GetComponent<GhostBehaviourScript>();
            info = GetComponent<GhostInfoScript>();

            dialogueUI = FindAnyObjectByType<DialogueMenu>();

            clues = FindAnyObjectByType<PlayerCluesListScript>();

            dhs = FindAnyObjectByType<DataHolderScript>();


            clueWeNeed = clueWeNeedToHaveToMakeDialogue.GetComponent<SetOfClues>().GetInfo().GetInfo(FindAnyObjectByType<PlayerHealthScript>().GetAmount());

            //if (dhs.EntityIsSatisfied(info.GetCharacter())) isSatisfied = true;
            //if (dhs.EntityIsTalked(info.GetCharacter())) isDialogued = true;
        }

        public void LockInteracting()
        {
            isInteractable = false;
        }
        public void UnlockInteracting()
        {
            isInteractable = true;
        }

        public void SatisfyGhost() {  }


        public List<PhraseToSay> TryToInteractWithAnObject(int itemID)
        {
            if (isInteractable)
            {
                foreach (GhostObjectReaction reaction in ghostReactionsToObjects)
                {
                    if (reaction.GetObject().GetID() == itemID)
                    {
                        GhostUtils.GhostReactions react = reaction.GetReaction();
                        if (react == GhostUtils.GhostReactions.ATTACKING)
                        {
                            LockInteracting();
                            inGoodRelationsWithPlayer = false;
                        }
                        else if (react == GhostUtils.GhostReactions.PEACEFUL)
                        {
                            inGoodRelationsWithPlayer = true;
                        }
                        GhostEventPublisher.i.DoOnGhostReacting(this, null, reaction.GetReaction());
                        return reaction.GetPhrases();
                    }
                }
                GhostEventPublisher.i.DoOnGhostReacting(this, null, GhostUtils.GhostReactions.NONE);
                return defaultReactionToObject;
            }
            else
            {
                return null;
            }
        }

        public List<PhraseToSay> Interact(bool justCheck = false)
        {
            bool playerHasAClue = FindAnyObjectByType<PlayerCluesListScript>().DoesHaveAClue(clueWeNeed);
            if (isInteractable)
            {
                if (!playerHasAClue)
                {
                    return reactionTryingToSpeakDefault;
                }
                else
                {
                    if (!dhs.EntityIsTalked(info.GetCharacter()) && dhs.EntityIsSatisfied(info.GetCharacter()))
                    {
                        if (!justCheck) UIEventPublisher.i.onDialogueBoxDeactivating += StartDialogue;
                        return reactionBeforeDialogue;
                    }
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void StartDialogue(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= StartDialogue;

            List<SetOfCluesInfo> list = new List<SetOfCluesInfo>();
            foreach(GameObject go in clueWellGet)
            {
                list.Add(go.GetComponent<SetOfClues>().GetInfo());
            }

            Debug.Log(list.Count);

            if(whatToGive != null) dialogueUI.StartDialogue(dialogueData, list, whatToGive.GetComponent<ObjectInInventoryInfo>(), info.GetCharacter());
            else dialogueUI.StartDialogue(dialogueData, list, null, info.GetCharacter());

            //GetComponent<GhostBehaviourScript>().Anger();
            FindAnyObjectByType<PlayerInteractionScript>().DeleteFromInteraction(this.name);
            GetComponent<GhostBehaviourScript>().Anger();
        }

        public void Survey()
        {
            behaviour.SubscribeToDeduction(itemThatWillSpawn);

            List<SetOfCluesInfo> clues = new List<SetOfCluesInfo>();
            foreach (GameObject go in cluesWellGet)
            {
                clues.Add(go.GetComponent<SetOfClues>().GetSet());
            }

            deductionUI.StartDeduction(deductionEvent, clues, itemWellGet, whatToTakeFromPlayer, info.GetCharacter());
            FindAnyObjectByType<PlayerInteractionScript>().DeleteFromInteraction(this.name);
        }

        public bool CanBeSurveyed()
        {
            Debug.Log(isInteractable && inGoodRelationsWithPlayer && !dhs.EntityIsSatisfied(info.GetCharacter()) && clues.GetClues().Count > 1);
            if (isInteractable && inGoodRelationsWithPlayer && !dhs.EntityIsSatisfied(info.GetCharacter()) && clues.GetClues().Count > 1) { return true; }
            return false;
        }

        public bool IsObject()
        {
            return false;
        }

        public TextUnit GetName()
        {
            return CharacterUtils.GetCharacterNickNameAsTextUnit(CharactersList.GetCharacter(info.GetCharacter()));
        }

        public bool CanItemsBeUsedOnIt()
        {
            if (isInteractable && !dhs.EntityIsSatisfied(info.GetCharacter())) return true;
            else return false;
        }

        public bool IsThisPuzzle() { return false; }
    }
}