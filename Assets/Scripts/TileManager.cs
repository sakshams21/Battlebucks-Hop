using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    [SerializeField]private Tile[] Tiles;
    
    [SerializeReference]private Transform[] StartPoints;
    [SerializeReference] private float TileMoveSpeed;
    [SerializeReference]private Transform StartPoint;

    private void Start()
    {
        PlayerController.OnGameOver += StopSpawningTile;
    }

    private void OnDestroy()
    {
        PlayerController.OnGameOver -= StopSpawningTile;
    }

    private void StopSpawningTile()
    {
       CancelInvoke(nameof(SpawnTiles));
    }

    public void StartGame()
    {
        for (int i = 0; i < 2; i++)
        {
            Tiles[i].Activate();
            Tiles[i].Rb.linearVelocity = Vector3.back * TileMoveSpeed;
        }
        InvokeRepeating(nameof(SpawnTiles), 0, 1);
    }
    
    /// <summary>
    /// being called every one second
    /// </summary>
    public void SpawnTiles()
    {
        int posX = Random.Range(0, 3);
        int tileIndex = -1;
        
        //checks for available tile from the poool
        for (int index = 0; index < Tiles.Length; index++)
        {
            if (Tiles[index].IsAvailable)
            {
                tileIndex = index;
                break;
            }
        }

        //Mark is out of pool
        Tiles[tileIndex].Activate();
        
        //put it at start of animation position
        Tiles[tileIndex].transform.position = StartPoint.position;
        
        //move it to the actual start position
        Tiles[tileIndex].transform.DOMove(StartPoints[posX].position, 0.5f).OnComplete(() =>
        {
            //after animation complete snap it to the position
            Tiles[tileIndex].transform.position = StartPoints[posX].position;
            
            //give it velocity
            Tiles[tileIndex].MoveTile(Vector3.back * TileMoveSpeed);
            
        });
    }
}
