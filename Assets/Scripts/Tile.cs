using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// individual tile script
/// </summary>
public class Tile : MonoBehaviour
{
    [SerializeField]private Transform Effect_Transform;
    
    public Rigidbody Rb;
    public GameObject Bonus_Go;
    public bool IsAvailable;

    private Vector3 _effectScale=new Vector3(0.5f,0.3f,0.5f);
    private Vector3 _effectScaleBonus=new Vector3(1.0f,0.3f,1.0f);

    public void Effector(bool isBonus)
    {
        Effect_Transform.DOPunchScale(isBonus?_effectScaleBonus:_effectScale, 0.2f, 10, 0.2f);   
    }

    public void MoveTile(Vector3 linearVelocity)
    {
        Rb.linearVelocity = linearVelocity;
    }

    //Marks it to not select for next tile
    public void Activate()
    {
        IsAvailable = false;
        gameObject.SetActive(true);
        
        //20% chance for bonus
        Bonus_Go.SetActive(Random.Range(0, 100) < 20);
    }
    
    //puts it back in pool to be selected
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BackToPool"))
        {
            gameObject.SetActive(false);
            IsAvailable = true;
        }
    }

}