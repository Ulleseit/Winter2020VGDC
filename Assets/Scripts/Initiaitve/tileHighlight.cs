using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileHighlight : MonoBehaviour
{
	public GameObject highlight;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 highlightStart = highlight.GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 position = createTileMouse();
	  highlight.GetComponent<Transform>().position = position;
    }

	Vector3 createTileMouse()
	{
		Vector3 positionA = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		positionA.z = 0;
		positionA.x = (float)(System.Math.Floor(positionA.x)+.5);
		positionA.y = (float)(System.Math.Floor(positionA.y)+.5);//Make mouse only count at center of each square, instead of true position
		return positionA;
	}
}
