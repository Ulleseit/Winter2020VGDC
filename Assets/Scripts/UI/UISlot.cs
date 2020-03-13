using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
	public Item item;
	public GameObject Imagehold;
	private Image spriteImage;
	public bool filled;
	public void Awake()
	{
		spriteImage = GetComponent<Image>();
	}
	public void Update(){
		if(item != null)
		{
			Imagehold.GetComponent<Image>().sprite = item.sprite;
		}
	}
}
