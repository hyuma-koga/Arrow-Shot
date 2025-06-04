using UnityEngine;

public class PlayerRotationAdjuster : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerVisualRoot; // Bip001 �〈���ڃ��f���̐e

    private Quaternion originalVisualRotation;
    private bool isRotated = false;

    void Start()
    {
        // �����ڃ��f���̏����p�x�����ۑ��i�{�̂ł͂Ȃ��j
        originalVisualRotation = playerVisualRoot.rotation;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Shoot"))
        {
            if (!isRotated)
            {
                // �v���C���[�̌����ڂ����E��90�x�X����i�J������{�̂ɂ͐G��Ȃ��j
                playerVisualRoot.rotation *= Quaternion.Euler(0, 90f, 0);
                isRotated = true;
            }
        }
        else
        {
            if (isRotated)
            {
                // �����ڂ̉�]�������ɖ߂�
                playerVisualRoot.rotation = originalVisualRotation;
                isRotated = false;
            }
        }
    }
}
