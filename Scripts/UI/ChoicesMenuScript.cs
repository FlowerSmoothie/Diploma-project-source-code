using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Classes;
using Utils.Text;

namespace Misc.UI
{
    public class ChoicesMenuScript : UIMenuScript
    {
        List<GameObject> choices;
        [SerializeField] GameObject optionPrefabsParent;
        [SerializeField] GameObject optionUnitPrefab;

        private void Start()
        {
            UIEventPublisher.i.onChoicesMenuActivating += ActivateMenu;
            UIEventPublisher.i.onChoicesMenuDeactivating += DeactivateMenu;

            PlayerEventPublisher.i.onTryingToInteractWithObects += ShowOptions;
        }

        protected override void ActivateMenu(object sender, EventArgs e)
        {
            animator.SetBool("visible", true);
        }

        private void DeactivateMenu(object sender, EventArgs e)
        {
            animator.SetBool("visible", false);
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onChoicesMenuActivating -= ActivateMenu;
            UIEventPublisher.i.onChoicesMenuDeactivating -= DeactivateMenu;

            PlayerEventPublisher.i.onTryingToInteractWithObects -= ShowOptions;
        }



        private void HideOptions(object sender, EventArgs e, int option)
        {
            foreach (Transform child in optionPrefabsParent.transform)
            {
                Destroy(child.gameObject);
            }

            SelectableUIPublisher.i.onOptionChoosing -= HideOptions;
        }

        private void ShowOptions(object sender, EventArgs e, List<Tuple<string, int>> names, bool needsCancel)
        {
            //List<SelectableOptionScript> list = new List<SelectableOptionScript>();
            choices = new List<GameObject>();
            SelectableOptionScript sos;
            GameObject go;

            foreach (Tuple<string, int> option in names)
            {
                go = Instantiate(optionUnitPrefab, optionPrefabsParent.transform);
                choices.Add(go);
                sos = go.GetComponent<SelectableOptionScript>();
                //list.Add(new SelectableOptionScript(i, names[i]));
                sos.SetEverything(option.Item2, option.Item1);
                sos.ShowHide();
            }

            SelectableUIPublisher.i.onOptionChoosing += HideOptions;

            //sos = Instantiate(optionUnitPrefab, optionPrefabsParent.transform).GetComponent<SelectableOptionScript>();
            //list.Add(new SelectableOptionScript(-1, TextUnitUtils.GetTextUnitText(TextUnitList.CANCEL_CHOOSING_INTERACTABLE_OBJECT_MENU)));
            if (needsCancel)
            {
                go = Instantiate(optionUnitPrefab, optionPrefabsParent.transform);
                sos = go.GetComponent<SelectableOptionScript>();
                sos.SetEverything(-1, TextUnitUtils.GetTextUnitText(TextUnitList.CANCEL_CHOOSING_INTERACTABLE_OBJECT_MENU));
                sos.ShowHide();
            }

        }


    }
}