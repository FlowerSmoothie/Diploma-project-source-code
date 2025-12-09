
using UnityEngine;
using Utils.Text;

namespace Utils.Classes
{
    public static class CharacterUtils
    {
        public static string GetCharacterNickName(GameCharacter character)
        {
            switch(PlayerPrefs.GetInt("language"))
            {
                case MainVariables.ENGLISH:
                return character.GetNicknameEng();
                case MainVariables.RUSSIAN:
                return character.GetNicknameRus();
                case MainVariables.BELARUSIAN:
                return character.GetNicknameBel();
                default:
                return null;
            }
        }

        public static TextUnit GetCharacterNickNameAsTextUnit(GameCharacter character)
        {
            return new TextUnit(character.GetNicknameEng(), character.GetNicknameBel(), character.GetNicknameRus());
        }
    }

    public static class TextUnitUtils
    {
        public static string GetTextUnitText(TextUnit text)
        {
            switch(PlayerPrefs.GetInt("language"))
            {
                case MainVariables.ENGLISH:
                return text.GetTextEng();
                case MainVariables.RUSSIAN:
                return text.GetTextRus();
                case MainVariables.BELARUSIAN:
                return text.GetTextBel();
                default:
                return null;
            }
        }
    }
}
