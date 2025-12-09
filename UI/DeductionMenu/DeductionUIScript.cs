using System;
using System.Collections;
using System.Collections.Generic;
using EntityUtils.NPCUtils;
using EntityUtils.PlayerUtils;
using General;
using Overworld.Clues;
using Overworld.Items;
using Overworld.Items.Containers;
using UI.Prefabs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using Utils.Classes;

namespace Misc.UI.Deduction
{
    public class DeductionUIScript : UIMenuScript
    {
        private Character character;
        private int currentRound;
        private int totalRounds;

        public enum Stage { GHOST_TALKING, PLAYER_THINKING, RESULT_GOOD, RESULT_BAD };
        private Stage currentStage;

        DeductionUIDataLoader dataLoader;

        PlayerCluesListScript playerClues;
        List<SetOfCluesInfo> cluesToAddIfWin = null;
        ObjectInInventoryInfoContainer itemWellGet = null;
        PlainObjectInInventoryInfoContainer itemWellLose = null;

        [SerializeField] GameObject shower;

        private bool waitingForMouseClick;

        private void Start()
        {
            dataLoader = GetComponent<DeductionUIDataLoader>();

            playerClues = FindAnyObjectByType<PlayerCluesListScript>();

            UIEventPublisher.i.onDeductionDeActivating += ActivateMenu;

            DeductionEventPublisher.i.onWaitForMouseClick += StartWaitingForMouse;
            DeductionEventPublisher.i.onSceneMoving += SceneManaging;
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onDeductionDeActivating -= ActivateMenu;

            DeductionEventPublisher.i.onWaitForMouseClick -= StartWaitingForMouse;
            DeductionEventPublisher.i.onSceneMoving -= SceneManaging;
        }

        private void StartWaitingForMouse(object sender, EventArgs eventArgs)
        {
            waitingForMouseClick = true;
        }


        private void Update()
        {
            if (waitingForMouseClick && Input.GetMouseButtonUp(0))
            {
                waitingForMouseClick = false;
                DeductionEventPublisher.i.MouseClicked(this, null);
                DeductionEventPublisher.i.SceneMoving(this, null);
            }
        }


        public void StartDeduction(DeductionUnit data, List<SetOfCluesInfo> cluesToAddIfWin, GameObject itemWellGet, GameObject whatToTakeFromPlayer, Character character)
        {
            this.character = character;

            shower.SetActive(true);

            this.cluesToAddIfWin = cluesToAddIfWin;

            if(itemWellGet != null) this.itemWellGet = (ObjectInInventoryInfoContainer)itemWellGet.GetComponent<ObjectInInventoryInfo>().GetItemInfo();
            else this.itemWellGet = null;
            if(whatToTakeFromPlayer != null) itemWellLose = whatToTakeFromPlayer.GetComponent<ObjectInInventoryInfo>().GetItemInfo();
            else itemWellLose = null;
            
            currentRound = 0;
            currentStage = Stage.GHOST_TALKING;

            totalRounds = data.neededClues.Count - 1;

            dataLoader.SetAll(data);

            DeductionEventPublisher.i.DoMovingToRound(this, null, currentRound);
            dataLoader.SetCluesFromInventory(FindAnyObjectByType<PlayerCluesListScript>().GetClues());
            dataLoader.LoadStage(currentStage, currentRound);

            UIEventPublisher.i.DoUIActivating(this, null);
            UIEventPublisher.i.DoDeductionDeActivating(this, null);
        }

        private void EndDeduction(bool isOkay)
        {
            if (isOkay)
            {
                //int health = FindAnyObjectByType<PlayerHealthScript>().GetAmount();
                foreach (SetOfCluesInfo set in cluesToAddIfWin)
                {
                    playerClues.AddSet(set);
                }
                FindAnyObjectByType<PlayerHealthScript>().Heal(15);
                FindAnyObjectByType<PlayerInventoryScript>().AddToInventory(itemWellGet);
                FindAnyObjectByType<PlayerInventoryScript>().DeleteFromInventory(itemWellLose);
            }
            UIEventPublisher.i.DoUIDeactivating(this, null);
            UIEventPublisher.i.DoDeductionDeActivating(this, null);
            DeductionEventPublisher.i.DeductionCompleted(this, null, character, isOkay);

            shower.SetActive(false);
            FindAnyObjectByType<GhostBehaviourScript>().ChangeToWander();
        }

        private void SceneManaging(object sender, EventArgs args, bool doesBreak)
        {
            if (currentRound == totalRounds && currentStage == Stage.RESULT_GOOD)
            {
                EndDeduction(true);
                return;
            }
            else if (currentStage == Stage.RESULT_BAD)
            {
                EndDeduction(false);
                return;
            }
            switch (currentStage)
            {
                case Stage.GHOST_TALKING:
                    currentStage = Stage.PLAYER_THINKING;
                    break;
                case Stage.PLAYER_THINKING:
                    if (!doesBreak)
                        currentStage = Stage.RESULT_GOOD;
                    else
                        currentStage = Stage.RESULT_BAD;
                    break;
                case Stage.RESULT_GOOD:
                    currentStage = Stage.GHOST_TALKING;
                    currentRound++;
                    break;
            }
            dataLoader.LoadStage(currentStage, currentRound);
        }
    }
}