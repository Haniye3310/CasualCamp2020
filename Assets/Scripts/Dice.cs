using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public static float  Number;
    static bool _lock;
    void Start() 
    {
        _lock = false;
    }
    public void Dice_OnClick()
    {
        int counter = 0;
        if (!_lock) 
        {
            Number = Mathf.Floor(Random.Range(1.0f, 7.0f));
            GetComponentInChildren<Text>().text = Number.ToString();
            Board.Instance.Piece.RecognizePath(Number);
            SwitchLock();
            
        }
        if (_lock) 
        {
            for(int i = 0; i< Board.LENGTH; i++) 
            { 
                for(int j = 0; j <Board.LENGTH;j++)
                {
                    if (!Board.Instance.Tiles[i, j].IsHighLight())
                    {
                        counter++;
                    }
                }
            }
            if (counter == 100)
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
