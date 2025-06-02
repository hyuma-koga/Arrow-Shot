using UnityEngine;

public class PlayerRotationAdjuster : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraHolder; // Cameraの親オブジェクト

    private Quaternion originalPlayerRotation;
    private Quaternion originalCameraRotation;
    private bool isRotated = false;

    void Start()
    {
        originalPlayerRotation = transform.rotation;
        originalCameraRotation = cameraHolder.rotation;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Shoot"))
        {
            if (!isRotated)
            {
                transform.rotation *= Quaternion.Euler(0, 90f, 0);
                cameraHolder.rotation *= Quaternion.Euler(0, 90f, 0);
                isRotated = true;
            }
        }
        else
        {
            if (isRotated)
            {
                transform.rotation = originalPlayerRotation;
                cameraHolder.rotation = originalCameraRotation;
                isRotated = false;
            }
        }
    }
}
