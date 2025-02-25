using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryUI; // The parent GameObject for inventory slots
    public List<InvItem> inventory = new List<InvItem>(); // List to store inventory items

    // Method to add an item to the inventory
    public void AddInvItem(string itemName, Sprite itemImage)
    {
        // Debug: Check if itemImage is null
        if (itemImage == null)
        {
            Debug.LogError("Item image is null!");
            return;
        }

        // Debug: Check if inventoryUI is null
        if (inventoryUI == null)
        {
            Debug.LogError("Inventory UI is not assigned!");
            return;
        }

        // Create a new InvItem and set its properties
        InvItem newItem = new InvItem
        {
            invItemImage = itemImage,
            invItemName = itemName
        };

        // Add the item to the inventory list
        inventory.Add(newItem);

        // Find an empty slot in the inventory UI
        for (int i = 0; i < inventoryUI.transform.childCount; i++)
        {
            Transform slot = inventoryUI.transform.GetChild(i);
            Image slotImage = slot.GetComponent<Image>();

            // Debug: Check if slotImage is null
            if (slotImage == null)
            {
                Debug.LogError("Slot " + i + " is missing an Image component!");
                continue;
            }

            // If the slot is empty, add the item's image to it
            if (slotImage.sprite == null)
            {
                slotImage.sprite = newItem.invItemImage;
                slotImage.gameObject.SetActive(true); // Ensure the slot is visible
                print("Picked up " + newItem.invItemName);
                return; // Exit after adding the item to the first empty slot
            }
        }

        // If no empty slot is found, log a warning
        Debug.LogWarning("Inventory is full!");
    }
}

// Class to represent an inventory item
public class InvItem
{
    public string invItemName; // Name of the item
    public Sprite invItemImage; // Image of the item
}
