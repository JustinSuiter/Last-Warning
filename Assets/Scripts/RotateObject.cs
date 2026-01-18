using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 50f;
    
    void Update()
    {
        // Rotate around the Y axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}