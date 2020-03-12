using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class endCombat : MonoBehaviour
{
	public GameObject endCombatScreen;
	public GameObject prefab;
	public GameObject overWorldCamera;
	public GameObject overWorld;
	public GameObject combat1Camera;
	public GameObject combat1;
	public GameObject combat2Camera;
	public GameObject combat2;
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
		Debug.Log(characters.Length);
		if(enemies.Length == 0)
		{
			endCombatScreen.SetActive(true);
			
		}
		else if(characters.Length == 0)
		{
			Debug.Log("Lost battle");
		}
    }

	void endCombatButton()
	{
		if(combat1.active)
		{
			overWorldCamera.tag = ("MainCamera");
			overWorld.SetActive(true);
			combat1Camera.tag = ("Untagged");
			combat1.SetActive(false);
		}
		else
		{
			overWorldCamera.tag = ("MainCamera");
			overWorld.SetActive(true);
			combat2Camera.tag = ("Untagged");
			combat2.SetActive(false);
		}
	}
}
