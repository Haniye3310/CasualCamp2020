using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public const int LENGTH = 10;
    [SerializeField] GameObject _tilePrefab;
    public Tile[,] Tiles;
    public Piece Piece;
    public int[,] Design; 
    public static Board Instance { get; private set; }
    void Awake() 
    { if(Instance == null)
            Instance = this;

    }
    void Start() 
    {
        Tiles = new Tile[LENGTH, LENGTH];
        ArrangeTiles();
        ArrangeMineOrPlane();
        
    }
    void ArrangeTiles() 
    { 
        for(int i = 0; i < LENGTH; i++) 
        {
            for (int j = 0; j < LENGTH; j++) 
            {
                GameObject tile = Instantiate(_tilePrefab,new Vector3(i, j, 0),Quaternion.identity) as GameObject;
                Tiles[i,j]=tile.GetComponent<Tile>();
                tile.name = "Tile(" + i + "," + j+ ")";
                tile.transform.parent = transform;
            }
        }
    }
     void ArrangeMineOrPlane() 
    {

        Design = ArrayUtil.TransformArrayToUnityCoordinate( new int[LENGTH, LENGTH] {
            { 0 , -1 , 0 , -1 , 0 , 0 , 0 , -1 , 0 , 0 },
            { 0 , -1 , 0 , 0 , 0 , -1 , 0 , 0 , -1 , 0 },
            { -1 , 0 , 0 , -1 , 0 , 0 , -1 , 0 , 1 , 0 },
            { 0 , -1 , 0 , 0 , -1 , 0 , 0 , -1 , 0 , 0 },
            { 0 , 1 , 0 , 0 , -1 , -1 , 0 , 0 , 0 , 0 },
            { 0 , -1 , 0 , 0 , 1 , 0 , 0 , -1 , 0 , -1},
            { -1 , 1 , 0 , 0 , 0 , 0 , -1 , 0 , 0 , 1 },
            { 0 , 0 , 0 , -1 , 1 , 0 , 0 , 0 , 0 , -1 },
            { 0 , 0 , 0 , -1 , 0 , 0 , 0 , 0 , -1 , 0 },
            { 0 , 0 , -1 , 0 , -1 , 0 , 1 , 0 , 0 , -1 }
        });

        for (int i = 0; i < LENGTH; i++)
        {
            for (int j = 0; j < LENGTH; j++)
            {
                if (Board.Instance.Design[i, j] == 1)
                    Tiles[i, j].transform.GetChild(1).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == -1)
                    Tiles[i, j].transform.GetChild(2).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == 0)
                    continue;
            }
        }
    }

}


