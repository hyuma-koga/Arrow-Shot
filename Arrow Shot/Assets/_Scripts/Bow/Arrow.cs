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
        Debug.Log("�ՓˑΏ�: " + other.name);

        if (other.CompareTag("Bird"))
        {
            Debug.Log("���ɖ����I");
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
