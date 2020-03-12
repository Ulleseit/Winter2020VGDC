using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapSetup : MonoBehaviour
{
	public GameObject[] A;
	public GameObject[] E;
	public GameObject overworldCamera;
	List<Character> characters;
	public GameObject prefab;
	public GameObject combat;
    // Start is called before the first frame update
    void Start()
    {
		characters = overworldCamera.GetComponent<CharacterHandler>().CharactersList;
        for(int i = 0; i < characters.Count; i++)
		{
			Vector3 pos = A[i].transform.position;
			Character t = (characters[i]);
			t.transform.position = pos;
			t.name = characters[i].name;
			t.transform.SetParent(combat.transform);
			Destroy(A[i]);
			pos = E[i].transform.position;
			Destroy(E[i]);
			GameObject c = Instantiate(prefab);
			c.transform.position = pos;
			c.name = prefab.name;
			c.transform.SetParent(combat.transform);
		}
		for(int j = characters.Count; j < A.Length; j++)
		{
			Destroy(A[j]);
		}
		for(int k = characters.Count; k < E.Length; k++)
		{
			Destroy(E[k]);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
