using UnityEngine;

public class Collectible : MonoBehaviour
{
    // This is called when another collider enters this trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the thing that touched us is the player
        if (other.CompareTag("Player"))
        {
            // Find the GameManager in the scene and tell it we were collected
            GameManager gameManager = FindObjectOfType<GameManager>();
            
            if (gameManager != null)
            {
                gameManager.CollectPart();
            }
            
            // Destroy this part (make it disappear)
            Destroy(gameObject);
        }
    }
}