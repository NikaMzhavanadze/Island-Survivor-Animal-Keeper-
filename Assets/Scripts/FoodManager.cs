using UnityEngine.UI;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public float foodCount;
    public float maxFoodCount;
    public Text foodText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foodText.text = "Food Count: " + foodCount.ToString();
    }
}
