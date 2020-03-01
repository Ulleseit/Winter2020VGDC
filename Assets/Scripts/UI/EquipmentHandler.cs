using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EquipmentHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public static GameObject itemDragged;
	Vector3 startPos;
	public void OnBeginDrag(PointerEventData data)
	{
		itemDragged = gameObject;
		startPos = gameObject.transform.position;
	}
	public void OnDrag(PointerEventData data)
	{
		transform.position = Input.mousePosition;
	}
	public void OnEndDrag(PointerEventData data)
	{
		itemDragged = null;
		transform.position = startPos;
	}
}
