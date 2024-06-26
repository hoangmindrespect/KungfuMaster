using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour // Responsible for loading data of items to inventory
{
    public RectTransform inventoryPanel;
    public RectTransform scrollViewContent;

    public RectTransform characterPanel;

    InventoryUIItem itemContainer { get; set; }
    List<InventoryUIItem> itemUIList = new List<InventoryUIItem>();
    bool menuIsActive { get; set; }
    bool characterPanelIsActive { get; set; }
    Item currentSelectedItem { get; set; }
    // Use this for initialization
    void Awake()
    {
        UIEventHandler.OnItemAddedToInventory += ItemAdded;
        itemContainer = Resources.Load<InventoryUIItem>("UI/Item_Container");
        //Debug.Log(itemContainer.gameObject.name);
        inventoryPanel.gameObject.SetActive(false);
        characterPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuIsActive = !menuIsActive;
            inventoryPanel.gameObject.SetActive(menuIsActive);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            characterPanelIsActive = !characterPanelIsActive;
            characterPanel.gameObject.SetActive(characterPanelIsActive);
        }
    }

    public void ItemAdded(Item item)
    {
        InventoryUIItem emptyItem = Instantiate(itemContainer);
        //Debug.Log(emptyItem.gameObject.name);
        emptyItem.SetItem(item);
        itemUIList.Add(emptyItem);
        emptyItem.transform.SetParent(scrollViewContent);
    }
}
