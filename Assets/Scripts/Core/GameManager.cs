using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Items;
using Player;
using UI;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private PlayerBaseData playerBaseData;

        private List<ItemData> items = new List<ItemData>();
        private PlayerController player;
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

            player = FindObjectOfType<PlayerController>();
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
            PlayerDataModel newPlayer = new PlayerDataModel(playerBaseData.Damage,
                playerBaseData.HealthPoints,
                playerBaseData.Defense,
                playerBaseData.LifeSteal,
                playerBaseData.CriticalStrikeChance,
                playerBaseData.AttackSpeed,
                playerBaseData.MovementSpeed,
                playerBaseData.Luck);

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
}