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
    public int unlockOne;
    public int unlockTwo;
    void start(){
    	butt.interactable = false ;
    }
    void Update(){
    	active = characterArray.getCurrentChar().SkillTree[number].active;
    	butt.interactable = characterArray.getCurrentChar().SkillTree[number].unlocked;
    }
    public void InteractOn(){
    	butt.interactable = true;
    }
    public void InteractOff(){
    	butt.interactable = false;
    	characterArray.getCurrentChar().SkillTree[number].active = false;
    }
    public void activate(){
    	characterArray.getCurrentChar().activateSkill(number);
    	characterArray.getCurrentChar().SkillTree[unlockOne].unlocked = true;
    	characterArray.getCurrentChar().SkillTree[unlockTwo].unlocked = true;
    }
}
