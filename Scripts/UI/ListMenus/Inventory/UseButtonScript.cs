using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Overworld.Items.Containers;
using Overworld.ObjectsAndNPCs;
using UnityEngine;
using Utils.Classes;
using Utils.Text;

namespace Misc.UI.Inventory
{

    public class UseButtonScript : ActionButtonScript
    {
        [SerializeField] InventoryMenuScript inventoryMenuScript;
        [SerializeField] PlayerInteractionScript playerInteractionScript;

        private DialogueBoxScript dialogueBox;

        private void Start()
        {
            dialogueBox = FindAnyObjectByType<DialogueBoxScript>();
            playerInteractionScript = FindAnyObjectByType<PlayerInteractionScript>();
        }

        protected override void DoOnClick()
        {
            PlainObjectInInventoryInfoContainer plainInfo = inventoryMenuScript.GetCurrentObject();
            if (plainInfo.IsMedicine())
            {
                MedicineItemContainer med = (MedicineItemContainer)plainInfo;
                med.TakeMedicine();

                GetComponent<HideUIScript>().HideUI();
                GetComponent<CloseInventoryScript>().CloseUI();
                return;
            }

            if(plainInfo.GetID() == 36)
            {
                FindAnyObjectByType<PlayerHealthScript>().Kill();
                return;
            }

            List<GameObject> interactableObjects = playerInteractionScript.GetListOfInteractableObjects();


            if (interactableObjects == null || interactableObjects.Count == 0)
            {
                dialogueBox.WriteTextFunc(PhrasesList.CANNOT_USE_THE_ITEM_HERE, 0);
                return;
            }

            ObjectInInventoryInfoContainer objectInfo = (ObjectInInventoryInfoContainer)plainInfo;

            foreach (GameObject go in interactableObjects)
            {
                if (!go.GetComponent<Interactable>().CanItemsBeUsedOnIt()) continue;

                InteractableWithObject obj = go.GetComponent<InteractableWithObject>();
                if (!obj.IsThisPuzzle())
                {
                    List<PhraseToSay> list = obj.TryToInteractWithAnObject(objectInfo.GetID());

                    if (list == null || list.Count == 0) continue;
                    dialogueBox.WriteTextFunc(list, 0);

                    GetComponent<HideUIScript>().HideUI();
                    GetComponent<CloseInventoryScript>().CloseUI();
                }
                else
                {
                    List<PhraseToSay> list = obj.TryToInteractWithAnObject(objectInfo.GetID());

                    //Debug.Log("List:" + list[0].GetText().GetTextEng());

                    if (list == null || list.Count == 0)
                    {
                        continue;
                    }
                    dialogueBox.WriteTextFunc(list, 0);

                    GetComponent<HideUIScript>().HideUI();
                    GetComponent<CloseInventoryScript>().CloseUI();
                }
                return;
            }

            dialogueBox.WriteTextFunc(PhrasesList.CANNOT_USE_THE_ITEM_HERE, 0);
        }

        private void InteractWithNotPuzzle()
        {

        }

        private void InteractWithPuzzle()
        {

        }
    }
}