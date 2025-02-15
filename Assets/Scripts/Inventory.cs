using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryUI;
    public List<InvItem> inventory = new List<InvItem>();

    public void AddInvItem(string itemName, Sprite itemImage)
    {

        InvItem newItem = new InvItem();
        newItem.invItemImage = itemImage;
        newItem.invItemName = itemName;
        inventory.Add(newItem);

        int emptyInvSlot = 0;
        for (int i = 0; i < inventoryUI.transform.childCount; i++)
        {
            if (inventoryUI.transform.GetChild(i).GetComponent<Image>().sprite == null)
            {
                emptyInvSlot = i;
                print("i = " + i);
                continue;
            }
        }
        
        GameObject inventoryIcon = inventoryUI.transform.GetChild(emptyInvSlot).gameObject;
        inventoryIcon.GetComponent<Image>().sprite = newItem.invItemImage;
        inventoryIcon.gameObject.SetActive(true);
        print("Picked up " + newItem.invItemName);
    }
}

public class InvItem
{
    public string invItemName;
    public Sprite invItemImage;
}
