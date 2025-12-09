using UnityEngine;
using UnityEngine.UI;

public class MakeThisButtonUninteractable : MonoBehaviour
{

    [SerializeField] Button anotherButton;

    private Button thisButton;

    private void Start()
    {
        thisButton = GetComponent<Button>();
    }

    public void MakeUninteractable()
    {
        thisButton.interactable = false;
    }

    public void MakeAnotherButtonInteractable()
    {
        anotherButton.interactable = true;
    }
}