using System;
using General;
using UnityEngine;

namespace Utils.Classes
{

    [Serializable]
    public class PhraseToSay
    {
        [SerializeField] Character character;
        [SerializeField] TextUnit text;
        private string v;

        public PhraseToSay(Character character, string textEng, string textBel, string textRus)
        {
            this.character = character;
            text = new TextUnit(textEng, textBel, textRus);
        }
        public PhraseToSay(string textEng)
        {
            character = Character.NONE;
            text = new TextUnit(textEng, textEng, textEng);
        }

        public GameCharacter GetCharacter() { return CharactersList.GetCharacter(character); }
        public TextUnit GetText() { return text; }
    }
}