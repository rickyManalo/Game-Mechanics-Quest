using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: As much as I want to have multiply key input part, it's too much work for now 
// considering the rnd I still need for the current thing I want to make work
[CreateAssetMenu(fileName = "New Move", menuName = "New Move")]
public class Move : ScriptableObject
{
    [SerializeField] string triggerName = "Move";
    [SerializeField] List<KeyCode> inputString; //the List and order of the Moves
    [SerializeField] int comboPriority = 0; //the more complicated the move the higher the Priorty

    //TODO: keycode to inputControl name conversion, maybe prepare the converted inputString onStart

    public bool isInputStringEqualTo(List<KeyCode> userInpString) //Check if we can perform this move from the entered keys
    {
        int comboIndex = 0;

        if(inputString.Count == 0){
            return false;//just for debugging so for empty input moves
        }

        for (int i = 0; i < userInpString.Count; i++)
        {
            if (userInpString[i] == inputString[comboIndex])
            {
                comboIndex++;
                if (comboIndex == inputString.Count) //The end of the Combo List
                    return true;
            }
            else
                comboIndex = 0;
        }
        return false;
    }

    public bool isInputStringLike(List<KeyCode> userInpString) //Check if we can perform this move from the entered keys
    {
        if(inputString.Count == 0){
            return false;//just for debugging so for empty input moves
        }

        if(inputString.Count < userInpString.Count){
            return false;//might the stupid weird index out of bounds error
        }

        Debug.Log("userInp: "+userInpString.Count+" inp: "+inputString.Count);

        for (int i = 0; i < userInpString.Count; i++)
        {
            Debug.Log("index: "+i+" mName: "+triggerName);
            if (userInpString[i] != inputString[i])
            {
                return false;
            }
        }

        Debug.Log("-X-X-");
        return true;
    }

    //Getters
    public int GetMoveComboCount()
    {
        return inputString.Count;
    }

    public List<KeyCode> GetInputString(){
        return inputString;
    }

    public string GetTriggerName(){
        return triggerName;
    }
}
