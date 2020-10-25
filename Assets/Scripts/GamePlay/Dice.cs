using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public static int  Number;
    static bool _lock;
    void Start() 
    {
        _lock = false;
    }
    public void Dice_OnClick()
    {
        var piecePos = Board.Instance.Piece.transform.position;
        if (!_lock) 
        {
            Number =(int) Mathf.Floor(Random.Range(1.0f, 7.0f));
            GetComponentInChildren<Text>().text = Number.ToString();
            Board.Instance.Piece.RecognizePath(Number);
            SwitchLock();
            
        }
        if (_lock 
            && piecePos.y == Board.LENGTH - 1 
            && Number> piecePos.x
            && Board.Instance.Design[(int)piecePos.x, (int)piecePos.y]!=-1)
        {
            SwitchLock();
        }
    }
    public static void SwitchLock() 
    {
        if (_lock)
            _lock = false;
        else if(!_lock)
            _lock = true;
    }
}
