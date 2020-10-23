using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    Tile _goal;
    public void RecognizePath(float amount)
    {
        int counter = 0;
        int i = (int)this.transform.position.x;
        int j = (int)this.transform.position.y;
        if (j % 2 == 0)
        {
            while (counter < amount && i != Board.LENGTH)
            {
                i++;
                counter++;
            }
            if (i == Board.LENGTH)
            {
                i--;
                j++;
                while (i != 0 && counter < amount)
                {
                    i--;
                    counter++;
                }
            }
        }
        if (j % 2 != 0)
        {
            while (counter < amount && i != -1)
            {
                i--;
                counter++;
            }
            if (i == -1)
            {
                i++;
                j++;
                while (i != Board.LENGTH && counter < amount)
                {
                    i++;
                    counter++;
                }
            }
        }
        if (j != Board.LENGTH)
        {
            Board.Instance.Tiles[i, j].HighLight();
            _goal = Board.Instance.Tiles[i, j];
        }
        
    }
    public void Move()
    {
        this.transform.position = _goal.transform.position;
        _goal.OffLight();
        Dice.SwitchLock();
    }
}
