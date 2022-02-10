using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public Image spriteImage;
    public UnityEvent<UIItem> OnClick;
    public UnityEvent<UIItem> OnHoverStart;
    public UnityEvent<UIItem> OnHoverEnd;


    private void Awake()
    {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Slot was clicked." + eventData);
        OnClick?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverStart?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverEnd?.Invoke(this);
    }
}
