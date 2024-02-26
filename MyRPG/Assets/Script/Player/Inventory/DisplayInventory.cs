using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private InventorySO inventory;

    [Header("Variable")]
    [SerializeField] private int X_START;
    [SerializeField] private int Y_START;
    [SerializeField] private int X_SPACE_BETWEEN_ITEM;
    [SerializeField] private int NUMBER_OF_COLUMN;
    [SerializeField] private int Y_SPACE_BETWEEN_ITEM;

    Dictionary<InventorySlot, GameObject> itemDisplayed = new Dictionary<InventorySlot, GameObject>();

    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        UpdateDisplay();
    }

    private void CreateDisplay()
    {
        for (int i = 0; i < inventory.inventoryContainer.Count; i++) 
        {
            var obj = Instantiate(inventory.inventoryContainer[i].item.prefab, Vector3.zero,Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventoryContainer[i].amountItem.ToString("n0");
            itemDisplayed.Add(inventory.inventoryContainer[i], obj);

        }
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < inventory.inventoryContainer.Count; i++)
        {
            if (itemDisplayed.ContainsKey(inventory.inventoryContainer[i]))
            {
                itemDisplayed[inventory.inventoryContainer[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventoryContainer[i].amountItem.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.inventoryContainer[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.inventoryContainer[i].amountItem.ToString("n0");
                itemDisplayed.Add(inventory.inventoryContainer[i], obj);
            }
        } 
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)),Y_START + (-Y_SPACE_BETWEEN_ITEM * (i/NUMBER_OF_COLUMN)), 0f); //Get Position for item to display the item in inventory
    }
}
