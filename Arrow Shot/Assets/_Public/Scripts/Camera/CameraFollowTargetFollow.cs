using UnityEngine;

public class CameraFollowTargetFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(0f, 1.5f, 0f); // ÉvÉåÉCÉÑÅ[ÇÃîwå„?å®Ç†ÇΩÇË

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}
