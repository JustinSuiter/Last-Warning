using UnityEngine;

public class Collectible : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            
            if (gameManager != null)
            {
                gameManager.CollectPart();
            }
            
            
            Destroy(gameObject);
        }
    }
}