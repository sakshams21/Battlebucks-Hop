using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{ 
    [SerializeReference] private PlayerController Ref_PlayerController;
    [SerializeReference]private TileManager Ref_TileManager;
    
    [Header("UI Elements")] [Space(10f)]
    
    [SerializeReference] private TextMeshProUGUI Score_Text;
    //in Restart Canvas
    [SerializeReference] private TextMeshProUGUI HighScore_Text;
    //in Restart Canvas
    [SerializeReference] private TextMeshProUGUI CurrentScore_Text;
    
    [SerializeReference]private Canvas GameOver_Canvas;
    [SerializeReference]private Canvas Overlay_Canvas;
    [SerializeReference]private Canvas StartGame_Canvas;

    [Header("UI Elements")] [Space(10f)] 
    
    [SerializeReference] private GameObject BonusParticle_Go;

    private int _highScore;
    private int _currentScore;
    
    private void Start()
    {
        GameOver_Canvas.enabled = false;
        Overlay_Canvas.enabled = false;
        StartGame_Canvas.enabled = true;
        
        Physics.gravity *= 2;
        PlayerController.OnPlayerJump += ScoreUpdate;
        PlayerController.OnGameOver += ResetGame;

        _highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerJump -= ScoreUpdate;
        PlayerController.OnGameOver -= ResetGame;
    }

    private void ScoreUpdate(bool isBonus)
    {
        if (isBonus)
        {
            _currentScore+=5;
            BonusParticle_Go.SetActive(true);
        }
        else
        {
            _currentScore++;
        }
        Score_Text.text = _currentScore.ToString();
    }

    private void ResetGame()
    {
        //reset score
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
        }

        CurrentScore_Text.text = $"Current: {_currentScore}";
        PlayerPrefs.SetInt("HighScore", _highScore);
        HighScore_Text.text = $"Best:{_highScore}";
        _currentScore = 0;
        Score_Text.text = "0";
        
        GameOver_Canvas.enabled = true;
        Overlay_Canvas.enabled = false;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
