using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "New Move")]
public class Move : ScriptableObject
{
    [SerializeField] string _name = "Move";
    [SerializeField] List<KeyCode> _inputString; //the List and order of the Moves
    [SerializeField] Moves moveType; //The kind of the move
    [SerializeField] int comboPriority = 0; //the more complicated the move the higher the Priorty

    //TODO: move this check earlier

    public bool isInputStringEqualTo(List<KeyCode> pInputString) //Check if we can perform this move from the entered keys
    {
        int comboIndex = 0;

        for (int i = 0; i < pInputString.Count; i++)
        {
            if (pInputString[i] == _inputString[comboIndex])
            {
                comboIndex++;
                if (comboIndex == _inputString.Count) //The end of the Combo List
                    return true;
            }
            else
                comboIndex = 0;
        }
        return false;
    }

    public bool isInputStringLike(List<KeyCode> pInputString) //Check if we can perform this move from the entered keys
    {
        for (int i = 0; i < pInputString.Count; i++)
        {
            if (pInputString[i] != _inputString[i])
            {
                return false;
            }
        }
        return true;
    }

    //Getters
    public int GetMoveComboCount()
    {
        return _inputString.Count;
    }
    public int GetMoveComboPriority()
    {
        return comboPriority;
    }
    public Moves GetMove()
    {
        return moveType;
    }
}
