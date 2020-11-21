using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnPathRecognized(int turn);
public delegate void SwitchTurnAction(int turn);
public class Dice : MonoBehaviour
{
    public static int Number;
    public static bool _lock = false;
    [SerializeField] Animator anim;
    [SerializeField] Sprite[] _diceNumbers;
    [SerializeField] Image _diceImage;
    public event OnPathRecognized OnPathRecognized;
    public event SwitchTurnAction OnSwitchTurn;
    private static Dice _instance;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void Dice_OnClick()
    {
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
        {
            Board.Instance.ScalePiecesInSamePos();
            Board.Instance.SwitchTurn();
            if (_instance.OnSwitchTurn != null) {
                _instance.OnSwitchTurn(Board.Instance.Turn);
            }
        }
    }

    IEnumerator SetDiceFixedImage()
    {
        anim.enabled = true;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        anim.enabled = false;
        _diceImage.sprite = _diceNumbers[Number - 1];
        if (Board.Instance.Pieces[Board.Instance.Turn].MoveToward(Board.Instance.Pieces[Board.Instance.Turn].transform.position, Number).y < Board.LENGTH)
        {
            Board.Instance.Pieces[Board.Instance.Turn].RecognizePath(Number);
            if (OnPathRecognized != null)
                OnPathRecognized(Board.Instance.Turn);
        }
        else SetLock(false);
       
    }
}
