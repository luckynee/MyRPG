using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private InventorySO inventory;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            inventory.Save();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            inventory.Load();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.inventoryContainer.Clear();
    }
}
