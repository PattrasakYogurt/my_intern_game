using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove2 : MonoBehaviour
{
    // Define the boundaries
    public Vector3 minBounds;
    public Vector3 maxBounds;

    // Movement speed
    public float speed = 10f;

    // Movement duration
    public float moveDuration = 5f;

    // Time interval between movements in seconds (180f = 3 minutes)
    public float moveInterval = 10f;

    // Movement direction
    private Vector3 direction;

    void Start()
    {
        // Initialize movement direction to a random direction
        direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Start the coroutine to move the object at intervals
        StartCoroutine(MoveObjectAtIntervals());
    }

    IEnumerator MoveObjectAtIntervals()
    {
        while (true)
        {
            // Wait for the interval
            yield return new WaitForSeconds(moveInterval);

            // Move the object
            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                transform.Translate(direction * speed * Time.deltaTime);

                // Clamp the object's position to the defined boundaries
                Vector3 clampedPosition = transform.position;
                clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
                clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);
                clampedPosition.z = Mathf.Clamp(clampedPosition.z, minBounds.z, maxBounds.z);

                // If the object's position is clamped, reverse the direction
                if (transform.position != clampedPosition)
                {
                    direction = -direction;
                }

                // Apply the clamped position
                transform.position = clampedPosition;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Reset direction to a new random direction
            direction = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f)).normalized;
        }
    }
}
