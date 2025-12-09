using System.Collections.Generic;
using Overworld.Items.Containers;
using Utils.Classes;

public interface PuttableInInventory
{
    public PlainObjectInInventoryInfoContainer GetItemInfo();



    public bool IsUsable();
    public bool IsComposite();
    public virtual bool IsNote() { return false; }
    public virtual bool IsMedicine() { return false; }


    public void SetID(int ID);


    public void SetBorders(int great, int normal);
}