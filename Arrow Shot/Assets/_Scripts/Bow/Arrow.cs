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
            //���_������G�t�F�N�g�ǉ��Ȃ�
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
