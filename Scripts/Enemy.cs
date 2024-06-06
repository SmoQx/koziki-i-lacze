using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class Enemy : MonoBehaviour
{
    SceneManagerScript scene = new SceneManagerScript();
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    public float moveSpeed;
    private Rigidbody2D rb;
    private Collider2D col;
    public EnemyAttack enemyAttack;
    public GameManager gameManager;
    string file_with_data = "user_data.json";
    JObject character;
    Transform target;
    Vector2 moveDirection;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        target = GameObject.FindWithTag("Player")?.transform;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");

        animator.SetBool("IsDead", true);

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        gameObject.layer = LayerMask.NameToLayer("Dead");

        if (enemyAttack != null)
        {
            enemyAttack.enabled = false;
        }

        this.enabled = false;

        StartCoroutine(DisableAnimatorAfterDelay());

        FindObjectOfType<GameManager>().Win();

        
        SaveLoadFile savefile = new SaveLoadFile();
        JObject character = JObject.Parse(savefile.Load_to_file(file_with_data));
        character["ItemsList"]["Money"] = int.Parse(character["ItemsList"]["Money"].ToString()) + 100;
        savefile.Save_to_file(character.ToString(), file_with_data);
    }

    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.IsPlayerDead())
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsRunning", false);
            return;
        }

        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance != null && GameManager.instance.IsPlayerDead())
        {
            rb.velocity = Vector2.zero;
            return;
        }

        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
    }

    IEnumerator DisableAnimatorAfterDelay()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(1.145f);

        animator.enabled = false;
    }
}
