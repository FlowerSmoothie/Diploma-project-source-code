using System;
using UnityEngine;

namespace Utils.Classes
{

    [Serializable]
    public class TextUnit
    {
        [SerializeField] string textEng;
        [SerializeField] string textBel;
        [SerializeField] string textRus;

        public TextUnit(string textEng, string textBel, string textRus)
        {

            this.textEng = textEng;
            this.textBel = textBel;
            this.textRus = textRus;
        }

        public string GetTextEng() { return textEng; }
        public string GetTextBel() { return textBel; }
        public string GetTextRus() { return textRus; }
    }
}