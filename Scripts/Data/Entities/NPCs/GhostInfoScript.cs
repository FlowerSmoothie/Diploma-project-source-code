using General;
using UnityEngine;

namespace EntityUtils.NPCUtils
{
    public class GhostInfoScript : MonoBehaviour
    {
        [SerializeField] Character character;

        public Character GetCharacter() { return character; }
    }
}