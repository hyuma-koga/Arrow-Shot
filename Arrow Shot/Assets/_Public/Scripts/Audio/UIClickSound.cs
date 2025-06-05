using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class UIClickSound : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip clickSound;
    private static AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null)
        {
            GameObject audioObj = new GameObject("UIClickSoundPlayer");
            audioSource = audioObj.AddComponent<AudioSource>();
            DontDestroyOnLoad(audioObj);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
