using System;
using System.Collections.Generic;
using EntityUtils.NPCUtils;
using EntityUtils.PlayerUtils;
using General;
using Overworld.Clues;
using Overworld.Items;
using UnityEngine;

namespace Misc.UI.Dialogue
{
    public class DialogueMenu : UIMenuScript
    {
        private Character character;
        private int currentRound;
        private int totalRounds;


        DialogueDataLoader dataLoader;

        List<SetOfCluesInfo> clueToAddIfWin = null;
        ObjectInInventoryInfo objectToAdd = null;


        private void Start()
        {
            dataLoader = GetComponent<DialogueDataLoader>();

            UIEventPublisher.i.onDialogueDeActivating += ActivateMenu;
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onDialogueDeActivating -= ActivateMenu;
        }


        public void StartDialogue(DialogueUnit data, List<SetOfCluesInfo> clueToAddIfWin, ObjectInInventoryInfo obj, Character character)
        {
            this.character = character;

            this.clueToAddIfWin = clueToAddIfWin;

            objectToAdd = obj;

            currentRound = 0;

            totalRounds = data.lines.Count - 1;

            dataLoader.SetAll(data);

            dataLoader.StartRound(currentRound);

            UIEventPublisher.i.DoUIActivating(this, null);
            UIEventPublisher.i.DoDialogueDeActivating(this, null);
        }

        private void EndDeduction(bool isOkay)
        {
            if (isOkay)
            {
                if(objectToAdd != null) FindAnyObjectByType<PlayerInventoryScript>().AddToInventory(objectToAdd.GetItemInfo());
                if(clueToAddIfWin != null) FindAnyObjectByType<PlayerCluesListScript>().AddList(clueToAddIfWin);
                dataLoader.FinalPhraseShow();
                UIEventPublisher.i.onDialogueBoxDeactivating += FinalizeDialogue;
            }
            else
            {
                dataLoader.WrongAnswerShow();
                UIEventPublisher.i.onDialogueBoxDeactivating += FinalizeDialogue;
            }
            FindAnyObjectByType<DataHolderScript>().UpdateEntityDialogueStatus(character, isOkay);
        }

        private void FinalizeDialogue(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= FinalizeDialogue;
            UIEventPublisher.i.DoUIDeactivating(this, null);
            UIEventPublisher.i.DoDialogueDeActivating(this, null);

            FindAnyObjectByType<GhostBehaviourScript>().ChangeToWander();
        }

        public void Move(bool isOkay)
        {
            if (currentRound == totalRounds && isOkay)
            {
                //DialogueBoxScript
                EndDeduction(true);                
                FindAnyObjectByType<PlayerHealthScript>().Heal(10);
                return;
            }
            else if (!isOkay)
            {
                EndDeduction(false);
                return;
            }

            currentRound++;
            dataLoader.StartRound(currentRound);
        }




    }
}