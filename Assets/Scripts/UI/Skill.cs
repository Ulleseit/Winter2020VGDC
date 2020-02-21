using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public Button butt;
    bool active = false;
    public void InteractOn(){
    	butt.interactable = true;
    }
    public void InteractOff(){
    	butt.interactable = false;
    }
}
