using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    Tile _goal;
    public void RecognizePath(int amount)
    {
        if (MoveToward(this.transform.position, amount).y != Board.LENGTH) 
        {
            var nextPos = MoveToward(this.transform.position, amount);
            Board.Instance.Tiles[(int)nextPos.x, (int)nextPos.y].HighLight();
            _goal = Board.Instance.Tiles[(int)nextPos.x, (int)nextPos.y];
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
        //if (_goal.transform.GetChild(1).gameObject.activeInHierarchy)
        //{
        //    var nextPos = new Vector2(-1, -1);
        //    int safety = 500;
        //    int counter = 0;
        //    while (nextPos.x != -1 && Board.Instance.Design[(int)nextPos.x, (int)nextPos.y] == 0)
        //    {
        //        int step = GetStep(this.transform.position);
        //        nextPos = MoveToward(this.transform.position, Random.Range(step, Board.LENGTH * Board.LENGTH));
        //        counter++;
        //        if (counter > safety) break;
        //    }

        //}
        else Dice.SwitchLock();
    }
    IEnumerator MineAction() 
    {
        while (Board.Instance.Tiles[(int)this.transform.position.x, (int)this.transform.position.y].transform.GetChild(2).gameObject.activeInHierarchy 
               && MoveToward(this.transform.position, -4).y != -1) 
        {
            yield return new WaitForSeconds(2);
            this.transform.position = MoveToward(this.transform.position, -4);
        }
        Dice.SwitchLock();
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

    //int GetStep(Vector2 currentPos)
    //{
    //    int ret = (int)currentPos.y * Board.LENGTH;

    //    if (currentPos.y % 2 == 0) ret += (int)currentPos.x;

    //    if (currentPos.y % 2 != 0) ret += Board.LENGTH - (int)currentPos.x;

    //    return ret;
    //}
}
