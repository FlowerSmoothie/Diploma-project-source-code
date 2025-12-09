using UnityEngine;
using UnityEngine.UI;

namespace Misc.UI.Main
{
    public class SoundSavingScript : MonoBehaviour
    {
        [SerializeField] Slider UISlider;
        [SerializeField] Slider ambientSlider;
        [SerializeField] Slider otherSlider;

        public void OnClick()
        {
            PlayerPrefs.SetFloat("UIVolume", UISlider.value);
            PlayerPrefs.SetFloat("AmbientVolume", ambientSlider.value);
            PlayerPrefs.SetFloat("OtherVolume", otherSlider.value);
        }
    }
}
