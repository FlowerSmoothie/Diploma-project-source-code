
namespace Utils.Classes
{
    public class GameCharacter
    {
        private string nameDBBel;
        private string nameDBEng;
        private string nameDBRus;

        private string fullNameBel;
        private string fullNameEng;
        private string fullNameRus;

        public string GetNicknameBel() { return nameDBBel; }
        public string GetNicknameEng() { return nameDBEng; }
        public string GetNicknameRus() { return nameDBRus; }


        public string GetFullNameBel() { return fullNameBel; }
        public string GetFullNameEng() { return fullNameEng; }
        public string GetFullNameRus() { return fullNameRus; }

        public GameCharacter(string nameDBBel, string nameDBEng, string nameDBRus, string fullNameBel, string fullNameEng, string fullNameRus)
        {
            this.nameDBBel = nameDBBel;
            this.nameDBEng = nameDBEng;
            this.nameDBRus = nameDBRus;
            
            this.fullNameBel = fullNameBel;
            this.fullNameEng = fullNameEng;
            this.fullNameRus = fullNameRus;
        }

        public GameCharacter(string nameDBInAllLanguages, string fullNameInAllLanguages)
        {
            nameDBBel = nameDBInAllLanguages;
            nameDBEng = nameDBInAllLanguages;
            nameDBRus = nameDBInAllLanguages;
            
            fullNameBel = fullNameInAllLanguages;
            fullNameEng = fullNameInAllLanguages;
            fullNameRus = fullNameInAllLanguages;
        }
    }

}