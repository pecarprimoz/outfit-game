using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOBChecker : MonoBehaviour {

    public void checkIfOOB(float x, float y, float z)
    {
        float yMax = Camera.main.orthographicSize;
        float yMin = -Camera.main.orthographicSize;
        float xMax = Camera.main.orthographicSize * Screen.width / Screen.height;
        float xMin = -Camera.main.orthographicSize * Screen.width / Screen.height;
        if (x > xMax)
        {
            transform.position += new Vector3(-x * 2, 0, 0);
        }
        else if (x < xMin)
        {
            transform.position -= new Vector3(x * 2, 0, 0);
        }
        else if (y > yMax)
        {
            transform.position += new Vector3(0, -y * 2, 0);
        }
        else if (y < yMin)
        {
            transform.position -= new Vector3(0, y * 2, 0);
        }
    }
}
