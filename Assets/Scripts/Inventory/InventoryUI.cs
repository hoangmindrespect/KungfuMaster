using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour // Responsible for loading data of items to inventory
{
    public RectTransform inventoryPanel;
    public RectTransform scrollViewContent;
    bool menuIsActive { get; set; }

    public RectTransform characterPanel;
    bool characterPanelIsActive { get; set; }

    InventoryUIItem itemContainer { get; set; }
    List<InventoryUIItem> itemUIList = new List<InventoryUIItem>();
 
    Item currentSelectedItem { get; set; }

    public RectTransform shopPanel;
    bool shopPanelIsActive { get; set; }
    // Use this for initialization
    void Awake()
    {
        UIEventHandler.OnItemAddedToInventory += ItemAdded;
        itemContainer = Resources.Load<InventoryUIItem>("UI/Item_Container");
        //Debug.Log(itemContainer.gameObject.name);
    }
    private void Start()
    {
        inventoryPanel.gameObject.SetActive(false);
        characterPanel.gameObject.SetActive(false);
        shopPanel.gameObject.SetActive(false);
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
        if (Input.GetKeyUp(KeyCode.P))
        {
            shopPanelIsActive = !shopPanelIsActive;
            shopPanel.gameObject.SetActive(shopPanelIsActive);
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
