using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapSetup : MonoBehaviour
{
	public GameObject[] A;
	public GameObject[] E;
	public GameObject[] characters;
	public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < characters.Length; i++)
		{
			Vector3 pos = A[i].transform.position;
			GameObject t = Instantiate(characters[i]);
			t.transform.position = pos;
			t.name = characters[i].name;
			Destroy(A[i]);
			pos = E[i].transform.position;
			Destroy(E[i]);
			t = Instantiate(prefab);
			t.transform.position = pos;
			t.name = prefab.name;
		}
		for(int j = characters.Length; j < A.Length; j++)
		{
			Destroy(A[j]);
		}
		for(int k = characters.Length; k < E.Length; k++)
		{
			Destroy(E[k]);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
