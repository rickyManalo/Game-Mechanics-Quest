using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    //The Time to reset the Combo Time
    //Basically the amount of time someone is given to input a key on a string for a move, before a move executes
    [SerializeField] float inputSpdLeniency = 0.5f; 
    [SerializeField] List<KeyCode> queuedKeys; //List of all the Keys Pressed so far

    [SerializeField] Text txtControlsTest; //Just for testing for printing the keys
    InputControlToKeyCodeConverter conv = new InputControlToKeyCodeConverter();

    MovesManager movesMngr;
    void Awake()
    {
        if (movesMngr == null)
            movesMngr = FindObjectOfType<MovesManager>();
    }

    void Update()
    {
        PrintControls(); //Just for testing
    }

    public void OnSkillKeyInput(InputAction.CallbackContext value){
        if(value.started){
            Debug.Log("inputCntrl: "+value.control.ToString());//actual keyboard key
            Debug.Log("inputCntrl: "+value.action);//binding name & keyboard key    
            queuedKeys.Add(conv.Convert(value.control));
            movesMngr.FindMoveWithInputLike(queuedKeys);//very important as this is the one that finds the move to be executed
            
            if (!movesMngr.HasMove(queuedKeys)){ //if there is no available Moves, stop the reset/execute move process
                Debug.Log("Stop coroutine");
                StopAllCoroutines();
                //TODO: support moves with the same starting input/keycode
            }

            StartCoroutine(ResetComboTimer()); //Start the Resetting process which also executes the moves
        }
    }

    public void ResetCombo() //Called to Reset the Combo after a move
    {
        Debug.Log("Reset combo");
        queuedKeys.Clear();
    }

    //TODO: need to rewrite combo reset timer as current one gives lag to single key moves
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
