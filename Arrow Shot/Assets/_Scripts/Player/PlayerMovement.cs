using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

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
        //射撃中はアニメーション再生を制御しない
        if(bowController != null && bowController.IsShooting)
        {
            return;
        }

        float inputX = Input.GetAxisRaw("Horizontal");

        if(Mathf.Abs(inputX) > 0f)
        {
            if (!wasMoving)
            {
                animationController.PlayMove();
                wasMoving = true;
            }
        }
        else
        {
            if (wasMoving)
            {
                animationController.PlayIdle();
                wasMoving = false;
            }
        }

        Vector3 move = new Vector3(inputX, 0, 0);
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }


}
