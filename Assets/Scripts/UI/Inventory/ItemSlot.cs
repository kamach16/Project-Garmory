using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private RectTransform rectTransform;

    private ItemTooltip itemTooltip;
    private ItemData itemData;

    public void Initialize(Sprite sprite, ItemTooltip itemTooltip, ItemData itemData)
    {
        this.itemTooltip = itemTooltip;
        this.itemData = itemData;

        itemImage.sprite = sprite;
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
