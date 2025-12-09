using UnityEngine;

namespace Misc.UI
{
    public class SoundClickPlaying : MonoBehaviour
    {
        [SerializeField] AudioClip soundToPlay;

        [SerializeField] AudioSource audioSource = null;

        private AudioSourcesScript asc;

        private void Start()
        {
            if(audioSource == null) asc = FindAnyObjectByType<AudioSourcesScript>();
        }

        public void OnClick()
        {
            if(audioSource == null) asc.PlayUI(soundToPlay);
            else { audioSource.clip = soundToPlay; audioSource.Play(); }
        }
    }
}