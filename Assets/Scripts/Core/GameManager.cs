using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }

    private List<ItemData> items = new List<ItemData>();
    private Player player;
    private WaitingScreen waitingScreen;
    private PanelsManager panelsManager;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    async void Start()
    {
        panelsManager = FindObjectOfType<PanelsManager>();
        waitingScreen = panelsManager.WaitingScreen;

        waitingScreen.Show();

        player = FindObjectOfType<Player>();
        items = await GetAllItemsList();

        InitializeSystems();

        waitingScreen.Hide();
    }

    public void ChangeCurrentGameState(GameState newGameState)
    {
        CurrentGameState = newGameState;
    }

    public bool IsAtThisGameState(GameState targetGameState)
    {
        return CurrentGameState == targetGameState;
    }

    private void InitializeSystems() // execute always after "GetAllItemsList" method
    {
        player.Initialize();
        panelsManager.Initialize(items, player);
    }

    private async Task<List<ItemData>> GetAllItemsList()
    {
        GameServerMock gameServerMock = new GameServerMock();
        string jsonString = await gameServerMock.GetItemsAsync();

        JObject rootObject = JObject.Parse(jsonString);
        List<ItemData> itemsArray = ((JArray)rootObject["Items"]).ToObject<List<ItemData>>();

        return itemsArray;
    }
}