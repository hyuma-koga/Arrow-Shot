using UnityEngine;

public class TPSCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;  // CameraHolder を指定
    [SerializeField] private float smoothSpeed = 10f;

    void LateUpdate()
    {
        // target（CameraHolder）の位置と回転に完全に一致
        transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, smoothSpeed * Time.deltaTime);
        transform.localPosition = new Vector3(0, 1.5f, -3f); // 強制固定
    }
}
