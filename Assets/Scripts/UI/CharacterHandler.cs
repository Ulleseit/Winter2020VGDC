using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public List<Character> CharactersList;
    public GameObject myPrefab;
    public int current = 0; 
    // Start is called before the first frame update
    void Start()
    {
        CharactersList = new List<Character>();
        createChar();
    }
    public void createChar()
    {
    	Character character = Instantiate(myPrefab, new Vector3(-100, -100, 0), Quaternion.identity).GetComponent<Character>();
		character.name = randomName();
    	character.GetComponent<Character>().Cname = CharactersList.Count.ToString("0000");
    	CharactersList.Add(character);
    }
    public void changeChar(){
        if(current + 2 > CharactersList.Count)
        {
        	current =0;
        }else{
        	current++;
        }
    }
    public Character getCurrentChar(){
    	return CharactersList[current];
    }
    public List<Character> getList(){
    	return CharactersList;
    }
	string randomName()
	{
		string[] names = new string[]{"Elyot", "Gawen", "Gilow"};
		return names[(int)Random.Range(0, 2.0f)];
	}
}
