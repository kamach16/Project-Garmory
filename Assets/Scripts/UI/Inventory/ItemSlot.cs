using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    public void Initialize(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }
}
