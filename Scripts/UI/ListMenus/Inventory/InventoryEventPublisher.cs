using System;

public class InventoryEventPublisher
{
    private static InventoryEventPublisher instance;
        protected InventoryEventPublisher() { }

        public static InventoryEventPublisher i
        {
            get
            {
                if (instance == null) instance = new InventoryEventPublisher();
                return instance;
            }
        }

        public delegate void OnDeletingFromInventory(object sender, EventArgs e, int ID);
        public event OnDeletingFromInventory onDeletingFromInventory;
        public void DeleteFromInventory(object sender, EventArgs e, int ID) => onDeletingFromInventory?.Invoke(sender, e, ID);
}