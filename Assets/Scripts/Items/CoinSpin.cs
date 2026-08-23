using UnityEngine;

public class CoinSpin : MonoBehaviour
{
   
    public float rotationSpeed = 100f;
    public float floatAmplitude = 0.25f; 
    public float floatSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
  
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        transform.position = new Vector3(
            startPosition.x,
            newY,
            startPosition.z
        );

    }
}
