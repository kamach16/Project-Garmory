using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Items;

namespace UI.Inventory.Item
{
    public class ItemTooltip : MonoBehaviour
    {
        [Header("Info")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI rarityText;
        [SerializeField] private TextMeshProUGUI categoryText;

        [Header("Stats")]
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI healthPointsText;
        [SerializeField] private TextMeshProUGUI defenseText;
        [SerializeField] private TextMeshProUGUI lifeStealText;
        [SerializeField] private TextMeshProUGUI criticalStrikeChanceText;
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        [SerializeField] private TextMeshProUGUI movementSpeedText;
        [SerializeField] private TextMeshProUGUI luckText;

        [Header("Components")]
        [SerializeField] private RectTransform rectTransform;

        private ItemData currentItemData;

        public void ShowTooltip(ItemData itemData, RectTransform itemTransform)
        {
            if (currentItemData != itemData)
                currentItemData = itemData;

            rectTransform.position = itemTransform.position - new Vector3(45, 40, 0); // add offset

            gameObject.SetActive(true);

            // I did percentages values with two decimal places because imo it looks much better than whole float number
            // and I didn't know if values should be rounded or not so I didn't round them
            nameText.text = $"{currentItemData.Name}";
            rarityText.text = $"RARITY: {currentItemData.Rarity}";
            categoryText.text = $"{currentItemData.Category}";
            damageText.text = $"DAMAGE: {currentItemData.Damage}";
            healthPointsText.text = $"HP: {currentItemData.HealthPoints}";
            defenseText.text = $"DEFENSE: {currentItemData.Defense}";
            lifeStealText.text = $"LIFE STEAL: {currentItemData.LifeSteal:0.00}%";
            criticalStrikeChanceText.text = $"CRIT CHANCE: {currentItemData.CriticalStrikeChance:0.00}%";
            attackSpeedText.text = $"ATTACK SPEED: {currentItemData.AttackSpeed:0.00}%";
            movementSpeedText.text = $"MOVE SPEED: {currentItemData.MovementSpeed:0.00}%";
            luckText.text = $"LUCK: {currentItemData.Luck:0.00}%";
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }
    }
}
