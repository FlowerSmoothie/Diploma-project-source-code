using Misc;
using UnityEngine;
using UnityEngine.Video;
using Utils;


namespace Overworld.Items
{
    public class ZoomableItemInOverworldVideo : ZoomableItemInOverworld
    {
        [SerializeField] protected VideoClip videoToShowGreat;
        [SerializeField] protected VideoClip videoToShowNormal;
        [SerializeField] protected VideoClip videoToShowBad;

        public virtual VideoClip GetImage(int mentalHealth)
        {
            if (mentalHealth < 100 && mentalHealth >= greatDownBorder)
            {
                return videoToShowGreat;
            }
            else if (mentalHealth < greatDownBorder && mentalHealth >= normalDownBorder)
            {
                return videoToShowNormal;
            }
            else
            {
                return videoToShowBad;
            }
        }
        public override bool IsStatic() { return false; }
    }
}