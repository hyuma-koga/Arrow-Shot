using UnityEngine;

public class BirdHitHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit by: " + other.name);  // ©‚Ü‚¸‚Í‚±‚ê‚Å‰½‚©‚ª“–‚½‚Á‚Ä‚¢‚é‚©Šm”F
        if (other.CompareTag("Arrow"))
        {
            Debug.Log("Hit by arrow! Destroying bird.");
            Destroy(gameObject);
        }
    }
}
