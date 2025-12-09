using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Misc.UI.Main
{
    public class SoundValueGettingScript : MonoBehaviour, IEndDragHandler
    {
        private Slider slider;
        private AudioSourcesScript aus;
        [SerializeField] private AudioType type;
        [SerializeField] AudioSource source = null;

        private void Start()
        {
            slider = GetComponent<Slider>();
            if(source == null) aus = FindAnyObjectByType<AudioSourcesScript>();
            switch (type)
            {
                case AudioType.UIAudio:
                    slider.value = PlayerPrefs.GetFloat("UIVolume");
                    break;
                case AudioType.AmbienceAudio:
                    slider.value = PlayerPrefs.GetFloat("AmbientVolume");
                    break;
                case AudioType.OtherAudio:
                    slider.value = PlayerPrefs.GetFloat("OtherVolume");
                    break;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (source != null)
            {
                source.volume = slider.value;
                source.Play();
            }
            else
            {
                switch(type)
            {
                case AudioType.UIAudio:
                    aus.UpdateUI(slider.value);
                    break;
                case AudioType.AmbienceAudio:
                    aus.UpdateAmbience(slider.value);
                    break;
                case AudioType.OtherAudio:
                    aus.UpdateOther(slider.value);
                    break;
            }
            }
        }
    }
}