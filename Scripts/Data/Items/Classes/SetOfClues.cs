using ItemUtils;
using UnityEngine;

namespace Overworld.Clues
{
    public class SetOfClues : MonoBehaviour
    {
        [SerializeField] SetOfCluesInfo info;

        public ClueInDiaryInfoContainer GetInfo(int health) { return info.GetInfo(health); }
        public ClueInDiaryInfoContainer GetInfo(ItemStates state)
        {
            switch(state)
            {
                case ItemStates.PERFECT:
                return info.greatMental;
                case ItemStates.GOOD:
                return info.normalMental;
                case ItemStates.BAD:
                return info.badMental;
            }
            return null;
        }
        public SetOfCluesInfo GetInfo() { return info; }
        public SetOfCluesInfo GetSet() { return info; }
    }
}