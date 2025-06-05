using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private AudioClip hitSound;

    private AudioSource audioSource;
    private bool hasHit = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasHit)
        {
            return;
        }

        if (other.CompareTag("Bird"))
        {
            hasHit = true;

            //�q�b�g�����Đ�
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            ScoreManager.Instance?.AddScore(10);
            Destroy(other.gameObject);

            //�����Đ������܂ŏ����҂��Ă������폜
            Destroy(gameObject, hitSound != null ? hitSound.length : 0.1f);
        }
    }
}
