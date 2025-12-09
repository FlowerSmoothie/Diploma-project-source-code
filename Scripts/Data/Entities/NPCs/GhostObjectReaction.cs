using System;
using System.Collections.Generic;
using Overworld.Items;
using Overworld.Items.Containers;
using UnityEngine;
using Utils.Classes;

namespace EntityUtils.NPCUtils
{
    [Serializable]
    public class GhostObjectReaction
    {
        [SerializeField] private GameObject obj;
        [SerializeField] private List<PhraseToSay> phrases;
        [SerializeField] private GhostUtils.GhostReactions reaction;

        public PlainObjectInInventoryInfoContainer GetObject() { return obj.GetComponent<ObjectInInventoryInfo>().GetItemInfo(); }
        public List<PhraseToSay> GetPhrases() { return phrases; }
        public GhostUtils.GhostReactions GetReaction() { return reaction; }

    }
}