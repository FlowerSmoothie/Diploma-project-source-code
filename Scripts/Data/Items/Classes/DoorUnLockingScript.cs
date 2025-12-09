using Misc;
using Misc.Overworld;
using UnityEngine;

namespace Overworld.Utils
{
    public class DoorUnLockingScript : MonoBehaviour
    {
        public void UnlockDoor(int ID)
        {
            OverworldEventPublisher.i.DoorUnLocked(this, null, ID, false);
        }
    }
}