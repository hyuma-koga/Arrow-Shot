using UnityEngine;

public class TPSCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // CameraHolder ���w��
    [SerializeField] private float smoothSpeed = 10f;

    void LateUpdate()
    {
        // target�iCameraHolder�j�̈ʒu�Ɖ�]�Ɋ��S�Ɉ�v
        transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smoothSpeed * Time.deltaTime);
        transform.localPosition = new Vector3(0, 1.5f, -3f); // �����Œ�
    }
}
