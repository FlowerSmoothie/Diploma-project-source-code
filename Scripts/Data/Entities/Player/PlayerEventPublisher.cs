using System;
using System.Collections.Generic;

public class PlayerEventPublisher
{
    private static PlayerEventPublisher instance;
    protected PlayerEventPublisher() { }

    public static PlayerEventPublisher i
    {
        get
        {
            if (instance == null) instance = new PlayerEventPublisher();
            return instance;
        }
    }

    public delegate void OnTryingToInteractWithObects(object sender, EventArgs e, List<Tuple<string, int>> names, bool needsCancel);
    public event OnTryingToInteractWithObects onTryingToInteractWithObects;
    public void DoOnTryingToInteractWithObects(object sender, EventArgs e, List<Tuple<string, int>> names, bool needsCancel = true) => onTryingToInteractWithObects?.Invoke(sender, e, names, needsCancel);

    public delegate void OnCollectingAnItem(object sender, EventArgs e, int ID);
    public event OnCollectingAnItem onCollectingAnItem;
    public void DoOnCollectingAnItem(object sender, EventArgs e, int ID) => onCollectingAnItem?.Invoke(sender, e, ID);

    public delegate void OnHealthChanged(object sender, EventArgs e, int health);
    public event OnHealthChanged onHealthChanged;
    public void DoOnHealthChanged(object sender, EventArgs e, int health) => onHealthChanged?.Invoke(sender, e, health);

    public delegate void OnItemFound(object sender, EventArgs e);
    public event OnItemFound onItemFound;
    public void DoOnItemFound(object sender, EventArgs e) => onItemFound?.Invoke(sender, e);

    public delegate void OnItemDefound(object sender, EventArgs e);
    public event OnItemFound onItemDefound;
    public void DoOnItemDefound(object sender, EventArgs e) => onItemDefound?.Invoke(sender, e);

    public delegate void OnHideUI(object sender, EventArgs e);
    public event OnHideUI onHideUI;
    public void DoOnHideUI(object sender, EventArgs e) => onHideUI?.Invoke(sender, e);

    public delegate void OnRetrieveUI(object sender, EventArgs e);
    public event OnRetrieveUI onRetrieveUI;
    public void DoOnRetrieveUI(object sender, EventArgs e) => onRetrieveUI?.Invoke(sender, e);


    public delegate void OnMedicineTook(object sender, EventArgs e, int health);
    public event OnMedicineTook onMedicineTook;
    public void TakeMedicine(object sender, EventArgs e, int health) => onMedicineTook?.Invoke(sender, e, health);

}