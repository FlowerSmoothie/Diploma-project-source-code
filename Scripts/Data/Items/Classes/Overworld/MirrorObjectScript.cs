using System;
using System.Collections.Generic;
using Misc;
using Misc.UI;
using UnityEngine;
using UnityEngine.Video;
using Utils;
using Utils.Classes;

namespace Overworld.Items
{
    [RequireComponent(typeof(SceneSwitcher))]
    public class MirrorObjectScript : ZoomableItemInOverworldVideo, InteractableWithObject
    {
        [SerializeField] private GameObject amulet;
        [SerializeField] private List<PhraseToSay> usingAmuletPhrases;
        [SerializeField] private List<PhraseToSay> tryingToSeeOneMoreTimePhrases;

        [SerializeField] private AudioClip winningSound;


        public bool IsThisPuzzle()
        {
            return false;
        }

        public override bool CanItemsBeUsedOnIt() { return true; }

        public List<PhraseToSay> TryToInteractWithAnObject(int itemID)
        {
            if (itemID != amulet.GetComponent<ObjectInInventoryInfo>().GetItemInfo().GetID()) return null;
            UIEventPublisher.i.onDialogueBoxDeactivating += ChangingSceneToEpilogue;
            return usingAmuletPhrases;
        }

        private void ChangingSceneToEpilogue(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= ChangingSceneToEpilogue;

            GetComponent<SceneSwitcher>().ChangeScene(8, 2);
            FindAnyObjectByType<AudioSourcesScript>().StartHeartbeat();
            FindAnyObjectByType<AudioSourcesScript>().PlayUI(winningSound);
        }

        public override List<PhraseToSay> Interact(bool justCheck = false)
        {
            if(FindAnyObjectByType<DataHolderScript>().WasCutsceneSeen()) return tryingToSeeOneMoreTimePhrases;
            return currentDescription;
        }

        public override VideoClip GetImage(int mentalHealth)
        {
            DataHolderScript dhs = FindAnyObjectByType<DataHolderScript>();
            if (dhs.WasCutsceneSeen()) return null;
            dhs.CutsceneIsSeen();
            if (mentalHealth < 100 && mentalHealth >= greatDownBorder)
            {
                return videoToShowGreat;
            }
            else if (mentalHealth < greatDownBorder && mentalHealth >= normalDownBorder)
            {
                return videoToShowNormal;
            }
            else
            {
                return videoToShowBad;
            }
        }
    }
}