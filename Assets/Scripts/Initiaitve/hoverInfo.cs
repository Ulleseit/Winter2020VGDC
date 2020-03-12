using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hoverInfo : MonoBehaviour
{
    public GameObject boardBackground;
    GameObject prevHit;
    bool moved = false;
    Text text;
    public GameObject characterDescription;
    // Start is called before the first frame update
    void Start()
    {
        text = characterDescription.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit) && hit.transform.tag == "Character" && (!moved || prevHit != hit.transform.gameObject))
        {
            prevHit = hit.transform.gameObject;
            boardBackground.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y+1, hit.transform.position.z);
            moved = true;
            string x = ("Name: " + hit.transform.gameObject.name + "\nHealth: " + hit.transform.gameObject.GetComponent<Character>().stats.currentHealth + "/" + hit.transform.gameObject.GetComponent<Character>().stats.health + "\nAP: " + hit.transform.gameObject.GetComponent<Character>().currentActionPoints + "/" + hit.transform.gameObject.GetComponent<Character>().maxActionPoints);
            text.text = x;
        }
        else if(Physics.Raycast(ray, out hit) && hit.transform.tag == "Enemy" && (!moved || prevHit != hit.transform.gameObject))
        {
            prevHit = hit.transform.gameObject;
            boardBackground.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y+1, hit.transform.position.z);
            moved = true;
            string x = ("Name: " + hit.transform.gameObject.name + "\nHealth: " + (int)(((float)hit.transform.gameObject.GetComponent<Character>().stats.currentHealth / (float)hit.transform.gameObject.GetComponent<Character>().stats.health) * 100f) + "%");
            text.text = x;
        }
        else if(moved && !Physics.Raycast(ray, out hit))
        {
            boardBackground.transform.position = new Vector3(-100, -100, -100);
            moved = false;
        }
    }
}
