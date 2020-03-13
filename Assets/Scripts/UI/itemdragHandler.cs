using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class itemdragHandler : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UISlot current;
     public static GameObject itemBeingDragged;
     GameObject canvas;
    //public GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
    public void OnBeginDrag(PointerEventData eventData) {
             itemBeingDragged = gameObject;
             canvas = GameObject.FindGameObjectWithTag("UI Canvas");
             transform.parent = canvas.transform;
    }
    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
    	var results = new List<RaycastResult>();
    	transform.localPosition = Vector3.zero;
    	pointerEventData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointerEventData, results);

	
    	
    	for (int i = 0; i < results.Count; i++)
	{
	   Debug.Log(results[i].gameObject.name + " " + i);
	}
	
    	foreach (var hit in results)
	{
	    // If we found slot.
	    //Debug.Log(hit.gameObject.tag);
	    var slot = hit.gameObject.GetComponent<UISlot>();
	    if(hit.gameObject.tag == "ItemSlot")
	    {
		// We should check if we can place ourselves​ there.
		if (!slot.filled)
		{
		    Debug.Log("OK");
		    // Swapping references.
		    slot.item = current.item;
		    current.item = null;
		    transform.parent = hit.gameObject.transform;
		    transform.position = transform.parent.position;
		}
	    }	
	    	
	}        
    }
}
