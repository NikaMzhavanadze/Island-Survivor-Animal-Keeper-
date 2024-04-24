using UnityEngine;

public class CrossSpawner : MonoBehaviour
{
    public GameObject animal;
    public GameObject crossPrefab;
    public AnimalScript animalScript;
    public float position;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CrossSpawnerOnDeath();
    }

    public void CrossSpawnerOnDeath()
    {
        Vector3 spawnPosition = animal.transform.position;
        spawnPosition.y -= position;

        if (animalScript.lifeTime <= 0)
        {
            Instantiate(crossPrefab, spawnPosition, Quaternion.identity);
            print("dead");
        }
    }

}
