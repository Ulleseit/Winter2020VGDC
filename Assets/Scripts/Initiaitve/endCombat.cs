using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class endCombat : MonoBehaviour
{
	public GameObject endCombatScreen1;
	public GameObject prefab;
	public GameObject overWorldCamera;
	public GameObject overWorld;
	public GameObject combat1Camera;
	public GameObject combat1;
	public GameObject tilemap1;
	public GameObject combat2Camera;
	public GameObject combat2;
	public GameObject tilemap2;
	public GameObject endCombatScreen2;
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
		if(enemies.Length == 0)
		{
			if(combat1.active)
			{
				tilemap1.GetComponent<CharacterTurnMove>().running = false;
				endCombatScreen1.SetActive(true);
			}
			else
			{
				tilemap2.GetComponent<CharacterTurnMove>().running = false;
				endCombatScreen2.SetActive(true);
			}
		}
		else if(characters.Length == 0)
		{
			Debug.Log("Lost battle");
		}
    }

	void endCombatButton()
	{
		foreach(Character i in overWorldCamera.GetComponent<CharacterHandler>().CharactersList)
		{
			i.curInitiative = i.initiative;
			i.stats.currentHealth = i.stats.health;
			if(prefab.name == "Spider")
			{
				i.experience += 45;
			}
			else if(prefab.name == "Slime")
			{
				i.experience += 60;
			}
		}
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
