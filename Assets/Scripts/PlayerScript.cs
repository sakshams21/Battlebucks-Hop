using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles movement of player
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeReference]private Camera MainCamera;
    
    [Space(10f)]
    [SerializeReference] private float JumpForce;

    // max limit on the screen for the ball(player)
    [SerializeReference] private float maxLeft;
    [SerializeReference] private float maxRight;
    
    public static event Action<bool> OnPlayerJump;
    public static event Action OnGameOver;
    
    private Rigidbody _playerRb;

    private bool _isBonusTouched;

    private void Awake()
    {
        _playerRb=GetComponent<Rigidbody>();
    }


    private void Update()
    {
        GetMousePosition(Mouse.current.position.ReadValue());
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            PlayerBounce();
            OnPlayerJump?.Invoke(_isBonusTouched);
            
            //For effect
            Tile tile = other.gameObject.GetComponent<Tile>();
            if (tile != null)
            {
                tile.Effector(_isBonusTouched);
            }
            _isBonusTouched = false;
        }
        //Checks if Bonus was touched
        if (other.gameObject.CompareTag("Bonus"))
        {
            other.gameObject.SetActive(false);
            _isBonusTouched = true;
        }

        if (other.gameObject.CompareTag("GameReset"))
        {
            OnGameOver?.Invoke();
        }

    }

    /// <summary>
    /// Player Jump movement(add upward velocity whenever it touches a tile)
    /// </summary>
    private void PlayerBounce()
    {
        Vector3 newVelocity = _playerRb.linearVelocity;
        newVelocity.y = JumpForce;
        _playerRb.linearVelocity = newVelocity;
    }
    
    /// <summary>
    /// Scales mouse movement on screen to world position in the game
    /// </summary>
    private void GetMousePosition(Vector2 mousePosition)
    {
        Vector3 data = MainCamera.ScreenToViewportPoint(mousePosition);
        Vector3 playerPosition = transform.position;
        playerPosition.x = Mathf.Lerp(maxLeft, maxRight, data.x);
        transform.position = playerPosition;
    }
}
