using System;
using System.Collections;
using Misc;
using Misc.UI;
using UnityEngine;

namespace EntityUtils.PlayerUtils
{
    public class FlashlightScript : MonoBehaviour
    {
        //[SerializeField] AudioSource audioSource;
        //[SerializeField] AudioClip sound;

        float speed = 500f;

        [SerializeField] float canBeOnFor;
        [SerializeField] float cooldown;

        private bool ableToMove;

        private bool isOn;
        private bool canBeLighted;
        private GameObject lightGO;

        private Coroutine lightingCoroutine;
        private Coroutine cooldownCoroutine;

        [SerializeField] private AudioClip onOff;
        [SerializeField] private AudioClip failure;
        private AudioSourcesScript asc;

        private void Start()
        {
            ableToMove = true;
            isOn = false;
            canBeLighted = true;
            lightGO = transform.GetChild(0).gameObject;

            asc = FindAnyObjectByType<AudioSourcesScript>();

            UIEventPublisher.i.onUIActivating += FreezeLight;
            UIEventPublisher.i.onUIFullyDeactivating += UnfreezeLight;
        }

        private void UnfreezeLight(object sender, EventArgs e)
        {
            ableToMove = true;
        }

        private void FreezeLight(object sender, EventArgs e)
        {
            ableToMove = false;
        }

        public bool IsOn() { return isOn; }

        public void OnOff()
        {
            if (ableToMove)
            {
                isOn = !isOn;
                //audioSource.clip = sound;
                //audioSource.Play();
            }
        }

        private void On()
        {
            isOn = true;
            lightGO.SetActive(true);
            asc.PlaySound(onOff);
            lightingCoroutine = StartCoroutine(WaitBeforeCooldown());
        }
        private void Off(bool voluntairy = true)
        {
            isOn = false;
            lightGO.SetActive(false);
            if (voluntairy)
                asc.PlaySound(onOff);
                else
                asc.PlaySound(failure);
            StopCoroutine(lightingCoroutine);
        }

        private IEnumerator WaitCooldown()
        {
            yield return new WaitForSecondsRealtime(cooldown);

            canBeLighted = true;
        }
        private IEnumerator WaitBeforeCooldown()
        {
            yield return new WaitForSecondsRealtime(canBeOnFor);

            canBeLighted = false;
            Off(false);
            StartCoroutine(WaitCooldown());
        }



        private void Update()
        {
            if (ableToMove)
            {
                if (Input.GetButtonDown("Flashlight") && ableToMove)
                {
                    if (isOn)
                    {
                        Off();
                    }
                    else if (canBeLighted)
                    {
                        On();
                    }
                    //OnOff();
                }
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                Vector3 movementDirection = new Vector3(-horizontalInput, 0, -verticalInput);
                movementDirection.Normalize();

                //transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);

                if (movementDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, speed * Time.deltaTime);
                }
            }

        }
    }
}