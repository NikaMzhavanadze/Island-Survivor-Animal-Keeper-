using UnityEngine;

public class FoodAnimation : MonoBehaviour
{
    public float moveDistance = 0.15f; // The distance the object will move up and down
    public float moveSpeed = 2.5f;    // The speed at which the object will move

    private Vector3 startPos;       // The starting position of the object

    void Start()
    {
        startPos = transform.position; // Store the starting position
    }

    void Update()
    {
        // Calculate the new position based on a sine wave to create smooth up and down movement
        float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
