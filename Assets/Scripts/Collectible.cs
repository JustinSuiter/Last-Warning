using UnityEngine;

public class Collectible : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        // Check if the thing that touched us is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " was collected!");
            
            // Find the GameManager in the scene and tell it we were collected
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            
            if (gameManager != null)
            {
                gameManager.CollectPart();
            }
            
            // Destroy this part (make it disappear)
            Destroy(gameObject);
        }
}
}