using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Utils.Classes;

namespace Misc.UI.Deduction
{
    [Serializable]
    public class DeductionUnit
    {
        [SerializeField] public VideoClip openingCilp; //{ get; set; }
        [SerializeField] public VideoClip thinkingClip; //{ get; set; }

        [SerializeField] public VideoClip greatClip; //{ get; set; }
        [SerializeField] public VideoClip failClip; //{ get; set; }

        [SerializeField] public VideoClip talkingClip; //{ get; set; }

        [SerializeField] public List<DeductionSetOfTexts> texts; //{ get; set; }
        [SerializeField] public List<GameObject> neededClues; //{ get; set; }
        [SerializeField] public List<ContinuationUnit> continuations; //{ get; set; }
        [SerializeField] public List<PhraseToSay> breaking; //{ get; set; }


    }
}