using System;
using TMPro;
using UnityEngine;
using Utils;

namespace Misc.UI.Deduction
{
    public class ClueNamePanelScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Animator animator;

        private void Start()
        {
            DeductionEventPublisher.i.onClueHover += ActivateThis;
            DeductionEventPublisher.i.onClueDehover += DeactivateThis;
        }

        private void OnDestroy()
        {
            DeductionEventPublisher.i.onClueHover -= ActivateThis;
            DeductionEventPublisher.i.onClueDehover -= DeactivateThis;
        }

        private void ActivateThis(object sender, EventArgs e, string name)
        {
            text.text = name;
            animator.SetBool("active", true);
        }

        private void DeactivateThis(object sender, EventArgs e)
        {
            animator.SetBool("active", false);
        }
    }
}