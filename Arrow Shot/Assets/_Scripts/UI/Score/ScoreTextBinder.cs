using UnityEngine;
using UnityEngine.UI;

public class ScoreTextBinder : MonoBehaviour
{
    [SerializeField] private Text gameScoreText;

    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SetScoreText(gameScoreText);
        }
    }
}
