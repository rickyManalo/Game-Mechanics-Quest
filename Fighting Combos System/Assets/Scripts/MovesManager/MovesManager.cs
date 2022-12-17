using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;

public class MovesManager : MonoBehaviour
{
    [SerializeField] List<Move> availableMovesLst; //All the Available Moves
    [SerializeField] List<Move> possibleMoveLst; //For improving speed
    PlayerController playerController;
    ControlManager controlManager;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        controlManager = FindObjectOfType<ControlManager>();

        availableMovesLst.Sort(Compare); //Sort all the moves based on their priority
    }

    public bool HasMove(List<KeyCode> pInputString) //return true if the list contain a move
    {
        foreach (Move move in availableMovesLst)
        {
            if (move.isInputStringEqualTo(pInputString))
                return true;
        }
        return false;
    }

    //This basically is like the auto suggest on search engines
    //this would find and throw the closest move based on the queued input
    public Move FindMoveWithInputLike(List<KeyCode> pInputString){
        
        possibleMoveLst = availableMovesLst.Where(m => m.isInputStringLike(pInputString)).ToList();
        return null;
    }

    public void ExecMove(List<KeyCode> pInputString) //Send the moves to the player starting from the highest priority
    {
        // foreach (Move move in availableMovesLst)
        // {
        //     if (move.isInputStringEqualTo(pInputString))
        //     {
        //         playerController.ExecMove(move.GetMType(), move.GetMoveComboPriority());
        //         break;
        //     }
        // }

        Move m = possibleMoveLst[0];
        playerController.ExecMove(m);
    }

    public int Compare(Move move1, Move move2)
    {
        return Comparer<int>.Default.Compare(move2.GetMoveComboPriority(), move1.GetMoveComboPriority());
    }
}
