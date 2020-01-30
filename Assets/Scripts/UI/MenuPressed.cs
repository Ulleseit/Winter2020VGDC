using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPressed : MonoBehaviour
{
	public void toMenu(){
		SceneManager.LoadScene("Menu");
	}
}
