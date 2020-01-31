using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPressed : MonoBehaviour
{
    public void nextScene()
    {
        SceneManager.LoadScene("Overworld_Map");
    }
}
