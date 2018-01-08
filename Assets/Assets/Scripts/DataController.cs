﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour {

    // Use this for initialization
    private string Path = "Assets/Assets/Scores/score";

    public void WriteScores(string name, int score)
    {
        if (name == "")
        {
            name = "UnknownPlayer";
        }
        StreamWriter sr = new StreamWriter(Path, true);
        sr.Write(name+">"+score.ToString()+"\n");
        sr.Close();
    }
}