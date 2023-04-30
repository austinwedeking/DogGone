using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    private string input;
    public bool drakemode = false;
    public bool whalemode = false;
    public bool original = true;

    public GameObject canvas;
    public GameObject whaleBG;
    public GameObject drakeBG;
    //public GameObject currentScreen;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Background");

        whaleBG = GameObject.Find("WhaleBG");

        drakeBG = GameObject.Find("DrakeBG");

        //currentScreen = canvas;

        audioManager = FindObjectOfType<AudioManager>();
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

            original = false;

            drakemode = true;

            Debug.Log("drake mode enabled: " + drakemode);
            Debug.Log("whale mode: " + whalemode);
        }

        if ((input == "WHALE") || (input == "whale"))
        {
            whalemode = true;

            original = false;

            drakemode = false;

            Debug.Log("whale mode enabled: " + whalemode);
            Debug.Log("drakemode: " + drakemode);
        }

        if ((input == "NORMAL") || (input == "normal"))
        {
            whalemode = false;

            original = true;

            drakemode = false;

            Debug.Log("og mode enabled: " + original);
        }
    }
}
