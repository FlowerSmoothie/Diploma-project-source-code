using System;
using System.Collections;
using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Overworld.Clues;
using UnityEngine;
using UnityEngine.Video;
using Utils.Classes;

namespace Misc.UI.Deduction
{
    public class DeductionUIDataLoader : MonoBehaviour
    {
        private VideoClip openingCilp;
        private VideoClip thinkingClip;
        private VideoClip greatClip;
        private VideoClip failClip;
        private VideoClip talkingClip;
        private void SetVideos(VideoClip openingCilp, VideoClip thinkingClip, VideoClip greatClip, VideoClip failClip, VideoClip talkingClip)
        {
            this.openingCilp = openingCilp;
            this.thinkingClip = thinkingClip;

            this.greatClip = greatClip;
            this.failClip = failClip;

            this.talkingClip = talkingClip;
        }

        private List<DeductionSetOfTexts> texts;
        private List<GameObject> neededClues;
        private List<ContinuationUnit> continuations;
        private List<PhraseToSay> breaking;
        private void SetRounds(List<DeductionSetOfTexts> texts, List<GameObject> neededClues, List<ContinuationUnit> continuations, List<PhraseToSay> breaking)
        {
            this.texts = texts;
            this.neededClues = neededClues;
            this.continuations = continuations;
            this.breaking = breaking;
        }

        private List<ClueInDiaryInfoContainer> cluesFromInventory;
        public void SetCluesFromInventory(List<ClueInDiaryInfoContainer> clues) { cluesFromInventory = clues; }

        public void SetAll(DeductionUnit data)
        {
            SetVideos(data.openingCilp, data.thinkingClip, data.greatClip, data.failClip, data.talkingClip);
            SetRounds(data.texts, data.neededClues, data.continuations, data.breaking);
        }

        [SerializeField] GameObject tellingGameObject;
        [SerializeField] GameObject thinkingGameObject;

        private AssumptionSolvingChecker asc;
        private DialogueBoxScript dialogueBox;


        [SerializeField] VideoPlayer animationPlayer;
        [SerializeField] CluesManager cluesManager;
        [SerializeField] GhostTextContainerScript ghostTextContainer;

        [SerializeField] AudioClip right;
        [SerializeField] AudioClip wrong;
        private AudioSourcesScript audios;


        private void PlayOpeningVideo() { animationPlayer.clip = openingCilp; /*animationPlayer.isLooping = false;*/ }
        public void PlayThinkingVideo() { animationPlayer.clip = thinkingClip; /*animationPlayer.isLooping = false;*/ }
        public void PlaySuccessVideo() { animationPlayer.clip = greatClip; }
        public void PlayFailVideo() { animationPlayer.clip = failClip; }
        public void PlayTalkVideo() { animationPlayer.clip = talkingClip; }

        private void InstantiateCluesFromInventory() { cluesManager.InstantiateClues(cluesFromInventory); }
        private void DeleteInstantiatedClues() { cluesManager.DeleteInstantiatedClues(); }


        private void Start()
        {
            asc = GetComponent<AssumptionSolvingChecker>();
            dialogueBox = FindAnyObjectByType<DialogueBoxScript>();

            audios = FindAnyObjectByType<AudioSourcesScript>();
        }

        private void GhostTalkingStageFunc(int round)
        {
            tellingGameObject.SetActive(true);
            PlayOpeningVideo();
            ghostTextContainer.SetTexts(texts[round]);
            StartCoroutine(waitThenWait());
        }

        private IEnumerator waitThenWait()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            DeductionEventPublisher.i.WaitForMouseClick(this, null);
        }

        private void PlayerThinkingStageFunc(int round)
        {
            tellingGameObject.SetActive(false);
            thinkingGameObject.SetActive(true);
            ghostTextContainer.DeleteTexts();
            PlayThinkingVideo();
            InstantiateCluesFromInventory();
            asc.SetInfo(neededClues[round].GetComponent<SetOfClues>().GetInfo().GetID());
        }

        private void ResultGoodStageFunc(int round)
        {
            thinkingGameObject.SetActive(false);
            DeleteInstantiatedClues();
            if (round != texts.Count - 1) PlayTalkVideo();
            else PlaySuccessVideo();
            dialogueBox.WriteTextFunc(continuations[round], 0.3f);
            UIEventPublisher.i.onDialogueBoxDeactivating += MoveSmoothly;
        }

        private void MoveSmoothly(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= MoveSmoothly;
            DeductionEventPublisher.i.SceneMoving(this, null);
        }

        private void ResultBadStageFunc(int round)
        {
            thinkingGameObject.SetActive(false);
            DeleteInstantiatedClues();
            PlayFailVideo();
            dialogueBox.WriteTextFunc(breaking, 0.3f);
            UIEventPublisher.i.onDialogueBoxDeactivating += MoveRoughly;
        }
        private void MoveRoughly(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= MoveRoughly;
            DeductionEventPublisher.i.SceneMoving(this, null, true);
        }

        public void LoadStage(DeductionUIScript.Stage stage, int round)
        {
            switch (stage)
            {
                case DeductionUIScript.Stage.GHOST_TALKING:
                    GhostTalkingStageFunc(round);
                    break;
                case DeductionUIScript.Stage.PLAYER_THINKING:
                    PlayerThinkingStageFunc(round);
                    break;
                case DeductionUIScript.Stage.RESULT_GOOD:
                    audios.PlayUI(right);
                    ResultGoodStageFunc(round);
                    break;
                case DeductionUIScript.Stage.RESULT_BAD:
                    audios.PlayUI(wrong);
                    ResultBadStageFunc(round);
                    break;
            }
        }

    }
}