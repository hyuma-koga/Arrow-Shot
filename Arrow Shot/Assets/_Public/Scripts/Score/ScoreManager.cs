using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    public static ScoreManager Instance;
    private int score = 0;
    private bool hasShownUI = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (!hasShownUI && scoreText != null)
        {
            scoreText.gameObject.SetActive(true);
            hasShownUI = true;
        }

        UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScoreText(Text newText)
    {
        scoreText = newText;
        UpdateUI();
    }

    public void ResetScore()
    {
        score = 0;
        hasShownUI = false;

        if (scoreText != null)
        {
            scoreText.gameObject.SetActive(false);
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{score}";
        }
    }
}
