using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Board : MonoBehaviour
{
    [SerializeField] bool _pickRandomly = true;
    public const int LENGTH = 14;
    public const int WIDTH = 10;
    [SerializeField] GameObject _tilePrefab;
    public Tile[,] Tiles;
    [SerializeField] List<Piece> _piecesPrefab = new List<Piece>();
    [HideInInspector]
    public Piece[] Pieces = new Piece[2];
    public int[,] Design;
    public int Turn;
    public GameObject Panel;
    public Text Text;
    public event SwitchTurnAction OnGameStart;
    [SerializeField]AudioClip _firstBG;
    [SerializeField]AudioClip _secondBG;
    AudioSource _audioSource;
    public static Board Instance { get; private set; }
    void Awake() 
    { 
        if(Instance == null)
            Instance = this;
    }
    void Start() 
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayBGSound());
        Tiles = new Tile[WIDTH, LENGTH];
        ArrangeTiles();
        ArrangeMineOrPlane();
        PickPiecesRandomly();
    }
    void ArrangeTiles() 
    { 
        for(int i = 0; i < WIDTH; i++) 
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
        //-1 = empty
        //1 = plane
        //2 = mine
        //3 = sangar
        //5 = boom
        //6 = machine
        //7 = Tench
        Design = ArrayUtil.TransformArrayToUnityCoordinate( new int[LENGTH, WIDTH] {
            { -1 , 5 , -1 , -1 , -1 , -1 , -1 , 2 , -1 , 7 },
            { -1 , -1 , -1 , 2 , 3 , 3 , 3 , 3 , 2 , -1 },
            { 3 , 3 , 2 , -1 , 6 , 2 , -1 , 6 , -1 , -1 },
            { -1 , -1 , 7 , 2 , 3 , 3 , 2 , -1 , 1 , -1 },
            { 2 , -1 , 2 , -1 , 3 , 3 , 3 , 3 , 2 , -1 },
            { 2 , -1 , 6 , 3 , 3 , 3 , 2 , -1 , -1 , 7},
            { 3 , 3 , 3 , 2 , 1 , -1 , 2 , -1 , 6 , -1 },
            { -1 , -1 , -1 , 5 , -1 , -1 , -1 , 7 , -1 , -1 },
            { -1 , -1 , -1 , 5 , -1 , -1 , -1 , 7 , -1 , -1 },
            { -1 , 1 , -1 , 2 , 3 , 3 , 3 , 6 , -1 , 2 },
            { -1 , -1 , 3 , 3 , 3 , 2 , -1 , 1 , -1 , 2 },
            { 7 , -1 , 3 , 3 , -1 , -1 , -1 , -1 , 6 , -1 },
            { -1 , -1 , 2 , -1 , 3 , 3 , 3 , 3 , 2 , -1 },
            { -1 , 3 , 3 , 3 , -1 , 6 , -1 , 2 , -1 , 1 },
        });

        for (int i = 0; i < WIDTH; i++)
        {
            for (int j = 0; j < LENGTH; j++)
            {
                if (Board.Instance.Design[i, j] == 1)
                    Tiles[i, j].transform.GetChild(1).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == 2)
                    Tiles[i, j].transform.GetChild(2).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == 3)
                    Tiles[i, j].transform.GetChild(3).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == 5)
                    Tiles[i, j].transform.GetChild(5).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == 6)
                    Tiles[i, j].transform.GetChild(6).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == 7)
                    Tiles[i, j].transform.GetChild(7).gameObject.SetActive(true);
                else if (Board.Instance.Design[i, j] == -1)
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
            Turn = -2;
        }
    }

    void PickPiecesRandomly()
    {
        if(!_pickRandomly)
        {
            Pieces = _piecesPrefab.ToArray();
            for (int i=0; i < Pieces.Length;i++) {
                Pieces[i].SetMyTurn(i);
            }
            return;
        }
        int n = 1;

        for (int i=0; i < Pieces.Length;i++)
        {
            int randIdx=(int)Mathf.Floor(Random.Range(0.0f, _piecesPrefab.Count- 0.00001f));
            Pieces[i] = _piecesPrefab[randIdx];
            Pieces[i].SetMyTurn(i);
            _piecesPrefab.RemoveAt(randIdx);
            Pieces[i].PieceStartPosition = new Vector2(i+n, -2f);
            Pieces[i].transform.position = Pieces[i].PieceStartPosition;
            if(i ==0)n++;
        }
        if(OnGameStart != null) {
            OnGameStart(0);
        }
    }

    public void ScalePiecesInSamePos()
    {
        for (int i = 0; i < Pieces.Length; i++)
        {
            bool setHalf = false;
            for (int j = 0; j < Pieces.Length; j++)
            {
                if (i == j) continue;
                if (Pieces[i].transform.position == Pieces[j].transform.position)
                {
                    Pieces[i].SetScaleHalf(i);
                    setHalf = true;
                    break;
                }
            }
            if(!setHalf)
                Pieces[i].SetScaleNormal();
        }
    }
    IEnumerator PlayBGSound() 
    {
        while (true) 
        {
            _audioSource.clip = _firstBG;
            _audioSource.Play();
            yield return new WaitForSeconds(_audioSource.clip.length);
            _audioSource.clip = _secondBG;
            _audioSource.Play();
            yield return new WaitForSeconds(_audioSource.clip.length);
        }
    }
}
