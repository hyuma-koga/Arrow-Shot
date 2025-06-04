using UnityEngine;
using UnityEngine.UI;

public class ClearSceneScoreDisplay : MonoBehaviour
{
    [SerializeField] private Text clearScoreText;

    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SetScoreText(clearScoreText);
        }
    }
}
