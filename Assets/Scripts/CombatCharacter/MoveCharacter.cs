using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {

    }
    public void move(float x, float y)
    {
      this.transform.position = new Vector3(x, y, 0);
    }
}
