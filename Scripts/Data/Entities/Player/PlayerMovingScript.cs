using EntityUtils.PlayerUtils.Graphics;
using Misc;
using Misc.UI;
using UnityEngine;


namespace EntityUtils.PlayerUtils
{
    public class PlayerMovingScript : MonoBehaviour
    {
        private Rigidbody rb;

        private Vector3 moveDir;

        [SerializeField] private float movementSpeed = 1f;

        [SerializeField] private AudioClip movingSound;

        private EntityStates.AnimationState animationState;
        private EntityStates.PositionState positionState = EntityStates.PositionState.FACING_LEFT;

        private bool movingEnabled;

        //private NormalFlippingScript normalFlippingScript;

        //[Space]
        //[SerializeField]
        private Animator playerAnimator;

        private AudioSourcesScript asc;


        void Start()
        {
            movingEnabled = true;

            rb = GetComponent<Rigidbody>();

            UIEventPublisher.i.onUIActivating += FreezePlayer;
            UIEventPublisher.i.onUIFullyDeactivating += UnfreezePlayer;

            asc = FindAnyObjectByType<AudioSourcesScript>();

            playerAnimator = GetComponentInChildren<Animator>();
        }
        void OnDestroy()
        {
            UIEventPublisher.i.onUIActivating -= FreezePlayer;
            UIEventPublisher.i.onUIFullyDeactivating -= UnfreezePlayer;
        }

        Vector3 standing = new Vector3(0,0,0);

        void FixedUpdate()
        {
            if (movingEnabled)
            {
                float x = Input.GetAxis("Horizontal");
                float y = Input.GetAxis("Vertical");

                moveDir = new Vector3(x, 0, y);

                if (!moveDir.Equals(Vector3.zero) && animationState == EntityStates.AnimationState.STANDING)
                    ChangeState(EntityStates.AnimationState.WALKING);
                else if (moveDir.Equals(Vector3.zero) && animationState == EntityStates.AnimationState.WALKING)
                    ChangeState(EntityStates.AnimationState.STANDING);
                else
                    UpdateState();


                if (x > 0 && positionState == EntityStates.PositionState.FACING_LEFT)
                    ChangePositionState(EntityStates.PositionState.FACING_RIGHT);
                else if (x < 0 && positionState == EntityStates.PositionState.FACING_RIGHT)
                    ChangePositionState(EntityStates.PositionState.FACING_LEFT);
            }
        }


        private void ChangeState(EntityStates.AnimationState state)
        {
            animationState = state;
            switch (state)
            {
                case EntityStates.AnimationState.STANDING:
                    rb.velocity = Vector3.zero;
                    playerAnimator.SetBool("isWalking", false);
                    asc.StopPlayer(true);
                    //audioPlayerScript.Stop();
                    break;
                case EntityStates.AnimationState.WALKING:
                    playerAnimator.SetBool("isWalking", true);
                    asc.PlayPlayer(movingSound, true);
                    //audioPlayerScript.ChangeAndPlay(AudioSourcePlayer.WALKING_SOUND);
                    break;
            }
        }

        private void ChangePositionState(EntityStates.PositionState state)
        {
            positionState = state;
            playerAnimator.SetTrigger("rotating");
            //normalFlippingScript.flipNormals(state);
            switch (state)
            {
                case EntityStates.PositionState.FACING_RIGHT:
                    playerAnimator.SetBool("turnedRight", true);
                    break;
                case EntityStates.PositionState.FACING_LEFT:
                    playerAnimator.SetBool("turnedRight", false);
                    break;
            }
        }
        private void UpdateState()
        {
            switch (animationState)
            {
                case EntityStates.AnimationState.WALKING:
                    rb.velocity = moveDir * movementSpeed;
                    break;
            }
        }

        private void FreezePlayer(object sender, System.EventArgs e)
        {
            rb.velocity = Vector3.zero;
            ChangeState(EntityStates.AnimationState.STANDING);
            movingEnabled = false;
        }

        private void UnfreezePlayer(object sender, System.EventArgs e)
        {
            rb.velocity = Vector3.zero;
            movingEnabled = true;
        }
        public void LockMoving()
        {
            FreezePlayer(this, null);
        }

    }

}
