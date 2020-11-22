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
#if DEBUG_MODE
    private DEBUG_DiceNumber DEBUG_diceNumber;
#endif

    void Awake()
    {
        if (_instance == null)
            _instance = this;
#if DEBUG_MODE
        DEBUG_diceNumber = gameObject.AddComponent<DEBUG_DiceNumber>();
        DEBUG_diceNumber.OnNumberSet += DEBUG_DiceOnClick;
#endif
    }

    public void Dice_OnClick()
    {
        if (!_lock) 
        {
            Number = (int)Mathf.Floor(Random.Range(1.0f, 7.0f));
            StartCoroutine(SetDiceFixedImage());
            SetLock(true);
        }
    }

#if DEBUG_MODE
    public void DEBUG_DiceOnClick(int num)
    {
        if (!_lock)
        {
            Number = num;
            StartCoroutine(SetDiceFixedImage());
            SetLock(true);
        }
    }
#endif

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
