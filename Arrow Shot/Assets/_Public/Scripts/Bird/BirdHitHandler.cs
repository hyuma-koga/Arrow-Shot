using UnityEngine;

public class BirdHitHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit by: " + other.name);  // ←まずはこれで何かが当たっているか確認
        if (other.CompareTag("Arrow"))
        {
            Debug.Log("Hit by arrow! Destroying bird.");
            Destroy(gameObject);
        }
    }
}
