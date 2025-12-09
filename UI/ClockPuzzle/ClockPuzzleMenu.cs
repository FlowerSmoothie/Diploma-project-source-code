using System.Collections;
using EntityUtils.PlayerUtils;
using Overworld.ObjectsAndNPC.Puzzles;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Misc.UI.Puzzles
{
    public class ClockPuzzleMenu : UIMenuScript
    {
        [Header("Clock")]
        [SerializeField] Image clockRenderer;
        [SerializeField] Sprite clockGreat;
        [SerializeField] Sprite clockNormal;
        [SerializeField] Sprite clockBad;

        [Space]

        [Header("Arrows")]
        [SerializeField] Image hourRenderer;
        [SerializeField] Sprite hourGreat;
        [SerializeField] Sprite hourNormal;
        [SerializeField] Sprite hourBad;
        [SerializeField] Image minuteRenderer;
        [SerializeField] Sprite minuteGreat;
        [SerializeField] Sprite minuteNormal;
        [SerializeField] Sprite minuteBad;

        [Space]

        [Header("Coockoo")]
        [SerializeField] Animator birdAnimator;
        [SerializeField] Image birdRenderer;
        [SerializeField] Sprite birdGreat;
        [SerializeField] Sprite birdNormal;
        [SerializeField] Sprite birdBad;

        [Space]

        [Header("Sound options:")]
        [SerializeField] private AudioClip timeMovingSound;
        [SerializeField] private AudioClip unabletimeMovingSound;
        [SerializeField] private AudioClip unlockSound;
        [SerializeField] private AudioClip coockooSound;
        [SerializeField] private AudioClip crowSound;
        private AudioClip playedSound;
        private AudioSourcesScript asc;

        [Space]
        [SerializeField] private int greatLowBorder = Utils.Consts.GREAT_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;
        [SerializeField] private int normalLowBorder = Utils.Consts.NORMAL_MENTAL_HEALTH_DOWN_BORDER_DEFAULT;


        private int hoursCurrent;
        private int minutesCurrent;

        private ClockPuzzleScript master;

        private bool movingEnabled = true;


        int coroutinesRunning = 0;

        bool enabledPressing = false;

        private void Start()
        {
            UIEventPublisher.i.onClockImageDeActivating += ActivateMenu;

            asc = FindAnyObjectByType<AudioSourcesScript>();

            enabledPressing = false;
            on = false;
        }

        //private bool holdingA;
        //private bool holdingD;

        private int framesPressedButton;

        private bool on;

        private void Update()
        {
            if (on)
            {
                if (enabledPressing)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Close();
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        master.CheckIfTimeIsCorrect();
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        ChangeTime(true);
                        //holdingA = true;
                        enabledPressing = false;
                    }
                    else if (Input.GetKeyDown(KeyCode.A))
                    {
                        ChangeTime(false);
                        //holdingD = true;
                        enabledPressing = false;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
                {
                    framesPressedButton = 0;
                    //holdingA = false;
                    //holdingD = false;
                    enabledPressing = true;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    framesPressedButton++;
                    if (framesPressedButton % 200 == 0)
                    {
                        ChangeTime(true);
                    }
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    framesPressedButton++;
                    if (framesPressedButton % 200 == 0)
                    {
                        ChangeTime(false);
                    }
                }
                else
                {
                    //holdingA = false;
                    //holdingD = false;
                }
            }
        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onClockImageDeActivating -= ActivateMenu;
        }

        public void RunABird()
        {
            //controller.clip = playedSound;
            //controller.Play();
            birdAnimator.SetBool("active", true);
        }

        public void DisableMoving() { movingEnabled = false; }


        public void Close()
        {
            birdAnimator.SetBool("active", false);
            enabledPressing = false;
            on = false;
            StartCoroutine(Closing());
        }

        private IEnumerator Closing()
        {
            yield return new WaitForSecondsRealtime(0.2f);
            UIEventPublisher.i.DoUIDeactivating(this, null);
            UIEventPublisher.i.DoOnClockImageDeActivating(this, null);
        }

        private IEnumerator Opening()
        {
            yield return new WaitForSecondsRealtime(0.2f);
            enabledPressing = true;
        }

        public void LoadData(int hoursCurrent, int minutesCurrent)
        {
            this.hoursCurrent = hoursCurrent;
            this.minutesCurrent = minutesCurrent;

            master = FindAnyObjectByType<ClockPuzzleScript>();

            //hourRenderer.transform.Rotate(hourRenderer.transform.eulerAngles.x, hourRenderer.transform.eulerAngles.y, hoursCurrent * 30);
            //minuteRenderer.transform.Rotate(minuteRenderer.transform.eulerAngles.x, minuteRenderer.transform.eulerAngles.y, minutesCurrent * 30);

            //hourRenderer.gameObject.transform.Rotate(0, 0, -hoursCurrent * 30);
            //minuteRenderer.gameObject.transform.Rotate(0, 0, -minutesCurrent * 30);

            hourRenderer.gameObject.transform.eulerAngles = new Vector3(0, 0, -hoursCurrent * 30);
            minuteRenderer.gameObject.transform.eulerAngles = new Vector3(0, 0, -minutesCurrent * 30);

            int health = FindAnyObjectByType<PlayerHealthScript>().GetAmount();

            if (health <= 100 && health >= greatLowBorder)
            {
                clockRenderer.sprite = clockGreat;
                hourRenderer.sprite = hourGreat;
                minuteRenderer.sprite = minuteGreat;
                birdRenderer.sprite = birdGreat;
                playedSound = coockooSound;
            }
            else if (health < greatLowBorder && health >= normalLowBorder)
            {
                clockRenderer.sprite = clockNormal;
                hourRenderer.sprite = hourNormal;
                minuteRenderer.sprite = minuteNormal;
                birdRenderer.sprite = birdNormal;
                playedSound = coockooSound;
            }
            else
            {
                clockRenderer.sprite = clockBad;
                hourRenderer.sprite = hourBad;
                minuteRenderer.sprite = minuteBad;
                birdRenderer.sprite = birdBad;
                playedSound = crowSound;
            }
            on = true;
            StartCoroutine(Opening());
        }

        public void ChangeTime(bool clockWise)
        {
            if (master != null)
            {
                if (movingEnabled)
                {
                    if (coroutinesRunning == 0)
                    {
                        asc.PlaySound(timeMovingSound);
                        bool hoursChanged = false;
                        if (clockWise)
                        {
                            minutesCurrent++;
                            if (minutesCurrent == 12)
                            {
                                minutesCurrent = 0;
                                hoursCurrent++;
                                hoursChanged = true;
                                if (hoursCurrent == 12) hoursCurrent = 0;
                            }
                        }
                        else
                        {
                            minutesCurrent--;
                            if (minutesCurrent == -1)
                            {
                                minutesCurrent = 11;
                                hoursCurrent--;
                                hoursChanged = true;
                                if (hoursCurrent == -1) hoursCurrent = 11;
                            }
                        }

                        master.UpdateData(hoursCurrent, minutesCurrent);

                        //controller.clip = timeMovingSound;
                        //controller.Play();
                        UpdateImage(hoursChanged, clockWise);
                    }
                }
                else
                {
                    //controller.clip = unabletimeMovingSound;
                    //controller.Play();
                }
            }

        }

        private void UpdateImage(bool hoursChanged, bool clockWise)
        {
            StartCoroutine(Rotate(minuteRenderer.gameObject, clockWise, 0.5f));
            if (hoursChanged) StartCoroutine(Rotate(hourRenderer.gameObject, clockWise, 0.5f));
        }

        private IEnumerator Rotate(GameObject whatToRotate, bool clockWise, float duration)
        {
            coroutinesRunning++;

            float startRotation = whatToRotate.transform.eulerAngles.z;
            float endRotation = clockWise ? startRotation - 30.0f : startRotation + 30.0f;

            if (endRotation < 0)
            {
                startRotation = 360;
                endRotation = 360 + endRotation;
            }
            float t = 0.0f;
            while (t < duration - 0.01)
            {
                t += Time.deltaTime;
                float zRotation = Mathf.LerpAngle(startRotation, endRotation, t / duration) % 30.0f;

                float temp = clockWise ? endRotation + zRotation : startRotation + zRotation;

                whatToRotate.transform.eulerAngles = new Vector3(0, 0, temp);
                yield return new WaitForEndOfFrame();
            }
            whatToRotate.transform.eulerAngles = new Vector3(0, 0, endRotation);
            if (whatToRotate.transform.eulerAngles.z >= 360 || whatToRotate.transform.eulerAngles.z <= -360)
                whatToRotate.transform.eulerAngles = new Vector3(0, 0, 0);

            coroutinesRunning--;
        }
    }
}