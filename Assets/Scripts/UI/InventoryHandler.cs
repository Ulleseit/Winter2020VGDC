using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryHandler : MonoBehaviour
{ 
 	public const int numItemSlot = 24;
	private List<Item> ItemList;
	public GameObject myPrefab;
	void Start()
	{
		ItemList = new List<Item>();

		// Tests
		// AddItem(new Item { name = "Sword", itemType = Item.ItemType.Weapon, amount = 1 });
		// AddItem(new Item { name = "Health Potion", itemType = Item.ItemType.Potion, amount = 5 });
		// RemoveItem(new Item { name = "Sword", itemType = Item.ItemType.Weapon, amount = 1 });
	}

        public void CreateItem() 
    	{
    		Item item = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Item>();
		item.GetComponent<Item>().name = ItemList.Count.ToString("0000");
		this.AddItem(item);
    		Debug.Log(ItemList.Count);
	}
        public void AddItem(Item item) 
	{
		if (ItemList.Count <= numItemSlot)
		{
			var Invenlist = gameObject.GetComponent<UiInventory>().Inventory;
			ItemList.Add(item);
			for(int i = 0; i < Invenlist.Count; i++){
				if(Invenlist[i].GetComponent<UISlot>().filled == false)
				{
					Invenlist[i].GetComponent<UISlot>().item = item;	
					item.GetComponent<itemdragHandler>().current = Invenlist[i].GetComponent<UISlot>();
					Invenlist[i].GetComponent<UISlot>().filled = true;
					Invenlist[i].GetComponent<UISlot>().item.transform.parent = Invenlist[i].GetComponent<UISlot>().gameObject.transform;
		    			Invenlist[i].GetComponent<UISlot>().item.transform.position = Invenlist[i].GetComponent<UISlot>().gameObject.transform.position;
					break;
				}
			}
				
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
