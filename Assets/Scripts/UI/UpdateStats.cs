using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStats : MonoBehaviour
{
	public Text Message; // Text Object to change
	public GameObject Character;
	//paste into any Object to acess any text to be changed
	public void Update()
	{
		Message.text = Character.GetComponent<CharacterHandler>().getCurrentChar().GetComponent<Character>().printStat();
	}
}
