using Misc;
using UnityEngine;
using Utils;

public class LoadingDataScript : MonoBehaviour
{
    [SerializeField] private AudioClip loadingAudio;

    private void Start()
    {
        DataHolderScript dhs = FindAnyObjectByType<DataHolderScript>();
        dhs.LoadData();

        FindAnyObjectByType<AudioSourcesScript>().PlayUI(loadingAudio);

        GetComponent<SceneSwitcher>().LoadScene(dhs.GetScene());   
    }

}