using TMPro;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    public float lifeTime = 60f;
    public float maxLifeTime;
    public PlayerScript ps;
    public FoodManager foodManager;
    public TextMeshPro timeLeft;
    public float maxFood;
    public float animalCount;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("NormalDifficulty") != 1)
        {
            lifeTime = 40f;
        }
        else
        {
            lifeTime = 60f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime < -0.3)
        {
            Destroy(gameObject);
        }

        animalCount = GameObject.FindGameObjectsWithTag("Animal").Length;

        if (animalCount == 3)
        {
            maxLifeTime = 45;
            if (lifeTime > 45)
            {
                lifeTime = 45;
            }
        }
        else if (animalCount == 2)
        {
            maxLifeTime = 35;
            if (lifeTime > 35)
            {
                lifeTime = 35;
            }
        }
        else if (animalCount == 1)
        {
            maxLifeTime = 25;
            if (lifeTime > 25)
            {
                lifeTime = 25;
            }
        }

        if (lifeTime > 80)
        {
            lifeTime = 80;
        }

        lifeTime -= Time.deltaTime;
        timeLeft.text = "Time Left: " + Mathf.Ceil(lifeTime).ToString();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (foodManager.foodCount < maxFood)
            {
                if (lifeTime <= maxLifeTime)
                {
                    lifeTime = lifeTime + foodManager.foodCount * 4.5f;
                    foodManager.foodCount = 0;
                }
            }
            else if (foodManager.foodCount >= maxFood)
            {
                if (lifeTime <= maxLifeTime)
                {
                    lifeTime = lifeTime + maxFood * 4f;
                    foodManager.foodCount -= maxFood;
                }
            }
        }
    }

}
