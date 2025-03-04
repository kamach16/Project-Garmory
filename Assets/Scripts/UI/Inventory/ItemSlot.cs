using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private RectTransform rectTransform;

    private ItemTooltip itemTooltip;
    private ItemData itemData;

    private bool isEquipped = false;

    public bool IsEquipped
    {
        get { return isEquipped; }
        set { isEquipped = value; }
    }

    public event Action<ItemSlot> OnClick;

    public ItemData ItemData => itemData;

    public void Initialize(Sprite sprite, ItemTooltip itemTooltip, ItemData itemData)
    {
        this.itemTooltip = itemTooltip;
        this.itemData = itemData;

        itemImage.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);

        isEquipped = !isEquipped;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemTooltip.ShowTooltip(itemData, rectTransform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemTooltip.HideTooltip();
    }
}
