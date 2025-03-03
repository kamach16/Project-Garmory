using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InventoryPanel inventoryPanel;
    [SerializeField] private WaitingScreen waitingScreen;

    private List<Item> items = new List<Item>();
    private Player player;

    async void Awake()
    {
        waitingScreen.Show();

        items = await GetAllItemsList();
        player = FindObjectOfType<Player>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InitializeSystems();

        waitingScreen.Hide();
    }

    private void InitializeSystems() // execute always after "GetAllItemsList" method
    {
        player.Initialize();
        inventoryPanel.Initialize(items, player);
    }

    private async Task<List<Item>> GetAllItemsList()
    {
        GameServerMock gameServerMock = new GameServerMock();
        string jsonString = await gameServerMock.GetItemsAsync();

        JObject rootObject = JObject.Parse(jsonString);
        List<Item> itemsArray = ((JArray)rootObject["Items"]).ToObject<List<Item>>();

        return itemsArray;
    }
}