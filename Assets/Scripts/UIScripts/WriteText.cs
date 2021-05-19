using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
*   Name: Sean Robbins
*   ID: 2328696
*   Email: srobbins@chapman.edu
*   Class: CPSC245
*   Turn Based Combat System
*   This is my own work. I did not cheat on this assignment
*/

public class WriteText : MonoBehaviour
{
    public Coroutine Write;
    public TextMeshProUGUI textToDisplay;
    private int framesToWaitBetweenLetter;


    /// <summary>
    /// Displays the Text Passed in, in its entirety without any write time
    /// Does not stop display after a time
    /// </summary>
    /// <param name="Text"></param>
    public void DisplayText(string Text)
    {
        textToDisplay.text = Text;
    }

    public void Start()
    {
        textToDisplay.enableAutoSizing = true;
        textToDisplay.text = "";
    }

    public void DisplayText(string Text, float TimeDisplay)
    {
        Write = StartCoroutine(writeText(Text, TimeDisplay));
    }

    public IEnumerator writeText(string TextToWrite, float DisplayTime)
    {
        
        textToDisplay.text = TextToWrite;
        yield return new WaitForSeconds(DisplayTime);
        textToDisplay.text = "";
    }
}
