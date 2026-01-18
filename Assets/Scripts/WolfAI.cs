using UnityEngine;

public class WolfAI : MonoBehaviour
{
    // Wolf behavior Settings
    public float chaseSpeed = 4f;
    public float detectionRange = 10f;
    
    // Wolf attack Settings
    public float biteDistance = 2f;
    public float biteDamage = 20f;
    public float biteCooldown = 1.5f;
    
    private Transform player;
    private PlayerController playerController;
    private Rigidbody rb;
    private Animator animator;
    private bool isChasing = false;
    private float lastBiteTime = -999f;
    
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerController = playerObject.GetComponent<PlayerController>();
        }
        
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (player == null || Time.timeScale == 0f)
            return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        
        if (isChasing)
        {
            if (distanceToPlayer <= biteDistance)
            {
                TryBitePlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
            }
        }
    }
    
    void ChasePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0;
        
        rb.linearVelocity = new Vector3(
            directionToPlayer.x * chaseSpeed,
            rb.linearVelocity.y,
            directionToPlayer.z * chaseSpeed
        );
        
        if (directionToPlayer != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
        
        if (animator != null)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isAttacking", false);
        }
    }
    
    void TryBitePlayer()
    {
        rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        
        if (animator != null)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isAttacking", true);
        }
        
        if (Time.time >= lastBiteTime + biteCooldown)
        {
            if (playerController != null)
            {
                playerController.TakeDamage(biteDamage);
                lastBiteTime = Time.time;
                
                Debug.Log("Wolf bit player for " + biteDamage + " damage!");
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, biteDistance);
    }
}