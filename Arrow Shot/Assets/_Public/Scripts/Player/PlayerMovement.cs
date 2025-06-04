using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Transform playerBody; // モデルの向きを変える用（通常は this.transform）

    private PlayerAnimationController animationController;
    private BowController bowController;

    private bool wasMoving = false;

    public void SetAnimationController(PlayerAnimationController controller)
    {
        animationController = controller;
    }

    public void SetBowController(BowController bow)
    {
        bowController = bow;
    }

    public void HandleMovement()
    {
        if (bowController != null && bowController.IsShooting)
            return;

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(inputX, 0f, inputZ).normalized;

        // 入力があるときだけ移動＆向き変更
        if (moveDirection.magnitude > 0f)
        {
            if (!wasMoving)
            {
                animationController.PlayMove();
                wasMoving = true;
            }

            // 向き変更（カメラのY軸方向を基準にする）
            Vector3 worldDirection = Camera.main.transform.TransformDirection(moveDirection);
            worldDirection.y = 0f;
            playerBody.forward = worldDirection.normalized;

            transform.Translate(worldDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            if (wasMoving)
            {
                animationController.PlayIdle();
                wasMoving = false;
            }
        }
    }
}
