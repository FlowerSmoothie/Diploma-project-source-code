using System.Security.Cryptography;
using UnityEngine;

namespace Misc
{
    public class AudioSourcesScript : MonoBehaviour
    {

        private static AudioSourcesScript instance;

        void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }

            DontDestroyOnLoad(gameObject);
        }



        [SerializeField] private AudioSource ambiencePlayerLooped = null;
        [SerializeField] private AudioSource ambiencePlayer2 = null;

        [SerializeField] private AudioSource soundPlayer1 = null;
        [SerializeField] private AudioSource soundPlayer2 = null;

        [SerializeField] private AudioSource UIPlayerLooped = null;
        [SerializeField] private AudioSource UIPlayer2 = null;
        [SerializeField] private AudioSource UIPlayer3 = null;

        [SerializeField] private AudioSource PlayerSoundLooped = null;
        [SerializeField] private AudioSource PlayerSound2 = null;

        [SerializeField] private AudioSource NPCNoticing = null;
        [SerializeField] private AudioSource NPCSound2 = null;

        [SerializeField] private AudioSource heartbeatSound = null;
        [SerializeField] private AudioSource breathingSound = null;

        private float UI, ambience, other;
        private float healthVol = 1;

        private void Start()
        {
            UpdateVolumes(PlayerPrefs.GetFloat("UIVolume"), PlayerPrefs.GetFloat("AmbientVolume"), PlayerPrefs.GetFloat("OtherVolume"));
        }

        public void UpdateVolumes(float UI, float ambience, float other)
        {
            UpdateUI(UI);
            UpdateAmbience(ambience);
            UpdateOther(other);
        }

        public void UpdateUI(float UI)
        {
            this.UI = UI;


            UIPlayerLooped.volume = UI;
            UIPlayer2.volume = UI;
            UIPlayer3.volume = UI;


            heartbeatSound.volume = healthVol * UI;
            breathingSound.volume = healthVol * UI;
        }

        public void UpdateAmbience(float ambience)
        {
            this.ambience = ambience;


            ambiencePlayerLooped.volume = ambience;
            ambiencePlayer2.volume = ambience;

            NPCNoticing.volume = ambience;
            NPCSound2.volume = ambience;
        }

        public void UpdateOther(float other)
        {
            this.other = other;



            soundPlayer1.volume = other;
            soundPlayer2.volume = other;
            PlayerSoundLooped.volume = other;
            PlayerSound2.volume = other;
        }

        public void UpdateVolume(float vol)
        {
            healthVol = vol;
            heartbeatSound.volume = vol * UI;
            breathingSound.volume = vol * UI;
        }

        public void PlayBreathing(AudioClip breathing)
        {
            if (breathingSound.isPlaying) return;
            breathingSound.clip = breathing;
            breathingSound.Play();
        }

        public void StartHeartbeat(AudioClip beating)
        {
            heartbeatSound.clip = beating;
            heartbeatSound.Play();
        }
        public void StartHeartbeat()
        {
            heartbeatSound.volume = 1 * UI;
        }

        public void PlayAmbience(AudioClip sound, bool loop = false)
        {
            if (loop) { ambiencePlayerLooped.clip = sound; ambiencePlayerLooped.Play(); return; }
            else { ambiencePlayer2.clip = sound; ambiencePlayer2.Play(); }
        }

        public void PlaySound(AudioClip sound)
        {
            if (soundPlayer1.isPlaying) { soundPlayer2.clip = sound; soundPlayer2.Play(); return; }
            soundPlayer1.clip = sound;
            soundPlayer1.Play();
        }

        public void PlayUI(AudioClip sound, bool loop = false)
        {
            try
            {
                if (loop) { UIPlayerLooped.clip = sound; UIPlayerLooped.Play(); return; }
                if (!UIPlayer2.isPlaying)
                {
                    UIPlayer2.clip = sound;
                    UIPlayer2.Play();
                }
                else
                {
                    UIPlayer3.clip = sound;
                    UIPlayer3.Play();
                }
            }
            catch
            {

            }
        }

        public void StopAll()
        {
            ambiencePlayerLooped.volume = 0;
            ambiencePlayer2.volume = 0;

            soundPlayer1.volume = 0;
            soundPlayer2.volume = 0;


            UIPlayerLooped.volume = 0;
            //UIPlayer2.volume = 0;
            //UIPlayer3.volume = 0;

            PlayerSoundLooped.volume = 0;
            PlayerSound2.volume = 0;

            NPCNoticing.volume = 0;
            NPCSound2.volume = 0;

            heartbeatSound.volume = 0;
            breathingSound.volume = 0;
        }

        public void PlayUIIfNotBusy(AudioClip sound)
        {
            if (!UIPlayer2.clip == sound && !UIPlayer2.isPlaying) { UIPlayer2.clip = sound; UIPlayer2.Play(); }
            else if (!UIPlayer3.clip == sound && !UIPlayer3.isPlaying) { UIPlayer3.clip = sound; UIPlayer3.Play(); }
        }

        public void PlayPlayer(AudioClip sound, bool loop = false)
        {
            if (loop) { PlayerSoundLooped.clip = sound; PlayerSoundLooped.Play(); return; }
            PlayerSound2.clip = sound;
            PlayerSound2.Play();
        }

        public void PlayNPC(AudioClip sound, bool noticing = false)
        {
            if (noticing) { NPCNoticing.clip = sound; NPCNoticing.Play(); return; }
            NPCSound2.clip = sound;
            NPCSound2.Play();
        }

        public void StopUI(bool loopingUI = false)
        {
            try
            {
                if (loopingUI) { UIPlayerLooped.Stop(); }
                else UIPlayer2.Stop();
            }
            catch
            {

            }
        }

        public void StopAmbience(bool loopingUI = false)
        {
            if (loopingUI) { ambiencePlayerLooped.Stop(); }
            else ambiencePlayer2.Stop();
        }

        public void StopPlayer(bool loopingUI = false)
        {
            if (loopingUI) { PlayerSoundLooped.Stop(); }
            else PlayerSound2.Stop();
        }

        public void DeleteThis()
        {
            Destroy(gameObject, 0.25f);
        }
    }
}