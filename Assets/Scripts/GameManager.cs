using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Handles starting of game, score and gameover conditions and manages and trigger methods for other scripts
/// </summary>
public class GameManager : MonoBehaviour
{ 
    [SerializeReference] private PlayerController Ref_PlayerController;
    [SerializeReference]private TileManager Ref_TileManager;
    
    [Header("UI Elements")] [Space(10f)]
    
    //Ingame
    [Tooltip("The UI elements that will be displayed on the scene.")]
    [SerializeReference] private TextMeshProUGUI Score_Text;
    
    //in Restart Canvas
    [Tooltip("The UI elements that will be displayed on the Game over Canvas.")]
    [SerializeReference] private TextMeshProUGUI HighScore_Text;
    
    //in Restart Canvas
    [Tooltip("The UI elements that will be displayed on the Game over Canvas.")]
    [SerializeReference] private TextMeshProUGUI CurrentScore_Text;
    
    
    [SerializeReference]private Canvas GameOver_Canvas;
    [SerializeReference]private Canvas Overlay_Canvas;
    [SerializeReference]private Canvas StartGame_Canvas;
    
    [Space(10f)] 
    [SerializeReference] private GameObject BonusParticle_Go;

    private int _highScore;
    private int _currentScore;
    
    private void Start()
    {
        BonusParticle_Go.SetActive(false);
        GameOver_Canvas.enabled = false;
        Overlay_Canvas.enabled = false;
        StartGame_Canvas.enabled = true;
        
        //To make the ball drop faster
        Physics.gravity *= 2;
        
        PlayerController.OnPlayerJump += ScoreUpdate;
        PlayerController.OnGameOver += ResetGame;

        //Load previous session high score
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerJump -= ScoreUpdate;
        PlayerController.OnGameOver -= ResetGame;
    }

    //Updates score live on the screen
    private void ScoreUpdate(bool isBonus)
    {
        if (isBonus)
        {
            _currentScore+=5;
        }
        else
        {
            _currentScore++;
        }
        Score_Text.text = _currentScore.ToString();
    }
    
    //Game over method

    private void ResetGame()
    {
        BonusParticle_Go.SetActive(true);
        
        //reset score
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
        }

        //Text element update
        CurrentScore_Text.text = $"Current: {_currentScore}";
        PlayerPrefs.SetInt("HighScore", _highScore);
        HighScore_Text.text = $"Best: {_highScore}";
        _currentScore = 0;
        Score_Text.text = "0";
        
        GameOver_Canvas.enabled = true;
        Overlay_Canvas.enabled = false;
    }

    //Retry Button link
    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
