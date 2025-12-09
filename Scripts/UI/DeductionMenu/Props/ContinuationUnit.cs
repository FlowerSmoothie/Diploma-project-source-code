using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Classes;

namespace Misc.UI.Deduction
{
    [Serializable]
    public class ContinuationUnit
    {
        /*[SerializeField] private TextUnit first;
        [SerializeField] private TextUnit second;
        [SerializeField] private TextUnit third;

        public Tuple<TextUnit, TextUnit, TextUnit> GetTexts()
        {
            return new Tuple<TextUnit, TextUnit, TextUnit>(first, second, third);
        }*/

        [SerializeField] private List<PhraseToSay> text;

        public List<PhraseToSay> GetTexts()
        {
            return text;
        }
    }
}