using Core;
using Items;
using Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UI.Inventory.Item;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private GameObject itemSlotPrefab;

        [Header("Components")]
        [SerializeField] private Transform itemsParent;
        [SerializeField] private Button playButton;
        [SerializeField] private TextMeshProUGUI playButtonText;
        [SerializeField] private GameObject crosshair;
        [SerializeField] private ItemTooltip itemTooltip;

        [Header("Character items slot")]
        [SerializeField] private Transform helmetSlot;
        [SerializeField] private Transform necklaceSlot;
        [SerializeField] private Transform armorSlot;
        [SerializeField] private Transform bootsSlot;
        [SerializeField] private Transform ringSlot;
        [SerializeField] private Transform weaponSlot;

        [Header("Stats texts")]
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI healthPointsText;
        [SerializeField] private TextMeshProUGUI defenseText;
        [SerializeField] private TextMeshProUGUI lifeStealText;
        [SerializeField] private TextMeshProUGUI criticalStrikeChanceText;
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        [SerializeField] private TextMeshProUGUI movementSpeedText;
        [SerializeField] private TextMeshProUGUI luckText;

        private List<ItemData> items = new List<ItemData>();
        private PlayerController player;
        private List<Sprite> itemSprites = new List<Sprite>();

        private bool isOpened;
        private bool startedGame = false;

        private void OnDestroy()
        {
            playButton.onClick.RemoveListener(PlayButton_OnClick);
        }

        public void Initialize(List<ItemData> items, PlayerController player)
        {
            this.items = items;
            this.player = player;

            itemSprites = new List<Sprite>(Resources.LoadAll("UI/Items", typeof(Sprite)).Cast<Sprite>().ToList());

            playButton.onClick.AddListener(PlayButton_OnClick);

            InitializeItems();

            Show();
        }

        private void InitializeItems()
        {
            foreach (Transform child in itemsParent)
            {
                Destroy(child.gameObject);
            }

            foreach (var item in items)
            {
                ItemSlot newItem = Instantiate(itemSlotPrefab, itemsParent).GetComponent<ItemSlot>();
                Sprite itemSprite = itemSprites.FirstOrDefault(x => x.name == item.Name);

                newItem.OnClick += x => ItemSlot_OnClick(x, item);

                newItem.Initialize(itemSprite, itemTooltip, item);
            }

            UpdateCharacterStatsTexts();
        }

        private void ItemSlot_OnClick(ItemSlot item, ItemData itemData)
        {
            Transform newParent = null;

            if (!item.IsEquipped)
            {
                switch (itemData.Category)
                {
                    case "Armor":
                        newParent = armorSlot;
                        break;
                    case "Boots":
                        newParent = bootsSlot;
                        break;
                    case "Helmet":
                        newParent = helmetSlot;
                        break;
                    case "Necklace":
                        newParent = necklaceSlot;
                        break;
                    case "Ring":
                        newParent = ringSlot;
                        break;
                    case "Weapon":
                        newParent = weaponSlot;
                        break;
                    default:
                        break;
                }

                if (newParent.childCount > 0) // override character item slot
                {
                    ItemSlot itemToDequip = newParent.GetChild(0).GetComponent<ItemSlot>();

                    itemToDequip.transform.SetParent(itemsParent);
                    DequipItem(itemToDequip.ItemData);
                    itemToDequip.IsEquipped = false;
                }

                EquipItem(itemData);
            }
            else
            {
                newParent = itemsParent;

                DequipItem(itemData);
            }

            item.transform.SetParent(newParent);
            ResetItemSlotPosition(item);

            UpdateCharacterStatsTexts();
        }

        private void PlayButton_OnClick()
        {
            Hide();

            if (!startedGame) // condition for entering the game
            {
                playButtonText.text = "Back to dungeon";
                startedGame = true;
            }
        }

        private void EquipItem(ItemData itemData)
        {
            PlayerDataModel playerDataModel = player.DataModel;

            playerDataModel.ModifyDamage(itemData.Damage);
            playerDataModel.ModifyHealthPoints(itemData.HealthPoints);
            playerDataModel.ModifyDefense(itemData.Defense);
            playerDataModel.ModifyLifeSteal(itemData.LifeSteal);
            playerDataModel.ModifyCriticalStrikeChance(itemData.CriticalStrikeChance);
            playerDataModel.ModifyAttackSpeed(itemData.AttackSpeed);
            playerDataModel.ModifyMovementSpeed(itemData.MovementSpeed);
            playerDataModel.ModifyLuck(itemData.Luck);
        }

        private void DequipItem(ItemData itemData)
        {
            PlayerDataModel playerDataModel = player.DataModel;

            playerDataModel.ModifyDamage(-itemData.Damage);
            playerDataModel.ModifyHealthPoints(-itemData.HealthPoints);
            playerDataModel.ModifyDefense(-itemData.Defense);
            playerDataModel.ModifyLifeSteal(-itemData.LifeSteal);
            playerDataModel.ModifyCriticalStrikeChance(-itemData.CriticalStrikeChance);
            playerDataModel.ModifyAttackSpeed(-itemData.AttackSpeed);
            playerDataModel.ModifyMovementSpeed(-itemData.MovementSpeed);
            playerDataModel.ModifyLuck(-itemData.Luck);
        }

        public void InteractWithPanel()
        {
            if (!startedGame)
                return;

            if (isOpened)
                Hide();
            else
                Show();
        }

        private void Hide()
        {
            Time.timeScale = 1;

            GameManager.Instance.ChangeCurrentGameState(GameState.Playing);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            gameObject.SetActive(false);
            crosshair.SetActive(true);

            isOpened = false;
        }

        private void Show()
        {
            Time.timeScale = 0;

            GameManager.Instance.ChangeCurrentGameState(GameState.Paused);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            gameObject.SetActive(true);
            crosshair.SetActive(false);

            isOpened = true;
        }

        private void ResetItemSlotPosition(ItemSlot item)
        {
            RectTransform itemRectTransform = item.GetComponent<RectTransform>();

            itemRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            itemRectTransform.anchorMin = new Vector2(0.5f, 0.5f);

            itemRectTransform.anchoredPosition = Vector2.zero;
        }

        private void UpdateCharacterStatsTexts()
        {
            PlayerDataModel playerDataModel = player.DataModel;

            // I did percentages values with two decimal places because imo it looks much better than whole float number
            // and I didn't know if values should be rounded or not so I didn't round them
            damageText.text = $"DAMAGE: {playerDataModel.Damage}";
            healthPointsText.text = $"HP: {playerDataModel.HealthPoints}";
            defenseText.text = $"DEFENSE: {playerDataModel.Defense}";
            lifeStealText.text = $"LIFE STEAL: {playerDataModel.LifeSteal:0.00}%";
            criticalStrikeChanceText.text = $"CRIT CHANCE: {playerDataModel.CriticalStrikeChance:0.00}%";
            attackSpeedText.text = $"ATTACK SPEED: {playerDataModel.AttackSpeed:0.00}%";
            movementSpeedText.text = $"MOVE SPEED: {playerDataModel.MovementSpeed:0.00}%";
            luckText.text = $"LUCK: {playerDataModel.Luck:0.00}%";
        }
    }
}
