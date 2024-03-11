using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private InventorySO inventory;
    
    public GameObject inventoryPrefab;

    [Header("Variable")]
    [SerializeField] private int X_START;
    [SerializeField] private int Y_START;
    [SerializeField] private int X_SPACE_BETWEEN_ITEM;
    [SerializeField] private int NUMBER_OF_COLUMN;
    [SerializeField] private int Y_SPACE_BETWEEN_ITEM;

    Dictionary<GameObject ,InventorySlot> itemDisplayed = new Dictionary<GameObject, InventorySlot>();
    
    private void Start()
    {
        CreateSlotDisplay();
    }

    private void Update()
    {
        UpdateSlotDislay();
    }

    public void UpdateSlotDislay()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemDisplayed)
        {
            if(_slot.Value.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _slot.Value.amountItem == 1? "": _slot.Value.amountItem.ToString("n0");
            } else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    private void CreateSlotDisplay()
    {
        itemDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.inventoryContainer.itemList.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            itemDisplayed.Add(obj, inventory.inventoryContainer.itemList[i]);
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),Y_START + (-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COLUMN)), 0f); //Get Position for item to display the item in inventory
    }
    
}
