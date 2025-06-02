using UnityEngine;
using System.Collections;

public class BowController : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxChargeTime = 2f;
    [SerializeField] private float minLaunchForce = 5f;
    [SerializeField] private float maxLaunchForce = 20f;

    private enum ShootState
    {
        None,
        Charging,
        Paused,
        Releasing
    }

    private ShootState shootState = ShootState.None;
    private PlayerAnimationController animationController;
    private Animator animator;
    private float chargeTime = 0f;
    private bool isCharging = false;
    private bool isPaused = false;

    public bool IsShooting => isCharging;
    public bool IsShootAnimationPlaying => isPaused || isCharging;

    public void SetAnimationController(PlayerAnimationController controller)
    {
        animationController = controller;
        animator = controller.GetAnimator();
    }

    public void HandleShooting()
    {
        // ボタンを押した瞬間：Shootトリガー → アニメ再生開始
        if (Input.GetMouseButtonDown(0) && shootState == ShootState.None)
        {
            isCharging = true;
            chargeTime = 0f;
            shootState = ShootState.Charging;
            animator.speed = 1.7f;
            animationController.PlayShoot();
            StartCoroutine(WaitAndPauseAnimation());
        }

        // 長押し中：チャージ時間を加算
        if (shootState == ShootState.Charging && Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
        }

        //追加：チャージ中にすぐ離されたら即発射
        if (shootState == ShootState.Charging && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Quick release during Charging - Fire immediately");
            animator.speed = 1f;
            animator.Play("Shoot", 0, 0.59f);
            shootState = ShootState.Releasing;
        }

        // ボタン離したらアニメ再開
        if (shootState == ShootState.Paused && Input.GetMouseButtonUp(0))
        {
            Debug.Log("Resume shoot animation");
            animator.speed = 1f; // アニメ続き再開
            animator.Play("Shoot", 0, 0.59f);
            shootState = ShootState.Releasing;
        }
    }

    private IEnumerator WaitAndPauseAnimation()
    {
        yield return new WaitForSeconds(0.1f); // Trigger反映待ち

        // Shootステートに遷移するまで待機
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            yield return null;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // ボタンが離される前に一定時間（3.03秒）経過したら止める
        while (stateInfo.normalizedTime < 0.6f)
        {
            if (!Input.GetMouseButton(0))
            {
                //途中で離されたら、強制的に続き再生＆発射準備
                Debug.Log("早めに離された → 発射に切り替え");
                animator.speed = 1f;
                animator.Play("Shoot", 0, 0.59f);
                shootState = ShootState.Releasing;
                yield break; // コルーチン終了
            }

            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        // 長押し時だけ停止させる
        animator.speed = 0f;
        isPaused = true;
        shootState = ShootState.Paused;
        Debug.Log("Shootアニメ 3.03秒で停止（normalizedTime 0.59）");
    }


    // アニメーションイベントで呼ばれる
    public void FireArrowFromAnimation()
    {
        Debug.Log("FireArrowFromAnimation CALLED");

        float normalizedCharge = Mathf.Clamp01(chargeTime / maxChargeTime);
        float launchForce = Mathf.Lerp(minLaunchForce, maxLaunchForce, normalizedCharge);

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * launchForce, ForceMode.VelocityChange);

        shootState = ShootState.None;
        isCharging = false;
        isPaused = false;
        animationController.PlayIdle();
    }
}
