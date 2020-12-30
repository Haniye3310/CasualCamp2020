using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    GameObject _childHighLight;
    void Start() 
    {
        _childHighLight = transform.GetChild(0).gameObject;
    }
    public void HighLight() 
    {
        if(Board.Instance.Pieces[Board.Instance.Turn].name == "EnglishSoldier") _childHighLight.GetComponent<SpriteRenderer>().color = new Color(0.25f,0.31f,0.27f,1);
        if (Board.Instance.Pieces[Board.Instance.Turn].name == "FrenchSoldier") _childHighLight.GetComponent<SpriteRenderer>().color = new Color(0.42f, 0.81f, 0.96f, 1);
        _childHighLight.SetActive(true);
    }
    public void OffLight() 
    {
        _childHighLight.SetActive(false);
    }
    public bool IsHighLight() 
    {
        return _childHighLight.activeInHierarchy;
    }
    void OnMouseDown() 
    {
        if (IsHighLight())
        {
            Board.Instance.Pieces[Board.Instance.Turn].Move();
        }
    }

}
