using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchController : MonoBehaviour
{
    public float velocity = 1f;
    public float lerpSpeed = 10f;

    public Vector2 pastPosition;

    private Vector3 targetPosition;

    void Start()
    {
        // Start with the target at the object's current position.
        targetPosition = transform.position;
    }

    void Update()
    {
        // Move based on the mouse's horizontal movement while holding the button.
        if (Input.GetMouseButton(0))
        {
            Move(Input.mousePosition.x - pastPosition.x);
        }

        pastPosition = Input.mousePosition;

        // Smoothly move toward the target position.
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * lerpSpeed
        );
    }

    public void Move(float speed)
    {
        // Update the target instead of moving the object directly.
        targetPosition += Vector3.right * speed * velocity * Time.deltaTime;
    }
}