using System;
using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Rigidbody Rb;
    public GameObject Bonus_Go;
    public Transform Effect_Transform;
    public bool IsAvailable;

    private Vector3 _effectScale=new Vector3(0.5f,0.3f,0.5f);
    private Vector3 _effectScaleBonus=new Vector3(1.0f,0.3f,1.0f);

    public void Effector(bool isBonus)
    {
        Effect_Transform.DOPunchScale(isBonus?_effectScaleBonus:_effectScale, 0.2f, 10, 0.2f);   
    }

    public void Activate()
    {
        IsAvailable = false;
        gameObject.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BackToPool"))
        {
            gameObject.SetActive(false);
            IsAvailable = true;
        }
    }

}