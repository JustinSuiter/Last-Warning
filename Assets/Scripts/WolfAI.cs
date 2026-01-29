using UnityEngine;

public class WolfAI : MonoBehaviour
{
    // Wolf behavior settings
    public float chaseSpeed = 4f;
    public float detectionRange = 10f;
    public float giveUpDistance = 15f;
    
    // Bite/Attack settings
    public float biteDistance = 2f;
    public float biteDamage = 20f;
    public float biteCooldown = 1.5f;
    public float attackDuration = 1f;

    public float maxHealth = 100f;
    private float currentHealth;
    
    private Transform player;
    private PlayerController playerController;
    private Rigidbody rb;
    private Animator animator;
    private bool isChasing = false;
    private float lastBiteTime = -999f;
    private float attackStartTime = -999f;
    private bool isCurrentlyAttacking = false;
    
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

        currentHealth = maxHealth;
    }
    
    void Update()
    {
        if (player == null || Time.timeScale == 0f)
            return;
        
        if (isCurrentlyAttacking && Time.time >= attackStartTime + attackDuration)
        {
            isCurrentlyAttacking = false;
            if (animator != null)
            {
                animator.SetBool("isAttacking", false);
            }
        }
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
        }
        else if (distanceToPlayer > giveUpDistance)
        {
            isChasing = false;
        }
        
        if (isChasing)
        {
            if (distanceToPlayer <= biteDistance && !isCurrentlyAttacking)
            {
                TryBitePlayer();
            }
            else if (!isCurrentlyAttacking)
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
            
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
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
        
        if (Time.time >= lastBiteTime + biteCooldown)
        {
            isCurrentlyAttacking = true;
            attackStartTime = Time.time;
            
            if (animator != null)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
            }
            
            if (playerController != null)
            {
                playerController.TakeDamage(biteDamage);
                lastBiteTime = Time.time;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, biteDistance);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, giveUpDistance);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        Debug.Log(gameObject.name + " took " + damage + " damage. Health: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        Destroy(gameObject);
    }
}