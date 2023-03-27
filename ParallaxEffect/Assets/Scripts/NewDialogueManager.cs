using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewDialogueManager : MonoBehaviour
{
    Text firstOptionText;
    Text secondOptionText;
    GameObject dialoguePanel;
    int state;

    // Start is called before the first frame update
    void Start()
    {
        firstOptionText = GameObject.Find("FirstOptionText").GetComponent<Text>();
        secondOptionText = GameObject.Find("SecondOptionText").GetComponent<Text>();
        dialoguePanel = GameObject.Find("DialoguePanel");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void firstChoiceClicked()
    {
        Debug.Log("First dialogue choice");
        if (state == 0)
        {
            firstOptionText.text = "You have clicked the first choice.";
            secondOptionText.text = "";
            state = 1;
        }
        else if (state == 1)
        {
            firstOptionText.text = "Why does everyone always pick the first choice?";
            secondOptionText.text = "";
            state = 3;
        }
        else if (state == 3)
        {
            firstOptionText.text = "Have you considered the second choice?";
            secondOptionText.text = "";
            state = 4;
        }
        else if (state == 4)
        {
            firstOptionText.text = "go fuck yourself";
            secondOptionText.text = "";
            state = 5;
        }
        else if (state == 5 || state == 2)
        {
            dialoguePanel.SetActive(false);
        }
    }

    public void secondChoiceClicked()
    {
        Debug.Log("Second dialogue choice");
        if (state == 0)
        {
            firstOptionText.text = "";
            secondOptionText.text = "You have clicked the second choice.";
            state = 2;
        }
    }
}