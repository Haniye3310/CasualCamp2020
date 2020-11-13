﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    Tile _goal;
    [HideInInspector]
    public Vector2 PieceStartPosition;
    public void RecognizePath(int amount)
    {
        if (this.transform.position.y < 0) this.transform.position = new Vector3(0, 0, 0);
        if (MoveToward(this.transform.position, amount).y < Board.LENGTH)
        {
            var nextPos = MoveToward(this.transform.position, amount);
            Board.Instance.Tiles[(int)nextPos.x, (int)nextPos.y].HighLight();
            _goal = Board.Instance.Tiles[(int)nextPos.x, (int)nextPos.y];
        }
        else
        {
            Dice.SetLock(false);
            Board.Instance.SwitchTurn();
        }

    }
    public void Move()
    {

        this.transform.position = _goal.transform.position;
        _goal.OffLight();
        if (_goal.transform.GetChild(2).gameObject.activeInHierarchy) 
        {
            StartCoroutine( MineAction());
            
        }
        else if (_goal.transform.GetChild(1).gameObject.activeInHierarchy)
        {
            StartCoroutine(PlaneAction());
           
        }
        else Dice.SetLock(false);
        //ScaleSimplePosPiece();
        Board.Instance.WinOrLoose();
        Board.Instance.SwitchTurn();
        
    }
    IEnumerator MineAction() 
    {
        
        while (Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(2).gameObject.activeInHierarchy) 
        {
            
            this.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            this.transform.GetChild(0).gameObject.SetActive(false);
            if (MoveToward(this.transform.position, -4).y < 0) { this.transform.position = this.PieceStartPosition;break; }
            this.transform.position = MoveToward(this.transform.position, -4);
            
        }
        Dice.SetLock(false);
        //ScaleSimplePosPiece();

    }
    IEnumerator PlaneAction() 
    {
        this.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
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
        //ScaleSimplePosPiece();
    }
    Vector2 MoveToward(Vector2 currentPos, int moveAmount) 
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
    //void ScaleSimplePosPiece()
    //{
    //    Debug.Log("call");
    //    int i = 0;
    //    while (i < Board.Instance.Pieces.Length)
    //    {

    //        if (this.transform.position == Board.Instance.Pieces[i].transform.position && Board.Instance.Pieces[i].GetInstanceID() != this.GetInstanceID())
    //        {
    //            Debug.Log("if");
    //            this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    //            Board.Instance.Pieces[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    //            if (i == 1) Board.Instance.Pieces[i].transform.position = new Vector3(Board.Instance.Pieces[i].transform.position.x+0.5f, Board.Instance.Pieces[i].transform.position.y + 0.5f, 0);
    //            if (i == 2) Board.Instance.Pieces[i].transform.position = new Vector3(Board.Instance.Pieces[i].transform.position.x + 0.5f, Board.Instance.Pieces[i].transform.position.y, 0);
    //            if (i == 3) Board.Instance.Pieces[i].transform.position = new Vector3(Board.Instance.Pieces[i].transform.position.x , Board.Instance.Pieces[i].transform.position.y + 0.5f, 0);
    //        }
    //        i++;
    //    }
    //}





    //int GetStep(Vector2 currentPos)
    //{
    //    int ret = (int)currentPos.y * Board.LENGTH;

    //    if (currentPos.y % 2 == 0) ret += (int)currentPos.x + 1;

    //    if (currentPos.y % 2 != 0) ret += Board.LENGTH - (int)currentPos.x;

    //    return ret;
    //}
}
