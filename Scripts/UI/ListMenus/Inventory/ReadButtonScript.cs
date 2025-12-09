using EntityUtils.PlayerUtils;
using Overworld.Items;
using Overworld.Items.Containers;
using UnityEngine;

namespace Misc.UI.Inventory
{
    public class ReadButtonScript : ActionButtonScript
    {
        private NoteReadingUI noteReadingUI; 
        private InventoryMenuScript inventory;
        private PlayerHealthScript healthScript;

        private void Start()
        {
            noteReadingUI = FindAnyObjectByType<NoteReadingUI>();
            inventory = FindAnyObjectByType<InventoryMenuScript>();
            healthScript = FindAnyObjectByType<PlayerHealthScript>();
        }

        protected override void DoOnClick()
        {
            NoteInInventoryInfoContainer note = (NoteInInventoryInfoContainer)inventory.GetCurrentObject();
            noteReadingUI.WriteTextFunc(note.GetName(), note.Read(healthScript.GetAmount()));
        }
    }
}