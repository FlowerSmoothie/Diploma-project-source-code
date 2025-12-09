using EntityUtils.PlayerUtils;
using Overworld.Items;
using Overworld.Items.Containers;
using TMPro;
using UnityEngine;
using Utils.Classes;
using Utils.Craft;
using Utils.Text;

namespace Misc.UI.Inventory
{

    public class CombineButtonScript : ActionButtonScript
    {
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] private GameObject closeButton;
        private bool isCombining;
        private InventoryMenuScript inventory;
        private PlayerInventoryScript playerInventory;
        private ItemDeCombiningScript kitchenCombine;

        private PlainObjectInInventoryInfoContainer infoFirst;
        private PlainObjectInInventoryInfoContainer infoSecond;

        private void Start()
        {
            kitchenCombine = FindAnyObjectByType<ItemDeCombiningScript>();
            inventory = FindAnyObjectByType<InventoryMenuScript>();
            playerInventory = FindAnyObjectByType<PlayerInventoryScript>();
            isCombining = true;
        }

        public void Refresh()
        {
            ObjectInListEventPublisher.i.onButonClicking -= Merge;
            infoFirst = null;
            isCombining = true;
            text.text = TextUnitUtils.GetTextUnitText(TextUnitList.COMBINE_IN_LIST_MENU);
            closeButton.SetActive(true);
        }

        protected override void DoOnClick()
        {
            if (isCombining)
            {
                isCombining = false;
                closeButton.SetActive(false);
                infoFirst = (ObjectInInventoryInfoContainer)inventory.GetCurrentObject();

                text.text = TextUnitUtils.GetTextUnitText(TextUnitList.CANCEL_CHOOSING_INTERACTABLE_OBJECT_MENU);

                ObjectInListEventPublisher.i.onButonClicking += Merge;
            }
            else
            {
                Refresh();
            }
        }

        private void OnDestroy()
        {
            ObjectInListEventPublisher.i.onButonClicking -= Merge;
        }

        private void Merge(object sender, System.EventArgs e, int ID)
        {
            infoSecond = inventory.GetCurrentObject();
            inventory.GetCurrentObject();

            PlainObjectInInventoryInfoContainer newObject = kitchenCombine.Craft(infoFirst, infoSecond);

            if (newObject != null)
            {
                playerInventory.AddToInventory(newObject);
                playerInventory.DeleteFromInventory(infoFirst);
                playerInventory.DeleteFromInventory(infoSecond);

                inventory.RefreshList();
            }

            Refresh();
        }
    }
}