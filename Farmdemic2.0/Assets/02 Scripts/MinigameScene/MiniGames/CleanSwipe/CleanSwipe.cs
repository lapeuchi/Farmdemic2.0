using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanSwipe : MonoBehaviour, IMinigame
{   
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    List<GameObject> trash = new List<GameObject>();
    public float fasfasdsa;

    int index_i {get {return index_i;} set {index_i = value; } }
    int index_j {get {return index_j;} set {index_j = value;}}

    Button[] rot_Buttons = new Button[4];

    Vector3[,] positions = new Vector3[3, 4] 
    { 
        // 벽면
        {
            new Vector3(0,0,0),
            new Vector3(0,-90,0),
            new Vector3(0,-180,0),
            new Vector3(0,-270,0)
        },
        // 천장
        {
            new Vector3(-45,0,0),
            new Vector3(-45,-90,0), 
            new Vector3(-45,-180,0), 
            new Vector3(-45,-270,0)
        },
        // 바닥
        {
            new Vector3(45,0,0),
            new Vector3(45,-90,0), 
            new Vector3(45,-180,0), 
            new Vector3(45,-270,0)
        }
    };

    void Start()
    {
        index_i = 0;
        index_j = 0;

        Camera.main.gameObject.SetActive(false);
 
    }

    void SetPos(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                
                break;
            case Direction.Down:
                break;
            case Direction.Left:
                break;
            case Direction.Right:
                break;
        }
    }

    void Load()
    {
        
    }

    public void GameStart()
    {
        
    }

    public void GameOver()
    {
        
    }   
}