using System;
using Misc;
using Misc.UI;
using UnityEngine;
using Utils;

namespace EntityUtils.PlayerUtils
{
    public class PlayerHealthScript : MonoBehaviour
    {
        [SerializeField] int health;

        [SerializeField] private AudioClip takingDamageSound;
        [SerializeField] private AudioClip healingSound;
        [SerializeField] private AudioClip dyingSound;
        
        [SerializeField] private AudioClip heartbeatSound;
        [SerializeField] private AudioClip killingSound;
        [SerializeField] private AudioClip breathSound;

        private AudioSourcesScript asc;
        private PostEffectsController postEffects;

        /*private float minHealth = -0.8f;
        private float maxHealth = 1.3f;
        private float healthStep;*/

        public void Hurt(int damageCount)
        {
            RecieveDamage(damageCount);
        }

        public void Heal(int healCount)
        {
            RecieveHealing(healCount);
        }

        public int GetAmount()
        {
            return health;
        }

        private void Start()
        {
            health = FindAnyObjectByType<DataHolderScript>().GetHealth();
            
            postEffects = FindAnyObjectByType<PostEffectsController>();

            asc = FindAnyObjectByType<AudioSourcesScript>();

            UpdateHealth();

            asc.StartHeartbeat(heartbeatSound);
            asc.UpdateVolume(0);

            PlayerEventPublisher.i.onMedicineTook += RecieveHealingThroughMedicine;
        }

        private void OnDestroy()
        {
            PlayerEventPublisher.i.onMedicineTook -= RecieveHealingThroughMedicine;
        }

        private void RecieveHealingThroughMedicine(object sender, EventArgs e, int health)
        {
            RecieveHealing(health);
        }

        private void Update()
        {
            /*if (Input.GetKeyDown(KeyCode.T))
            {
                RecieveDamage(5);
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                RecieveHealing(5);
            }*/
        }

        private void RecieveDamage(int damageCount)
        {
            health -= damageCount;
            if (health <= Consts.MIN_MENTAL_HEALTH)
            {
                health = 0;
                Die();
            }

            if(health <= Consts.MAX_MENTAL_HEALTH && health > 60)
            {
                asc.UpdateVolume(0);
            }
            else if(health <= 60 && health > 40)
            {
                asc.UpdateVolume(0.3f);
                asc.PlayBreathing(breathSound);
            }
            else if(health <= 40 && health > 20)
            {
                asc.UpdateVolume(0.6f);
                asc.PlayBreathing(breathSound);
            }
            else if(health <= 20 && health > 0)
            {
                asc.UpdateVolume(0.9f);
                asc.PlayBreathing(breathSound);
            }

            asc.PlayUIIfNotBusy(takingDamageSound);

            PlayerEventPublisher.i.DoOnHealthChanged(this, null, -damageCount);
            UpdateHealth();
        }

        private void RecieveHealing(int healCount)
        {
            health += healCount;
            if (health > Consts.MAX_MENTAL_HEALTH)
            {
                health = Consts.MAX_MENTAL_HEALTH;
            }

            if(health <= Consts.MAX_MENTAL_HEALTH && health > 60)
            {
                asc.UpdateVolume(0);
            }
            else if(health <= 60 && health > 40)
            {
                asc.UpdateVolume(0.3f);
            }
            else if(health <= 40 && health > 20)
            {
                asc.UpdateVolume(0.6f);
            }
            else if(health <= 20 && health > 0)
            {
                asc.UpdateVolume(0.9f);
            }

            asc.PlayUI(healingSound);

            PlayerEventPublisher.i.DoOnHealthChanged(this, null, healCount);
            UpdateHealth();
        }

        private void UpdateHealth()
        {
            postEffects.SetRadius(-1 + (0.02f * health));
        }

        private void Die()
        {
            FindAnyObjectByType<AudioSourcesScript>().PlayUI(dyingSound);
            UIEventPublisher.i.DoUIActivating(this, null);
            GetComponent<SceneSwitcher>().ChangeScene(0, 10);
        }

        public void Kill()
        {
            FindAnyObjectByType<AudioSourcesScript>().PlayUI(killingSound);
            UIEventPublisher.i.DoUIActivating(this, null);
            GetComponent<SceneSwitcher>().ChangeScene(0, 10);
        }

    }
}
