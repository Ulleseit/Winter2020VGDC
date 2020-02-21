using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeText : MonoBehaviour
{
	public Text Message; // Text Object to change
	public GameObject Character;
	//paste into any Object to acess any text to be changed
	public void updateText(string s){
		
		Message.text = s;
	}
}
