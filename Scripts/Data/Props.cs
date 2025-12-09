using Utils.Classes;


namespace General
{
    public static class CharactersList
    {
        private static readonly GameCharacter NONE = new GameCharacter("-", "-");


        private static readonly GameCharacter MargarethWeiss = new GameCharacter("Маргарэт", "Margareth", "Маргарет", "Маргарэт Вайф", "Margareth Weiss", "Маргарет Вайс");
        private static readonly GameCharacter JaneFox = new GameCharacter("Джэйн", "Jane", "Джейн", "Джэйн Фокс", "Jane Fox", "Джейн Фокс");
        private static readonly GameCharacter LilySchmidt = new GameCharacter("Лілі", "Lily", "Лили", "Лілі Шмідт", "Lily Schmidt", "Лили Шмидт");
        private static readonly GameCharacter CharlotteBrockhoven = new GameCharacter("Шарлотта", "Charlotte", "Шарлотта", "Шарлотта Брукховэн", "Charlotte Brockhoven", "Шарлотта Брукховен");
        private static readonly GameCharacter LudwigReichmann = new GameCharacter("Людвіг", "Ludwig", "Людвиг", "Людвіг Рэйхман", "Ludwig Reichmann", "Людвиг Рейхманн");
        private static readonly GameCharacter Griffith = new GameCharacter("Грыффіт", "Griffith", "Гриффит", "Грыффіт Берсерк", "Griffith berserk", "Гриффит Берсерк");


        public static GameCharacter GetCharacter(Character character)
        {
            switch(character)
            {
                case Character.MargarethWeiss:
                return MargarethWeiss;
                case Character.JaneFox:
                return JaneFox;
                case Character.LilySchmidt:
                return LilySchmidt;
                case Character.CharlotteBrockhoven:
                return CharlotteBrockhoven;
                case Character.LudwigReichmann:
                return LudwigReichmann;
                case Character.Griffith:
                return Griffith;
                default:
                return NONE;
            }
        }
    }

    public enum Character
    {
        NONE, MargarethWeiss, JaneFox, LilySchmidt, CharlotteBrockhoven, LudwigReichmann, Griffith
    };

    

    public static class UtilityPhrases
    {
        public static readonly PhraseToSay CannotUseTheItemHereDefault = new PhraseToSay(Character.MargarethWeiss, "I can't use this item here.", "Я не магу выкарыстаць гэты прадмет тут.", "Я не могу использовать этот предмет тут.");
        public static readonly PhraseToSay CannotCombineItemsDefault = new PhraseToSay(Character.MargarethWeiss, "I can't combine these items.", "Я не магу паяднаць гэтыя прадметы.", "Я не могу объединить эти предметы.");
    }

    


}
