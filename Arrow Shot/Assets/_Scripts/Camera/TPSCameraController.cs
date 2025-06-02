using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;     // Y軸回転（左右）担当
    [SerializeField] private Transform cameraHolder;   // X軸回転（上下）担当

    [SerializeField] private float mouseSensitivity = 2f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 initialAngles = cameraHolder.eulerAngles;
        xRotation = initialAngles.x;
        yRotation = initialAngles.y;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 視点回転を変数で管理（←これがポイント）
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 75f);

        // カメラホルダーの回転を明示的にセット
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
