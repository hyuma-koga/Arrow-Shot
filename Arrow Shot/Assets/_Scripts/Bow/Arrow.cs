using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bird"))
        {
            //得点処理やエフェクト追加など
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
