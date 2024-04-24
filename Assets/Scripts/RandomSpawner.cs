using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] ItemPrefab;
    public Camera mainCamera;
    public float spawnRate;
    public int maxSpawns = 1600; // Maximum number of spawns
    public float radius;
    public int spawnCounter = 0; // Counter for the number of spawns
    public Rect baseArea; // Define the base area using a Rect

    // Start is called before the first frame update
    void Start()
    {
        // Define the base area. Adjust the values as needed.
        baseArea = new Rect(-25.1f, -15.7f, 51.2f, 30.1f);
        if (PlayerPrefs.GetInt("NormalDifficulty") != 1)
        {
            maxSpawns = 1200;
        }
        else
        {
            maxSpawns = 1600;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the maximum number of spawns has been reached
        if (spawnCounter < maxSpawns)
        {
            StartCoroutine(SpawnTimer());
        }
        else if (spawnCounter >= maxSpawns)
        {
            StopCoroutine(SpawnTimer()); // Stop the spawning coroutine
        }
    }

    IEnumerator SpawnTimer()
    {
        while (spawnCounter < maxSpawns)
        {
            Vector2 cameraPosition = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y);
            Vector2 spawnPosition = cameraPosition + Random.insideUnitCircle * radius;

            // Check if the spawn position is inside the base area
            if (!baseArea.Contains(spawnPosition))
            {
                GameObject newObject = Instantiate(ItemPrefab[Random.Range(0, ItemPrefab.Length)], spawnPosition, Quaternion.identity);

                spawnCounter++;

                Vector3 viewportPoint = mainCamera.WorldToViewportPoint(newObject.transform.position);

                if (viewportPoint.z > 0 && viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1)
                {
                    Destroy(newObject);
                }
            }

            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Draw the base area
        Gizmos.DrawWireCube(baseArea.center, new Vector3(baseArea.width, baseArea.height, 0));
    }
}
