using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    [SerializeField] bool _pickRandomly = true;
    public const int LENGTH = 10;
    [SerializeField] GameObject _tilePrefab;
    public Tile[,] Tiles;
    [SerializeField] List<Piece> _piecesPrefab = new List<Piece>();
    [HideInInspector]
    public Piece[] Pieces = new Piece[2];
    public int[,] Design;
    public int Turn;
    public GameObject Panel;
    public Text Text;
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
        PickPiecesRandomly();
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
            { 0 , -1 , 2 , 2 , 2 , -1 , 0 , 0 , -1 , 0 },
            { -1 , 0 , 0 , -1 , 2 , 2 , -1 , 0 , 1 , 0 },
            { 0 , -1 , 2 , 2 , -1 , 0 , 0 , -1 , 0 , 0 },
            { 0 , 1 , 2 , 2 , -1 , -1 , 2 , 2 , 2 , 2 },
            { 0 , -1 , 0 , 0 , 1 , 0 , 0 , -1 , 0 , -1},
            { -1 , 1 , 2 , 2 , 2 , 2 , -1 , 0 , 0 , 1 },
            { 2 , 2 , 2 , -1 , 1 , 2 , 2 , 2 , 2 , -1 },
            { 0 , 0 , 0 , -1 , 2 , 2 , 2 , 2 , -1 , 0 },
            { 2 , 2 , -1 , 0 , -1 , 0 , 1 , 0 , 0 , -1 }
        });

        for (int i = 0; i < LENGTH; i++)
        {
            for (int j = 0; j < LENGTH; j++)
            {
                if (Board.Instance.Design[i, j] == 1)
                    Tiles[i, j].transform.GetChild(1).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == -1)
                    Tiles[i, j].transform.GetChild(2).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == 2)
                    Tiles[i, j].transform.GetChild(3).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == 0)
                    continue;
            }
        }
    }
    public void SwitchTurn() 
    {
        Turn++;
        if (Turn == Pieces.Length) Turn = 0;
    }
    public void WinOrLoose() 
    {
        if (Pieces[Turn].transform.position.y == Board.LENGTH - 1 && Pieces[Turn].transform.position.x==0) 
        {
            Panel.SetActive(true);
            Text.text = ""+Pieces[Turn].name+" Win!";
        }
    }

    private void PickPiecesRandomly() 
    {
        if(!_pickRandomly)
        {
            Pieces = _piecesPrefab.ToArray();
            return;
        }

        for (int i=0; i < Pieces.Length;i++)
        {
            int randIdx=(int)Mathf.Floor(Random.Range(0.0f, _piecesPrefab.Count- 0.00001f));
            Pieces[i] = _piecesPrefab[randIdx];
            _piecesPrefab.RemoveAt(randIdx);
            Pieces[i].PieceStartPosition = new Vector2(i, -1.3f);
            Pieces[i].transform.position = Pieces[i].PieceStartPosition;

        }

    }

}


