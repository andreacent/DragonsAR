using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ErrorManager : MonoBehaviour{

    public static int error;   // The player's error.
    Text textError; // Reference to the Text component.

    void Awake (){
        // Set up the reference.
		textError = GetComponent <Text> ();

        // Reset the error.
        error = 0;
    }

    void Update (){
        // Set the displayed text to be the word "error" followed by the error value.
        textError.text = "FALLOS: " + error;
    }
}