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
        Debug.Log("Õ“Ë‘ÎÛ: " + other.name);

        if (other.CompareTag("Bird"))
        {
            Debug.Log("’¹‚É–½’†I");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
