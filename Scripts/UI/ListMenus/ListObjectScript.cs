using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ListObjectScript : MonoBehaviour
{

    protected int objectID;
    [SerializeField] protected Image image;
    protected string itemName;
    [SerializeField] protected TextMeshProUGUI text;

    public void OnClick()
    {
        ObjectInListEventPublisher.i.DoOnButonClicking(this, null, objectID);
    }


    public void SetID(int ID) { objectID = ID; }
    public int GetID() { return objectID; }


    public void SetEverything(int id, string text, Sprite sprite)
    {
        image.sprite = sprite;
        itemName = text;
        this.text.text = itemName;
        objectID = id;
    }
    public string GetName() { return itemName; }

}