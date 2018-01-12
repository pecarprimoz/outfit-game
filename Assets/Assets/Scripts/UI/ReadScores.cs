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
        if (!File.Exists("score")) { 
            System.IO.FileStream oFileStream = null;
            oFileStream = new System.IO.FileStream("score", System.IO.FileMode.Create);
            oFileStream.Close();
        }
        Path = "score";
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
        scores.Sort((pair1, pair2) => pair1.CompareTo(pair2));

        int cnt = 10;
        foreach (string s in scores)
        {
            if (cnt < 0)
                break;
            string[] tmp = s.Split('>');
            int c = 0;
            foreach (string str in tmp)
                c++;
            text.text += tmp[0] + " - " + tmp[c-1]+"\n";
            cnt--;
        }
        sr.Close();
    }
	
	
}
