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
	}

    public void AddItem(Item item) 
	{
		ItemList.Add(item);
	}

    public void RemoveItem (Item item)
    {
    	for (int i = 0; i < numItemSlot; i++)
		{
		    if (ItemList[i] == null)
		    {
				break;
		    }
			else if (ItemList[i].name == item.name)
			{
				ItemList.RemoveAt(i);
				break;
			}
		}
	}
   
}
