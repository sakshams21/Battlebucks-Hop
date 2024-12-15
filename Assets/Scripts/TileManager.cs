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
    
    public void SpawnTiles()
    {
        int posX = Random.Range(0, 3);
        int tileIndex = -1;
        for (int index = 0; index < Tiles.Length; index++)
        {
            if (Tiles[index].IsAvailable)
            {
                tileIndex = index;
                break;
            }
        }

        Tiles[tileIndex].Activate();
        
        //20% chance for bonus
        Tiles[tileIndex].Bonus_Go.SetActive(Random.Range(0, 100) < 20);
        
        Tiles[tileIndex].transform.position = StartPoint.position;
        Tiles[tileIndex].transform.DOMove(StartPoints[posX].position, 0.5f).OnComplete(() =>
        {
            Tiles[tileIndex].transform.position = StartPoints[posX].position;
            Tiles[tileIndex].Rb.linearVelocity = Vector3.back * TileMoveSpeed;
        });
    }
}
