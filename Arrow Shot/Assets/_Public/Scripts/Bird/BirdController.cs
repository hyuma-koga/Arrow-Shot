using UnityEngine;
using System.Collections.Generic;

public class BirdController : MonoBehaviour
{
    [SerializeField] private GameObject birdPrefab;
    [SerializeField] private int birdCount = 10;
    [SerializeField] private float spawnRadius = 30f;

    private List<GameObject> birds = new List<GameObject>();

    private void Start()
    {
        SpawnBirds();
    }

    private void SpawnBirds()
    {
        for (int i = 0; i < birdCount; i++)
        {
            // …•½•ûŒü‚ÉŽU‚ç‚· (XZ•½–Ê)
            Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
            float height = Random.Range(1f, 5f); // ‚‚³‚àƒ‰ƒ“ƒ_ƒ€‚ÉÝ’è

            Vector3 spawnPos = transform.position + new Vector3(randomCircle.x, height, randomCircle.y);

            Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
            GameObject bird = Instantiate(birdPrefab, spawnPos, randomRotation);
            birds.Add(bird);
        }
    }
}
