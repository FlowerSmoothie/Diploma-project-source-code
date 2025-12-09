using System;
using System.Collections;
using System.Collections.Generic;
using Misc;
using Misc.UI;
using UnityEngine;
using Utils;
using Utils.Classes;

public class PrologueEpilogueScript : MonoBehaviour
{
    private SceneSwitcher sceneSwitcher;
    [SerializeField] List<string> linesToShow;

    [SerializeField] int sceneNumberToGoTo;

    [SerializeField] AudioSource UIaudioSource;

    private List<PhraseToSay> phrases;

    private void Start()
    {
        UIEventPublisher.i.onDialogueBoxDeactivating += OnDialogueBoxFinished;

        //DialogueBoxEventPublisher.i.PhraseWritingFinishing(this, null);

        sceneSwitcher = GetComponent<SceneSwitcher>();

        phrases = new List<PhraseToSay>();
        for (int i = 0; i < linesToShow.Count; i++)
        {
            PhraseToSay p = new PhraseToSay(linesToShow[i]);
            phrases.Add(p);
        }

        //Debug.Log(FindAnyObjectByType<DialogueBoxScript>());

        Debug.Log(phrases);


        FindAnyObjectByType<DialogueBoxScript>().WriteTextFunc(phrases, 1);
    }

    private void OnDialogueBoxFinished(object sender, EventArgs e)
    {
        sceneSwitcher.ChangeScene(sceneNumberToGoTo, sceneNumberToGoTo != 0 ? 3 : 7);

        if (sceneNumberToGoTo == 0)
        {
            DataHolderScript dhs = FindAnyObjectByType<DataHolderScript>();
            dhs.DeleteData();
            dhs.ClearData(true);
        }
    }

    private void OnDestroy()
    {
        UIEventPublisher.i.onDialogueBoxDeactivating -= OnDialogueBoxFinished;
    }
}
