using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReadScores : MonoBehaviour {

    // Use this for initialization
    private Text text;
    private string Path;

    void Awake () {
        Path = "Assets/Assets/Scores/score";
        text = GetComponent<Text>();
        SetHighScores();
    }
    void SetHighScores()
    {
        text.text = "";
        StreamReader sr = new StreamReader(Path, true);
        List<string> scores = new List<string>();
        while (sr.Peek() >= 0)
        {
            scores.Add(sr.ReadLine());
        }
        int cnt = 10;
        foreach(string s in scores)
        {
            if (cnt < 0)
                break;
            string[] tmp = s.Split('>');
            text.text += tmp[0] + " - " + tmp[1]+"\n";
        }
        sr.Close();
    }
	
	
}
