using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Utils.Classes;

namespace Misc.UI.Deduction
{
    public class GhostTextContainerScript : MonoBehaviour
    {

        [SerializeField] List<TextMeshProUGUI> ghostTalkingUI;
        [SerializeField] private Animator animator;

        public void SetTexts(DeductionSetOfTexts set)
        {
            Tuple<TextUnit, TextUnit, TextUnit> texts = set.GetTexts();
            ghostTalkingUI[0].text = TextUnitUtils.GetTextUnitText(texts.Item1);
            ghostTalkingUI[1].text = TextUnitUtils.GetTextUnitText(texts.Item2);
            ghostTalkingUI[2].text = TextUnitUtils.GetTextUnitText(texts.Item3);

            animator.SetBool("visible", true);
        }

        public void DeleteTexts()
        {
            animator.SetBool("visible", false);
        }
    }
}