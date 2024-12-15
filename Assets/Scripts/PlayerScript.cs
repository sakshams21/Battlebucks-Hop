using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeReference]private Camera MainCamera;
    
    [Space(10f)]
    [SerializeReference] private Rigidbody Player_Rb;
    [SerializeReference] private GameObject Player;
    public float JumpForce;

    [SerializeReference] private float maxLeft;
    [SerializeReference] private float maxRight;
    
    public static event Action<bool> OnPlayerJump;
   
    public static event Action OnGameOver;

    private bool _isBonusTouched = false;
    
    private void Update()
    {
        GetMousePosition(Mouse.current.position.ReadValue());
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            PlayerBounce(1);
            Tile tile = other.gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                tile.Effector(_isBonusTouched);
                OnPlayerJump?.Invoke(_isBonusTouched);
                _isBonusTouched = false;
            }
        }
        if (other.gameObject.CompareTag("Bonus"))
        {
            _isBonusTouched = true;
        }

        if (other.gameObject.CompareTag("GameReset"))
        {
            OnGameOver?.Invoke();
        }

    }

    private void PlayerBounce(int direction)
    {
        Vector3 newVelocity = Player_Rb.linearVelocity;
        newVelocity.y = JumpForce*direction;
        Player_Rb.linearVelocity = newVelocity;
    }
    
    
    private void GetMousePosition(Vector2 mousePosition)
    {
        Vector3 data = MainCamera.ScreenToViewportPoint(mousePosition);
        Vector3 playerPosition = Player.transform.position;
        playerPosition.x = Mathf.Lerp(maxLeft, maxRight, data.x);
        Player.transform.position = playerPosition;
    }
}
