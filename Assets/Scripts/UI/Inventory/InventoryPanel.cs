using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject crosshair;

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
        foreach (var item in items)
        {
            ItemSlot newItem = Instantiate(itemSlotPrefab, itemsParent).GetComponent<ItemSlot>();
            newItem.Initialize(itemSprites.FirstOrDefault(x => x.name == item.Name));
        }
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
}
