using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // This function will be called when the Quit button is clicked
    public void QuitApplication()
    {
        Debug.Log("Quit button clicked!"); // For testing in the editor
        Application.Quit(); // Quits the application
    }
}


