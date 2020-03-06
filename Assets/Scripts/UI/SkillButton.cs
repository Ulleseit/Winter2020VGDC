using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{ 
    public Button butt;
    public Sprite sprite;
    public CharacterHandler characterArray;
    public bool active;
    public int number;
    void start(){
    	butt.interactable = false ;
    }
    void Update(){
    	active = characterArray.getCurrentChar().SkillsActive[number];
    	butt.interactable = active;
    	
    }
    public void InteractOn(){
    	butt.interactable = true;
    	characterArray.getCurrentChar().SkillsActive[number] = true;
    }
    public void InteractOff(){
    	butt.interactable = false;
    	characterArray.getCurrentChar().SkillsActive[number] = false;
    }
}
