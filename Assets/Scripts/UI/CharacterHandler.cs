using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public List<GameObject> CharactersList;
    public GameObject myPrefab;
    public int current = 0; 
    // Start is called before the first frame update
    void Start()
    {
        CharactersList = new List<GameObject>();
        createChar();
    }
    public void createChar()
    {
    	GameObject character = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    	character.GetComponent<Character>().Cname = CharactersList.Count.ToString("0000");
    	CharactersList.Add(character);
    	Debug.Log(CharactersList.Count);
    }
    public void changeChar(){
        if(current + 2 > CharactersList.Count)
        {
        	current =0;
        }else{
        	current++;
        }
    }
    public GameObject getCurrentChar(){
    	return CharactersList[current];
    }
    public List<GameObject> getList(){
    	return CharactersList;
    }
}
