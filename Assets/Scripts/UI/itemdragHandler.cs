using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class itemdragHandler : MonoBehaviour , IDragHandler, IEndDragHandler
{
    public UIItem current;
    //public GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
    public void OnDrag(PointerEventData eventData)
    {
    	transform.position = Input.mousePosition;
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
    	var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
    	transform.localPosition = Vector3.zero;

	pointerEventData.position = Input.mousePosition;
    	
	
    	foreach (var hit in results)
	{
	    // If we found slot.
	    var slot = hit.gameObject.GetComponent<UIItem>();
	    if(hit.gameObject.tag == "ItemSlot")
	    {
		// We should check if we can place ourselves​ there.
		if (!slot.filled)
		{
		    Debug.Log("OK");
		    // Swapping references.
		    slot.item = current.item;
		    current.item = null;
		}
	    }	
	    	
	}        
    }
}
