using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadInput : MonoBehaviour
{
    private string input;
    public bool drakemode = false;
    public bool whalemode = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);

        if ((input == "DRAKE") || (input == "drake"))
        {
            whalemode = false;
            drakemode = true;
            Debug.Log("drake mode enabled: " + drakemode);
            Debug.Log("whale mode: " + whalemode);
        }

        if ((input == "WHALE") || (input == "whale"))
        {
            drakemode = false;
            whalemode = true;
            Debug.Log("whale mode enabled: " + whalemode);
            Debug.Log("drakemode: " + drakemode);
        }
    }
}
