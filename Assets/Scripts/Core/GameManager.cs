using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

public class GameManager : Singleton<GameManager>
{
    [Header("Base Player Stats")]
    [SerializeField] private int damage;
    [SerializeField] private int healthPoints;
    [SerializeField] private int defense;
    [SerializeField] private float lifeSteal;
    [SerializeField] private float criticalStrikeChance;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float luck;

    private List<ItemData> items = new List<ItemData>();
    private Player player;
    private WaitingScreen waitingScreen;
    private PanelsManager panelsManager;


    public GameState CurrentGameState { get; private set; }

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
        InitializePlayer();
        panelsManager.Initialize(items, player);
    }

    private void InitializePlayer()
    {
        PlayerDataModel newPlayer = new PlayerDataModel(damage,
            healthPoints,
            defense,
            lifeSteal,
            criticalStrikeChance,
            attackSpeed,
            movementSpeed,
            luck);

        player.Initialize(newPlayer);
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