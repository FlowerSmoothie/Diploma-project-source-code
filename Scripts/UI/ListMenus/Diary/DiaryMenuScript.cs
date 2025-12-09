using System.Collections.Generic;
using EntityUtils.PlayerUtils;
using Overworld.Clues;
using TMPro;
using UI.Prefabs;
using UnityEngine;
using UnityEngine.UI;
using Utils.Classes;

namespace Misc.UI.Diary
{
    public class DiaryMenuScript : UIMenuScript
    {

        private PlayerCluesListScript playerClues;

        private List<ClueInDiaryInfoContainer> objectsToShow;
        private ClueInDiaryInfoContainer currentObject;

        [SerializeField] private Transform parentForPrefabs;
        [SerializeField] private GameObject prefabToInstantiate;


        [Space]

        [SerializeField] private Image imageShower;
        [SerializeField] private GameObject descHolder;
        [SerializeField] private TextMeshProUGUI descShower;
        [SerializeField] private GameObject markShower;
        [SerializeField] private GameObject saveButton;
        private SaveButtonScript saveButtonScript;


        [Space]
        [SerializeField] private Image bgShower;
        [SerializeField] private Sprite sleepNotSaved;
        [SerializeField] private Sprite activeNotSaved;
        [SerializeField] private Sprite saved;

        bool active;

        bool isSaved;

        public ClueInDiaryInfoContainer GetCurrentObject() { return currentObject; }


        private void Start()
        {
            UIEventPublisher.i.onDiaryMenuDeActivating += ActivateMenu;
            objectsToShow = new List<ClueInDiaryInfoContainer>();

            active = false;

            playerClues = FindAnyObjectByType<PlayerCluesListScript>();

            saveButtonScript = saveButton.GetComponent<SaveButtonScript>();
        }

        protected override void ActivateMenu(object sender, System.EventArgs e)
        {
            active = !active;
            animator.SetTrigger(Utils.Consts.DEFAULT_TRIGGER_CONST);

            if (active)
            {
                LoadObjects(playerClues.GetClues());

                isSaved = FindAnyObjectByType<DataHolderScript>().IsSaved();
                saveButtonScript.SetSaved(isSaved);

                if (!isSaved)
                {
                    SetNotSaved();
                }
                else
                {
                    SetSaved();
                }

                ObjectInListEventPublisher.i.onButonClicking += ReactOnClicking;
            }
            else
            {
                ClearObjects();

                ObjectInListEventPublisher.i.onButonClicking -= ReactOnClicking;
            }
        }

        private void ReactOnClicking(object sender, System.EventArgs e, int ID)
        {
            currentObject = objectsToShow[ID];
            ShowObject(currentObject);
        }

        private void ShowObject(ClueInDiaryInfoContainer obj)
        {
            imageShower.gameObject.SetActive(true);
            imageShower.sprite = obj.GetIcon();

            descHolder.SetActive(true);
            descShower.text = "";
            List<TextUnit> texts = obj.GetDescription();
            foreach (TextUnit text in texts)
            {
                descShower.text += TextUnitUtils.GetTextUnitText(text);
                descShower.text += "\n\n ";
            }
        }

        private void ClearObjects()
        {
            objectsToShow.Clear();
            descHolder.SetActive(false);
            imageShower.gameObject.SetActive(false);
            foreach (Transform child in parentForPrefabs)
            {
                Destroy(child.gameObject);
            }
        }

        private void LoadObjects(List<ClueInDiaryInfoContainer> objectsToShow)
        {
            for (int i = 0; i < objectsToShow.Count; i++)
                AddItemToList(objectsToShow[i], i);
        }

        private void AddItemToList(ClueInDiaryInfoContainer itemToAdd, int ID)
        {
            GameObject newObject;
            newObject = Instantiate(prefabToInstantiate, parentForPrefabs);
            newObject.GetComponent<ClueInDiary>().SetEverything(ID, itemToAdd.GetName(), itemToAdd.GetIcon());

            objectsToShow.Add(itemToAdd);
        }

        public void SetNotSaved()
        {
            SetSleep();
            saveButton.SetActive(true);
            markShower.SetActive(false);
        }

        public void SetSleep()
        {
            bgShower.sprite = sleepNotSaved;
        }

        public void SetActive()
        {
            bgShower.sprite = activeNotSaved;
        }
        public void SetSaved()
        {
            bgShower.sprite = saved;
            saveButton.SetActive(false);
            markShower.SetActive(true);
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onDiaryMenuDeActivating -= ActivateMenu;
        }
    }
}