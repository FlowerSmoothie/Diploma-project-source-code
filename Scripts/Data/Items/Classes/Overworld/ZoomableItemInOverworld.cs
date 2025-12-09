using Overworld.ObjectsAndNPCs;
using UnityEngine;
using Utils;

namespace Overworld.Items
{
    public abstract class ZoomableItemInOverworld : OverworldItemInfo
    {

        [SerializeField] float howLongToWaitBeforeDescriptionSHowing = 2f;

        public override float DelayedInteracting() { return howLongToWaitBeforeDescriptionSHowing; }

        public override bool IsZoomable() { return true; }

        public abstract bool IsStatic();

    }
}