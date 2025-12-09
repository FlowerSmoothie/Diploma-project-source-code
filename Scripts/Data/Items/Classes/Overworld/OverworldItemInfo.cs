using System;
using System.Collections;
using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Misc;
using Misc.Overworld;
using Misc.UI;
using Overworld.Utils;
using UnityEngine;
using Utils;
using Utils.Classes;

namespace Overworld.ObjectsAndNPCs
{
    [Serializable]
    public class OverworldItemInfo : MonoBehaviour, InteractableObject
    {
        [Header("Base options:")]
        [SerializeField] protected int itemID;
        [SerializeField] protected TextUnit itemName;
        [SerializeField] private bool willDoorOpenAfterExamination;
        [SerializeField] private int whatDoorWillOpen;
        protected bool interactedAlready;


        private Sprite currentSprite = null;
        protected List<PhraseToSay> currentDescription;

        [Space]
        [Header("Great mental health options:")]

        [SerializeField] protected Sprite spriteGreat;
        [SerializeField] protected List<PhraseToSay> descriptionOnDiscoveringGreat;
        [SerializeField] protected int greatDownBorder = Consts.GREAT_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;

        [Space]
        [Header("Normal mental health options:")]

        [SerializeField] protected Sprite spriteNormal;
        [SerializeField] protected List<PhraseToSay> descriptionOnDiscoveringNormal;
        [SerializeField] protected int normalDownBorder = Consts.NORMAL_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;

        [Space]
        [Header("Bad mental health options:")]

        [SerializeField] protected Sprite spriteBad;
        [SerializeField] protected List<PhraseToSay> descriptionOnDiscoveringBad;

        [Space]
        [Header("Visualisation objects:")]

        [SerializeField] GameObject upperVisual;
        [SerializeField] GameObject lowerVisual;
        private SpriteRenderer upperSprite;
        private SpriteRenderer lowerSprite;

        private int mcHealth;

        [SerializeField] private bool isChanging = true;

        protected virtual void Start()
        {
            interactedAlready = FindAnyObjectByType<DataHolderScript>().DidInteractWithAnItem(itemID);
            //if (interactedAlready) { Destroy(gameObject, 0.1f); return; }

            mcHealth = FindAnyObjectByType<DataHolderScript>().GetHealth();
            if(isChanging) PlayerEventPublisher.i.onHealthChanged += UpdateHealth;
            currentSprite = null;

            if (isChanging)
            {
                upperSprite = upperVisual.GetComponent<SpriteRenderer>();

                lowerSprite = lowerVisual.GetComponent<SpriteRenderer>();
            }

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
            //if(upperSprite == null) return;
            if(isChanging && upperSprite != null) upperSprite.sprite = currentSprite;

            if (mcHealth <= 100 && mcHealth >= greatDownBorder)
            {
                if(isChanging && upperSprite != null) currentSprite = spriteGreat;
                currentDescription = descriptionOnDiscoveringGreat;
            }
            else if (mcHealth < greatDownBorder && mcHealth >= normalDownBorder)
            {
                if(isChanging && upperSprite != null) currentSprite = spriteNormal;
                currentDescription = descriptionOnDiscoveringNormal;
            }
            else
            {
                if(isChanging && upperSprite != null) currentSprite = spriteBad;
                currentDescription = descriptionOnDiscoveringBad;
            }
            if(isChanging && upperSprite != null) StartCoroutine(ChangeImage());
        }

        private IEnumerator ChangeImage()
        {
            upperSprite.sprite = lowerSprite.sprite;
            upperSprite.color = new Color(upperSprite.color.r, upperSprite.color.g, upperSprite.color.b, 1);
            lowerSprite.sprite = currentSprite;
            while (upperSprite.color.a > 0)
            {
                upperSprite.color = new Color(upperSprite.color.r, upperSprite.color.g, upperSprite.color.b, upperSprite.color.a - (Time.deltaTime / 0.5f));
                yield return new WaitForEndOfFrame();
            }
            upperSprite.color = new Color(upperSprite.color.r, upperSprite.color.g, upperSprite.color.b, 0);
        }


        public TextUnit GetName() { return itemName; }
        public int GetID() { return itemID; }

        public virtual bool IsCollectable() { return false; }
        public virtual bool IsNote() { return false; }
        public bool CanBeInteractedWithItems() { return false; }

        public virtual List<PhraseToSay> Interact(bool justCheck = false)
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

        public virtual float DelayedInteracting() { return 0; }

        public virtual bool IsZoomable() { return false; }

        public bool CanBeSurveyed()
        {
            return false;
        }

        public bool IsObject()
        {
            return true;
        }

        public virtual bool CanItemsBeUsedOnIt()
        {
            return false;
        }

        public bool IsItem()
        {
            return true;
        }
    }
}