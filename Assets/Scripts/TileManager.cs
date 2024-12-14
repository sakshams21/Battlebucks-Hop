using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    [SerializeReference]private Rigidbody[] Tiles;
    [SerializeReference]private GameObject[] Bonus;
    
    [SerializeReference]private Transform[] StartPoints;
    [SerializeReference] private float TileMoveSpeed;
    [SerializeReference]private Transform StartPoint;

    public void StartGame()
    {
        for (int i = 0; i < 2; i++)
        {
            Tiles[i].linearVelocity = Vector3.back * TileMoveSpeed;
        }
        InvokeRepeating(nameof(SpawnTiles), 0, 1);
    }
    
    public void SpawnTiles()
    {
        int posX = Random.Range(0, 3);
        (Rigidbody,int) tile = GetFreeTile();
        
        if (tile.Item1 == null) return;
        tile.Item1.gameObject.SetActive(true);
        
        //20% chance for bonus
        Bonus[tile.Item2].SetActive(Random.Range(0, 100) < 20);
        
        tile.Item1.transform.position = StartPoint.position;
        tile.Item1.transform.DOMove(StartPoints[posX].position, 0.5f).OnComplete(() =>
        {
            tile.Item1.transform.position = StartPoints[posX].position;
            tile.Item1.linearVelocity = Vector3.back * TileMoveSpeed;
        });
    }

    private (Rigidbody,int) GetFreeTile()
    {
        for (int index = 0; index < Tiles.Length; index++)
        {
            if (!Tiles[index].gameObject.activeSelf)
                return (Tiles[index],index);
        }

        return (null,0);
    }
}
