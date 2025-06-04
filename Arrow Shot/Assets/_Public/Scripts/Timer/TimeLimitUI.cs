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

        // �ŏ��͔�\���ɂ��Ă���
        timerText.gameObject.SetActive(false);

        // �x��ă^�C�}�[���J�n�i0�b��ɊJ�n�j
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

        timerText.text = "�c�莞��: " + Mathf.CeilToInt(remainingTime) + "�b";

        if (remainingTime <= 0f)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("ClearScene");
        }
    }
}
