using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Linq;

public class MovesManager : MonoBehaviour
{
    [SerializeField] List<Move> availableMovesLst; //All the Available Moves
    [SerializeField] List<Move> possibleMoveLst; //For improving speed, I think
    [SerializeField] Move queuedMove;
    PlayerController playerController;
    ControlManager controlManager;

    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        controlManager = FindObjectOfType<ControlManager>();
    }

    public bool HasMove(List<KeyCode> pInputString) //return true if the list contain a move
    {
        foreach (Move move in availableMovesLst)
        {
            return move.isInputStringLike(pInputString);
        }

        return false;
    }

    //This basically is like the auto suggest on search engines
    //this would find and throw the closest move based on the queued input
    public Move FindMoveWithInputLike(List<KeyCode> pInputString){
        possibleMoveLst = availableMovesLst.Where(m => m.isInputStringLike(pInputString)).ToList();
        possibleMoveLst = possibleMoveLst.OrderBy(m => m.GetInputString().Count).ToList();

        queuedMove = possibleMoveLst.Find(m => IsKeyCodeInputSame(pInputString, m.GetInputString()));
        return null;
    }

    public void ExecMove(List<KeyCode> pInputString)
    {
        if(queuedMove != null){
            playerController.ExecMove(queuedMove);
            queuedMove = null;
        }
    }

    private bool IsKeyCodeInputSame(List<KeyCode> userInput, List<KeyCode> moveInputString){
        return userInput.SequenceEqual(moveInputString);
    }
}
