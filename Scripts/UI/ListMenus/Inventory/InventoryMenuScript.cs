using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Overworld.Items;
using Overworld.Items.Containers;
using UI.Prefabs;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Misc.UI.Inventory
{
    public class InventoryMenuScript : UIMenuScript
    {

        private PlayerInventoryScript playerInventory;

        private List<PlainObjectInInventoryInfoContainer> objectsToShow;
        private PlainObjectInInventoryInfoContainer currentObject;

        [SerializeField] private Transform parentForPrefabs;
        [SerializeField] private GameObject prefabToInstantiate;


        [Space]

        [SerializeField] private Image imageShower;
        [SerializeField] private ActionButtonsScript buttonsControllingScript;

        bool active;

        public PlainObjectInInventoryInfoContainer GetCurrentObject() { return currentObject; }


        private void Start()
        {
            UIEventPublisher.i.onInventoryMenuDeActivating += ActivateMenu;
            objectsToShow = new List<PlainObjectInInventoryInfoContainer>();

            active = false;

            playerInventory = FindAnyObjectByType<PlayerInventoryScript>();
        }

        protected override void ActivateMenu(object sender, System.EventArgs e)
        {
            active = !active;
            animator.SetTrigger(Consts.DEFAULT_TRIGGER_CONST);

            if (active)
            {
                LoadObjects(playerInventory.GetItemsData());

                ObjectInListEventPublisher.i.onButonClicking += ReactOnClicking;
            }
            else
            {
                ClearMenu();

                ObjectInListEventPublisher.i.onButonClicking -= ReactOnClicking;
            }
        }

        public void RefreshList()
        {
            LoadObjects(playerInventory.GetItemsData());
            buttonsControllingScript.HideAllButtons();
            currentObject = null;
        }

        private void ReactOnClicking(object sender, System.EventArgs e, int ID)
        {
            Debug.Log(objectsToShow.Count + " " + ID);
            PlainObjectInInventoryInfoContainer obj = objectsToShow[ID];
            currentObject = obj;
            ShowObject(obj);
        }

        private void ShowObject(PlainObjectInInventoryInfoContainer obj)
        {
            buttonsControllingScript.HideAllButtons();

            imageShower.sprite = obj.GetIcon();
            imageShower.gameObject.SetActive(true);
            buttonsControllingScript.ShowExamineButton();
            if (obj.isCombinable) buttonsControllingScript.ShowCombineButton();
            if (obj.IsUsable()) buttonsControllingScript.ShowUseButton();
            if (obj.IsComposite()) buttonsControllingScript.ShowDecombineButton();
            if (obj.IsNote()) buttonsControllingScript.ShowReadButton();
        }

        private void ClearObjects()
        {
            objectsToShow.Clear();
            foreach (Transform child in parentForPrefabs)
            {
                Destroy(child.gameObject);
            }
        }

        private void ClearMenu()
        {
            buttonsControllingScript.HideAllButtons();
            imageShower.gameObject.SetActive(false);
            ClearObjects();
        }

        private void LoadObjects(List<PlainObjectInInventoryInfoContainer> objectsToShow)
        {
            ClearMenu();
            int health = FindAnyObjectByType<PlayerHealthScript>().GetAmount();

            int j = 0;
            for (int i = 0; i < objectsToShow.Count; i++)
            {
                if (objectsToShow[i].isVisible == false) continue;
                if (!objectsToShow[i].IsMedicine())
                {
                    ObjectInInventoryInfoContainer obj = (ObjectInInventoryInfoContainer)objectsToShow[i];
                    obj.UpdateInformation(health);
                }
                else
                {
                    MedicineItemContainer med = (MedicineItemContainer)objectsToShow[i];
                    med.UpdateData();
                }
                AddItemToList(objectsToShow[i], j);
                j++;
            }
        }

        private void AddItemToList(PlainObjectInInventoryInfoContainer itemToAdd, int ID)
        {
            GameObject newObject;
            newObject = Instantiate(prefabToInstantiate, parentForPrefabs);
            newObject.GetComponent<ObjectInInventory>().SetEverything(ID, itemToAdd.GetName(), itemToAdd.GetIcon());

            objectsToShow.Add(itemToAdd);
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onInventoryMenuDeActivating -= ActivateMenu;
        }
    }
}