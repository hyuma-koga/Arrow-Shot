using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToStartScene : MonoBehaviour
{
    public void ReturnToStart()
    {
        // �X�R�A�����Z�b�g
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }

        // �J�[�\����\��
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // �X�^�[�g��ʂ�
        SceneManager.LoadScene("StartScene");
    }
}
