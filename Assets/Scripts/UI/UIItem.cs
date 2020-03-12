using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
	public Item item;
	private Image spriteImage;
	public bool filled;
	public void Awake()
	{
		spriteImage = GetComponent<Image>();
	}
}
