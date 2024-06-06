using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackRate = 1.0f;
    public int attackDamage = 10;
    public Transform attackPoint;
    public LayerMask playerLayer;

    private float nextAttackTime = 0f;
    private Transform player;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            CheckForPlayer();
        }
    }

    void CheckForPlayer()
    {
        Collider2D playerInRange = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if (playerInRange != null)
        {
            player = playerInRange.transform;
            AttackPlayer();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    void AttackPlayer()
    {
        // Trigger attack animation
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // Reduce player's health
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
