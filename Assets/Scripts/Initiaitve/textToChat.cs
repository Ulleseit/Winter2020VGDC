using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textToChat : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
	int maxLines = 8;
	int curLines = 0;
	string x;
    void Start()
    {
      text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        x = text.text;
    }
	
	public void processLine(string y)
	{
		x += (y + "\n");
		curLines += 1;
		Debug.Log(curLines);
		Debug.Log(maxLines);
		if(curLines > maxLines)
		{	
			Debug.Log(x);
			int i = x.IndexOf("\n");
			x = x.Substring(i+1);
			curLines -= 1;
			Debug.Log(x);
		}
		text.text = x;
	}
}
