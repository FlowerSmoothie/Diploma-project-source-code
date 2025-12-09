using System;
using System.Collections.Generic;
using Misc;
using Misc.UI;
using UnityEngine;

namespace EntityUtils.NPCUtils
{
    public class GhostSoundsScript : MonoBehaviour
    {
        [SerializeField] AudioClip playerNoticingSound;
        [SerializeField] List<AudioClip> ambienceSounds;

        [SerializeField] private int ambienceSoundsMinFrequency;
        [SerializeField] private int ambienceSoundsMaxFrequency;
        private int frame;
        private int targetFrame;

        private AudioSourcesScript asc;

        private bool isFrozen;

        private void Start()
        {
            asc = FindAnyObjectByType<AudioSourcesScript>();

            isFrozen = false;

            GenerateTargetFrame();
            frame = 0;

            UIEventPublisher.i.onUIActivating += FreezeSounds;
            UIEventPublisher.i.onUIFullyDeactivating += UnfreezeSounds;
        }

        private void FreezeSounds(object sender, EventArgs e) { isFrozen = true; }

        private void UnfreezeSounds(object sender, EventArgs e) { isFrozen = false; }

        private void GenerateTargetFrame()
        {
            targetFrame = new System.Random().Next(ambienceSoundsMinFrequency, ambienceSoundsMaxFrequency);
        }


        private void Update()
        {
            if (!isFrozen)
            {
                frame++;
                if (frame == targetFrame)
                {
                    frame = 0;
                    GenerateTargetFrame();
                    asc.PlayNPC(ambienceSounds[new System.Random().Next(0, ambienceSounds.Count + 1)]);
                }
            }
        }

        public void NoticeCharacter() { asc.PlayNPC(playerNoticingSound, true); }


    }
}