using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        transform.position = new Vector3(
            0f,                
            0f,                
            player.position.z  
        );
    }
}