using UnityEngine;
using System.Collections;

public class BowController : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform playerVisualRoot;
    [SerializeField] private CrosshairController crosshair;
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private float maxChargeTime = 2f;
    [SerializeField] private float minLaunchForce = 5f;
    [SerializeField] private float maxLaunchForce = 20f;

    // ▼ 追加（レイヤー制御）
    [Header("Layer Settings")]
    [SerializeField] private GameObject playerRootForLayerSwitch;
    [SerializeField] private int normalLayer = 0;      // Default
    [SerializeField] private int hiddenLayer = 8;      // PlayerHiddenZoom（例）
    private Coroutine hideLayerCoroutine;

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
    private AudioSource audioSource;
    private float chargeTime = 0f;
    private bool isCharging = false;
    private bool isPaused = false;
    private bool isZooming = false;

    public bool IsShooting => isCharging;
    public bool IsShootAnimationPlaying => isPaused || isCharging;

    private void Start()
    {
        // ゲーム開始時にレイヤーを初期化（念のため）
        RestorePlayerLayer();
    }

    public void SetAnimationController(PlayerAnimationController controller)
    {
        animationController = controller;
        animator = controller.GetAnimator();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void HandleShooting()
    {
        if (isCharging)
        {
            Vector3 lookDir = Camera.main.transform.forward;
            lookDir.y = 0f;
            lookDir = Quaternion.Euler(0, 90f, 0) * lookDir;
            if (playerVisualRoot != null)
            {
                playerVisualRoot.forward = lookDir.normalized;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndZoomAndHideCrosshair();

            if (audioSource.isPlaying && audioSource.clip == chargeSound)
            {
                audioSource.Stop();
            }

            //レイヤーを元に戻す
            RestorePlayerLayer();
        }

        if (Input.GetMouseButtonDown(0) && shootState == ShootState.None)
        {
            Vector3 lookDir = Camera.main.transform.forward;
            lookDir.y = 0f;
            lookDir = Quaternion.Euler(0, 90f, 0) * lookDir;
            playerVisualRoot.forward = lookDir.normalized;

            isCharging = true;
            chargeTime = 0f;
            shootState = ShootState.Charging;
            animator.speed = 1.7f;
            animationController.PlayShoot();
            StartCoroutine(WaitAndPauseAnimation());

            if (chargeSound != null)
            {
                audioSource.clip = chargeSound;
                audioSource.loop = false;
                audioSource.Play();
            }

            if (!isZooming)
            {
                crosshair.ShowCrosshair();
                crosshair.StartZoom();
                isZooming = true;

                // 2秒後にレイヤーを切り替えて非表示に
                if (hideLayerCoroutine != null) StopCoroutine(hideLayerCoroutine);
                hideLayerCoroutine = StartCoroutine(HidePlayerAfterDelay(2f));
            }
        }

        if (shootState == ShootState.Charging && Input.GetMouseButton(0))
        {
            chargeTime += Time.deltaTime;
        }

        if (shootState == ShootState.Charging && Input.GetMouseButtonUp(0))
        {
            animator.speed = 1f;
            animator.Play("Shoot", 0, 0.59f);
            shootState = ShootState.Releasing;
            RestorePlayerLayer();
        }

        if (shootState == ShootState.Paused && Input.GetMouseButtonUp(0))
        {
            animator.speed = 1f;
            animator.Play("Shoot", 0, 0.59f);
            shootState = ShootState.Releasing;
            RestorePlayerLayer();
        }
    }

    private IEnumerator WaitAndPauseAnimation()
    {
        yield return new WaitForSeconds(0.1f);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            yield return null;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        while (stateInfo.normalizedTime < 0.6f)
        {
            if (!Input.GetMouseButton(0))
            {
                animator.speed = 1f;
                animator.Play("Shoot", 0, 0.59f);
                shootState = ShootState.Releasing;
                RestorePlayerLayer();
                yield break;
            }

            yield return null;
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }

        animator.speed = 0f;
        isPaused = true;
        shootState = ShootState.Paused;
    }

    public void FireArrowFromAnimation()
    {
        float normalizedCharge = Mathf.Clamp01(chargeTime / maxChargeTime);
        float launchForce = Mathf.Lerp(minLaunchForce, maxLaunchForce, normalizedCharge);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 shootDirection = ray.direction;

        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.linearVelocity = shootDirection * launchForce;

        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        shootState = ShootState.None;
        isCharging = false;
        isPaused = false;
        animationController.PlayIdle();

        RestorePlayerLayer();
    }

    private void EndZoomAndHideCrosshair()
    {
        if (crosshair != null && isZooming)
        {
            crosshair.ForceResetFOV();
            crosshair.EndZoom();
            crosshair.HideCrosshair();
            isZooming = false;
        }
    }

    //レイヤー変更処理
    private IEnumerator HidePlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (isZooming && playerRootForLayerSwitch != null)
        {
            SetLayerRecursively(playerRootForLayerSwitch, hiddenLayer);
        }
    }

    private void RestorePlayerLayer()
    {
        if (playerRootForLayerSwitch != null)
        {
            SetLayerRecursively(playerRootForLayerSwitch, normalLayer);
        }

        if (hideLayerCoroutine != null)
        {
            StopCoroutine(hideLayerCoroutine);
            hideLayerCoroutine = null;
        }
    }

    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
