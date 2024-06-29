using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrownRotate : MonoBehaviour
{
    public float rotationSpeed = 45f; // Degrees per second
    public float rotationDuration = 5f; // Duration of rotation in seconds
    public float interval = 30f; // Time between rotations in seconds

    private float timer;
    private bool isRotating;
    private float rotationTime;
    private int rotationDirection;

    void Start()
    {
        timer = 0f;
        isRotating = false;
        rotationTime = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!isRotating && timer >= interval)
        {
            isRotating = true;
            timer = 0f;
            rotationDirection = Random.Range(0, 2) == 0 ? 1 : -1; // Randomly set rotation direction to 1 (right) or -1 (left)
        }

        if (isRotating)
        {
            rotationTime += Time.deltaTime;
            if (rotationTime < rotationDuration)
            {
                transform.Rotate(Vector3.up, rotationSpeed * rotationDirection * Time.deltaTime);
            }
            else
            {
                isRotating = false;
                rotationTime = 0f;
            }
        }
    }
}
