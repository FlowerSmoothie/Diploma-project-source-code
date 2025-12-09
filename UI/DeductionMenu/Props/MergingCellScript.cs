using UnityEngine;
using UnityEngine.EventSystems;
using Utils.Craft;

namespace Misc.UI.Deduction
{
    public class MergingCellScript : ClueCellScript
    {

        [SerializeField] private AssumptionSolvingChecker asc;
        private ClueDeCombiningScript kitchenCombine;

        private GameObject child;

        private void Start()
        {
            //asc = FindAnyObjectByType<AssumptionSolvingChecker>();
            kitchenCombine = FindAnyObjectByType<ClueDeCombiningScript>();
        }

        private void Clear()
        {
            foreach (Transform child in gameObject.transform)
            {
                Destroy(child.gameObject, 0.3f);
            }
        }

        public override void OnDrop(PointerEventData eventData)
        {
            if (transform.childCount == 0)
            {
                eventData.pointerDrag.GetComponent<DragDropClueScript>().parentAfterDrag = transform;
                child = eventData.pointerDrag.gameObject;
            }
            else if (transform.childCount == 1)
            {
                DeductionEventPublisher.i.SceneMoving(this, null, !asc.CheckIfOkay(kitchenCombine.Craft(child.GetComponent<ClueInDeductionScript>().GetData(), eventData.pointerDrag.GetComponent<ClueInDeductionScript>().GetData())));
                Clear();
            }
        }


    }
}