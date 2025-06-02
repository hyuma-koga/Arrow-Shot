using UnityEngine;

public class CameraFollowTargetFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, 0f); // �v���C���[�̔w��?��������

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
