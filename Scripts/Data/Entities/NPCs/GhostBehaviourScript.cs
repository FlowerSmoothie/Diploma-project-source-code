using System.Collections;
using EntityUtils.PlayerUtils;
using General;
using Misc.UI;
using Misc.UI.Deduction;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace EntityUtils.NPCUtils
{
    public class GhostBehaviourScript : MonoBehaviour
    {
        private EntityStates.PositionState positionState;
        private EnemyStates.BehaviourState behaviourState;

        private NavMeshAgent agent;
        [SerializeField] private float range;
        [SerializeField] private Transform whatToFollow = null;


        [SerializeField] int hurtStrength;

        private Transform whatToEscape;

        [SerializeField] private GameObject entityVisual;
        [SerializeField] private GameObject entityAudial;
        //private AudioSourceEnemy audioEnemyScript;


        [SerializeField] private float fieldOfView;
        private Animator animator;

        private Transform centrePoint;

        [SerializeField] private int howLongCanAvoid;
        [SerializeField] private int howLongWillRecover;
        private bool isTiredOfAvoiding = false;
        private Coroutine waitingCoroutine;

        private bool breakCoroutine = false;

        private bool isFrozen = false;

        private GhostInteractingScript interactionScript;

        private float speedOnHold;

        private float defaultSpeed;

        private PlayerHealthScript playerHealth;
        private FlashlightScript flashlightScript;

        private GhostSoundsScript ghostSoundsScript;


        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.angularSpeed = 0;

            animator = entityVisual.GetComponent<Animator>();
            //audioEnemyScript = entityAudial.GetComponent<AudioSourceEnemy>();

            centrePoint = transform;

            positionState = EntityStates.PositionState.FACING_LEFT;
            behaviourState = EnemyStates.BehaviourState.WANDERING;

            isFrozen = false;

            //handler = GetComponent<EntityEventHandler>();
            //interactionScript = GetComponent<AuroraInteractionScript>();
            interactionScript = GetComponent<GhostInteractingScript>();
            interactionScript.LockInteracting();
            ghostSoundsScript = GetComponent<GhostSoundsScript>();

            playerHealth = FindAnyObjectByType<PlayerHealthScript>();
            flashlightScript = FindAnyObjectByType<FlashlightScript>();

            defaultSpeed = agent.speed;

            if (whatToFollow == null) whatToFollow = FindAnyObjectByType<PlayerMovingScript>().transform;

            UIEventPublisher.i.onUIActivating += FreezeEntity;
            UIEventPublisher.i.onUIFullyDeactivating += UnfreezeEntity;

        }

        private void OnDestroy()
        {
            UIEventPublisher.i.onUIActivating -= FreezeEntity;
            UIEventPublisher.i.onUIFullyDeactivating -= UnfreezeEntity;

            DeductionEventPublisher.i.onDeductionCompleted -= ReactToDeductionCompletion;
        }

        GameObject itemToSpawn;

        public void SubscribeToDeduction(GameObject itemToSpawnAfterCompletion)
        {
            itemToSpawn = itemToSpawnAfterCompletion;

            DeductionEventPublisher.i.onDeductionCompleted += ReactToDeductionCompletion;
        }

        private void ReactToDeductionCompletion(object sender, System.EventArgs e, Character character, bool everythingIsOkay)
        {
            DeductionEventPublisher.i.onDeductionCompleted -= ReactToDeductionCompletion;
            if (everythingIsOkay)
            {
                interactionScript.SatisfyGhost();
                if(itemToSpawn != null) Instantiate(itemToSpawn);
            }
            else
            {
                playerHealth.Hurt(5);
            }
        }


        private void FreezeEntity(object sender, System.EventArgs e)
        {
            if (!isFrozen)
            {
                speedOnHold = agent.speed;
                agent.speed = 0;
                isFrozen = true;
            }
        }

        private void UnfreezeEntity(object sender, System.EventArgs e)
        {
            agent.speed = speedOnHold;
            speedOnHold = 0;
            isFrozen = false;
        }

        private void ChangePositionState(EntityStates.PositionState state)
        {
            positionState = state;
            animator.SetTrigger("rotating");
            switch (state)
            {
                case EntityStates.PositionState.FACING_RIGHT:
                    animator.SetBool("turnedRight", true);
                    break;
                case EntityStates.PositionState.FACING_LEFT:
                    animator.SetBool("turnedRight", false);
                    break;
            }
        }

        private IEnumerator WaitForAvoiding()
        {
            isTiredOfAvoiding = false;

            yield return WaitForTime(howLongCanAvoid);
            if (!breakCoroutine)
            {
                isTiredOfAvoiding = true;
            }
            breakCoroutine = false;

        }

        float secsPassed, secsNeeded;

        private IEnumerator WaitForTime(int time)
        {
            float secsPassed = 0;
            while (secsPassed <= time)
            {
                if (isFrozen) { yield return new WaitForEndOfFrame(); }
                else
                {
                    if (breakCoroutine) break;
                    yield return new WaitForSecondsRealtime(0.1f);
                    secsPassed += 0.1f;
                }
            }
        }

        private IEnumerator WaitForResting()
        {
            agent.speed = 0;
            isTiredOfAvoiding = true;

            yield return WaitForTime(howLongWillRecover);
            if (!breakCoroutine)
            {
                isTiredOfAvoiding = false;
            }
            breakCoroutine = false;
        }

        private void ChangeState(EnemyStates.BehaviourState newState, bool should = false)
        {
            if (isFrozen && !should) return;
            if (waitingCoroutine != null) StopCoroutine(waitingCoroutine);
            behaviourState = newState;
            switch (newState)
            {
                case EnemyStates.BehaviourState.CHASING:
                    //audioEnemyScript.ChangeAndPlay(AudioSourceEnemy.NOTICING_SOUND);
                    ghostSoundsScript.NoticeCharacter();
                    break;
                case EnemyStates.BehaviourState.AVOIDING:
                    waitingCoroutine = StartCoroutine(WaitForAvoiding());
                    break;
                case EnemyStates.BehaviourState.STANDING:
                    interactionScript.UnlockInteracting();
                    waitingCoroutine = StartCoroutine(WaitForResting());
                    break;
                case EnemyStates.BehaviourState.WANDERING:
                    agent.speed = defaultSpeed;
                    interactionScript.LockInteracting();
                    break;
            }
        }

        public void ChangeToWander() { ChangeState(EnemyStates.BehaviourState.WANDERING); }
        public void Anger() { ChangeState(EnemyStates.BehaviourState.CHASING, true); }


        void Update()
        {
            if (!isFrozen)
            {
                Vector2.Distance(transform.position, whatToFollow.position);
                switch (behaviourState)
                {
                    case EnemyStates.BehaviourState.WANDERING:
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            Vector3 point;
                            if (RandomPoint(centrePoint.position, range, out point))
                                agent.SetDestination(point);
                        }
                        if (Vector2.Distance(transform.position, whatToFollow.position) <= fieldOfView)
                            ChangeState(EnemyStates.BehaviourState.CHASING);
                        break;
                    case EnemyStates.BehaviourState.CHASING:
                        if (Vector2.Distance(transform.position, whatToFollow.position) <= fieldOfView)
                        {
                            agent.SetDestination(whatToFollow.position);
                        }
                        else
                            ChangeState(EnemyStates.BehaviourState.WANDERING);
                        break;
                    case EnemyStates.BehaviourState.AVOIDING:
                        //Debug.Log(chasedByPlayer);
                        /*if (breakCoroutine)
                        {
                            breakCoroutine = false;
                            ChangeState(EnemyStates.BehaviourState.WANDERING);
                        }*/
                        if (!flashlightScript.IsOn())
                        {
                            ChangeState(EnemyStates.BehaviourState.WANDERING);
                        }
                        else
                        {
                            if (isTiredOfAvoiding)
                                ChangeState(EnemyStates.BehaviourState.STANDING);
                            else
                            {
                                Vector3 direction = (whatToEscape.position - transform.position).normalized;

                                direction = Quaternion.AngleAxis(Random.Range(0, 45), Vector3.up) * direction;

                                agent.SetDestination(transform.position - (direction * 10));
                            }
                        }
                        break;
                    case EnemyStates.BehaviourState.STANDING:
                        if (!isTiredOfAvoiding)
                            ChangeState(EnemyStates.BehaviourState.WANDERING);
                        break;
                }

                if (agent.velocity.x > 0 && positionState == EntityStates.PositionState.FACING_LEFT)
                    ChangePositionState(EntityStates.PositionState.FACING_RIGHT);
                else if (agent.velocity.x < 0 && positionState == EntityStates.PositionState.FACING_RIGHT)
                    ChangePositionState(EntityStates.PositionState.FACING_LEFT);
            }
        }
        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
            result = Vector3.zero;
            return false;
        }

        public void Neutralize()
        {
            isFrozen = true;
            agent.velocity = Vector3.zero;
            if (waitingCoroutine != null) StopCoroutine(waitingCoroutine);
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<SpotOfLightScript>(out SpotOfLightScript light))
            {
                whatToEscape = other.gameObject.transform;
                ChangeState(EnemyStates.BehaviourState.AVOIDING);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (behaviourState != EnemyStates.BehaviourState.STANDING && !isFrozen)
            {
                if (other.gameObject.TryGetComponent(out PlayerUtils.PlayerHealthScript player) && !couroutineRunning)
                {
                    StartCoroutine(WaitThenHurt(player));
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent<SpotOfLightScript>(out SpotOfLightScript light))
            {
                breakCoroutine = true;
                ChangeState(EnemyStates.BehaviourState.WANDERING);
            }
        }



        private bool couroutineRunning = false;

        private IEnumerator WaitThenHurt(PlayerUtils.PlayerHealthScript player)
        {
            couroutineRunning = true;
            //yield return new WaitForSecondsRealtime(hurtStrength);
            //Debug.Log(hurtStrength / 3f);
            yield return new WaitForSecondsRealtime(hurtStrength / 5f);
            player.Hurt(1);
            couroutineRunning = false;
        }
    }
}