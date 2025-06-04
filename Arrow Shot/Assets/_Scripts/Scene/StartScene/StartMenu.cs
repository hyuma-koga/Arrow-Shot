using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitButton()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //�G�f�B�^�p
#endif
    }
}
