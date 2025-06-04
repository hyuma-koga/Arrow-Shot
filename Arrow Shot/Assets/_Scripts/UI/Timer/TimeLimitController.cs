using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLimitController : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeLimit)
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
