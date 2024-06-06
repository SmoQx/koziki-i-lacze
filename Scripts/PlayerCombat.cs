using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    SaveLoadFile io = new SaveLoadFile();
    string player_data_file = "user_data.json";
    JObject player_data;
    public Animator animator;
    public LayerMask enemyLayers;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;


    void Start()
    {
        player_data = JObject.Parse(io.Load_to_file(player_data_file));
        int strong = 1;
        int.TryParse(player_data["Skills"]["Strong"].ToString(), out strong);
        Weapon(player_data["ItemsList"]["Weapon"].ToString());
        attackDamage = attackDamage * (1 + strong);

    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    void Weapon(string weapon)
    {
        if (weapon == "Spear")
        {
            attackDamage = 20;
            attackRange = 100f;
            attackRate = 3.5f;
        }
        else if (weapon == "Sword")
        {
            attackDamage = 50;
            attackRange = 50f;
            attackRate = 2.5f;
        }
        else if (weapon == "Axe")
        {
            attackDamage = 70;
            attackRange = 30f;
            attackRate = 1.5f;
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
  
}
