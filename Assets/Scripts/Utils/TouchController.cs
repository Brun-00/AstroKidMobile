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
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Move(Input.mousePosition.x - pastPosition.x);
        }

        pastPosition = Input.mousePosition;

        // Movimento suave (Lerp)
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
    }

    public void Move(float speed)
    {
        // Atualiza só o alvo, não o transform diretamente
        targetPosition += Vector3.right * speed * velocity * Time.deltaTime;
    }
}
