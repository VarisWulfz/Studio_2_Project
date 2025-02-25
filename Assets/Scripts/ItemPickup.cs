using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName; // Name of the item
    public Sprite itemImage; // Image of the item for the inventory UI

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.name); // Log the collided object
        if (other.CompareTag("Player")) // Check if the player collided with the item
        {
            Debug.Log("Player collided with item: " + itemName);
            // Get the Inventory component from the player
            Inventory inventory = other.GetComponent<Inventory>();

            if (inventory != null)
            {
                // Add the item to the inventory
                inventory.AddInvItem(itemName, itemImage);

                // Destroy the item in the game world
                Destroy(gameObject);
            }
        }
    }
}