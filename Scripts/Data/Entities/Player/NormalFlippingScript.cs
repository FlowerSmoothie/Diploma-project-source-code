using System.Collections;
using UnityEngine;

namespace EntityUtils.PlayerUtils.Graphics
{
public class NormalFlippingScript : MonoBehaviour
{

    [SerializeField] private Renderer _renderer;

    [SerializeField] private int amount = 1;

    private void OnEnable()
    {
        flipNormals();
    }

    private void flipNormals()
    {
        /*if(playerPositionState == EntityStates.PositionState.FACING_LEFT) _renderer.sharedMaterial.SetFloat("_FlipNormals",  1);
        else _renderer.sharedMaterial.SetFloat("_FlipNormals",  -1);*/
        _renderer.sharedMaterial.SetFloat("_FlipNormals",  amount);
        //Debug.Log(_renderer.sharedMaterial.GetFloat("_FlipNormals"));
    }
}
}
