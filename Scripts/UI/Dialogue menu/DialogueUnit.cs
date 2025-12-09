using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Utils.Classes;

namespace Misc.UI.Dialogue
{
    [Serializable]
    public class DialogueUnit
    {
        [SerializeField] public List<DialogueLine> lines;
        [SerializeField] public List<PhraseToSay> finalPhrase;
        [SerializeField] public List<PhraseToSay> wrongPhrase;
        [SerializeField] public VideoClip ghostTalkingCilp;
        [SerializeField] public VideoClip margarethThinkingClip;
    }
}