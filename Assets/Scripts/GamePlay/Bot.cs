using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    Piece _botPiece;
    Dice _dice;
    void Awake() {
        _botPiece =gameObject.GetComponent<Piece>();
        _dice = FindObjectOfType<Dice>();

        _dice.OnSwitchTurn          += RollDice;
        _dice.OnPathRecognized      += MovePiece;
        Board.Instance.OnGameStart  += RollDiceOnFirstTurn;
    }
    void RollDice(int turn) 
    {
        if (turn != _botPiece.MyTurn) return;
        _dice.Dice_OnClick();
    }

    void RollDiceOnFirstTurn(int turn) 
    {
        if (0 == _botPiece.MyTurn) {
            _dice.Dice_OnClick();
        }
    }

    void MovePiece(int turn) 
    {
        if (turn != _botPiece.MyTurn) return;
        _botPiece.Move();
    }
}
