using System;

namespace Misc.Overworld
{
    public class OverworldEventPublisher
    {
        private static OverworldEventPublisher instance;
        protected OverworldEventPublisher() { }

        public static OverworldEventPublisher i
        {
            get
            {
                if (instance == null) instance = new OverworldEventPublisher();
                return instance;
            }
        }

        public delegate void OnItemPickup(object sender, EventArgs e);
        public event OnItemPickup onItemPickup;
        public void DoOnItemPickup(object sender, EventArgs e) => onItemPickup?.Invoke(sender, e);

        public delegate void OnDoorUnLocked(object sender, EventArgs e, int doorID, bool isLockedNow);
        public event OnDoorUnLocked onDoorUnLocked;
        public void DoorUnLocked(object sender, EventArgs e, int doorID, bool isLockedNow) => onDoorUnLocked?.Invoke(sender, e, doorID, isLockedNow);

        public delegate void OnSavingData(object sender, EventArgs e);
        public event OnSavingData onSavingData;
        public void SaveData(object sender, EventArgs e) => onSavingData?.Invoke(sender, e);



    }
}