using UnityEngine;

public class Collectible : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            // Find the GameManager in the scene and tell it we were collected
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            
            if (gameManager != null)
            {
                gameManager.CollectPart();
            }
            
            
            Destroy(gameObject);
        }
    }
}