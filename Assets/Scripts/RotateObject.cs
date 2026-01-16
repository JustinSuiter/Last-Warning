using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // How fast the object spins
    public float rotationSpeed = 50f;
    
    // Called every frame
    void Update()
    {
        // Rotate around the Y axis (upward axis)
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}