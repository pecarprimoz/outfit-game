using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour {
    
    //Fixed path, save scores in assets
    private string Path = "score";
    void Awake()
    {
        if (!File.Exists("score"))
        {
            System.IO.FileStream oFileStream = null;
            oFileStream = new System.IO.FileStream("score", System.IO.FileMode.Create);
            oFileStream.Close();
        }
    }

    //Simple script to save scores
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
