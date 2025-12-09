using System;
using UnityEngine;

namespace EntityUtils.NPCUtils
{
public class GhostEventPublisher
{
    private static GhostEventPublisher instance;
        protected GhostEventPublisher() { }

        public static GhostEventPublisher i
        {
            get
            {
                if (instance == null) instance = new GhostEventPublisher();
                return instance;
            }
        }

        public delegate void OnGhostReacting(object sender, EventArgs e, GhostUtils.GhostReactions reaction);
        public event OnGhostReacting onGhostReacting;
        public void DoOnGhostReacting(object sender, EventArgs e, GhostUtils.GhostReactions reaction) => onGhostReacting?.Invoke(sender, e, reaction);
}
}