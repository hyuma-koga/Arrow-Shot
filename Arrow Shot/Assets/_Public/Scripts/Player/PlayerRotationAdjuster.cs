using UnityEngine;

public class PlayerRotationAdjuster : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerVisualRoot; // Bip001 や見た目モデルの親

    private Quaternion originalVisualRotation;
    private bool isRotated = false;

    void Start()
    {
        // 見た目モデルの初期角度だけ保存（本体ではなく）
        originalVisualRotation = playerVisualRoot.rotation;
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName("Shoot"))
        {
            if (!isRotated)
            {
                // プレイヤーの見た目だけ右に90度傾ける（カメラや本体には触らない）
                playerVisualRoot.rotation *= Quaternion.Euler(0, 90f, 0);
                isRotated = true;
            }
        }
        else
        {
            if (isRotated)
            {
                // 見た目の回転だけ元に戻す
                playerVisualRoot.rotation = originalVisualRotation;
                isRotated = false;
            }
        }
    }
}
