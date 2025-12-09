using TMPro;
using UnityEngine;
using Utils;

namespace Misc.UI
{
    public class SelectableOptionScript : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private TextMeshProUGUI textMeshPro;

        private int optionNumber;

        private void Start()
        {
            //animator = GetComponent<Animator>();
        }

        public void OnClick()
        {
            SelectableUIPublisher.i.DoOptionChoosing(this, null, optionNumber);
            UIEventPublisher.i.DoChoicesMenuDeactivating(this, null);
            UIEventPublisher.i.DoUIDeactivating(this, null);
            ShowHide();
        }

        public void SetEverything(int option, string text)
        {
            optionNumber = option;
            textMeshPro.text = text;
        } 

        public int GetOption() { return optionNumber; }

        public void ShowHide()
        {
            //animator.SetTrigger(Consts.DEFAULT_TRIGGER_CONST);
        }
        //public string GetText() { return TextUnitUtils.GetTextUnitText(text); }
    }
}