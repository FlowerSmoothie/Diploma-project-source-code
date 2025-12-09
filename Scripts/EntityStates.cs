using System;

namespace EntityUtils
{
    public class EntityStates
    {
        public enum PositionState { FACING_LEFT, FACING_RIGHT }
        public enum AnimationState { WALKING, STANDING }
    }

    public class EnemyStates
    {
        public enum BehaviourState { WANDERING, CHASING, AVOIDING, STANDING }
    }

    public class GhostUtils
    {
        public enum GhostReactions { NONE, PEACEFUL, IGNORING, ATTACKING }
        public enum GhostState { NOT_SPAWNED, WAITING_FOR_QUESTIONING, DONE }
    }

}

namespace ItemUtils
{
    public enum ItemStates { PERFECT, GOOD, BAD }
}

namespace ObjectUtils
{
    public enum DoorState { OPENED, CLOSED }
}