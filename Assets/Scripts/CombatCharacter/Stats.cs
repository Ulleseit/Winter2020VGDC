﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int initiative;
    public int maxActionPoints;
    public int currentActionPoints;
    void Start()
    {

    }
    void Update()
    {

    }
    public void reduceInitiative()//Used to reduce initiative at the end of the turn, should initiatives ever end up higher than 100, change value to minus 1000 unless high initiative means taking an extra turn
    {
      initiative -= 100;
    }
    public void reduceActionPoints(int n)//Used to reduce action points, given an int to reduce by
    {
      currentActionPoints -= n;
    }
}