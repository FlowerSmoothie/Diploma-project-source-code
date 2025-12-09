using EntityUtils.PlayerUtils;
using Overworld.Clues;
using Overworld.Items.Containers;
using UnityEngine;

namespace Misc.UI.Inventory
{


    public class ExamineButtonScript : ActionButtonScript
    {
        InventoryMenuScript inventoryScript;
        ItemShowingUIScript itemShowing;
        DialogueBoxScript dialogueBoxScript;

        PlayerCluesListScript clues;
        PlayerHealthScript healthScript;

        private void Start()
        {
            inventoryScript = FindAnyObjectByType<InventoryMenuScript>();
            itemShowing = FindAnyObjectByType<ItemShowingUIScript>();
            dialogueBoxScript = FindAnyObjectByType<DialogueBoxScript>();
            clues = FindAnyObjectByType<PlayerCluesListScript>();
            healthScript = FindAnyObjectByType<PlayerHealthScript>();
        }

        protected override void DoOnClick()
        {
            PlainObjectInInventoryInfoContainer obj = inventoryScript.GetCurrentObject();

            itemShowing.SetImage(obj.GetIcon());
            dialogueBoxScript.WriteTextFunc(obj.GetDescription(), 0.5f);

            if (!obj.IsMedicine())
            {
                ObjectInInventoryInfoContainer o = (ObjectInInventoryInfoContainer)obj;
                ClueInDiaryInfoContainer clue = o.GetClue(healthScript.GetAmount());
                if (clue != null) clues.AddToClueList(clue);
            }
        }

    }
}