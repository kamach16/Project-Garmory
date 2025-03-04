using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    [SerializeField] private InventoryPanel inventoryPanel;
    [SerializeField] private WaitingScreen waitingScreen;

    public WaitingScreen WaitingScreen => waitingScreen;

    public void Initialize(List<ItemData> items, Player player)
    {
        inventoryPanel.Initialize(items, player);
    }
}
