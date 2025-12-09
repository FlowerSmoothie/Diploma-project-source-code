using System;
using System.Collections;
using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Overworld.Clues;
using UnityEngine;
using UnityEngine.Video;
using Utils.Classes;

namespace Misc.UI.Dialogue
{
    public class DialogueDataLoader : MonoBehaviour
    {
        private VideoClip ghostTalkingCilp;
        private VideoClip margarethThinkingClip;
        private void SetVideos(VideoClip ghostTalkingCilp, VideoClip margarethThinkingClip)
        {
            this.ghostTalkingCilp = ghostTalkingCilp;
            this.margarethThinkingClip = margarethThinkingClip;
        }

        private List<DialogueLine> texts;
        private List<PhraseToSay> wrongPhrase;
        private List<PhraseToSay> finalPhrase;
        private void SetRounds(List<DialogueLine> texts, List<PhraseToSay> finalPhrase, List<PhraseToSay> wrongPhrase)
        {
            this.texts = texts;
            this.finalPhrase = finalPhrase;
            this.wrongPhrase = wrongPhrase;
        }

        public void SetAll(DialogueUnit data)
        {
            SetVideos(data.ghostTalkingCilp, data.margarethThinkingClip);
            SetRounds(data.lines, data.finalPhrase, data.wrongPhrase);
        }

        private DialogueBoxScript dialogueBox;

        private DialogueMenu menu;


        [SerializeField] VideoPlayer animationPlayer;

        [SerializeField] private AudioClip right;
        [SerializeField] private AudioClip wrong;
        private AudioSourcesScript audios;


        private void PlayOpeningVideo() { animationPlayer.clip = ghostTalkingCilp; /*animationPlayer.isLooping = false;*/ }
        public void PlayThinkingVideo() { animationPlayer.clip = margarethThinkingClip; /*animationPlayer.isLooping = false;*/ }


        private void Start()
        {
            dialogueBox = FindAnyObjectByType<DialogueBoxScript>();
            menu = GetComponent<DialogueMenu>();

            audios = FindAnyObjectByType<AudioSourcesScript>();
        }

        private int round;
        public void StartRound(int round)
        {
            this.round = round;
            GhostTalkingStageFunc();
        }


        private void GhostTalkingStageFunc()
        {
            PlayOpeningVideo();
            dialogueBox.WriteTextFunc(texts[round].GetTalkingLines(), 0.25f);

            UIEventPublisher.i.onDialogueBoxDeactivating += PlayerTalkingStageFunc;
        }

        public List<T> Shuffle<T>(List<T> list)
        {
            System.Random rng = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }
        private List<TextUnit> textsToAnswer;
        private void PlayerTalkingStageFunc(object sender, EventArgs e)
        {
            UIEventPublisher.i.onDialogueBoxDeactivating -= PlayerTalkingStageFunc;

            PlayThinkingVideo();

            dialogueBox.SelfClosingDeactivate();
            dialogueBox.WriteTextFunc(texts[round].GetPlayerQuestion(), 0.25f);

            textsToAnswer = new List<TextUnit>();
            textsToAnswer.AddRange(texts[round].GetAnswersWrong());
            textsToAnswer.Add(texts[round].GetAnswerRight());
            textsToAnswer = Shuffle(textsToAnswer);

            List<Tuple<string, int>> strings = new List<Tuple<string, int>>();
            for (int i = 0; i < textsToAnswer.Count; i++)
            {
                strings.Add(new Tuple<string, int>(TextUnitUtils.GetTextUnitText(textsToAnswer[i]), i));
            }

            PlayerEventPublisher.i.DoOnTryingToInteractWithObects(this, null, strings, false);
            UIEventPublisher.i.DoChoicesMenuActivating(this, null);

            SelectableUIPublisher.i.onOptionChoosing += AnalyzeAnswer;
        }

        private void AnalyzeAnswer(object sender, EventArgs e, int option)
        {
            SelectableUIPublisher.i.onOptionChoosing -= AnalyzeAnswer;
            UIEventPublisher.i.DoDialogueBoxDeactivating(this, null);
            menu.Move(TextUnitUtils.GetTextUnitText(textsToAnswer[option]) == TextUnitUtils.GetTextUnitText(texts[round].GetAnswerRight()) ? true : false);
            textsToAnswer.Clear();
        }

        public void WrongAnswerShow()
        {
            PlayOpeningVideo();

            audios.PlayUI(wrong);
            
            FindAnyObjectByType<DialogueBoxScript>().SelfClosingActivate();
            dialogueBox.WriteTextFunc(wrongPhrase, 0.5f);
        }

        public void FinalPhraseShow()
        {
            PlayOpeningVideo();
            
            FindAnyObjectByType<DialogueBoxScript>().SelfClosingActivate();
            dialogueBox.WriteTextFunc(finalPhrase, 0.5f);
        }

    }
}