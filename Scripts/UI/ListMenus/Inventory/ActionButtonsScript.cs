using UnityEngine;

namespace Misc.UI.Inventory
{
public class ActionButtonsScript : MonoBehaviour
{
    [SerializeField] GameObject examineButton;
    [SerializeField] ExamineButtonScript examineButtonScript;
    [SerializeField] GameObject useButton;
    [SerializeField] UseButtonScript useButtonScript;
    [SerializeField] GameObject combineButton;
    [SerializeField] CombineButtonScript combineButtonScript;
    [SerializeField] GameObject decombineButton;
    [SerializeField] DecombineButtonScript decombineButtonScript;
    [SerializeField] GameObject readButton;
    [SerializeField] ReadButtonScript readButtonScript;


    public void HideAllButtons()
    {
        examineButton.SetActive(false);
        useButton.SetActive(false);
        combineButton.SetActive(false);
        decombineButton.SetActive(false);
        readButton.SetActive(false);
    }


    public void ShowExamineButton()
    {
        examineButton.SetActive(true);
        //examineButtonScript.UnShow();
    }

    public void HideExamineButton()
    {
        examineButton.SetActive(false);
        //examineButtonScript.UnShow();
    }

    public void ShowUseButton()
    {
        useButton.SetActive(true);
        //examineButtonScript.UnShow();
    }

    public void HideUseButton()
    {
        useButton.SetActive(false);
        //examineButtonScript.UnShow();
    }

    public void ShowCombineButton()
    {
        combineButton.SetActive(true);
        //examineButtonScript.UnShow();
    }

    public void HideCombineButton()
    {
        combineButton.SetActive(false);
        //examineButtonScript.UnShow();
    }

    public void ShowDecombineButton()
    {
        decombineButton.SetActive(true);
        //examineButtonScript.UnShow();
    }

    public void HideDecombineButton()
    {
        decombineButton.SetActive(false);
        //examineButtonScript.UnShow();
    }

    public void ShowReadButton()
    {
        readButton.SetActive(true);
        //examineButtonScript.UnShow();
    }

    public void HideReadButton()
    {
        readButton.SetActive(false);
        //examineButtonScript.UnShow();
    }
}
}