using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private List<Item> items = new List<Item>();
    private Player player;

    public void Initialize(List<Item> items, Player player)
    {
        this.items = items;
        this.player = player;
    }
}
