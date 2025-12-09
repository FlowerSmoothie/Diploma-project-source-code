
using System;
using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Misc;
using Misc.UI;
using Overworld.Utils;
using UnityEngine;
using Utils;
using Utils.Classes;

namespace Overworld.ObjectsAndNPCs
{
    public class InteractableObjectInOverworld : InteractableItemInOverworld
    {
        [Header("Base options:")]
        [SerializeField] protected int itemID;
        [SerializeField] protected TextUnit itemName;
        [SerializeField] private bool willDoorOpenAfterExamination;
        [SerializeField] private int whatDoorWillOpen;
        private bool interactedAlready;


        [SerializeField] private MeshRenderer meshRenderer;
        private Material currentMaterial = null;
        private List<PhraseToSay> currentDescription;

        [Space]
        [Header("Great mental health options:")]

        [SerializeField] protected Material materialGreat;
        [SerializeField] protected List<PhraseToSay> descriptionOnDiscoveringGreat;
        [SerializeField] protected int greatDownBorder = Consts.GREAT_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;

        [Space]
        [Header("Normal mental health options:")]

        [SerializeField] protected Material materialNormal;
        [SerializeField] protected List<PhraseToSay> descriptionOnDiscoveringNormal;
        [SerializeField] protected int normalDownBorder = Consts.NORMAL_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;

        [Space]
        [Header("Bad mental health options:")]

        [SerializeField] protected Material materialBad;
        [SerializeField] protected List<PhraseToSay> descriptionOnDiscoveringBad;


        private int mcHealth;

        protected virtual void Start()
        {
            interactedAlready = FindAnyObjectByType<DataHolderScript>().DidInteractWithAnItem(itemID);

            meshRenderer = GetComponentInChildren<MeshRenderer>();

            mcHealth = FindAnyObjectByType<PlayerHealthScript>().GetAmount();
            PlayerEventPublisher.i.onHealthChanged += UpdateHealth;
            currentMaterial = null;

            UpdateImage();
        }

        private void OnDestroy()
        {
            PlayerEventPublisher.i.onHealthChanged -= UpdateHealth;
        }

        private void UpdateHealth(object sender, EventArgs e, int health)
        {
            mcHealth += health;
            if (mcHealth > 100) mcHealth = 100;
            else if (mcHealth < 0) mcHealth = 0;
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (mcHealth <= 100 && mcHealth >= greatDownBorder)
            {
                currentMaterial = materialGreat;
                currentDescription = descriptionOnDiscoveringGreat;
            }
            else if (mcHealth < greatDownBorder && mcHealth >= normalDownBorder)
            {
                currentMaterial = materialNormal;
                currentDescription = descriptionOnDiscoveringNormal;
            }
            else
            {
                currentMaterial = materialBad;
                currentDescription = descriptionOnDiscoveringBad;
            }
            try
            {
                meshRenderer.material = currentMaterial;
            }
            catch
            {

            }
        }


        public override TextUnit GetName() { return itemName; }
        public override int GetID() { return itemID; }

        public override bool IsCollectable() { return false; }
        public override bool IsNote() { return false; }
        public override bool CanBeInteractedWithItems() { return false; }

        public override List<PhraseToSay> Interact(bool justCheck = false)
        {
            if (willDoorOpenAfterExamination && !interactedAlready) UIEventPublisher.i.onDialogueBoxActivating += UnlockDoor;

            interactedAlready = true;
            FindAnyObjectByType<DataHolderScript>().AddToInteractedItems(itemID);
            return currentDescription;
        }

        private void UnlockDoor(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxActivating -= UnlockDoor;
            GetComponent<DoorUnLockingScript>().UnlockDoor(whatDoorWillOpen);
        }

        public override float DelayedInteracting() { return 0; }

        public override bool IsZoomable() { return false; }

        public override bool CanItemsBeUsedOnIt() { return false; }

    }
}