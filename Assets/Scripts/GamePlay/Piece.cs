using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
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
            yield return new WaitForSeconds(1.3f);
            _mineClip.Stop();
            this.transform.GetChild(0).gameObject.SetActive(false);
            Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(4).gameObject.SetActive(false);
            if (MoveToward(this.transform.position, -4).y < 0) { this.transform.position = this.PieceStartPosition;break; }
            this.transform.position = MoveToward(this.transform.position, -4);
            
        }
        Dice.SetLock(false);

    }
    IEnumerator PlaneAction() 
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        _planeClip.Play();
        yield return new WaitForSeconds(1.3f);
        _planeClip.Stop();
        this.transform.GetChild(1).gameObject.SetActive(false);
        Vector2Int airPlanePos = new Vector2Int((int)_goal.transform.position.x, (int)_goal.transform.position.y);
        List<Vector2Int> emptySpaces = new List<Vector2Int>();
        int loopEnd = 0;
        if (airPlanePos.y < 3) loopEnd = airPlanePos.y + 3;
        else if (airPlanePos.y >= 3 && airPlanePos.y < 6) loopEnd = airPlanePos.y + 2;
        else if (airPlanePos.y >= 6 && airPlanePos.y < 9) loopEnd = airPlanePos.y + 1;
        for (int i = 0; i < Board.LENGTH; i++)
        {
            for (int j = airPlanePos.y + 1; j <= loopEnd; j++)
            {
                if (Board.Instance.Design[i, j] == 0)
                {
                    emptySpaces.Add(new Vector2Int(i, j));
                }
            }
        }
        var vec2 = emptySpaces[Random.Range(0, emptySpaces.Count - 1)];
        this.transform.position = new Vector2(vec2.x, vec2.y);
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
                while (counter < moveAmount && i != Board.LENGTH)
                {
                    i++;
                    counter++;
                }
                if (i == Board.LENGTH)
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
                    while (i != Board.LENGTH && counter < moveAmount)
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
                    while (i != Board.LENGTH && counter < Mathf.Abs(moveAmount))
                    {
                        i++;
                        counter++;
                    }
                }
            }
            if (j % 2 != 0)
            {
                while (counter < Mathf.Abs(moveAmount) && i != Board.LENGTH)
                {
                    i++;
                    counter++;
                }
                if (i == Board.LENGTH)
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
        else
        {
            Board.Instance.WinOrLoose();
            Dice.SetLock(false);


        }
    }
    
}
