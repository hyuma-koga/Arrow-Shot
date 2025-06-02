using UnityEngine;

public class TPSCameraController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;     // Y����]�i���E�j�S��
    [SerializeField] private Transform cameraHolder;   // X����]�i�㉺�j�S��

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

        // ���_��]��ϐ��ŊǗ��i�����ꂪ�|�C���g�j
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 75f);

        // �J�����z���_�[�̉�]�𖾎��I�ɃZ�b�g
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
