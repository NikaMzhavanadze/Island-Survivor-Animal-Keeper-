using SlimUI.ModernMenu;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Camera mainCamera;
    public float spawnRate;
    public int maxSpawns;
    public float radius;
    public int spawnCounter = 0;
    public Rect baseArea;

    private UISettingsManager settingsManager;
    private EnemyScript enemyScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCounter < maxSpawns)
        {
            StartCoroutine(SpawnTimer());
        }
        else if (spawnCounter >= maxSpawns)
        {
            StopCoroutine(SpawnTimer());
        }

        if (PlayerPrefs.GetInt("NormalDifficulty") != 1)
        {
            maxSpawns = 150;
            //enemyScript.damage += 10;
        }
        else
        {
            maxSpawns = 75;
        }

    }

    IEnumerator SpawnTimer()
    {
        while (spawnCounter < maxSpawns)
        {
            Vector2 cameraPosition = new Vector2(mainCamera.transform.position.x, mainCamera.transform.position.y);
            Vector2 spawnPosition = cameraPosition + Random.insideUnitCircle * radius;

            if (!baseArea.Contains(spawnPosition))
            {
                GameObject newObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.DrawWireCube(baseArea.center, new Vector3(baseArea.width, baseArea.height, 0));
    }
}
