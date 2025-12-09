using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Classes;

namespace Misc.UI.Dialogue
{
    [Serializable]
    public class DialogueLine
    {
        [SerializeField] private List<PhraseToSay> talking;
        [SerializeField] private PhraseToSay playerQuestion;
        [SerializeField] private List<TextUnit> wrongContinuations;
        [SerializeField] private TextUnit rightContinuation;

        public List<PhraseToSay> GetTalkingLines() { return talking; }
        public PhraseToSay GetPlayerQuestion() { return playerQuestion; }
        public List<TextUnit> GetAnswersWrong() { return wrongContinuations; }
        public TextUnit GetAnswerRight() { return rightContinuation; }
    }
}