using UnityEngine;

namespace Overworld.Items
{
public class ZoomableItemInOverworldImage : ZoomableItemInOverworld
{
    [SerializeField] Sprite imageToShowGreat;
    [SerializeField] Sprite imageToShowNormal;
    [SerializeField] Sprite imageToShowBad;

    public Sprite GetImage(int mentalHealth)
    {
        if(mentalHealth <= 100 && mentalHealth >= greatDownBorder)
        {
            return imageToShowGreat;
        }
        else if(mentalHealth < greatDownBorder && mentalHealth >= normalDownBorder)
        {
            return imageToShowNormal;
        }
        else
        {
            return imageToShowBad;
        }
    }
    public override bool IsStatic() { return true; }
}
}