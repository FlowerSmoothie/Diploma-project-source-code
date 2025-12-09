using System;
using Overworld.Items;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Utils;

namespace Misc.UI
{
    public class ZoomedImageShower : UIMenuScript
    {
        [SerializeField] GameObject image;
        private Animator imageAnimator;
        private Image imageImage;
        [SerializeField] GameObject video;
        private Animator videoAnimator;
        private VideoPlayer videoImage;

        private UIControllerScript uics;

        private bool isActive;

        private void OnDestroy()
        {
            UIEventPublisher.i.onCGDeActivating -= ActivateMenu;
        }

        private void Start()
        {
            UIEventPublisher.i.onCGDeActivating += ActivateMenu;

            imageAnimator = image.GetComponent<Animator>();
            imageImage = image.GetComponent<Image>();

            videoAnimator = video.GetComponent<Animator>();
            videoImage = video.GetComponent<VideoPlayer>();

            uics = FindAnyObjectByType<UIControllerScript>();
        }
        private void Update()
        {
            if (isActive)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    if (uics.GetUICount() == 1) Hide();
                }
            }
        }

        public void Show(ZoomableItemInOverworld obj, int health)
        {
            UIEventPublisher.i.DoUIActivating(this, null);
            UIEventPublisher.i.DoCGDeActivating(this, null);
            isActive = true;

            if (obj.IsStatic())
            {
                Sprite sprite = ((ZoomableItemInOverworldImage)obj).GetImage(health);
                Show(sprite);
            }
            else
            {
                VideoClip clip = ((ZoomableItemInOverworldVideo)obj).GetImage(health);
                Show(clip);
            }
        }

        public void Show(ZoomableObjectInOverworldImage obj, int health)
        {
            UIEventPublisher.i.DoUIActivating(this, null);
            UIEventPublisher.i.DoCGDeActivating(this, null);
            isActive = true;

            Sprite sprite = obj.GetImage(health);
            Show(sprite);
        }

        private void Hide()
        {
            //video.SetActive(false);
            //image.SetActive(false);
            videoAnimator.SetBool("active", false);
            imageAnimator.SetBool("active", false);

            isActive = false;

            UIEventPublisher.i.DoCGDeActivating(this, null);
            UIEventPublisher.i.DoUIDeactivating(this, null);
        }

        private void Show(Sprite imageToShow)
        {
            video.SetActive(false);

            image.SetActive(true);
            imageImage.sprite = imageToShow;
            imageAnimator.SetBool("active", true);
        }
        private void Show(VideoClip videoToShow)
        {
            image.SetActive(false);

            video.SetActive(true);
            videoImage.clip = videoToShow;
            videoAnimator.SetBool("active", true);
        }
    }
}