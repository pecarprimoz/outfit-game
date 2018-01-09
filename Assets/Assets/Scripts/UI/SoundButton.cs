using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {

	// Use this for initialization 
	void Start () {
        if (PlayerPrefs.GetString("sound").Equals("on"))
        {
            GetComponent<Button>().GetComponent<Image>().color = Color.red;
        }
        else if (PlayerPrefs.GetString("sound").Equals("off"))
        {
            GetComponent<Button>().GetComponent<Image>().color = Color.white;
        }
            GetComponent<Button>().onClick.AddListener(VolumePreset);
    }

    void VolumePreset()
    {
        if (PlayerPrefs.GetString("sound").Equals("on")) {
            PlayerPrefs.SetString("sound", "off");
            GetComponent<Button>().GetComponent<Image>().color = Color.white;
        }
        else if (PlayerPrefs.GetString("sound").Equals("off"))
        {
            GetComponent<Button>().GetComponent<Image>().color = Color.red;
            PlayerPrefs.SetString("sound", "on");
        }
        PlayerPrefs.Save();
    }
}
