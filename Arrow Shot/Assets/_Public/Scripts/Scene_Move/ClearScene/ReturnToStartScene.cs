using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToStartScene : MonoBehaviour
{
    public void ReturnToStart()
    {
        // スコアをリセット
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        // カーソルを表示
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // スタート画面へ
        SceneManager.LoadScene("StartScene");
    }
}
