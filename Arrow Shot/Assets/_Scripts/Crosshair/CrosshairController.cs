using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private GameObject crosshairUI;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float zoomFOV = 30f;
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float zoomSpeed = 5f;

    private Coroutine zoomCoroutine;

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
    }

    private IEnumerator ZoomTo(float targetFOV)
    {
        Debug.Log($"ZoomTo started: current={mainCamera.fieldOfView}, target={targetFOV}");

        while (!Mathf.Approximately(mainCamera.fieldOfView, targetFOV))
        {
            mainCamera.fieldOfView = Mathf.MoveTowards(mainCamera.fieldOfView, targetFOV, zoomSpeed * Time.deltaTime);
            Debug.Log($"Zooming... FOV={mainCamera.fieldOfView}");
            yield return null;
        }

        mainCamera.fieldOfView = targetFOV;
        Debug.Log("ZoomTo complete. Final FOV = " + mainCamera.fieldOfView);
    }
}
