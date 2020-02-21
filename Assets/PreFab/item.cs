using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : ScriptableObject
{
	public string name;
	public Sprite icon;
	public void Item(string nam, Sprite ic){
		this.name = nam;
		this.icon = ic;
	}
}
