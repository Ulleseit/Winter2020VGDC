using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenPanel : MonoBehaviour
{
    public GameObject Panel;
    
    public void OpenUpPanel(){
    	Panel.gameObject.SetActive(true);
    }
}
