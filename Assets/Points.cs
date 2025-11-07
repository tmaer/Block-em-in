using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    // Variables 
    public int blueScore = 0;
    public int redScore = 0;
    public int purpleScore = 0;

    // GameObjects
    public Text blueText;
    public Text redText;
    public Text purpleText;

    // Other files
    public InputHandling inputHandling;

    void Start()
    {
        inputHandling = GameObject.FindGameObjectWithTag("Player Logic").GetComponent<InputHandling>(); 
    }

    public void AssignText()
    {
        blueText = GameObject.FindGameObjectWithTag("Text1").GetComponent<Text>();
        if (inputHandling.numPlayers >= 2)
        {
            redText = GameObject.FindGameObjectWithTag("Text2").GetComponent<Text>();
        }
        if (inputHandling.numPlayers >= 3)
        {
            purpleText = GameObject.FindGameObjectWithTag("Text3").GetComponent<Text>();
        }
    }

    public void bluePoint()
    {
        blueScore++;
        blueText.text = Convert.ToString(blueScore);
    }
    public void redPoint()
    {
        redScore++;
        redText.text = Convert.ToString(redScore);
    }
    public void purplePoint()
    {
        purpleScore++;
        purpleText.text = Convert.ToString(purpleScore);
    }
}
