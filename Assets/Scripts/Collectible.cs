using UnityEngine;

public class Collectible : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " was collected!");
            
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            
            if (gameManager != null)
            {
                gameManager.CollectPart();
            }
            
            Destroy(gameObject);
        }
}
}