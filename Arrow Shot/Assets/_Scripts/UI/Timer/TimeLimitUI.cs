using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLimitUI : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;
    [SerializeField] private Text timerText;
    private float remainingTime;
    private bool timerStarted = false;

    private void Start()
    {
        remainingTime = timeLimit;

        // 最初は非表示にしておく
        timerText.gameObject.SetActive(false);

        // 遅れてタイマーを開始（0秒後に開始）
        Invoke(nameof(StartTimer), 0f);
    }

    private void StartTimer()
    {
        timerStarted = true;
        timerText.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!timerStarted) return;

        remainingTime -= Time.deltaTime;
        remainingTime = Mathf.Max(remainingTime, 0f);

        timerText.text = "残り時間: " + Mathf.CeilToInt(remainingTime) + "秒";

        if (remainingTime <= 0f)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("ClearScene");
        }
    }
}
