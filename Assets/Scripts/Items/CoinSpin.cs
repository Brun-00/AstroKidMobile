using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float floatAmplitude = 0.25f;
    public float floatSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        // Store the initial position as the base for the floating motion.
        startPosition = transform.position;
    }

    void Update()
    {
        // Continuously rotate the coin around its vertical axis.
        transform.Rotate(
            Vector3.up,
            rotationSpeed * Time.deltaTime
        );

        // Calculate the vertical offset using a sine wave.
        float newY =
            startPosition.y +
            Mathf.Sin(Time.time * floatSpeed) *
            floatAmplitude;

        // Keep the original X and Z while applying the floating motion.
        transform.position = new Vector3(
            startPosition.x,
            newY,
            startPosition.z
        );
    }
}