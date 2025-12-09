using System.Collections.Generic;
using System;
using UnityEngine;

namespace Misc
{
    public class AmbienceSoundScript : MonoBehaviour
    {
        [SerializeField] private AudioClip loopedAmbienceSounds;

        [SerializeField] private int ambienceSoundsMinFrequency;
        [SerializeField] private int ambienceSoundsMaxFrequency;
        private int frame;
        private int targetFrame;
        [SerializeField] private List<AudioClip> ambienceSounds;

        public void PlayAmbience()
        {
            FindAnyObjectByType<AudioSourcesScript>().PlayAmbience(loopedAmbienceSounds, true);
        }

        public void PlayAmbience(AudioClip ambienceToPlay)
        {
            FindAnyObjectByType<AudioSourcesScript>().PlayAmbience(ambienceToPlay);
        }

        public void StopAmbience()
        {
            FindAnyObjectByType<AudioSourcesScript>().StopAmbience(true);
        }

        private void Start()
        {
            PlayAmbience();
            GenerateTargetFrame();
            frame = 0;
        }

        private void GenerateTargetFrame()
        {
            targetFrame = new System.Random().Next(ambienceSoundsMinFrequency, ambienceSoundsMaxFrequency);
        }

        private void Update()
        {
            frame++;
            if (frame == targetFrame)
            {
                frame = 0;
                GenerateTargetFrame();
                PlayAmbience(ambienceSounds[new System.Random().Next(0, ambienceSounds.Count + 1)]);
            }
        }
    }

}