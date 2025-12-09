using System;
using UnityEngine;
using Utils;

namespace Misc.UI
{
public abstract class UIMenuScript : MonoBehaviour
{
    [SerializeField] protected Animator animator;

    protected virtual void ActivateMenu(object sender, EventArgs e)
    {
        Debug.Log("aaa");
        animator.SetTrigger(Consts.DEFAULT_TRIGGER_CONST);
    }
}
}