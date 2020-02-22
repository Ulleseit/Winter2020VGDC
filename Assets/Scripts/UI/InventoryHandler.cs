using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryHandler : MonoBehaviour
{ 
 	public const int numItemSlot = 24;
	private List<Item> ItemList;

	void Start()
	{
		ItemList = new List<Item>();

		// Tests
		// AddItem(new Item { name = "Sword", itemType = Item.ItemType.Weapon, amount = 1 });
		// AddItem(new Item { name = "Health Potion", itemType = Item.ItemType.Potion, amount = 5 });
		// RemoveItem(new Item { name = "Sword", itemType = Item.ItemType.Weapon, amount = 1 });
	}
	
    public void AddItem(Item item) 
	{
		if (ItemList.Count <= numItemSlot)
		{
			ItemList.Add(item);
		}
		else
		{
			Debug.Log("Inventory Full!");
		}
		
	}

    public void RemoveItem (Item item)
    {
    	for (int i = 0; i < numItemSlot; i++)
		{
		    if (ItemList[i] == null)
		    {
				break;
		    }
			else if (ItemList[i].itemType == item.itemType && ItemList[i].name == item.name)
			{
				if (ItemList[i].amount > 1)
				{
					ItemList[i].amount -= 1;
				}
				else
				{
					ItemList.RemoveAt(i);
				}
				break;
			}
		}
	}
   
}
