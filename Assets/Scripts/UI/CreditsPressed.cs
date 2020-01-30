using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsPressed : MonoBehaviour
{
	public void toCredits(){
		SceneManager.LoadScene("Credits");
	}
}
