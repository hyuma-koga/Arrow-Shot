using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private GameObject crosshairUI;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private RectTransform crosshairRect;
    [SerializeField] private float zoomFOV = 30f;
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float shrinkSpeed = 2f;

    private Coroutine zoomCoroutine;
    private Coroutine scaleCoroutine;
    private Vector2 originalSize;

    private void Start()
    {
        if(crosshairRect != null)
        {
            originalSize = crosshairRect.sizeDelta;
        }
    }

    public void ShowCrosshair()
    {
        crosshairUI.SetActive(true);
    }

    public void HideCrosshair()
    {
        crosshairUI.SetActive(false);
    }

    public void StartZoom()
    {
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }

        zoomCoroutine = StartCoroutine(ZoomTo(zoomFOV));
        StartShrinkCrosshair();
    }

    public void EndZoom()
    {
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }

        zoomCoroutine = StartCoroutine(ZoomTo(normalFOV));
    }

    public void ForceResetFOV()
    {
        if(zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
            zoomCoroutine = null;
        }

        mainCamera.fieldOfView = normalFOV;
        ResetCrosshairScale();
    }

    private IEnumerator ZoomTo(float targetFOV)
    {
        while (Mathf.Abs(mainCamera.fieldOfView - targetFOV) > 0.01f)
        {
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void StartShrinkCrosshair()
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        scaleCoroutine = StartCoroutine(ShrinkCrosshair());
    }


    private IEnumerator ShrinkCrosshair()
    {
        Vector2 targetSize = originalSize * minScale;

        while (Vector2.Distance(crosshairRect.sizeDelta, targetSize) > 0.01f)
        {
            crosshairRect.sizeDelta = Vector2.Lerp(crosshairRect.sizeDelta, targetSize, Time.deltaTime * shrinkSpeed);
            yield return null;
        }

        crosshairRect.sizeDelta = targetSize;
    }

    public void ResetCrosshairScale()
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }

        crosshairRect.sizeDelta = originalSize;
    }
}
