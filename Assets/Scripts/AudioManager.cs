using System;
using UnityEngine;

/// <summary>
/// Handles Audio for the game
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeReference]private AudioSource Sound_AudioSource;

    [SerializeReference]private AudioClip JumpSound_Clip;
    [SerializeReference]private AudioClip JumpSoundBonus_Clip;
    [SerializeReference]private AudioClip GameOverSound_Clip;
    private void Start()
    {
        PlayerController.OnPlayerJump += JumpSound;
        PlayerController.OnGameOver += GameOver;
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerJump -= JumpSound;
        PlayerController.OnGameOver -= GameOver;
    }

    //Game Over Sound played
    private void GameOver()
    {
        Sound_AudioSource.PlayOneShot(GameOverSound_Clip);
    }

    //Jump sound played, different sound for bonus
    private void JumpSound(bool isBonus)
    {
        Sound_AudioSource.PlayOneShot(isBonus?JumpSoundBonus_Clip:JumpSound_Clip);
    }
}
