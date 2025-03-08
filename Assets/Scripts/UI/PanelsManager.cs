using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UI.Inventory;

namespace UI
{
    public class PanelsManager : MonoBehaviour
    {
        [SerializeField] private InventoryPanel inventoryPanel;
        [SerializeField] private WaitingScreen waitingScreen;

        public WaitingScreen WaitingScreen => waitingScreen;

        public void Initialize(List<ItemData> items, PlayerController player)
        {
            inventoryPanel.Initialize(items, player);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
                inventoryPanel.InteractWithPanel();
        }
    }
}
