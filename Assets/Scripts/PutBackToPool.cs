using UnityEngine;

public class PutBackToPool : MonoBehaviour
{
    [SerializeReference]private TileManager Ref_TileManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
