using UnityEngine;

public class BirdMover : MonoBehaviour
{
    [SerializeField] private float speed = 2f;  // �ɂ₩�ȑ��x
    [SerializeField] private float changeDirectionInterval = 5f;
    [SerializeField] private float minY = 5f;
    [SerializeField] private float maxY = 8f;
    [SerializeField] private float maxDistanceFromCenter = 25f;

    private Vector3 direction;
    private Vector3 center = Vector3.zero;

    private void Start()
    {
        ChangeDirection();
        InvokeRepeating(nameof(ChangeDirection), changeDirectionInterval, changeDirectionInterval);
    }

    private void Update()
    {
        Vector3 nextPos = transform.position + direction * speed * Time.deltaTime;

        // ���������͈͂ɐ����i�ɋ}�Ȃ��j
        nextPos.y = Mathf.Clamp(nextPos.y, minY, maxY);

        // �t�B�[���h���S���痣�ꂷ���Ȃ��悤����
        if (Vector3.Distance(center, nextPos) > maxDistanceFromCenter)
        {
            Vector3 toCenter = (center - transform.position).normalized;
            direction = (direction + toCenter * 0.3f).normalized;
        }

        transform.position = nextPos;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void ChangeDirection()
    {
        Vector3 randomDir = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.1f, 0.1f), // �����ω������₩��
            Random.Range(-1f, 1f)
        ).normalized;

        direction = randomDir;
    }
}
