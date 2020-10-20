using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int LENGTH = 10;
    [SerializeField] GameObject _tilePrefab;
    public Tile[,] Tiles;
    public Piece Piece;
    public static Board Instance { get; private set; }
    void Awake() 
    { 
        Instance = this;
    }
    void Start() 
    {
        Tiles = new Tile[LENGTH, LENGTH];
        ArrangeTiles();
    }
    void ArrangeTiles() 
    { 
        for(int i = 0; i < LENGTH; i++) 
        {
            for (int j = 0; j < LENGTH; j++) 
            {
                GameObject tile = Instantiate(_tilePrefab,new Vector3(i, j, 0),Quaternion.identity) as GameObject;
                Tiles[i, j] = tile.GetComponent<Tile>();
                tile.name = "Tile(" + i + "," + j + ")";
                tile.transform.parent = transform;
            }
        }
    }
}
