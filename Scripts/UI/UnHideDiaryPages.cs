using UnityEngine;
using Utils;

public class UnHideDiaryPages : MonoBehaviour
{
    [SerializeField] Animator evidences;
    [SerializeField] Animator tasks;

    public void ShowEvidences()
    {
        evidences.SetBool("fadeIn", true);
        tasks.SetBool("fadeIn", false);
    }

    public void ShowTasks()
    {
        tasks.SetBool("fadeIn", true);
        evidences.SetBool("fadeIn", false);
    }
}