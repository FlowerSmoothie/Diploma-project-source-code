using System;

namespace Overworld.ObjectsAndNPCs
{
    [Serializable]
    public class DoorData
    {
        public int ID;
        public bool state;

        public DoorData(int ID, bool state)
        {
            this.ID = ID;
            this.state = state;
        }
    }
}