using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeReference]private AudioSource Sound_AudioSource;

    [SerializeReference]private AudioClip JumpSound_Clip;
    [SerializeReference]private AudioClip GameOverSound_Clip;
    private void Start()
    {
        PlayerController.OnPlayerJump += JumpSound;
        PlayerController.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        Sound_AudioSource.PlayOneShot(GameOverSound_Clip);
    }

    private void JumpSound(bool isBonus)
    {
        Sound_AudioSource.PlayOneShot(JumpSound_Clip);
    }
}
