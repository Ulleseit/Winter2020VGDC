using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHander : MonoBehaviour
{
    public GameObject CharArray;

    void Update()
    {
            gameObject.GetComponent<Image>().sprite = CharArray.GetComponent<CharacterHandler>().getCurrentChar().GetComponent<Character>().CharSprite;
    }
}
