using Utils.Classes;

namespace Utils.Text
{
    public static class TextUnitList
    {
        public static TextUnit CANCEL_CHOOSING_INTERACTABLE_OBJECT_MENU = new TextUnit("Cancel", "Скасаваць", "Отмена");
        public static TextUnit COMBINE_IN_LIST_MENU = new TextUnit("Combine", "Аб'яднаць", "Объединить");
    }
    public static class DefaultObjectsList
    {
        public static TextUnit DOOR = new TextUnit("Door", "Дзверы", "Дверь");
        public static TextUnit STAIRS = new TextUnit("Stairs", "Лесвіца", "Лестница");
    }

    public static class PhrasesList
    {
        public static PhraseToSay CANNOT_USE_THE_ITEM_HERE = new PhraseToSay(General.Character.NONE, "Cannot use it here now.", "Немагчыма выкарыстаць гэта тут.", "Невозможно использовать это тут.");
        public static PhraseToSay CANNOT_USE_THE_DOOR = new PhraseToSay(General.Character.NONE, "The door won't budge.", "Дзверы не паддаюцца.", "Дверь не поддаётся.");
        public static PhraseToSay OPENING_THE_DOOR = new PhraseToSay(General.Character.NONE, "Chill's coming from behind the door...", "З-за дзвярэй цягнецца холад...", "Из-за двери тянется холод...");
        public static PhraseToSay DOWN_THE_STAIRS = new PhraseToSay(General.Character.NONE, "Chill§s coming from the other side...", "З таго боку цягнецца холад...", "С той стороны тянется холод...");
        public static PhraseToSay SAVE_GAME_OPTIONS = new PhraseToSay(General.Character.NONE, "Press E to save the game, or Esc to cancel saving.", "Націсніце Е, каб захаваць гульню, ці Esc, каб скасаваць захаванне.", "Нажмите E, чтобы сохранить игру, или Esc, чтобы отменить сохранение.");
        public static PhraseToSay NEW_GAME_OPTIONS = new PhraseToSay(General.Character.NONE, "Save file found! Press E to delete it and start a new game, or Esc to undo.", "Выяўлены файл захавання! Націсніце E, каб сцерці яго і пачаць новую гульню, ці Esc, каб скасаваць дзеянне.", "Обнаружен файл сохранения! Нажмите E, чтобы стереть его и начать новую игру, или Esc, чтобы отменить действие.");
    }

}