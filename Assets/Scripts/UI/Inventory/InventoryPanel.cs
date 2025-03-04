using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab;

    [Header("Components")]
    [SerializeField] private Transform itemsParent;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private ItemTooltip itemTooltip;

    [Header("Character items slot")]
    [SerializeField] private Transform helmetSlot;
    [SerializeField] private Transform necklaceSlot;
    [SerializeField] private Transform armorSlot;
    [SerializeField] private Transform bootsSlot;
    [SerializeField] private Transform ringSlot;
    [SerializeField] private Transform weaponSlot;

    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI healthPointsText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI lifeStealText;
    [SerializeField] private TextMeshProUGUI criticalStrikeChanceText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    [SerializeField] private TextMeshProUGUI movementSpeedText;
    [SerializeField] private TextMeshProUGUI luckText;

    private List<ItemData> items = new List<ItemData>();
    private Player player;
    private List<Sprite> itemSprites = new List<Sprite>();

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(PlayButton_OnClick);
    }

    public void Initialize(List<ItemData> items, Player player)
    {
        this.items = items;
        this.player = player;

        itemSprites = new List<Sprite>(Resources.LoadAll("UI/Items", typeof(Sprite)).Cast<Sprite>().ToList());

        playButton.onClick.AddListener(PlayButton_OnClick);

        InitializeItems();
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

    private void PlayButton_OnClick()
    {
        Hide();
    }

    private void Hide()
    {
        Time.timeScale = 1;

        GameManager.Instance.ChangeCurrentGameState(GameState.Playing);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameObject.SetActive(false);
        crosshair.SetActive(true);
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

        damageText.text = $"DAMAGE: {playerDataModel.Damage}";
        healthPointsText.text = $"HP: {playerDataModel.HealthPoints}";
        defenseText.text = $"DEFENSE: {playerDataModel.Defense}";
        lifeStealText.text = $"LIFE STEAL: {playerDataModel.LifeSteal}";
        criticalStrikeChanceText.text = $"CRIT CHANCE: {playerDataModel.CriticalStrikeChance}";
        attackSpeedText.text = $"ATTACK SPEED: {playerDataModel.AttackSpeed}";
        movementSpeedText.text = $"MOVE SPEED: {playerDataModel.MovementSpeed}";
        luckText.text = $"LUCK: {playerDataModel.Luck}";
    }
}
