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

            //ƒqƒbƒg‰¹‚ğÄ¶
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            ScoreManager.Instance?.AddScore(10);
            Destroy(other.gameObject);

            //‰¹‚ªÄ¶‚³‚ê‚é‚Ü‚Å­‚µ‘Ò‚Á‚Ä‚©‚ç–î‚ğíœ
            Destroy(gameObject, hitSound != null ? hitSound.length : 0.1f);
        }
    }
}
