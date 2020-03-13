using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquip : MonoBehaviour
{
	public UISlot slot;
	public string name;
	public CharacterHandler character;
	public void Update(){
		if(slot.item != null)
		{
			if(!slot.filled){
				character.getCurrentChar().equipItem(name, slot.item);
				slot.filled = true;
			}
		}
	}	
}
