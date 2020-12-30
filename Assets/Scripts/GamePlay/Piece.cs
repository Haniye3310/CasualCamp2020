using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Piece : MonoBehaviour
{
    public SoldierType SoldierType;
    Tile _goal;
    [HideInInspector]
    public Vector2 PieceStartPosition;
    [SerializeField]
    private SpriteRenderer _mainSprite;
    [SerializeField]
    private SpriteRenderer _halfScaleSprite;
    [SerializeField]
    AudioSource _mineClip;
    [SerializeField]
    AudioSource _planeClip;
    [SerializeField]
    AudioSource _BombClip;
    [SerializeField]float _shakeDuration;
    [SerializeField]float _flyDuration;
    [SerializeField] Ease _ease;
    //Sangar
    //[SerializeField] GameObject _rejectTurnBtn;
    public int MyTurn { get; private set; }
    public void SetMyTurn(int turn) 
    {
        MyTurn = turn;
    }
    public void RecognizePath(int amount)
    {
        if (this.transform.position.y < 0) this.transform.position = new Vector3(0, 0, 0);
        var nextPos = MoveToward(this.transform.position, amount);
        Board.Instance.Tiles[(int)nextPos.x, (int)nextPos.y].HighLight();
        _goal = Board.Instance.Tiles[(int)nextPos.x, (int)nextPos.y];
        //Sangar
        //if(Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(3).gameObject.activeInHierarchy) { _rejectTurnBtn.SetActive(true); }
    }
    public void Move()
    {
        StartCoroutine(MoveCoroutine());

        
    }
    IEnumerator MineAction() 
    {
        
        while (Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(2).gameObject.activeInHierarchy) 
        {
            
            this.transform.GetChild(0).gameObject.SetActive(true);
            Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(4).gameObject.SetActive(true);
            _mineClip.Play();
            transform.DOShakeRotation(_shakeDuration);
            yield return new WaitForSeconds(_shakeDuration);
            _mineClip.Stop();
            this.transform.GetChild(0).gameObject.SetActive(false);
            Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(4).gameObject.SetActive(false);
            if (MoveToward(this.transform.position, -4).y < 0) { this.transform.position = this.PieceStartPosition;break; }
            this.transform.position = MoveToward(this.transform.position, -4);
            
        }
        Dice.SetLock(false);

    }
    IEnumerator TenchAction()
    {

        while (Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(7).gameObject.activeInHierarchy)
        {

            this.transform.GetChild(0).gameObject.SetActive(true);
            Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(4).gameObject.SetActive(true);
            _mineClip.Play();
            transform.DOShakeRotation(_shakeDuration);
            yield return new WaitForSeconds(_shakeDuration);
            _mineClip.Stop();
            this.transform.GetChild(0).gameObject.SetActive(false);
            Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(4).gameObject.SetActive(false);
            if (MoveToward(this.transform.position, -8).y < 0) { this.transform.position = this.PieceStartPosition; break; }
            this.transform.position = MoveToward(this.transform.position, -8);

        }
        Dice.SetLock(false);

    }
    IEnumerator BombAction()
    {

        while (Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(5).gameObject.activeInHierarchy)
        {

            this.transform.GetChild(0).gameObject.SetActive(true);
            Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(4).gameObject.SetActive(true);
            _BombClip.Play();
            transform.DOShakeRotation(_shakeDuration);
            yield return new WaitForSeconds(_shakeDuration);
            _BombClip.Stop();
            this.transform.GetChild(0).gameObject.SetActive(false);
            Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(4).gameObject.SetActive(false);
            if (MoveToward(this.transform.position, -16).y < 0) { this.transform.position = this.PieceStartPosition; break; }
            this.transform.position = MoveToward(this.transform.position, -16);

        }
        Dice.SetLock(false);

    }
    IEnumerator PlaneAction() 
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        _planeClip.Play();
        yield return new WaitForSeconds(1.3f);
        _planeClip.Stop();
        
        Vector2Int airPlanePos = new Vector2Int((int)_goal.transform.position.x, (int)_goal.transform.position.y);
        List<Vector2Int> emptySpaces = new List<Vector2Int>();
        int loopEnd = 0;
        if (airPlanePos.y < 3) loopEnd = airPlanePos.y + 3;
        else if (airPlanePos.y >= 3 && airPlanePos.y < 6) loopEnd = airPlanePos.y + 2;
        else if (airPlanePos.y >= 6 && airPlanePos.y < 9) loopEnd = airPlanePos.y + 1;
        for (int i = 0; i < Board.WIDTH; i++)
        {
            for (int j = airPlanePos.y + 1; j <= loopEnd; j++)
            {
                if (Board.Instance.Design[i, j] == -1)
                {
                    emptySpaces.Add(new Vector2Int(i, j));
                }
            }
        }
        var vec2 = emptySpaces[Random.Range(0, emptySpaces.Count - 1)];
        transform.DOMove(new Vector3(vec2.x,vec2.y,0), _flyDuration).SetEase(_ease).Play();
        yield return new WaitForSeconds(_flyDuration);
        this.transform.GetChild(1).gameObject.SetActive(false);
        //this.transform.position = new Vector2(vec2.x, vec2.y);
        Dice.SetLock(false);
    }
    IEnumerator MachineAction() 
    {
        while (Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(6).gameObject.activeInHierarchy)
        {
            this.transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(1.3f);
            if (MoveToward(this.transform.position, 8).y < 0) { this.transform.position = this.PieceStartPosition; break; }
            this.transform.position = MoveToward(this.transform.position, 8);
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
        Dice.SetLock(false);
    }
    public Vector2 MoveToward(Vector2 currentPos, int moveAmount) 
    {
        int counter = 0;
        int i = (int)currentPos.x;
        int j = (int)currentPos.y;
        if (moveAmount>0)
        {
            if(j %2 == 0) 
            {
                while (counter < moveAmount && i != Board.WIDTH)
                {
                    i++;
                    counter++;
                }
                if (i == Board.WIDTH)
                {
                    i--;
                    j++;
                    while (i != 0 && counter < moveAmount)
                    {
                        i--;
                        counter++;
                    }
                }
            }
            if (j % 2 != 0)
            {
                while (counter < moveAmount && i != -1)
                {
                    i--;
                    counter++;
                }
                if (i == -1)
                {
                    i++;
                    j++;
                    while (i != Board.WIDTH && counter < moveAmount)
                    {
                        i++;
                        counter++;
                    }
                }
            }
        }
        if (moveAmount<0)
        {
            if (j % 2 == 0)
            {
                while (counter < Mathf.Abs(moveAmount) && i != -1)
                {
                    i--;
                    counter++;
                }
                if (i == -1)
                {
                    i++;
                    j--;
                    while (i != Board.WIDTH && counter < Mathf.Abs(moveAmount))
                    {
                        i++;
                        counter++;
                    }
                }
            }
            if (j % 2 != 0)
            {
                while (counter < Mathf.Abs(moveAmount) && i != Board.WIDTH)
                {
                    i++;
                    counter++;
                }
                if (i == Board.WIDTH)
                {
                    i--;
                    j--;
                    while (i != 0 && counter < Mathf.Abs(moveAmount))
                    {
                        i--;
                        counter++;
                    }
                }
            }
        }
        return new Vector2(i,j);
    }
    public void SetScaleNormal() 
    {
        _mainSprite.enabled = true;
        _halfScaleSprite.enabled = false;
    }
    public void SetScaleHalf(int idx) 
    {
        _mainSprite.enabled = false;
        _halfScaleSprite.enabled = true;
        switch (idx)
        {
            case 0:
                _halfScaleSprite.transform.localPosition = new Vector2(0, 0);
                break;
            case 1:
                _halfScaleSprite.transform.localPosition = new Vector2(.5f, .5f);
                break;
            case 2:
                _halfScaleSprite.transform.localPosition = new Vector2(.5f, 0);
                break;
            case 3:
                _halfScaleSprite.transform.localPosition = new Vector2(0, .5f);
                break;
        }
    }
    IEnumerator MoveCoroutine() 
    {
        //Sangar
        //if(_rejectTurnBtn.activeInHierarchy) _rejectTurnBtn.SetActive(false);
        while (Vector2.Distance(transform.position,_goal.transform.position)>0.01f) 
        {
            transform.position = Vector2.MoveTowards(transform.position,_goal.transform.position,5*Time.deltaTime);
            yield return null;
        }
        transform.position = _goal.transform.position;
        _goal.OffLight();
        if (_goal.transform.GetChild(2).gameObject.activeInHierarchy)
        {
            StartCoroutine(MineAction());

        }
        else if (_goal.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            StartCoroutine(PlaneAction());

        }
        else if (_goal.transform.GetChild(7).gameObject.activeInHierarchy)
        {
            StartCoroutine(TenchAction());

        }
        else if (_goal.transform.GetChild(5).gameObject.activeInHierarchy)
        {
            StartCoroutine(BombAction());

        }
        else if (_goal.transform.GetChild(6).gameObject.activeInHierarchy)
        {
            StartCoroutine(MachineAction());

        }
        else
        {
            Board.Instance.WinOrLoose();
            Dice.SetLock(false);


        }
        
    }

    //public void RotateCharacter() 
    //{
    //    Debug.Log("pp");
    //    if(this.transform.position.y %2 != 0) { this.transform.rotation = new Quaternion(0f,180f,0f,0);this.transform.position = new Vector3(this.transform.position.x+1,this.transform.position.y); }
    //    if (this.transform.position.y % 2 != 0) { this.transform.rotation = new Quaternion(0f, 0f, 0f, 0); this.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y); }
    //}


    //Sangar
    //public void RejectTurnBtn_OnClick()
    //{
    //    for(int i = 0; i < Board.WIDTH; i++) 
    //    {
    //        for (int j = 0; j < Board.LENGTH; j++) 
    //        {
    //            if (Board.Instance.Tiles[i, j].IsHighLight()) { Board.Instance.Tiles[i, j].OffLight(); }
    //        }
    //    }
            
    //    Dice.SetLock(false);
        
    //}



}
