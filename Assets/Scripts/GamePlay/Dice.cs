using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public static int  Number;
    static bool _lock;
    [SerializeField] Animator anim;
    [SerializeField] Sprite[] _diceNumbers;
    [SerializeField] Image _diceImage;
    void Start() 
    {
        _lock = false;
    }
    public void Dice_OnClick()
    {
         
        var piecePos = Board.Instance.pieces[Board.Instance.Turn].transform.position;
        if (!_lock) 
        {
            
            Number =(int) Mathf.Floor(Random.Range(1.0f, 7.0f));
            StartCoroutine(SetDiceFixedImage());
            SwitchLock();
            
        }
        if (_lock
            && piecePos.y == Board.LENGTH - 1
            && Number > piecePos.x
            && Board.Instance.Design[(int)piecePos.x, (int)piecePos.y] != -1)
        {
            Board.Instance.SwitchTurn();
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
    IEnumerator SetDiceFixedImage() 
    {
        anim.enabled = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.enabled = false;
        _diceImage.sprite = _diceNumbers[Number - 1];
        Board.Instance.pieces[Board.Instance.Turn].RecognizePath(Number);
    }
}
