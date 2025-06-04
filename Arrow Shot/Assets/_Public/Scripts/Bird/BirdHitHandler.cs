using UnityEngine;

public class BirdHitHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit by: " + other.name);  // ���܂��͂���ŉ������������Ă��邩�m�F
        if (other.CompareTag("Arrow"))
        {
            Debug.Log("Hit by arrow! Destroying bird.");
            Destroy(gameObject);
        }
    }
}
