using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCurrentSelected : MonoBehaviour
{
    Text text;
    GameObject selected;
    // Start is called before the first frame update
    void Start()
    {
      text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        selected = Camera.main.GetComponent<CharacterTurnMove>().selected;
        text.text = ("Current Character: " + selected);
    }
}
