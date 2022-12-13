using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    //The Time to reset the Combo Time
    //Basically the amount of time someone is given to input a key on a string for a move, before a move executes
    [SerializeField] float inputSpdLeniency = 0.5f; 
    [SerializeField] List<KeyCode> queuedKeys; //List of all the Keys Pressed so far

    [SerializeField] Text txtControlsTest; //Just for testing for printing the keys

    MovesManager movesMngr;
    void Awake()
    {
        if (movesMngr == null)
            movesMngr = FindObjectOfType<MovesManager>();
    }

    void Update()
    {
        DetectPressedKey(); //Get the Pressed Key

        PrintControls(); //Just for testing
    }

    public void DetectPressedKey()
    {
        //Go through all the Keys
        //To make it faster we can attach a class and put all the keys that are allowed to be pressed
        //This will make the process a bit faster rather than moving through all keys
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))//TODO: need to change this as this is too much
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log("KeyPressed: "+kcode);
                queuedKeys.Add(kcode); //Add the Key to the List
                movesMngr.FindMoveWithInputLike(queuedKeys);

                if (!movesMngr.HasMove(queuedKeys)){ //if there is no available Moves reset the list
                    Debug.Log("Stop coroutine");
                    StopAllCoroutines();
                }

                StartCoroutine(ResetComboTimer()); //Start the Resetting process
            }//every keypress, the resetcombotimer is essentially restarted. and the system rely on the person stop inputting to exec move
        }
    }

    public void ResetCombo() //Called to Reset the Combo after a move
    {
        Debug.Log("Reset combo");
        queuedKeys.Clear();
    }

    //TODO: need to rewrite combo reset timer as current one gives lag to single key moves
    //TODO: make animator not rely on move name but on the key input
    //TODO: support new inputsystem
    IEnumerator ResetComboTimer()
    {
        yield return new WaitForSeconds(inputSpdLeniency);
        Debug.Log("Reset timer");
        movesMngr.ExecMove(queuedKeys); //Run the move from the list
        queuedKeys.Clear(); //Empty the list
    }

    public void PrintControls() //Printing Keys just for testing
    {
        txtControlsTest.text = " Keys Pressed (";

        foreach (KeyCode kcode in queuedKeys)
            txtControlsTest.text += kcode + ",";

        txtControlsTest.text += ")";
    }
}
