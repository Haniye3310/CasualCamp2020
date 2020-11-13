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
        var piecePos = Board.Instance.Pieces[Board.Instance.Turn].transform.position;
        if (!_lock) 
        {
            Number =(int) Mathf.Floor(Random.Range(1.0f, 7.0f));
            StartCoroutine(SetDiceFixedImage());
            SetLock(true);
        }
    }

    public static void SetLock(bool locked)
    {
        if (_lock == locked)
        {
            if (locked)
                Debug.LogError("You are lock again!");
            else
                Debug.LogError("You are unlock again!");
        }

        _lock = locked;

        if (locked == false)
            Board.Instance.ScalePiecesInSamePos();
    }

    IEnumerator SetDiceFixedImage()
    {
        anim.enabled = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.enabled = false;
        _diceImage.sprite = _diceNumbers[Number - 1];
        Board.Instance.Pieces[Board.Instance.Turn].RecognizePath(Number);

    }
}
