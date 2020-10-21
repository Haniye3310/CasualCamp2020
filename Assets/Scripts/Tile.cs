using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    GameObject _childHighLight;
    void Start() 
    {
        _childHighLight = transform.GetChild(0).gameObject;
    }
    public void HighLight() 
    {
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
            Board.Instance.Piece.Move();
        }
    }
  
}
